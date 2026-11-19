using LectroLend.Core.Models;
using LectroLend.Desktop.Controllers;
using LectroLend.Desktop.Views.UI;

namespace LectroLend.Desktop.Views;

// The IT office "People" page: all accounts. The IT office can add accounts and turn them on or off.
// The look of the page is in PeoplePage.Designer.cs (open it in Design view).
// MVC: this is a VIEW. It shows things and asks the controllers (app.Account, app.Equipment,
// app.Rooms, app.Reservations) to do the work.
public partial class PeoplePage : UserControl
{
    private AppControllers app;

    public PeoplePage(AppControllers app)
    {
        InitializeComponent(); // creates the controls you see in Design view

        this.app = app;
        Ui.StyleGrid(grid); // give the table the app's colours
    }

    private void PeoplePage_Load(object sender, EventArgs e)
    {
        LoadData();
    }

    // Reads all accounts and fills the table.
    private void LoadData()
    {
        try
        {
            grid.Rows.Clear();
            foreach (User user in app.Account.GetAllUsers())
            {
                string account = "Active";
                if (!user.Active)
                    account = "Deactivated";

                int row = grid.Rows.Add(user.FullName, user.Email, Labels.Of(user.Role), user.Phone, account);
                grid.Rows[row].Tag = user; // keep the user inside the row
            }
        }
        catch (Exception ex)
        {
            Ui.ShowError(ex);
        }
    }

    private void AddButton_Click(object sender, EventArgs e)
    {
        RegisterForm form = new RegisterForm(app, true); // true = the admin may also create admins
        if (form.ShowDialog(this) == DialogResult.OK)
            LoadData();
    }

    private void ActivateButton_Click(object sender, EventArgs e)
    {
        SetActive(true);
    }

    private void DeactivateButton_Click(object sender, EventArgs e)
    {
        SetActive(false);
    }

    // Turns the selected account on (true) or off (false).
    private void SetActive(bool active)
    {
        if (grid.SelectedRows.Count == 0)
        {
            Ui.ShowInfo("Please select a person first.");
            return;
        }

        User user = (User)grid.SelectedRows[0].Tag;
        try
        {
            app.Account.SetActive(user, active);
            LoadData();
        }
        catch (Exception ex)
        {
            Ui.ShowError(ex); // for example "You cannot deactivate your own account."
        }
    }
}
