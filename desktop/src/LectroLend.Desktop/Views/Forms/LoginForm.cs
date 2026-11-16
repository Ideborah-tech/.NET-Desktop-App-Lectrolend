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
