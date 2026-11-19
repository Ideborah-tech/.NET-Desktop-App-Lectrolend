using LectroLend.Desktop.Controllers;
using LectroLend.Desktop.Views.UI;

namespace LectroLend.Desktop.Views;

// VIEW: the sign-in window: a white card in the middle with the logo, the sign-in form and the demo accounts.
// The look of the window is in LoginForm.Designer.cs (open it in Design view).
// When a button is clicked, this window asks the AccountController to do the work.
public partial class LoginForm : Form
{
    private AppControllers app;

    public LoginForm(AppControllers app)
    {
        InitializeComponent(); // creates the controls you see in Design view

        this.app = app;
        Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath); // the app icon

        // Load the university logo from the Assets folder (replace the file to change it).
        logoPicture.Image = Ui.LoadPicture("auca-logo.png");
    }

    private void SignInButton_Click(object sender, EventArgs e)
    {
        try
        {
            app.Account.SignIn(emailBox.Text, passwordBox.Text);
            DialogResult = DialogResult.OK; // close the window and say "OK"
        }
        catch (Exception ex)
        {
            Ui.ShowError(ex); // for example "Incorrect email or password."
        }
    }

    // ---- Demo accounts: one click fills in the email and password and signs in ----
    // (Remove these three buttons before the app is used for real.)

    private void StudentDemoButton_Click(object sender, EventArgs e)
    {
        UseDemoAccount("student@auca.ac.rw", "Student@123");
    }

    private void LecturerDemoButton_Click(object sender, EventArgs e)
    {
        UseDemoAccount("lecturer@auca.ac.rw", "Lecturer@123");
    }

    private void AdminDemoButton_Click(object sender, EventArgs e)
    {
        UseDemoAccount("admin@auca.ac.rw", "Admin@123");
    }

    private void UseDemoAccount(string email, string password)
    {
        emailBox.Text = email;
        passwordBox.Text = password;
        SignInButton_Click(null, EventArgs.Empty); // the same as clicking "Sign in"
    }

    // "Back" closes this window and says "Retry", then Program.cs shows the welcome page again.
    private void BackLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        DialogResult = DialogResult.Retry;
    }

    private void RegisterButton_Click(object sender, EventArgs e)
    {
        RegisterForm registerForm = new RegisterForm(app, false);
        if (registerForm.ShowDialog(this) == DialogResult.OK)
        {
            // The new account is already signed in by the controller.
            DialogResult = DialogResult.OK;
        }
    }
}
