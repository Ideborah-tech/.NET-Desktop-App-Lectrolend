using LectroLend.Core.Data;
using LectroLend.Desktop.Views.UI;

namespace LectroLend.Desktop.Views;

// This window appears when the app cannot connect to PostgreSQL.
// The user types the connection details (usually just the password) and clicks "Connect".
// The look of the window is in ConnectionForm.Designer.cs (open it in Design view).
// MVC: this is a VIEW. It shows things and asks the controllers (app.Account, app.Equipment,
// app.Rooms, app.Reservations) to do the work.
public partial class ConnectionForm : Form
{
    // The details the user typed. Program.cs reads this after the window closes.
    public DbSettings Settings;

    public ConnectionForm(DbSettings current, string problem)
    {
        InitializeComponent(); // creates the controls you see in Design view

        Settings = current;

        // Show the error, if there was one.
        if (problem != null)
            errorLabel.Text = "Could not connect: " + problem;

        // Fill the boxes with the current values.
        hostBox.Text = current.Host;
        portBox.Text = current.Port.ToString();
        databaseBox.Text = current.Database;
        userBox.Text = current.Username;
        passwordBox.Text = current.Password;
    }

    private void ConnectButton_Click(object sender, EventArgs e)
    {
        // The port must be a number.
        int port;
        if (!int.TryParse(portBox.Text, out port))
        {
            Ui.ShowInfo("The port must be a number (usually 5432).");
            return;
        }

        Settings = new DbSettings();
        Settings.Host = hostBox.Text.Trim();
        Settings.Port = port;
        Settings.Database = databaseBox.Text.Trim();
        Settings.Username = userBox.Text.Trim();
        Settings.Password = passwordBox.Text;
        Settings.Save(); // remember these details for next time

        DialogResult = DialogResult.OK; // close the window and say "OK"
    }
}
