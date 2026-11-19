using LectroLend.Core.Models;
using LectroLend.Desktop.Controllers;
using LectroLend.Desktop.Views.UI;

namespace LectroLend.Desktop.Views;

// The "My reservations" page: the signed-in person's bookings, with a "Cancel booking" button.
// The look of the page is in MyReservationsPage.Designer.cs (open it in Design view).
// MVC: this is a VIEW. It shows things and asks the controllers (app.Account, app.Equipment,
// app.Rooms, app.Reservations) to do the work.
public partial class MyReservationsPage : UserControl
{
    private AppControllers app;

    public MyReservationsPage(AppControllers app)
    {
        InitializeComponent(); // creates the controls you see in Design view

        this.app = app;
        Ui.StyleGrid(grid); // give the table the app's colours
    }

    // Reads my bookings and puts one row per booking in the table.
    private void LoadData()
    {
        try
        {
            grid.Rows.Clear();
            foreach (Reservation r in app.Reservations.GetMine())
            {
                string room = "-";
                if (r.Room != null)
                    room = r.Room.RoomCode;

                int row = grid.Rows.Add(r.Equipment.Name, r.Equipment.AssetTag,
                    Ui.FormatTime(r.StartTime), Ui.FormatTime(r.EndTime), room, Labels.Of(r.Status));

                // Keep the booking inside the row, so we know which booking is selected later.
                grid.Rows[row].Tag = r;
            }
        }
        catch (Exception ex)
        {
            Ui.ShowError(ex);
        }
    }

    // Runs when the page opens, and when "Refresh" is clicked.
    private void RefreshButton_Click(object sender, EventArgs e)
    {
        LoadData();
    }

    private void CancelButton_Click(object sender, EventArgs e)
    {
        if (grid.SelectedRows.Count == 0)
        {
            Ui.ShowInfo("Please select a booking first.");
            return;
        }

        Reservation r = (Reservation)grid.SelectedRows[0].Tag; // the booking stored in the selected row
        if (!Ui.Confirm("Cancel your booking of " + r.Equipment.Name + "?"))
            return;

        try
        {
            app.Reservations.CancelMine(r);
            LoadData();
        }
        catch (Exception ex)
        {
            Ui.ShowError(ex); // for example "Only a booked reservation can be cancelled."
        }
    }
}
