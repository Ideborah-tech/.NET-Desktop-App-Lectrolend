using LectroLend.Core.Data;
using LectroLend.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace LectroLend.Core.Services;

// The main rules of the app: booking, cancelling, handing over and checking in equipment.
public class ReservationService
{
    // The IT office can hand an item over from 30 minutes before the start time...
    public const int EarlyPickupMinutes = 30;
    // ...until 30 minutes after it. After that the booking expires.
    public const int PickupWindowMinutes = 30;
    // A booking can last at most 7 days.
    public const int MaxDays = 7;

    private DbSettings settings;

    public ReservationService(DbSettings settings)
    {
        this.settings = settings;
    }

    // ---------------- Reading bookings ----------------

    // The bookings of one user, newest first.
    public List<Reservation> Mine(long userId)
    {
        using (LectroLendDbContext db = new LectroLendDbContext(settings))
        {
            return LoadWithDetails(db)
                .Where(r => r.UserId == userId)
                .OrderByDescending(r => r.StartTime)
                .ToList();
        }
    }

    // All bookings, newest first. Give a status to see only those bookings, or null to see all.
    public List<Reservation> All(ReservationStatus? status = null)
    {
        using (LectroLendDbContext db = new LectroLendDbContext(settings))
        {
            List<Reservation> all = LoadWithDetails(db).OrderByDescending(r => r.StartTime).ToList();
            if (status == null)
                return all;

            List<Reservation> result = new List<Reservation>();
            foreach (Reservation r in all)
            {
                if (r.Status == status)
                    result.Add(r);
            }
            return result;
        }
    }

    // How many bookings a user holds right now (booked, picked up or late).
    public int ActiveCount(long userId)
    {
        using (LectroLendDbContext db = new LectroLendDbContext(settings))
        {
            return CountActive(db, userId);
        }
    }

    // ---------------- Making a booking (the sequence diagram) ----------------

    public Reservation Create(long userId, long equipmentId, DateTime start, DateTime end, long? roomId)
    {
        using (LectroLendDbContext db = new LectroLendDbContext(settings))
        // A transaction means "all or nothing": if anything fails, nothing is saved.
        using (var transaction = db.Database.BeginTransaction())
        {
            // 1. Only students and lecturers can book.
            User user = db.Users.FirstOrDefault(u => u.UserId == userId);
            if (!(user is IBorrower))
                throw new AppException("Only students and lecturers can book equipment.");
            IBorrower borrower = (IBorrower)user;

            // 2. The times must make sense.
            CheckTimes(start, end, DateTime.Now);

            // 3. The user must not be over their limit.
            int active = CountActive(db, userId);
            if (active >= borrower.MaxActiveReservations)
                throw new AppException("You already have " + active + " active bookings. Your limit is "
                                       + borrower.MaxActiveReservations + ".");

            // 4. Lock the item so nobody else can book it at the same moment.
            Equipment item = EquipmentService.LoadAndLock(db, equipmentId);
            if (!item.IsReservable())
                throw new AppException(item.Name + " is " + Labels.InSentence(item.Status) + " and cannot be booked.");

            // 5. The item must not be booked for any part of that time.
            List<Reservation> bookings = db.Reservations.Where(r => r.EquipmentId == equipmentId).ToList();
            if (HasOverlap(bookings, start, end))
                throw new AppException(item.Name + " is already booked for part of that time. Please choose another time.");

            // 6. Save the new booking.
            Reservation reservation = new Reservation();
            reservation.UserId = userId;
            reservation.EquipmentId = equipmentId;
            reservation.RoomId = roomId;
            reservation.StartTime = start;
            reservation.EndTime = end;
            reservation.Status = ReservationStatus.BOOKED;
            reservation.CreatedAt = DateTime.Now;

            db.Reservations.Add(reservation);
            db.SaveChanges();
            transaction.Commit();
            return reservation;
        }
    }

    // ---------------- Cancelling ----------------

    // Borrowers can cancel their own bookings. Admins can cancel any booking.
    public void Cancel(long reservationId, long userId, bool isAdmin)
    {
        using (LectroLendDbContext db = new LectroLendDbContext(settings))
        {
            Reservation reservation = FindReservation(db, reservationId);
            if (!isAdmin && reservation.UserId != userId)
                throw new AppException("You can only cancel your own bookings.");

            reservation.Cancel();
            db.SaveChanges();
        }
    }

    // ---------------- Handing an item over ----------------

    public void Issue(long reservationId)
    {
        using (LectroLendDbContext db = new LectroLendDbContext(settings))
        using (var transaction = db.Database.BeginTransaction())
        {
            Reservation reservation = FindReservation(db, reservationId);
            DateTime now = DateTime.Now;

            if (reservation.Status != ReservationStatus.BOOKED)
                throw new AppException("Only a booked reservation can be handed over (this one is "
                                       + Labels.Of(reservation.Status).ToLower() + ").");
            if (now < reservation.StartTime.AddMinutes(-EarlyPickupMinutes))
                throw new AppException("Too early. Pick-up opens " + EarlyPickupMinutes + " minutes before the start time.");
            if (now > reservation.StartTime.AddMinutes(PickupWindowMinutes))
                throw new AppException("Too late. The pick-up time has passed, so this booking will expire.");

            Equipment item = EquipmentService.LoadAndLock(db, reservation.EquipmentId);
            if (!item.IsAvailable())
                throw new AppException(item.Name + " is " + Labels.InSentence(item.Status) + " and cannot be handed over now.");

            reservation.MarkIssued(now);
            item.Status = EquipmentStatus.IN_USE;

            db.SaveChanges();
            transaction.Commit();
        }
    }

