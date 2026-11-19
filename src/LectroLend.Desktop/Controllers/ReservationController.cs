using LectroLend.Core.Models;
using LectroLend.Core.Services;

namespace LectroLend.Desktop.Controllers;

// Controller for bookings: reserving, cancelling, handing over, checking in, the dashboard and the CSV export.
public class ReservationController
{
    private ReservationService reservationService;
    private ReportService reportService;

    public ReservationController(ReservationService reservationService, ReportService reportService)
    {
        this.reservationService = reservationService;
        this.reportService = reportService;
    }

    // ---- For students and lecturers ----

    // Books an item for the signed-in person. room can be null (no room).
    public void Reserve(Equipment item, DateTime start, DateTime end, Room room)
    {
        long? roomId = null;
        if (room != null)
            roomId = room.RoomId;

        reservationService.Create(Session.CurrentUser.UserId, item.EquipmentId, start, end, roomId);
    }

    // The signed-in person's bookings, newest first.
    public List<Reservation> GetMine()
    {
        return reservationService.Mine(Session.CurrentUser.UserId);
    }

    // How many bookings the signed-in person holds right now.
    public int MyActiveCount()
    {
        return reservationService.ActiveCount(Session.CurrentUser.UserId);
    }

    // A borrower cancels one of their own bookings.
    public void CancelMine(Reservation reservation)
    {
        reservationService.Cancel(reservation.ReservationId, Session.CurrentUser.UserId, false);
    }

    // ---- For the IT office ----

    // All bookings. status = null means all of them.
    public List<Reservation> GetAll(ReservationStatus? status)
    {
        return reservationService.All(status);
    }

    // Bookings waiting to be picked up (for the dashboard).
    public List<Reservation> GetToCollect()
    {
        return reservationService.All(ReservationStatus.BOOKED);
    }

    // Items that people have right now, including late ones (for the dashboard).
    public List<Reservation> GetOutNow()
    {
        List<Reservation> result = new List<Reservation>();
        foreach (Reservation r in reservationService.All(null))
        {
            if (r.Status == ReservationStatus.ISSUED || r.Status == ReservationStatus.OVERDUE)
                result.Add(r);
        }
        return result;
    }

    public DashboardNumbers GetDashboardNumbers()
    {
        return reportService.GetDashboardNumbers();
    }

    // The borrower picks the item up.
    public void HandOver(Reservation reservation)
    {
        reservationService.Issue(reservation.ReservationId);
    }

    // The item comes back. If "damaged" is ticked but the condition still says "Good", we write "Damaged".
    public void CheckIn(Reservation reservation, bool damaged, string condition, string remarks)
    {
        if (damaged && condition == "Good")
            condition = "Damaged";

        reservationService.ConfirmReturn(reservation.ReservationId, Session.CurrentUser.UserId, damaged, condition, remarks);
    }

    // The IT office cancels any booking.
    public void CancelAny(Reservation reservation)
    {
        reservationService.Cancel(reservation.ReservationId, Session.CurrentUser.UserId, true);
    }

    // Saves all bookings into a CSV file (opens in Excel).
    public void ExportCsv(string filePath)
    {
        File.WriteAllText(filePath, reportService.GenerateCsv());
    }

    // ---- Every minute ----

    // Expires bookings nobody picked up and marks late items as overdue.
    public void RunHousekeeping()
    {
        reservationService.RunHousekeeping();
    }
}
