using LectroLend.Core.Models;
using LectroLend.Desktop.Controllers;
using LectroLend.Desktop.Views.UI;

namespace LectroLend.Desktop.Views;

// The IT office "Equipment" page: add, edit and remove items.
// The look of the page is in EquipmentPage.Designer.cs (open it in Design view).
// MVC: this is a VIEW. It shows things and asks the controllers (app.Account, app.Equipment,
// app.Rooms, app.Reservations) to do the work.
public partial class EquipmentPage : UserControl
{
    private AppControllers app;

    public EquipmentPage(AppControllers app)
    {
        InitializeComponent(); // creates the controls you see in Design view

        this.app = app;
        Ui.StyleGrid(grid); // give the table the app's colours
    }

    private void EquipmentPage_Load(object sender, EventArgs e)
    {
        LoadData();
    }

    // Reads all items and fills the table.
    private void LoadData()
    {
        try
        {
            grid.Rows.Clear();
            foreach (Equipment item in app.Equipment.GetAll())
            {
                int row = grid.Rows.Add(item.Name, item.AssetTag, Labels.Of(item.Type), Labels.Of(item.Status), item.Description);
                grid.Rows[row].Tag = item; // keep the item inside the row
            }
        }
        catch (Exception ex)
        {
            Ui.ShowError(ex);
        }
    }

    // Returns the selected item, or null (with a message) if nothing is selected.
    private Equipment SelectedItem()
    {
        if (grid.SelectedRows.Count == 0)
        {
            Ui.ShowInfo("Please select an item first.");
            return null;
        }
        return (Equipment)grid.SelectedRows[0].Tag;
    }

    private void AddButton_Click(object sender, EventArgs e)
    {
        EquipmentForm form = new EquipmentForm(app, null); // null = a new item
        if (form.ShowDialog(this) == DialogResult.OK)
            LoadData();
    }

    private void EditButton_Click(object sender, EventArgs e)
    {
        Equipment item = SelectedItem();
        if (item == null)
            return;

        EquipmentForm form = new EquipmentForm(app, item);
        if (form.ShowDialog(this) == DialogResult.OK)
            LoadData();
    }

    private void RemoveButton_Click(object sender, EventArgs e)
    {
        Equipment item = SelectedItem();
        if (item == null)
            return;
        if (!Ui.Confirm("Remove " + item.Name + "?"))
            return;

        try
        {
            bool retired = app.Equipment.Remove(item);
            if (retired)
                Ui.ShowInfo("This item was booked before, so it was marked Retired instead of deleted.");
            LoadData();
        }
        catch (Exception ex)
        {
            Ui.ShowError(ex);
        }
    }
}