    // ---------------- Checking an item in ----------------

    public void ConfirmReturn(long reservationId, long adminId, bool damaged, string condition, string remarks)
    {
        using (LectroLendDbContext db = new LectroLendDbContext(settings))
        using (var transaction = db.Database.BeginTransaction())
        {
            Reservation reservation = FindReservation(db, reservationId);
            DateTime now = DateTime.Now;

            if (reservation.Status != ReservationStatus.ISSUED && reservation.Status != ReservationStatus.OVERDUE)
                throw new AppException("Only an item that was handed over can be checked in.");
            if (db.ReturnRecords.Any(rr => rr.ReservationId == reservationId))
                throw new AppException("This item has already been checked in.");

            // If no condition was typed, write "Good" or "Damaged".
            if (Check.IsEmpty(condition))
            {
                if (damaged)
                    condition = "Damaged";
                else
                    condition = "Good";
            }
            Check.MaxLength(condition, "Condition", 100);

            ReturnRecord record = new ReturnRecord();
            record.ReservationId = reservationId;
            record.ConfirmedById = adminId;
            record.ReturnedAt = now;
            record.ConditionOnReturn = condition.Trim();
            record.LateReturn = now > reservation.EndTime;  // brought back after the end time?
            record.Remarks = Check.Clean(remarks);
            db.ReturnRecords.Add(record);

            reservation.MarkReturned();

            // A damaged item goes to maintenance. Otherwise it goes back on the shelf.
            Equipment item = EquipmentService.LoadAndLock(db, reservation.EquipmentId);
            if (damaged)
                item.Status = EquipmentStatus.UNDER_MAINTENANCE;
            else
                item.Status = EquipmentStatus.AVAILABLE;

            db.SaveChanges();
            transaction.Commit();
        }
    }

    // ---------------- Automatic updates (every minute) ----------------

    // 1. Bookings nobody picked up in time become EXPIRED.
    // 2. Items not returned by the end time become OVERDUE.
    // Returns how many bookings were changed.
    public int RunHousekeeping()
    {
        using (LectroLendDbContext db = new LectroLendDbContext(settings))
        {
            DateTime now = DateTime.Now;
            int changed = 0;

            List<Reservation> open = db.Reservations
                .Where(r => r.Status == ReservationStatus.BOOKED || r.Status == ReservationStatus.ISSUED)
                .ToList();

            foreach (Reservation r in open)
            {
                if (r.Status == ReservationStatus.BOOKED && now > r.StartTime.AddMinutes(PickupWindowMinutes))
                {
                    r.Expire();
                    changed++;
                }
                else if (r.Status == ReservationStatus.ISSUED && now > r.EndTime)
                {
                    r.MarkOverdue();
                    changed++;
                }
            }

            db.SaveChanges();
            return changed;
        }
    }

    // ---------------- Helpers ----------------

    // Checks that the start and end times make sense. "now" is passed in so tests can use any time.
    public static void CheckTimes(DateTime start, DateTime end, DateTime now)
    {
        if (end <= start)
            throw new AppException("The end time must be after the start time.");
        if (start < now.AddMinutes(-5)) // 5 minutes of tolerance
            throw new AppException("The start time cannot be in the past.");
        if (end - start > TimeSpan.FromDays(MaxDays))
            throw new AppException("A booking cannot be longer than " + MaxDays + " days.");
    }

    // True if any active booking in the list shares time with start to end.
    public static bool HasOverlap(List<Reservation> bookings, DateTime start, DateTime end)
    {
        foreach (Reservation booking in bookings)
        {
            if (booking.IsActive() && booking.Overlaps(start, end))
                return true;
        }
        return false;
    }

    private static int CountActive(LectroLendDbContext db, long userId)
    {
        return db.Reservations.Count(r => r.UserId == userId
            && (r.Status == ReservationStatus.BOOKED
                || r.Status == ReservationStatus.ISSUED
                || r.Status == ReservationStatus.OVERDUE));
    }

    // Bookings together with the linked user, item, room and return record (so we can show their names).
    private static IQueryable<Reservation> LoadWithDetails(LectroLendDbContext db)
    {
        return db.Reservations
            .Include(r => r.User)
            .Include(r => r.Equipment)
            .Include(r => r.Room)
            .Include(r => r.ReturnRecord);
    }

    private static Reservation FindReservation(LectroLendDbContext db, long reservationId)
    {
        Reservation reservation = db.Reservations.FirstOrDefault(r => r.ReservationId == reservationId);
        if (reservation == null)
            throw new AppException("Booking not found.");
        return reservation;
    }
}
