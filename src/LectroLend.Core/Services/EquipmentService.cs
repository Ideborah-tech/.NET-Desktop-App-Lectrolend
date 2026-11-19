using LectroLend.Core.Data;
using LectroLend.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace LectroLend.Core.Services;

// Everything about equipment: listing, checking if it is free, adding, editing and removing.
public class EquipmentService
{
    private DbSettings settings;

    public EquipmentService(DbSettings settings)
    {
        this.settings = settings;
    }

    // All equipment, sorted by name.
    // type: only this type (or null for all types). search: text to look for in the name or asset tag.
    public List<Equipment> List(EquipmentType? type = null, string search = null)
    {
        List<Equipment> all;
        using (LectroLendDbContext db = new LectroLendDbContext(settings))
        {
            all = db.Equipment.OrderBy(e => e.Name).ThenBy(e => e.AssetTag).ToList();
        }

        string text = "";
        if (search != null)
            text = search.Trim().ToLower();

        List<Equipment> result = new List<Equipment>();
        foreach (Equipment item in all)
        {
            bool typeMatches = type == null || item.Type == type;
            bool textMatches = text == "" || item.Name.ToLower().Contains(text) || item.AssetTag.ToLower().Contains(text);
            if (typeMatches && textMatches)
                result.Add(item);
        }
        return result;
    }

    // Is the item free between start and end? Also returns its upcoming bookings.
    public AvailabilityResult Availability(long equipmentId, DateTime start, DateTime end)
    {
        AvailabilityResult result = new AvailabilityResult();

        using (LectroLendDbContext db = new LectroLendDbContext(settings))
        {
            Equipment item = FindItem(db, equipmentId);
            List<Reservation> bookings = db.Reservations
                .Where(r => r.EquipmentId == equipmentId)
                .OrderBy(r => r.StartTime)
                .ToList();

            // Upcoming bookings (only the times, no names).
            foreach (Reservation booking in bookings)
            {
                if (booking.IsActive() && booking.EndTime > DateTime.Now)
                {
                    TimeSlot slot = new TimeSlot();
                    slot.Start = booking.StartTime;
                    slot.End = booking.EndTime;
                    result.BookedTimes.Add(slot);
                }
            }

            if (!item.IsReservable())
            {
                result.Available = false;
                result.Reason = "This item is " + Labels.InSentence(item.Status) + " and cannot be booked.";
            }
            else if (end <= start)
            {
                result.Available = false;
                result.Reason = "The end time must be after the start time.";
            }
            else if (ReservationService.HasOverlap(bookings, start, end))
            {
                result.Available = false;
                result.Reason = "Already booked for part of that time.";
            }
            else
            {
                result.Available = true;
            }
        }
        return result;
    }

    // Adds a new item.
    public void Create(long adminId, string name, EquipmentType type, string assetTag, EquipmentStatus status, string description)
    {
        Check.Required(name, "Name", 80);
        Check.Required(assetTag, "Asset tag", 30);
        if (status == EquipmentStatus.IN_USE)
            throw new AppException("'In use' is set automatically when an item is handed over.");

        using (LectroLendDbContext db = new LectroLendDbContext(settings))
        {
            if (AssetTagExists(db, assetTag, 0))
                throw new AppException("This asset tag already exists.");

            Equipment item = new Equipment();
            item.AddedById = adminId;
            item.Name = name.Trim();
            item.Type = type;
            item.AssetTag = assetTag.Trim();
            item.Status = status;
            item.Description = Check.Clean(description);

            db.Equipment.Add(item);
            db.SaveChanges();
        }
    }

    // Changes an existing item.
    public void Update(long equipmentId, string name, EquipmentType type, string assetTag, EquipmentStatus status, string description)
    {
        Check.Required(name, "Name", 80);
        Check.Required(assetTag, "Asset tag", 30);

        using (LectroLendDbContext db = new LectroLendDbContext(settings))
        using (var transaction = db.Database.BeginTransaction())
        {
            Equipment item = LoadAndLock(db, equipmentId);

            if (AssetTagExists(db, assetTag, equipmentId))
                throw new AppException("This asset tag already exists.");

            // The status has its own rules (see below).
            if (status != item.Status)
                CheckStatusChange(db, item, status);

            item.Name = name.Trim();
            item.Type = type;
            item.AssetTag = assetTag.Trim();
            item.Status = status;
            item.Description = Check.Clean(description);

            db.SaveChanges();
            transaction.Commit();
        }
    }

