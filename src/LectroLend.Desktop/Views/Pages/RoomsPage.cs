using LectroLend.Core.Models;
using LectroLend.Desktop.Controllers;
using LectroLend.Desktop.Views.UI;

namespace LectroLend.Desktop.Views;

// The IT office "Rooms" page: add, edit and delete rooms.
// The look of the page is in RoomsPage.Designer.cs (open it in Design view).
// MVC: this is a VIEW. It shows things and asks the controllers (app.Account, app.Equipment,
// app.Rooms, app.Reservations) to do the work.
public partial class RoomsPage : UserControl
{
    private AppControllers app;

    public RoomsPage(AppControllers app)
    {
        InitializeComponent(); // creates the controls you see in Design view

        this.app = app;
        Ui.StyleGrid(grid); // give the table the app's colours
    }

    private void RoomsPage_Load(object sender, EventArgs e)
    {
        LoadData();
    }

    // Reads all rooms and fills the table.
    private void LoadData()
    {
        try
        {
            grid.Rows.Clear();
            foreach (Room room in app.Rooms.GetRooms())
            {
                int row = grid.Rows.Add(room.RoomCode, room.Building, room.Capacity);
                grid.Rows[row].Tag = room; // keep the room inside the row
            }
        }
        catch (Exception ex)
        {
            Ui.ShowError(ex);
        }
    }

    // Returns the selected room, or null (with a message) if nothing is selected.
    private Room SelectedRoom()
    {
        if (grid.SelectedRows.Count == 0)
        {
            Ui.ShowInfo("Please select a room first.");
            return null;
        }
        return (Room)grid.SelectedRows[0].Tag;
    }

    private void AddButton_Click(object sender, EventArgs e)
    {
        RoomForm form = new RoomForm(app, null); // null = a new room
        if (form.ShowDialog(this) == DialogResult.OK)
            LoadData();
    }

    private void EditButton_Click(object sender, EventArgs e)
    {
        Room room = SelectedRoom();
        if (room == null)
            return;

        RoomForm form = new RoomForm(app, room);
        if (form.ShowDialog(this) == DialogResult.OK)
            LoadData();
    }

    private void DeleteButton_Click(object sender, EventArgs e)
    {
        Room room = SelectedRoom();
        if (room == null)
            return;
        if (!Ui.Confirm("Delete room " + room.RoomCode + "?"))
            return;

        try
        {
            app.Rooms.Delete(room);
            LoadData();
        }
        catch (Exception ex)
        {
            Ui.ShowError(ex); // for example "This room is used by bookings and cannot be deleted."
        }
    }
}
