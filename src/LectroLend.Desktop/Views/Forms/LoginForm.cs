using LectroLend.Desktop.Controllers;
using LectroLend.Desktop.Views.UI;

namespace LectroLend.Desktop.Views;

// VIEW: the sign-in window. A picture on the left and the sign-in form on the right.
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

        // Load the pictures from the Assets folder (replace a file to change its picture).
        picture.Image = Ui.LoadPicture("welcome.jpg");       // the welcome picture on the left
        logoPicture.Image = Ui.LoadPicture("auca-logo.png"); // the university logo
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
