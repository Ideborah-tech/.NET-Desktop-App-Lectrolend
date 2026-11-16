using LectroLend.Core.Models;
using LectroLend.Desktop.Controllers;
using LectroLend.Desktop.Views.UI;

namespace LectroLend.Desktop.Views;

// The "Reserve" window: choose when you need the item and (if you want) a room.
// The look of the window is in ReserveForm.Designer.cs (open it in Design view).
// MVC: this is a VIEW. It shows things and asks the controllers (app.Account, app.Equipment,
// app.Rooms, app.Reservations) to do the work.
public partial class ReserveForm : Form
{
    private AppControllers app;
    private Equipment item;

    public ReserveForm(AppControllers app, Equipment item)
    {
        InitializeComponent(); // creates the controls you see in Design view

        this.app = app;
        this.item = item;

        // About the item.
        Text = "Reserve " + item.Name;
        titleLabel.Text = item.Name;
        itemLabel.Text = item.AssetTag + "  ·  " + Labels.Of(item.Type) + "\n" + item.Description;

        // Start: the next quarter hour (for example 10:45). End: two hours later.
        DateTime soon = DateTime.Now.AddMinutes(15);
        int minutes = (soon.Minute / 15) * 15; // round down to 0, 15, 30 or 45
        DateTime start = new DateTime(soon.Year, soon.Month, soon.Day, soon.Hour, minutes, 0);
        startPicker.Value = start;
        endPicker.Value = start.AddHours(2);

        // The rooms. The first line, "No room", means the booking has no room.
        roomBox.Items.Add("No room");
        foreach (Room room in app.Rooms.GetRooms())
            roomBox.Items.Add(room); // the list shows room.ToString(), for example "A101 (Main Building)"
        roomBox.SelectedIndex = 0;
    }

    // The chosen times, without seconds.
    private DateTime StartTime()
    {
        DateTime t = startPicker.Value;
        return new DateTime(t.Year, t.Month, t.Day, t.Hour, t.Minute, 0);
    }

    private DateTime EndTime()
    {
        DateTime t = endPicker.Value;
        return new DateTime(t.Year, t.Month, t.Day, t.Hour, t.Minute, 0);
    }

    // Runs when the window opens and every time a time changes:
    // asks the service if the item is free, and shows the answer.
    private void Time_Changed(object sender, EventArgs e)
    {
        if (item == null)
            return; // the window is still being built

        try
        {
            AvailabilityResult result = app.Equipment.CheckAvailability(item, StartTime(), EndTime());

            // Show the times that are already booked.
            bookedList.Items.Clear();
            foreach (TimeSlot slot in result.BookedTimes)
                bookedList.Items.Add(Ui.FormatTime(slot.Start) + "  to  " + Ui.FormatTime(slot.End));
            if (result.BookedTimes.Count == 0)
                bookedList.Items.Add("Nothing booked yet");

            // Green "Free" or a red reason.
            if (result.Available)
            {
                freeLabel.Text = "Free for that time";
                freeLabel.ForeColor = Ui.Green;
            }
            else
            {
                freeLabel.Text = result.Reason;
                freeLabel.ForeColor = Ui.Red;
            }
        }
        catch (Exception ex)
        {
            freeLabel.Text = ex.Message;
            freeLabel.ForeColor = Ui.Red;
        }
    }

    private void ReserveButton_Click(object sender, EventArgs e)
    {
        // The first line of the room list is "No room". Any other line is a Room.
        Room room = null;
        if (roomBox.SelectedIndex > 0)
            room = (Room)roomBox.SelectedItem;

        try
        {
            app.Reservations.Reserve(item, StartTime(), EndTime(), room);
            Ui.ShowInfo("Booked! Collect " + item.Name + " from the IT office on " + Ui.FormatTime(StartTime()) + ".");
            DialogResult = DialogResult.OK; // close the window
        }
        catch (Exception ex)
        {
            Ui.ShowError(ex); // for example "already booked for part of that time"
        }
    }
}
