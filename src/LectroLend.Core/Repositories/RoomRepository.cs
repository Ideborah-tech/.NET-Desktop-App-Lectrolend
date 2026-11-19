using LectroLend.Core.Data;
using LectroLend.Core.Models;

namespace LectroLend.Core.Repositories;

// REPOSITORY: the only place that reads and writes rooms in the database.
public class RoomRepository
{
    private LectroLendDbContext db;

    public RoomRepository(LectroLendDbContext db)
    {
        this.db = db;
    }

    // All rooms, sorted by room code.
    public List<Room> GetAll()
    {
        return db.Rooms.OrderBy(r => r.RoomCode).ToList();
    }

    // Finds a room by id. Returns null if there is none.
    public Room FindById(long roomId)
    {
        return db.Rooms.FirstOrDefault(r => r.RoomId == roomId);
    }

    // True if another room (not the one with ignoreId) already has this code.
    public bool CodeExists(string roomCode, long ignoreId)
    {
        string code = roomCode.Trim().ToLower();
        return db.Rooms.Any(r => r.RoomCode.ToLower() == code && r.RoomId != ignoreId);
    }

    public void Add(Room room)
    {
        db.Rooms.Add(room);
    }

    public void Delete(Room room)
    {
        db.Rooms.Remove(room);
    }

    // Writes all changes to the database.
    public void Save()
    {
        db.SaveChanges();
    }
}
