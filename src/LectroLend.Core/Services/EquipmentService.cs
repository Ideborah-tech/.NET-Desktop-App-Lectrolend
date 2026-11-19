using LectroLend.Core.Data;
using LectroLend.Core.Models;
using LectroLend.Core.Repositories;

namespace LectroLend.Core.Services;

// SERVICE: the rules for equipment: listing, checking if it is free, adding, editing and removing.
// It gets and saves data through the EquipmentRepository and the ReservationRepository.
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
            EquipmentRepository equipment = new EquipmentRepository(db);
            all = equipment.GetAll();
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
            EquipmentRepository equipment = new EquipmentRepository(db);
            ReservationRepository reservations = new ReservationRepository(db);

            Equipment item = FindItem(equipment, equipmentId);
            List<Reservation> bookings = reservations.GetForEquipment(equipmentId);

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
            EquipmentRepository equipment = new EquipmentRepository(db);
            if (equipment.AssetTagExists(assetTag, 0))
                throw new AppException("This asset tag already exists.");

            Equipment item = new Equipment();
            item.AddedById = adminId;
            item.Name = name.Trim();
            item.Type = type;
            item.AssetTag = assetTag.Trim();
            item.Status = status;
            item.Description = Check.Clean(description);

            equipment.Add(item);
            equipment.Save();
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
            EquipmentRepository equipment = new EquipmentRepository(db);
            ReservationRepository reservations = new ReservationRepository(db);

            Equipment item = equipment.FindByIdAndLock(equipmentId);
            if (item == null)
                throw new AppException("Equipment not found.");
            if (equipment.AssetTagExists(assetTag, equipmentId))
                throw new AppException("This asset tag already exists.");

            // Rules for changing the status by hand:
            //  - while someone has the item, it must stay "in use" (check it in first);
            //  - "in use" is only set by handing the item over.
            if (status != item.Status)
            {
                if (reservations.SomeoneHasItem(equipmentId))
                    throw new AppException("Someone has this item right now. Check it in first.");
                if (status == EquipmentStatus.IN_USE)
                    throw new AppException("'In use' is set automatically when an item is handed over.");
            }

            item.Name = name.Trim();
            item.Type = type;
            item.AssetTag = assetTag.Trim();
            item.Status = status;
            item.Description = Check.Clean(description);

            equipment.Save();
            transaction.Commit();
        }
    }

    // Removes an item. If it was ever booked, we keep it for the history and mark it RETIRED instead.
    // Returns true if it was retired, false if it was really deleted.
    public bool Remove(long equipmentId)
    {
        using (LectroLendDbContext db = new LectroLendDbContext(settings))
        {
            EquipmentRepository equipment = new EquipmentRepository(db);
            ReservationRepository reservations = new ReservationRepository(db);

            Equipment item = FindItem(equipment, equipmentId);
            List<Reservation> bookings = reservations.GetForEquipment(equipmentId);

            foreach (Reservation booking in bookings)
            {
                if (booking.IsActive())
                    throw new AppException("This item has active bookings. Finish or cancel them first.");
            }

            if (bookings.Count > 0)
            {
                item.Status = EquipmentStatus.RETIRED;
                equipment.Save();
                return true;
            }

            equipment.Delete(item);
            equipment.Save();
            return false;
        }
    }

    // Asks the repository for the item, and stops with a message if there is none.
    private static Equipment FindItem(EquipmentRepository equipment, long equipmentId)
    {
        Equipment item = equipment.FindById(equipmentId);
        if (item == null)
            throw new AppException("Equipment not found.");
        return item;
    }
}

// SERVICE: the rules for rooms. It gets and saves data through the RoomRepository.
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
            RoomRepository rooms = new RoomRepository(db);
            return rooms.GetAll();
        }
    }

    public void Create(string roomCode, string building, int capacity)
    {
        CheckRoom(roomCode, building, capacity);

        using (LectroLendDbContext db = new LectroLendDbContext(settings))
        {
            RoomRepository rooms = new RoomRepository(db);
            if (rooms.CodeExists(roomCode, 0))
                throw new AppException("This room code already exists.");

            Room room = new Room();
            room.RoomCode = roomCode.Trim();
            room.Building = building.Trim();
            room.Capacity = capacity;

            rooms.Add(room);
            rooms.Save();
        }
    }

    public void Update(long roomId, string roomCode, string building, int capacity)
    {
        CheckRoom(roomCode, building, capacity);

        using (LectroLendDbContext db = new LectroLendDbContext(settings))
        {
            RoomRepository rooms = new RoomRepository(db);
            Room room = FindRoom(rooms, roomId);
            if (rooms.CodeExists(roomCode, roomId))
                throw new AppException("This room code already exists.");

            room.RoomCode = roomCode.Trim();
            room.Building = building.Trim();
            room.Capacity = capacity;
            rooms.Save();
        }
    }

    // A room that was used in a booking cannot be deleted (the booking still points to it).
    public void Delete(long roomId)
    {
        using (LectroLendDbContext db = new LectroLendDbContext(settings))
        {
            RoomRepository rooms = new RoomRepository(db);
            ReservationRepository reservations = new ReservationRepository(db);

            Room room = FindRoom(rooms, roomId);
            if (reservations.RoomIsUsed(roomId))
                throw new AppException("This room is used by bookings and cannot be deleted.");

            rooms.Delete(room);
            rooms.Save();
        }
    }

    private static void CheckRoom(string roomCode, string building, int capacity)
    {
        Check.Required(roomCode, "Room code", 20);
        Check.Required(building, "Building", 60);
        if (capacity < 1)
            throw new AppException("The capacity must be at least 1.");
    }

    // Asks the repository for the room, and stops with a message if there is none.
    private static Room FindRoom(RoomRepository rooms, long roomId)
    {
        Room room = rooms.FindById(roomId);
        if (room == null)
            throw new AppException("Room not found.");
        return room;
    }
}
