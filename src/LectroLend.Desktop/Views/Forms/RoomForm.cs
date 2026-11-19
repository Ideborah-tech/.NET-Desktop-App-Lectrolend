using LectroLend.Core.Models;
using LectroLend.Desktop.Controllers;
using LectroLend.Desktop.Views.UI;

namespace LectroLend.Desktop.Views;

// Add a new room, or change an existing one.
// Pass null as "existing" to add a new room.
// The look of the window is in RoomForm.Designer.cs (open it in Design view).
// MVC: this is a VIEW. It shows things and asks the controllers (app.Account, app.Equipment,
// app.Rooms, app.Reservations) to do the work.
public partial class RoomForm : Form
{
    private AppControllers app;
    private Room existing;

    public RoomForm(AppControllers app, Room existing)
    {
        InitializeComponent(); // creates the controls you see in Design view

        this.app = app;
        this.existing = existing;

        // When editing, fill the boxes with the current values.
        if (existing != null)
        {
            Text = "Edit room";
            titleLabel.Text = "Edit room";
            codeBox.Text = existing.RoomCode;
            buildingBox.Text = existing.Building;
            capacityBox.Value = existing.Capacity;
        }
    }

    private void SaveButton_Click(object sender, EventArgs e)
    {
        int capacity = (int)capacityBox.Value;

        try
        {
            // The controller adds a new room when "existing" is null, or changes the existing one.
            app.Rooms.Save(existing, codeBox.Text, buildingBox.Text, capacity);

            DialogResult = DialogResult.OK; // close the window
        }
        catch (Exception ex)
        {
            Ui.ShowError(ex);
        }
    }
}
