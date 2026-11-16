using LectroLend.Core.Models;
using LectroLend.Core.Services;

namespace LectroLend.Desktop.Controllers;

// Controller for rooms: the Rooms page and the Add / Edit room window.
public class RoomController
{
    private RoomService roomService;

    public RoomController(RoomService roomService)
    {
        this.roomService = roomService;
    }

    public List<Room> GetRooms()
    {
        return roomService.List();
    }

    // Saves the Add / Edit room window. existing = null means "add a new room".
    public void Save(Room existing, string roomCode, string building, int capacity)
    {
        if (existing == null)
            roomService.Create(roomCode, building, capacity);
        else
            roomService.Update(existing.RoomId, roomCode, building, capacity);
    }

    public void Delete(Room room)
    {
        roomService.Delete(room.RoomId);
    }
}
