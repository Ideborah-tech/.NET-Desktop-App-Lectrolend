using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LectroLend.Core.Models;

// One item that can be borrowed, for example a projector.
[Table("equipment")]
public class Equipment
{
    [Key]
    [Column("equipment_id")]
    public long EquipmentId { get; set; }

    // The admin who added this item.
    [Column("added_by")]
    public long AddedById { get; set; }

    [ForeignKey("AddedById")]
    public Admin AddedBy { get; set; }

    [Column("name")]
    public string Name { get; set; }

    [Column("type")]
    public EquipmentType Type { get; set; }

    // The code stuck on the item, for example "PRJ-001".
    [Column("asset_tag")]
    public string AssetTag { get; set; }

    [Column("status")]
    public EquipmentStatus Status { get; set; } = EquipmentStatus.AVAILABLE;

    [Column("description")]
    public string Description { get; set; }

    // True when the item is on the shelf and can be handed over now.
    public bool IsAvailable()
    {
        return Status == EquipmentStatus.AVAILABLE;
    }

    // True when the item can be booked.
    // An item that is in use can still be booked for later. Broken or retired items cannot.
    public bool IsReservable()
    {
        return Status == EquipmentStatus.AVAILABLE || Status == EquipmentStatus.IN_USE;
    }
}

// A classroom where the equipment will be used.
[Table("room")]
public class Room
{
    [Key]
    [Column("room_id")]
    public long RoomId { get; set; }

    [Column("room_code")]
    public string RoomCode { get; set; }

    [Column("building")]
    public string Building { get; set; }

    [Column("capacity")]
    public int Capacity { get; set; }

    // What a drop-down list shows for this room, for example "A101 (Main Building)".
    public override string ToString()
    {
        return RoomCode + " (" + Building + ")";
    }
}
