using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LectroLend.Core.Models;

// A booking: one person borrows one item from a start time to an end time.
[Table("reservation")]
public class Reservation
{
    [Key]
    [Column("reservation_id")]
    public long ReservationId { get; set; }

    // Who booked it.
    [Column("user_id")]
    public long UserId { get; set; }

    [ForeignKey("UserId")]
    public User User { get; set; }

    // Which item.
    [Column("equipment_id")]
    public long EquipmentId { get; set; }

    [ForeignKey("EquipmentId")]
    public Equipment Equipment { get; set; }

    // Which room (optional, can be empty).
    [Column("room_id")]
    public long? RoomId { get; set; }

    [ForeignKey("RoomId")]
    public Room Room { get; set; }

    [Column("created_at", TypeName = "timestamp without time zone")]
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    [Column("start_time", TypeName = "timestamp without time zone")]
    public DateTime StartTime { get; set; }

    [Column("end_time", TypeName = "timestamp without time zone")]
    public DateTime EndTime { get; set; }

    [Column("status")]
    public ReservationStatus Status { get; set; } = ReservationStatus.BOOKED;

    // When the item was handed over (empty until then).
    [Column("issued_at", TypeName = "timestamp without time zone")]
    public DateTime? IssuedAt { get; set; }

    // Filled in when the item comes back.
    public ReturnRecord ReturnRecord { get; set; }

    // ---- The state diagram ----
    // Each method below is one arrow of the state diagram.
    // If the arrow is not allowed from the current status, we stop with an error.

    // BOOKED -> CANCELLED
    public void Cancel()
    {
        if (Status != ReservationStatus.BOOKED)
            throw new InvalidOperationException("Only a booked reservation can be cancelled.");
        Status = ReservationStatus.CANCELLED;
    }

    // BOOKED -> ISSUED (the borrower picks the item up)
    public void MarkIssued(DateTime now)
    {
        if (Status != ReservationStatus.BOOKED)
            throw new InvalidOperationException("Only a booked reservation can be handed over.");
        Status = ReservationStatus.ISSUED;
        IssuedAt = now;
    }

    // ISSUED or OVERDUE -> RETURNED
    public void MarkReturned()
    {
        if (Status != ReservationStatus.ISSUED && Status != ReservationStatus.OVERDUE)
            throw new InvalidOperationException("Only an item that was handed over can be returned.");
        Status = ReservationStatus.RETURNED;
    }

    // BOOKED -> EXPIRED (nobody came to pick it up)
    public void Expire()
    {
        if (Status != ReservationStatus.BOOKED)
            throw new InvalidOperationException("Only a booked reservation can expire.");
        Status = ReservationStatus.EXPIRED;
    }

    // ISSUED -> OVERDUE (the end time has passed)
    public void MarkOverdue()
    {
        if (Status != ReservationStatus.ISSUED)
            throw new InvalidOperationException("Only an item that was handed over can become overdue.");
        Status = ReservationStatus.OVERDUE;
    }

    // True when the borrower has the item and the end time has passed.
    public bool IsLate(DateTime now)
    {
        bool borrowerHasIt = Status == ReservationStatus.ISSUED || Status == ReservationStatus.OVERDUE;
        return borrowerHasIt && now > EndTime;
    }

    // True when this booking and the given time (start to end) share some time.
    public bool Overlaps(DateTime start, DateTime end)
    {
        return StartTime < end && EndTime > start;
    }

    // True while the booking still holds the item (booked, picked up or late).
    public bool IsActive()
    {
        return Status == ReservationStatus.BOOKED
            || Status == ReservationStatus.ISSUED
            || Status == ReservationStatus.OVERDUE;
    }
}

// What happened when an item was brought back.
[Table("return_record")]
public class ReturnRecord
{
    [Key]
    [Column("return_id")]
    public long ReturnId { get; set; }

    [Column("reservation_id")]
    public long ReservationId { get; set; }

    public Reservation Reservation { get; set; }

    // The admin who checked the item in.
    [Column("confirmed_by")]
    public long ConfirmedById { get; set; }

    [ForeignKey("ConfirmedById")]
    public Admin ConfirmedBy { get; set; }

    [Column("returned_at", TypeName = "timestamp without time zone")]
    public DateTime ReturnedAt { get; set; }

    [Column("condition_on_return")]
    public string ConditionOnReturn { get; set; }

    [Column("late_return")]
    public bool LateReturn { get; set; }

    [Column("remarks")]
    public string Remarks { get; set; }
}
