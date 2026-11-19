using LectroLend.Core.Models;
using LectroLend.Desktop.Controllers;
using LectroLend.Desktop.Views.UI;

namespace LectroLend.Desktop.Views;

// VIEW: the "Create account" window.
// Students and lecturers use it to register themselves.
// The IT office uses it too (with byAdmin = true) to add any account, including administrators.
// The look of the window is in RegisterForm.Designer.cs (open it in Design view).
public partial class RegisterForm : Form
{
    private AppControllers app;
    private bool byAdmin;

    public RegisterForm(AppControllers app, bool byAdmin)
    {
        InitializeComponent(); // creates the controls you see in Design view

        this.app = app;
        this.byAdmin = byAdmin;

        // The list of roles. Only the IT office may create administrators.
        roleBox.Items.Add("Student");
        roleBox.Items.Add("Lecturer");
        if (byAdmin)
            roleBox.Items.Add("IT Administrator");
        roleBox.SelectedIndex = 0;

        // When the IT office opens this window, change the texts a little.
        if (byAdmin)
        {
            Text = "Add an account";
            titleLabel.Text = "Add an account";
            saveButton.Text = "Add account";
        }
    }

    private void SaveButton_Click(object sender, EventArgs e)
    {
        // Which role was chosen in the list?
        Role role = Role.STUDENT;
        if (roleBox.SelectedIndex == 1)
            role = Role.LECTURER;
        if (roleBox.SelectedIndex == 2)
            role = Role.ADMIN;

        try
        {
            app.Account.CreateAccount(role, nameBox.Text, emailBox.Text, phoneBox.Text, passwordBox.Text,
                numberBox.Text, facultyBox.Text, programBox.Text, departmentBox.Text, byAdmin);

            DialogResult = DialogResult.OK; // close the window
        }
        catch (Exception ex)
        {
            Ui.ShowError(ex); // for example "An account with this email already exists."
        }
    }
}
