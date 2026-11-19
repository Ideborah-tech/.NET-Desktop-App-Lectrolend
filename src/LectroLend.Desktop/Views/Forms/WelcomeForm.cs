using LectroLend.Desktop.Views.UI;

namespace LectroLend.Desktop.Views;

// VIEW: the welcome page, the first window you see.
// It shows the AUCA logo, a short text, the welcome picture and a "Get started" button
// that opens the sign-in page.
// The look of the window is in WelcomeForm.Designer.cs (open it in Design view).
public partial class WelcomeForm : Form
{
    public WelcomeForm()
    {
        InitializeComponent(); // creates the controls you see in Design view

        Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath); // the app icon

        // Load the pictures from the Assets folder (replace a file to change its picture).
        logoPicture.Image = Ui.LoadPicture("auca-logo.png");
        picture.Image = Ui.LoadPicture("welcome.jpg");
    }

    // "Get started" closes this window and says "OK", then Program.cs opens the sign-in page.
    private void GetStartedButton_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.OK;
    }
}
