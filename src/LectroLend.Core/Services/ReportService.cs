using System.Text;
using LectroLend.Core.Data;
using LectroLend.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace LectroLend.Core.Services;

// Numbers for the dashboard and the CSV export (a file that opens in Excel).
public class ReportService
{
    private DbSettings settings;

    public ReportService(DbSettings settings)
    {
        this.settings = settings;
    }

    public DashboardNumbers GetDashboardNumbers()
    {
        using (LectroLendDbContext db = new LectroLendDbContext(settings))
        {
            DashboardNumbers numbers = new DashboardNumbers();
            numbers.TotalEquipment = db.Equipment.Count();
            numbers.AvailableEquipment = db.Equipment.Count(e => e.Status == EquipmentStatus.AVAILABLE);
            return numbers;
        }
    }

    // Makes a CSV text: one line per booking, values separated by commas.
    public string GenerateCsv()
    {
        List<Reservation> all;
        using (LectroLendDbContext db = new LectroLendDbContext(settings))
        {
            all = db.Reservations
                .Include(r => r.User)
                .Include(r => r.Equipment)
                .Include(r => r.Room)
                .Include(r => r.ReturnRecord)
                .OrderByDescending(r => r.StartTime)
                .ToList();
        }

        StringBuilder csv = new StringBuilder();
        csv.AppendLine("ID,Borrower,Email,Equipment,Asset Tag,Status,Start,End,Room,Late Return");

        foreach (Reservation r in all)
        {
            string room = "";
            if (r.Room != null)
                room = r.Room.RoomCode;

            string late = "No";
            if (r.ReturnRecord != null && r.ReturnRecord.LateReturn)
                late = "Yes";

            csv.AppendLine(r.ReservationId + ","
                + CsvValue(r.User.FullName) + ","
                + CsvValue(r.User.Email) + ","
                + CsvValue(r.Equipment.Name) + ","
                + CsvValue(r.Equipment.AssetTag) + ","
                + r.Status + ","
                + r.StartTime.ToString("yyyy-MM-dd HH:mm") + ","
                + r.EndTime.ToString("yyyy-MM-dd HH:mm") + ","
                + CsvValue(room) + ","
                + late);
        }
        return csv.ToString();
    }

    // A value with a comma or a quote must be put inside quotes, or Excel splits it into two columns.
    private static string CsvValue(string value)
    {
        if (value == null)
            return "";
        if (value.Contains(",") || value.Contains("\""))
            return "\"" + value.Replace("\"", "\"\"") + "\"";
        return value;
    }
}