    // Removes an item. If it was ever booked, we keep it for the history and mark it RETIRED instead.
    // Returns true if it was retired, false if it was really deleted.
    public bool Remove(long equipmentId)
    {
        using (LectroLendDbContext db = new LectroLendDbContext(settings))
        {
            Equipment item = FindItem(db, equipmentId);
            List<Reservation> bookings = db.Reservations.Where(r => r.EquipmentId == equipmentId).ToList();

            foreach (Reservation booking in bookings)
            {
                if (booking.IsActive())
                    throw new AppException("This item has active bookings. Finish or cancel them first.");
            }

            if (bookings.Count > 0)
            {
                item.Status = EquipmentStatus.RETIRED;
                db.SaveChanges();
                return true;
            }

            db.Equipment.Remove(item);
            db.SaveChanges();
            return false;
        }
    }

    // Rules for changing the status by hand:
    //  - while someone has the item, it must stay "in use" (check it in first);
    //  - "in use" is only set by handing the item over.
    private static void CheckStatusChange(LectroLendDbContext db, Equipment item, EquipmentStatus newStatus)
    {
        bool someoneHasIt = db.Reservations.Any(r => r.EquipmentId == item.EquipmentId
            && (r.Status == ReservationStatus.ISSUED || r.Status == ReservationStatus.OVERDUE));

        if (someoneHasIt)
            throw new AppException("Someone has this item right now. Check it in first.");
        if (newStatus == EquipmentStatus.IN_USE)
            throw new AppException("'In use' is set automatically when an item is handed over.");
    }

    // True if another item (not the one with ignoreId) already has this asset tag.
    private static bool AssetTagExists(LectroLendDbContext db, string assetTag, long ignoreId)
    {
        string tag = assetTag.Trim().ToLower();
        return db.Equipment.Any(e => e.AssetTag.ToLower() == tag && e.EquipmentId != ignoreId);
    }

    public static Equipment FindItem(LectroLendDbContext db, long equipmentId)
    {
        Equipment item = db.Equipment.FirstOrDefault(e => e.EquipmentId == equipmentId);
        if (item == null)
            throw new AppException("Equipment not found.");
        return item;
    }

    // Loads an item and "locks" its row until the transaction ends.
    // If two people try to book the same item at the same moment, the second one waits,
    // and then sees the first booking. This is how we prevent double booking.
    public static Equipment LoadAndLock(LectroLendDbContext db, long equipmentId)
    {
        db.Database.ExecuteSqlRaw("SELECT 1 FROM equipment WHERE equipment_id = {0} FOR UPDATE", equipmentId);
        return FindItem(db, equipmentId);
    }
}

// Adding, editing and deleting rooms.
public class RoomService
{
    private DbSettings settings;

    public RoomService(DbSettings settings)
    {
        this.settings = settings;
    }

    public List<Room> List()
    {
        using (LectroLendDbContext db = new LectroLendDbContext(settings))
        {
            return db.Rooms.OrderBy(r => r.RoomCode).ToList();
        }
    }

    public void Create(string roomCode, string building, int capacity)
    {
        CheckRoom(roomCode, building, capacity);

        using (LectroLendDbContext db = new LectroLendDbContext(settings))
        {
            if (CodeExists(db, roomCode, 0))
                throw new AppException("This room code already exists.");

            Room room = new Room();
            room.RoomCode = roomCode.Trim();
            room.Building = building.Trim();
            room.Capacity = capacity;

            db.Rooms.Add(room);
            db.SaveChanges();
        }
    }

    public void Update(long roomId, string roomCode, string building, int capacity)
    {
        CheckRoom(roomCode, building, capacity);

        using (LectroLendDbContext db = new LectroLendDbContext(settings))
        {
            Room room = FindRoom(db, roomId);
            if (CodeExists(db, roomCode, roomId))
                throw new AppException("This room code already exists.");

            room.RoomCode = roomCode.Trim();
            room.Building = building.Trim();
            room.Capacity = capacity;
            db.SaveChanges();
        }
    }

    // A room that was used in a booking cannot be deleted (the booking still points to it).
    public void Delete(long roomId)
    {
        using (LectroLendDbContext db = new LectroLendDbContext(settings))
        {
            Room room = FindRoom(db, roomId);
            if (db.Reservations.Any(r => r.RoomId == roomId))
                throw new AppException("This room is used by bookings and cannot be deleted.");

            db.Rooms.Remove(room);
            db.SaveChanges();
        }
    }

    private static void CheckRoom(string roomCode, string building, int capacity)
    {
        Check.Required(roomCode, "Room code", 20);
        Check.Required(building, "Building", 60);
        if (capacity < 1)
            throw new AppException("The capacity must be at least 1.");
    }

    private static bool CodeExists(LectroLendDbContext db, string roomCode, long ignoreId)
    {
        string code = roomCode.Trim().ToLower();
        return db.Rooms.Any(r => r.RoomCode.ToLower() == code && r.RoomId != ignoreId);
    }

    private static Room FindRoom(LectroLendDbContext db, long roomId)
    {
        Room room = db.Rooms.FirstOrDefault(r => r.RoomId == roomId);
        if (room == null)
            throw new AppException("Room not found.");
        return room;
    }
}
