using LectroLend.Core.Models;
using LectroLend.Desktop.Controllers;
using LectroLend.Desktop.Views.UI;

namespace LectroLend.Desktop.Views;

// The "Profile" page: change your details and your password.
// The look of the page is in ProfilePage.Designer.cs (open it in Design view).
// MVC: this is a VIEW. It shows things and asks the controllers (app.Account, app.Equipment,
// app.Rooms, app.Reservations) to do the work.
public partial class ProfilePage : UserControl
{
    private AppControllers app;
    private MainForm mainForm;

    public ProfilePage(AppControllers app, MainForm mainForm)
    {
        InitializeComponent(); // creates the controls you see in Design view

        this.app = app;
        this.mainForm = mainForm;

        // Students have a faculty and a program. Lecturers have a department. Admins have neither.
        bool isStudent = Session.CurrentUser is Student;
        bool isLecturer = Session.CurrentUser is Lecturer;

        facultyLabel.Visible = isStudent;
        facultyBox.Visible = isStudent;
        programLabel.Visible = isStudent;
        programBox.Visible = isStudent;
        departmentLabel.Visible = isLecturer;
        departmentBox.Visible = isLecturer;

        // For a lecturer, move "Department" up to where "Faculty" is, so there is no empty gap.
        if (isLecturer)
        {
            departmentLabel.Top = facultyLabel.Top;
            departmentBox.Top = facultyBox.Top;
        }
    }

    // Fills the boxes with the saved details.
    private void ProfilePage_Load(object sender, EventArgs e)
    {
        try
        {
            User user = app.Account.GetMyProfile();
            nameBox.Text = user.FullName;
            emailBox.Text = user.Email;
            phoneBox.Text = user.Phone;

            if (user is Student)
            {
                Student student = (Student)user;
                facultyBox.Text = student.Faculty;
                programBox.Text = student.Program;
            }
            if (user is Lecturer)
            {
                Lecturer lecturer = (Lecturer)user;
                departmentBox.Text = lecturer.Department;
            }
        }
        catch (Exception ex)
        {
            Ui.ShowError(ex);
        }
    }

    private void SaveButton_Click(object sender, EventArgs e)
    {
        try
        {
            app.Account.UpdateMyProfile(nameBox.Text, phoneBox.Text, facultyBox.Text, programBox.Text, departmentBox.Text);
            mainForm.ShowUserName(); // the new name appears in the top bar
            Ui.ShowInfo("Your details were saved.");
        }
        catch (Exception ex)
        {
            Ui.ShowError(ex);
        }
    }

    private void ChangeButton_Click(object sender, EventArgs e)
    {
        try
        {
            app.Account.ChangeMyPassword(currentPasswordBox.Text, newPasswordBox.Text, repeatPasswordBox.Text);

            // Empty the boxes.
            currentPasswordBox.Text = "";
            newPasswordBox.Text = "";
            repeatPasswordBox.Text = "";
            Ui.ShowInfo("Your password was changed.");
        }
        catch (Exception ex)
        {
            Ui.ShowError(ex); // for example "Your current password is incorrect."
        }
    }
}
