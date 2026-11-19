using LectroLend.Core.Models;
using LectroLend.Core.Services;
using Xunit;

namespace LectroLend.Core.Tests;

// Tests for the state diagram and the booking rules.
// Each [Fact] is one test. "Assert" checks that something is true; if not, the test fails.
// Run them with:  dotnet test
public class ReservationStateTests
{
    // A fixed "now" so the tests always give the same result: 2 March 2026 at 10:00.
    private DateTime now = new DateTime(2026, 3, 2, 10, 0, 0);

    // A new booking from 11:00 to 13:00.
    private Reservation MakeBooking()
    {
        Reservation r = new Reservation();
        r.StartTime = now.AddHours(1);
        r.EndTime = now.AddHours(3);
        r.Status = ReservationStatus.BOOKED;
        return r;
    }

    [Fact]
    public void A_booking_can_be_cancelled()
    {
        Reservation r = MakeBooking();
        r.Cancel();
        Assert.Equal(ReservationStatus.CANCELLED, r.Status);
    }

    [Fact]
    public void Hand_over_then_return()
    {
        Reservation r = MakeBooking();

        r.MarkIssued(now);
        Assert.Equal(ReservationStatus.ISSUED, r.Status);
        Assert.Equal(now, r.IssuedAt);

        r.MarkReturned();
        Assert.Equal(ReservationStatus.RETURNED, r.Status);
    }

    [Fact]
    public void A_handed_over_item_cannot_be_cancelled()
    {
        Reservation r = MakeBooking();
        r.MarkIssued(now);
        Assert.Throws<InvalidOperationException>(() => r.Cancel());
    }

    [Fact]
    public void A_booking_that_was_not_handed_over_cannot_be_returned()
    {
        Reservation r = MakeBooking();
        Assert.Throws<InvalidOperationException>(() => r.MarkReturned());
    }

    [Fact]
    public void An_overdue_item_can_still_be_returned()
    {
        Reservation r = MakeBooking();
        r.MarkIssued(now);
        r.MarkOverdue();
        Assert.Equal(ReservationStatus.OVERDUE, r.Status);

        r.MarkReturned();
        Assert.Equal(ReservationStatus.RETURNED, r.Status);
    }

    [Fact]
    public void Only_a_booking_can_expire()
    {
        Reservation r = MakeBooking();
        r.Expire();
        Assert.Equal(ReservationStatus.EXPIRED, r.Status);

        // It is already expired, so it cannot expire again.
        Assert.Throws<InvalidOperationException>(() => r.Expire());
    }

    [Fact]
    public void Overlapping_times()
    {
        Reservation r = MakeBooking(); // 11:00 to 13:00

        Assert.True(r.Overlaps(now.AddHours(2), now.AddHours(4)));  // 12:00-14:00 shares time
        Assert.True(r.Overlaps(now, now.AddHours(5)));              // 10:00-15:00 covers it
        Assert.False(r.Overlaps(now.AddHours(3), now.AddHours(4))); // 13:00-14:00 starts when it ends: OK
        Assert.False(r.Overlaps(now, now.AddHours(1)));             // 10:00-11:00 ends when it starts: OK
    }

    [Fact]
    public void An_item_is_late_after_the_end_time()
    {
        Reservation r = MakeBooking();
        r.MarkIssued(now);
        Assert.False(r.IsLate(now.AddHours(2))); // 12:00, before the end
        Assert.True(r.IsLate(now.AddHours(4)));  // 14:00, after the end
    }

    [Fact]
    public void Students_can_hold_3_bookings_and_lecturers_5()
    {
        Assert.Equal(3, new Student().MaxActiveReservations);
        Assert.Equal(5, new Lecturer().MaxActiveReservations);
        Assert.Equal(Role.ADMIN, new Admin().Role);
    }

    [Fact]
    public void Which_items_can_be_booked()
    {
        Equipment item = new Equipment();

        item.Status = EquipmentStatus.IN_USE;
        Assert.True(item.IsReservable());   // in use now, but can be booked for later
        Assert.False(item.IsAvailable());   // but it cannot be handed over now

        item.Status = EquipmentStatus.UNDER_MAINTENANCE;
        Assert.False(item.IsReservable());

        item.Status = EquipmentStatus.RETIRED;
        Assert.False(item.IsReservable());
    }

    [Fact]
    public void The_end_must_be_after_the_start()
    {
        AppException error = Assert.Throws<AppException>(() =>
            ReservationService.CheckTimes(now.AddHours(2), now.AddHours(1), now));
        Assert.Equal("The end time must be after the start time.", error.Message);
    }

    [Fact]
    public void The_start_cannot_be_in_the_past()
    {
        AppException error = Assert.Throws<AppException>(() =>
            ReservationService.CheckTimes(now.AddHours(-1), now.AddHours(1), now));
        Assert.Equal("The start time cannot be in the past.", error.Message);
    }

    [Fact]
    public void A_booking_lasts_at_most_7_days()
    {
        AppException error = Assert.Throws<AppException>(() =>
            ReservationService.CheckTimes(now, now.AddDays(7).AddMinutes(1), now));
        Assert.Equal("A booking cannot be longer than 7 days.", error.Message);
    }

    [Fact]
    public void Good_times_are_accepted()
    {
        // These must not throw an error.
        ReservationService.CheckTimes(now.AddMinutes(-2), now.AddHours(2), now); // 2 minutes ago is fine
        ReservationService.CheckTimes(now, now.AddDays(7), now);                  // exactly 7 days is fine
    }

    [Fact]
    public void Cancelled_bookings_do_not_block_the_item()
    {
        Reservation cancelled = MakeBooking();
        cancelled.Cancel();

        List<Reservation> bookings = new List<Reservation>();
        bookings.Add(cancelled);

        // Same time as the cancelled booking, but it is free.
        Assert.False(ReservationService.HasOverlap(bookings, now.AddHours(1), now.AddHours(3)));
    }
}
