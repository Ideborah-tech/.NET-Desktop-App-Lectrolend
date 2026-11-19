using LectroLend.Core.Data;
using LectroLend.Core.Models;
using LectroLend.Core.Repositories;

namespace LectroLend.Core.Services;

// SERVICE: the main rules of the app: booking, cancelling, handing over and checking in equipment.
// It gets and saves data through the repositories.
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
            ReservationRepository reservations = new ReservationRepository(db);
            return reservations.GetForUserWithDetails(userId);
        }
    }

    // All bookings, newest first. Give a status to see only those bookings, or null to see all.
    public List<Reservation> All(ReservationStatus? status = null)
    {
        List<Reservation> all;
        using (LectroLendDbContext db = new LectroLendDbContext(settings))
        {
            ReservationRepository reservations = new ReservationRepository(db);
            all = reservations.GetAllWithDetails();
        }

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

    // How many bookings a user holds right now (booked, picked up or late).
    public int ActiveCount(long userId)
    {
        using (LectroLendDbContext db = new LectroLendDbContext(settings))
        {
            ReservationRepository reservations = new ReservationRepository(db);
            return reservations.CountActiveForUser(userId);
        }
    }

    // ---------------- Making a booking (the sequence diagram) ----------------

    public Reservation Create(long userId, long equipmentId, DateTime start, DateTime end, long? roomId)
    {
        using (LectroLendDbContext db = new LectroLendDbContext(settings))
        // A transaction means "all or nothing": if anything fails, nothing is saved.
        using (var transaction = db.Database.BeginTransaction())
        {
            UserRepository users = new UserRepository(db);
            EquipmentRepository equipment = new EquipmentRepository(db);
            ReservationRepository reservations = new ReservationRepository(db);

            // 1. Only students and lecturers can book.
            User user = users.FindById(userId);
            if (!(user is IBorrower))
                throw new AppException("Only students and lecturers can book equipment.");
            IBorrower borrower = (IBorrower)user;

            // 2. The times must make sense.
            CheckTimes(start, end, DateTime.Now);

            // 3. The user must not be over their limit.
            int active = reservations.CountActiveForUser(userId);
            if (active >= borrower.MaxActiveReservations)
                throw new AppException("You already have " + active + " active bookings. Your limit is "
                                       + borrower.MaxActiveReservations + ".");

            // 4. Lock the item so nobody else can book it at the same moment.
            Equipment item = equipment.FindByIdAndLock(equipmentId);
            if (item == null)
                throw new AppException("Equipment not found.");
            if (!item.IsReservable())
                throw new AppException(item.Name + " is " + Labels.InSentence(item.Status) + " and cannot be booked.");

            // 5. The item must not be booked for any part of that time.
            if (HasOverlap(reservations.GetForEquipment(equipmentId), start, end))
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

            reservations.Add(reservation);
            reservations.Save();
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
            ReservationRepository reservations = new ReservationRepository(db);
            Reservation reservation = FindReservation(reservations, reservationId);
            if (!isAdmin && reservation.UserId != userId)
                throw new AppException("You can only cancel your own bookings.");

            reservation.Cancel();
            reservations.Save();
        }
    }

    // ---------------- Handing an item over ----------------

    public void Issue(long reservationId)
    {
        using (LectroLendDbContext db = new LectroLendDbContext(settings))
        using (var transaction = db.Database.BeginTransaction())
        {
            ReservationRepository reservations = new ReservationRepository(db);
            EquipmentRepository equipment = new EquipmentRepository(db);

            Reservation reservation = FindReservation(reservations, reservationId);
            DateTime now = DateTime.Now;

            if (reservation.Status != ReservationStatus.BOOKED)
                throw new AppException("Only a booked reservation can be handed over (this one is "
                                       + Labels.Of(reservation.Status).ToLower() + ").");
            if (now < reservation.StartTime.AddMinutes(-EarlyPickupMinutes))
                throw new AppException("Too early. Pick-up opens " + EarlyPickupMinutes + " minutes before the start time.");
            if (now > reservation.StartTime.AddMinutes(PickupWindowMinutes))
                throw new AppException("Too late. The pick-up time has passed, so this booking will expire.");

            Equipment item = equipment.FindByIdAndLock(reservation.EquipmentId);
            if (!item.IsAvailable())
                throw new AppException(item.Name + " is " + Labels.InSentence(item.Status) + " and cannot be handed over now.");

            reservation.MarkIssued(now);
            item.Status = EquipmentStatus.IN_USE;

            reservations.Save();
            transaction.Commit();
        }
    }

    // ---------------- Checking an item in ----------------

    public void ConfirmReturn(long reservationId, long adminId, bool damaged, string condition, string remarks)
    {
        using (LectroLendDbContext db = new LectroLendDbContext(settings))
        using (var transaction = db.Database.BeginTransaction())
        {
            ReservationRepository reservations = new ReservationRepository(db);
            EquipmentRepository equipment = new EquipmentRepository(db);

            Reservation reservation = FindReservation(reservations, reservationId);
            DateTime now = DateTime.Now;

            if (reservation.Status != ReservationStatus.ISSUED && reservation.Status != ReservationStatus.OVERDUE)
                throw new AppException("Only an item that was handed over can be checked in.");
            if (reservations.ReturnRecordExists(reservationId))
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
            reservations.AddReturnRecord(record);

            reservation.MarkReturned();

            // A damaged item goes to maintenance. Otherwise it goes back on the shelf.
            Equipment item = equipment.FindByIdAndLock(reservation.EquipmentId);
            if (damaged)
                item.Status = EquipmentStatus.UNDER_MAINTENANCE;
            else
                item.Status = EquipmentStatus.AVAILABLE;

            reservations.Save();
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
            ReservationRepository reservations = new ReservationRepository(db);
            DateTime now = DateTime.Now;
            int changed = 0;

            foreach (Reservation r in reservations.GetBookedOrIssued())
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

            reservations.Save();
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

    // Asks the repository for the booking, and stops with a message if there is none.
    private static Reservation FindReservation(ReservationRepository reservations, long reservationId)
    {
        Reservation reservation = reservations.FindById(reservationId);
        if (reservation == null)
            throw new AppException("Booking not found.");
        return reservation;
    }
}
