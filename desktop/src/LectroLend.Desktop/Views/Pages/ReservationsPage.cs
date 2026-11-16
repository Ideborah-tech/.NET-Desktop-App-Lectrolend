using LectroLend.Core.Models;
using LectroLend.Desktop.Controllers;
using LectroLend.Desktop.Views.UI;

namespace LectroLend.Desktop.Views;

// The IT office "Reservations" page: all bookings, with buttons to
// hand over, check in, cancel, and save the list to a CSV file (opens in Excel).
// The look of the page is in ReservationsPage.Designer.cs (open it in Design view).
// MVC: this is a VIEW. It shows things and asks the controllers (app.Account, app.Equipment,
// app.Rooms, app.Reservations) to do the work.
public partial class ReservationsPage : UserControl
{
    private AppControllers app;

    public ReservationsPage(AppControllers app)
    {
        InitializeComponent(); // creates the controls you see in Design view

        this.app = app;
        Ui.StyleGrid(grid); // give the table the app's colours

        // The status filter. Selecting the first line also loads the table for the first time.
        statusBox.Items.Add("All");
        statusBox.Items.Add(ReservationStatus.BOOKED);
        statusBox.Items.Add(ReservationStatus.ISSUED);
        statusBox.Items.Add(ReservationStatus.OVERDUE);
        statusBox.Items.Add(ReservationStatus.RETURNED);
        statusBox.Items.Add(ReservationStatus.CANCELLED);
        statusBox.Items.Add(ReservationStatus.EXPIRED);
        statusBox.SelectedIndex = 0;
    }

    private void StatusBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadData();
    }

    // Reads the bookings (only the chosen status, or all) and fills the table.
    private void LoadData()
    {
        if (app == null)
            return; // the page is still being built

        try
        {
            // The first line, "All", means no filter.
            ReservationStatus? status = null;
            if (statusBox.SelectedIndex > 0)
                status = (ReservationStatus)statusBox.SelectedItem;

            grid.Rows.Clear();
            foreach (Reservation r in app.Reservations.GetAll(status))
            {
                string room = "-";
                if (r.Room != null)
                    room = r.Room.RoomCode;

                string item = r.Equipment.Name + " (" + r.Equipment.AssetTag + ")";
                int row = grid.Rows.Add(r.ReservationId, r.User.FullName, item,
                    Ui.FormatTime(r.StartTime), Ui.FormatTime(r.EndTime), room, Labels.Of(r.Status));
                grid.Rows[row].Tag = r; // keep the booking inside the row
            }
        }
        catch (Exception ex)
        {
            Ui.ShowError(ex);
        }
    }

    // Returns the selected booking, or null (with a message) if nothing is selected.
    private Reservation SelectedReservation()
    {
        if (grid.SelectedRows.Count == 0)
        {
            Ui.ShowInfo("Please select a booking first.");
            return null;
        }
        return (Reservation)grid.SelectedRows[0].Tag;
    }

    private void HandOverButton_Click(object sender, EventArgs e)
    {
        Reservation r = SelectedReservation();
        if (r == null)
            return;

        try
        {
            app.Reservations.HandOver(r);
            LoadData();
        }
        catch (Exception ex)
        {
            Ui.ShowError(ex);
        }
    }

    private void CheckInButton_Click(object sender, EventArgs e)
    {
        Reservation r = SelectedReservation();
        if (r == null)
            return;

        ReturnForm returnForm = new ReturnForm(app, r);
        if (returnForm.ShowDialog(this) == DialogResult.OK)
            LoadData();
    }

    private void CancelButton_Click(object sender, EventArgs e)
    {
        Reservation r = SelectedReservation();
        if (r == null)
            return;
        if (!Ui.Confirm("Cancel this booking of " + r.Equipment.Name + "?"))
            return;

        try
        {
            app.Reservations.CancelAny(r);
            LoadData();
        }
        catch (Exception ex)
        {
            Ui.ShowError(ex);
        }
    }

    // Saves all bookings to a CSV file that the user chooses.
    private void ExportButton_Click(object sender, EventArgs e)
    {
        SaveFileDialog dialog = new SaveFileDialog();
        dialog.Filter = "CSV file (*.csv)|*.csv";
        dialog.FileName = "reservations.csv";
        if (dialog.ShowDialog() != DialogResult.OK)
            return; // the user clicked "Cancel"

        try
        {
            app.Reservations.ExportCsv(dialog.FileName);
            Ui.ShowInfo("Saved to " + dialog.FileName);
        }
        catch (Exception ex)
        {
            Ui.ShowError(ex);
        }
    }
}
