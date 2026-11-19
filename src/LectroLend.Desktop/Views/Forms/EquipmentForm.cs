using LectroLend.Core.Models;
using LectroLend.Desktop.Controllers;
using LectroLend.Desktop.Views.UI;

namespace LectroLend.Desktop.Views;

// Add a new item, or change an existing one.
// Pass null as "existing" to add a new item.
// The look of the window is in EquipmentForm.Designer.cs (open it in Design view).
// MVC: this is a VIEW. It shows things and asks the controllers (app.Account, app.Equipment,
// app.Rooms, app.Reservations) to do the work.
public partial class EquipmentForm : Form
{
    private AppControllers app;
    private Equipment existing;

    public EquipmentForm(AppControllers app, Equipment existing)
    {
        InitializeComponent(); // creates the controls you see in Design view

        this.app = app;
        this.existing = existing;

        // The lists of types and statuses.
        typeBox.Items.Add(EquipmentType.PROJECTOR);
        typeBox.Items.Add(EquipmentType.HDMI_CABLE);
        typeBox.Items.Add(EquipmentType.REMOTE_CONTROL);
        typeBox.Items.Add(EquipmentType.OTHER);
        typeBox.SelectedIndex = 0;

        statusBox.Items.Add(EquipmentStatus.AVAILABLE);
        statusBox.Items.Add(EquipmentStatus.IN_USE);
        statusBox.Items.Add(EquipmentStatus.UNDER_MAINTENANCE);
        statusBox.Items.Add(EquipmentStatus.RETIRED);
        statusBox.SelectedIndex = 0;

        // When editing, fill the boxes with the current values.
        if (existing != null)
        {
            Text = "Edit item";
            titleLabel.Text = "Edit item";
            nameBox.Text = existing.Name;
            tagBox.Text = existing.AssetTag;
            typeBox.SelectedItem = existing.Type;
            statusBox.SelectedItem = existing.Status;
            descriptionBox.Text = existing.Description;
        }
    }

    private void SaveButton_Click(object sender, EventArgs e)
    {
        EquipmentType type = (EquipmentType)typeBox.SelectedItem;
        EquipmentStatus status = (EquipmentStatus)statusBox.SelectedItem;

        try
        {
            // The controller adds a new item when "existing" is null, or changes the existing one.
            app.Equipment.Save(existing, nameBox.Text, type, tagBox.Text, status, descriptionBox.Text);

            DialogResult = DialogResult.OK; // close the window
        }
        catch (Exception ex)
        {
            Ui.ShowError(ex);
        }
    }
}
