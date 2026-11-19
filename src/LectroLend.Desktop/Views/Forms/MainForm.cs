using LectroLend.Core.Models;
using LectroLend.Desktop.Controllers;
using LectroLend.Desktop.Views.UI;

namespace LectroLend.Desktop.Views;

// The main window: a dark bar at the top, a menu on the left and the chosen page on the right.
// Students and lecturers see the borrowing pages. The IT administrator sees the office pages.
// The look of the window is in MainForm.Designer.cs (open it in Design view).
// MVC: this is a VIEW. It shows things and asks the controllers (app.Account, app.Equipment,
// app.Rooms, app.Reservations) to do the work.
public partial class MainForm : Form
{
    private AppControllers app;

    // True when the user clicked "Sign out" (then Program.cs shows the sign-in window again).
    public bool SignedOut = false;

    public MainForm(AppControllers app)
    {
        InitializeComponent(); // creates the controls you see in Design view

        this.app = app;
        Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath); // the app icon
        logoPicture.Image = Ui.LoadPicture("auca-logo.png");            // the university logo in the top bar

        // The menu depends on who signed in, so we add its buttons here in code.
        if (Session.IsAdmin())
        {
            AddMenuButton("Dashboard");
            AddMenuButton("Reservations");
            AddMenuButton("Equipment");
            AddMenuButton("Rooms");
            AddMenuButton("People");
        }
        else
        {
            AddMenuButton("Borrow");
            AddMenuButton("My reservations");
        }
        AddMenuButton("Profile");

        ShowUserName();
    }

    // Runs once, when the window opens.
    private void MainForm_Load(object sender, EventArgs e)
    {
        RunHousekeeping();
        timer.Start(); // then run it again every minute

        // Show the first page.
        if (Session.IsAdmin())
            OpenPage("Dashboard");
        else
            OpenPage("Borrow");
    }

    // Adds one button to the menu. Its text is also the name of the page it opens.
    private void AddMenuButton(string text)
    {
        Button button = Ui.MakeButton(text, Ui.White);
        button.ForeColor = Ui.Navy;
        button.TextAlign = ContentAlignment.MiddleLeft;
        button.Dock = DockStyle.Top;
        button.Click += MenuButton_Click;

        // The window was already sized for the screen, so we make the new button bigger ourselves
        // on larger screen settings (42 pixels high on a normal 100% screen).
        float scale = DeviceDpi / 96f;
        button.Height = (int)(42 * scale);

        menuPanel.Controls.Add(button);
        // Controls docked to the Top are placed in reverse order.
        // Moving the new button to position 0 puts it under the others.
        menuPanel.Controls.SetChildIndex(button, 0);
    }

    private void MenuButton_Click(object sender, EventArgs e)
    {
        Button button = (Button)sender; // the button that was clicked
        OpenPage(button.Text);
    }

    // Shows a page and highlights its menu button.
    public void OpenPage(string pageName)
    {
        // Highlight the chosen menu button (navy), the others are white.
        foreach (Control menuButton in menuPanel.Controls)
        {
            if (menuButton.Text == pageName)
            {
                menuButton.BackColor = Ui.Navy;
                menuButton.ForeColor = Color.White;
            }
            else
            {
                menuButton.BackColor = Ui.White;
                menuButton.ForeColor = Ui.Navy;
            }
        }

        // Make the page.
        UserControl page;
        if (pageName == "Dashboard") page = new DashboardPage(app);
        else if (pageName == "Reservations") page = new ReservationsPage(app);
        else if (pageName == "Equipment") page = new EquipmentPage(app);
        else if (pageName == "Rooms") page = new RoomsPage(app);
        else if (pageName == "People") page = new PeoplePage(app);
        else if (pageName == "Borrow") page = new BorrowPage(app);
        else if (pageName == "My reservations") page = new MyReservationsPage(app);
        else page = new ProfilePage(app, this);

        // Remove the old page and show the new one.
        foreach (Control oldPage in pagePanel.Controls)
            oldPage.Dispose();
        pagePanel.Controls.Clear();
        page.Dock = DockStyle.Fill;
        pagePanel.Controls.Add(page);
    }

    // Writes the name and role in the top bar, for example "Ingabire Deborah (Student)".
    public void ShowUserName()
    {
        userLabel.Text = app.Account.MyNameAndRole();
    }

    private void SignOutButton_Click(object sender, EventArgs e)
    {
        app.Account.SignOut();
        SignedOut = true;
        Close();
    }

    private void Timer_Tick(object sender, EventArgs e)
    {
        RunHousekeeping();
    }

    // Expire bookings nobody picked up, and mark late items as overdue.
    private void RunHousekeeping()
    {
        try
        {
            app.Reservations.RunHousekeeping();
        }
        catch (Exception)
        {
            // If it fails once, it will simply run again in a minute.
        }
    }
}
