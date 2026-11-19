using LectroLend.Core.Data;
using LectroLend.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace LectroLend.Core.Repositories;

// REPOSITORY: the only place that reads and writes bookings (and return records) in the database.
public class ReservationRepository
{
    private LectroLendDbContext db;

    public ReservationRepository(LectroLendDbContext db)
    {
        this.db = db;
    }

    // All bookings, newest first, together with the user, item, room and return record (to show their names).
    public List<Reservation> GetAllWithDetails()
    {
        return WithDetails().OrderByDescending(r => r.StartTime).ToList();
    }

    // The bookings of one user, newest first, with details.
    public List<Reservation> GetForUserWithDetails(long userId)
    {
        return WithDetails().Where(r => r.UserId == userId).OrderByDescending(r => r.StartTime).ToList();
    }

    // All bookings of one item, oldest first.
    public List<Reservation> GetForEquipment(long equipmentId)
    {
        return db.Reservations.Where(r => r.EquipmentId == equipmentId).OrderBy(r => r.StartTime).ToList();
    }

    // Bookings that are waiting to be picked up or that someone has (used by the automatic updates).
    public List<Reservation> GetBookedOrIssued()
    {
        return db.Reservations
            .Where(r => r.Status == ReservationStatus.BOOKED || r.Status == ReservationStatus.ISSUED)
            .ToList();
    }

    // Finds a booking by id. Returns null if there is none.
    public Reservation FindById(long reservationId)
    {
        return db.Reservations.FirstOrDefault(r => r.ReservationId == reservationId);
    }

    // How many bookings a user holds right now (booked, picked up or late).
    public int CountActiveForUser(long userId)
    {
        return db.Reservations.Count(r => r.UserId == userId
            && (r.Status == ReservationStatus.BOOKED
                || r.Status == ReservationStatus.ISSUED
                || r.Status == ReservationStatus.OVERDUE));
    }

    // True if someone has this item right now (picked up or late).
    public bool SomeoneHasItem(long equipmentId)
    {
        return db.Reservations.Any(r => r.EquipmentId == equipmentId
            && (r.Status == ReservationStatus.ISSUED || r.Status == ReservationStatus.OVERDUE));
    }

    // True if any booking uses this room.
    public bool RoomIsUsed(long roomId)
    {
        return db.Reservations.Any(r => r.RoomId == roomId);
    }

    public void Add(Reservation reservation)
    {
        db.Reservations.Add(reservation);
    }

    // ---- Return records (what happened when an item came back) ----

    public bool ReturnRecordExists(long reservationId)
    {
        return db.ReturnRecords.Any(rr => rr.ReservationId == reservationId);
    }

    public void AddReturnRecord(ReturnRecord record)
    {
        db.ReturnRecords.Add(record);
    }

    // Writes all changes to the database.
    public void Save()
    {
        db.SaveChanges();
    }

    // Bookings together with the linked user, item, room and return record.
    private IQueryable<Reservation> WithDetails()
    {
        return db.Reservations
            .Include(r => r.User)
            .Include(r => r.Equipment)
            .Include(r => r.Room)
            .Include(r => r.ReturnRecord);
    }
}
