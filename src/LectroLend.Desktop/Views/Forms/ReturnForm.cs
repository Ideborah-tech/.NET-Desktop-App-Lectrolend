using LectroLend.Core.Models;
using LectroLend.Desktop.Controllers;
using LectroLend.Desktop.Views.UI;

namespace LectroLend.Desktop.Views;

// The "Check in" window: the IT office records that an item came back.
// If the item is damaged, it is sent to maintenance.
// The look of the window is in ReturnForm.Designer.cs (open it in Design view).
// MVC: this is a VIEW. It shows things and asks the controllers (app.Account, app.Equipment,
// app.Rooms, app.Reservations) to do the work.
public partial class ReturnForm : Form
{
    private AppControllers app;
    private Reservation reservation;

    public ReturnForm(AppControllers app, Reservation reservation)
    {
        InitializeComponent(); // creates the controls you see in Design view

        this.app = app;
        this.reservation = reservation;

        // About the booking. A late return is shown in red.
        infoLabel.Text = reservation.Equipment.Name + " borrowed by " + reservation.User.FullName
                         + "\nDue back " + Ui.FormatTime(reservation.EndTime);
        if (DateTime.Now > reservation.EndTime)
        {
            infoLabel.Text = infoLabel.Text + "  (LATE - this will be recorded)";
            infoLabel.ForeColor = Ui.Red;
        }
    }

    private void ConfirmButton_Click(object sender, EventArgs e)
    {
        try
        {
            app.Reservations.CheckIn(reservation, damagedBox.Checked, conditionBox.Text, remarksBox.Text);
            DialogResult = DialogResult.OK; // close the window
        }
        catch (Exception ex)
        {
            Ui.ShowError(ex);
        }
    }
}
