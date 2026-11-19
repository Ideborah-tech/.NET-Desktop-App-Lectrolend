using LectroLend.Core.Models;
using LectroLend.Desktop.Controllers;
using LectroLend.Desktop.Views.UI;

namespace LectroLend.Desktop.Views;

// The IT office "Dashboard": four numbers at the top, then two lists:
//   "To collect" - booked items waiting to be picked up (button: Hand over)
//   "Out now"    - items people have right now (button: Check in)
// The look of the page is in DashboardPage.Designer.cs (open it in Design view).
// MVC: this is a VIEW. It shows things and asks the controllers (app.Account, app.Equipment,
// app.Rooms, app.Reservations) to do the work.
public partial class DashboardPage : UserControl
{
    private AppControllers app;

    public DashboardPage(AppControllers app)
    {
        InitializeComponent(); // creates the controls you see in Design view

        this.app = app;
        Ui.StyleGrid(toCollectGrid); // give the tables the app's colours
        Ui.StyleGrid(outNowGrid);
    }

    private void DashboardPage_Load(object sender, EventArgs e)
    {
        LoadData();
    }

    // Reads all bookings and fills the two lists and the four numbers.
    private void LoadData()
    {
        try
        {
            // List 1: bookings waiting to be picked up.
            toCollectGrid.Rows.Clear();
            foreach (Reservation r in app.Reservations.GetToCollect())
            {
                string item = r.Equipment.Name + " (" + r.Equipment.AssetTag + ")";
                int row = toCollectGrid.Rows.Add(r.User.FullName, item, Ui.FormatTime(r.StartTime));
                toCollectGrid.Rows[row].Tag = r; // keep the booking inside the row
            }

            // List 2: items people have right now. Late items are written in red.
            outNowGrid.Rows.Clear();
            int lateCount = 0;
            foreach (Reservation r in app.Reservations.GetOutNow())
            {
                string item = r.Equipment.Name + " (" + r.Equipment.AssetTag + ")";
                int row = outNowGrid.Rows.Add(r.User.FullName, item, Ui.FormatTime(r.EndTime));
                outNowGrid.Rows[row].Tag = r;

                if (r.IsLate(DateTime.Now))
                {
                    lateCount++;
                    outNowGrid.Rows[row].DefaultCellStyle.ForeColor = Ui.Red;
                }
            }

            DashboardNumbers numbers = app.Reservations.GetDashboardNumbers();
            outNowNumber.Text = outNowGrid.Rows.Count.ToString();
            toCollectNumber.Text = toCollectGrid.Rows.Count.ToString();
            lateNumber.Text = lateCount.ToString();
            onShelfNumber.Text = numbers.AvailableEquipment + " / " + numbers.TotalEquipment;
        }
        catch (Exception ex)
        {
            Ui.ShowError(ex);
        }
    }

    // "Hand over": the borrower picks the item up.
    private void HandOverButton_Click(object sender, EventArgs e)
    {
        if (toCollectGrid.SelectedRows.Count == 0)
        {
            Ui.ShowInfo("Select a booking in 'To collect' first.");
            return;
        }

        Reservation r = (Reservation)toCollectGrid.SelectedRows[0].Tag;
        if (!Ui.Confirm("Hand over " + r.Equipment.Name + " to " + r.User.FullName + "?"))
            return;

        try
        {
            app.Reservations.HandOver(r);
            LoadData();
        }
        catch (Exception ex)
        {
            Ui.ShowError(ex); // for example "Too early. Pick-up opens 30 minutes before the start time."
        }
    }

    // "Check in": the item comes back.
    private void CheckInButton_Click(object sender, EventArgs e)
    {
        if (outNowGrid.SelectedRows.Count == 0)
        {
            Ui.ShowInfo("Select an item in 'Out now' first.");
            return;
        }

        Reservation r = (Reservation)outNowGrid.SelectedRows[0].Tag;
        ReturnForm returnForm = new ReturnForm(app, r);
        if (returnForm.ShowDialog(this) == DialogResult.OK)
            LoadData();
    }
}
