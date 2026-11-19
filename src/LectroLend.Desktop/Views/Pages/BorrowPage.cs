using LectroLend.Core.Models;
using LectroLend.Desktop.Controllers;
using LectroLend.Desktop.Views.UI;

namespace LectroLend.Desktop.Views;

// The "Borrow" page: a search box, a type filter, and one card for each item.
// The look of the page is in BorrowPage.Designer.cs (open it in Design view).
// The cards themselves are made in code (MakeCard below), because they depend on the equipment in the database.
// MVC: this is a VIEW. It shows things and asks the controllers (app.Account, app.Equipment,
// app.Rooms, app.Reservations) to do the work.
public partial class BorrowPage : UserControl
{
    private AppControllers app;

    public BorrowPage(AppControllers app)
    {
        InitializeComponent(); // creates the controls you see in Design view

        typeBox.SelectedIndex = 0; // "All types"
        this.app = app;            // set after, so the cards are loaded only once (in BorrowPage_Load)
    }

    private void BorrowPage_Load(object sender, EventArgs e)
    {
        LoadCards();
    }

    // Runs when the search text or the type changes.
    private void Filter_Changed(object sender, EventArgs e)
    {
        LoadCards();
    }

    // Reads the equipment from the database and makes one card per item.
    private void LoadCards()
    {
        if (app == null)
            return; // the page is still being built

        try
        {
            // Which type was chosen? (The first line, "All types", means no filter.)
            EquipmentType? type = null;
            if (typeBox.SelectedIndex == 1) type = EquipmentType.PROJECTOR;
            if (typeBox.SelectedIndex == 2) type = EquipmentType.HDMI_CABLE;
            if (typeBox.SelectedIndex == 3) type = EquipmentType.REMOTE_CONTROL;
            if (typeBox.SelectedIndex == 4) type = EquipmentType.OTHER;

            List<Equipment> items = app.Equipment.GetForBorrowing(type, searchBox.Text);

            // Remove the old cards.
            cardsPanel.SuspendLayout();
            foreach (Control oldCard in cardsPanel.Controls)
                oldCard.Dispose();
            cardsPanel.Controls.Clear();

            // Add a new card for every item.
            foreach (Equipment item in items)
                cardsPanel.Controls.Add(MakeCard(item));
            cardsPanel.ResumeLayout();

            // The information line.
            int active = app.Reservations.MyActiveCount();
            int limit = app.Account.MyBookingLimit();
            infoLabel.Text = items.Count + " items  ·  You have " + active + " of " + limit + " active bookings";
        }
        catch (Exception ex)
        {
            Ui.ShowError(ex);
        }
    }

    // Makes one card: the type, the name, the asset tag, the status and a "Reserve" button.
    private CardPanel MakeCard(Equipment item)
    {
        CardPanel card = new CardPanel();
        card.Size = new Size(250, 182);
        card.Margin = new Padding(0, 0, 16, 16); // space between the cards

        Label typeLabel = new Label();
        typeLabel.Text = Labels.Of(item.Type).ToUpper();
        typeLabel.Font = new Font("Segoe UI", 8.5f, FontStyle.Bold);
        typeLabel.ForeColor = Ui.Blue;
        typeLabel.SetBounds(18, 16, 214, 20); // x, y, width, height inside the card

        Label nameLabel = new Label();
        nameLabel.Text = item.Name;
        nameLabel.Font = new Font("Segoe UI", 11.5f, FontStyle.Bold);
        nameLabel.ForeColor = Ui.Navy;
        nameLabel.SetBounds(18, 38, 214, 46);

        Label tagLabel = new Label();
        tagLabel.Text = item.AssetTag;
        tagLabel.ForeColor = Ui.Gray;
        tagLabel.SetBounds(18, 86, 110, 22);

        Label statusLabel = new Label();
        statusLabel.Text = "● " + Labels.Of(item.Status);
        statusLabel.Font = Ui.BoldFont;
        statusLabel.ForeColor = Ui.StatusColor(Labels.Of(item.Status));
        statusLabel.TextAlign = ContentAlignment.MiddleRight;
        statusLabel.SetBounds(122, 86, 110, 22);

        Button reserveButton = Ui.MakeButton("Reserve", Ui.Navy);
        reserveButton.SetBounds(18, 124, 214, 40);
        reserveButton.Tag = item; // remember which item this button is for
        reserveButton.Click += ReserveButton_Click;

        // Broken or retired items cannot be booked: grey button that cannot be clicked.
        if (!item.IsReservable())
        {
            reserveButton.Text = "Not available";
            reserveButton.BackColor = Color.FromArgb(241, 245, 249);
            reserveButton.ForeColor = Ui.Gray;
            reserveButton.Enabled = false;
        }

        card.Controls.Add(typeLabel);
        card.Controls.Add(nameLabel);
        card.Controls.Add(tagLabel);
        card.Controls.Add(statusLabel);
        card.Controls.Add(reserveButton);

        // The sizes above are for a normal screen (100%).
        // The card is made after the page opened, so we make it bigger ourselves on larger screen settings.
        float scale = DeviceDpi / 96f;
        card.Scale(new SizeF(scale, scale));
        return card;
    }

    private void ReserveButton_Click(object sender, EventArgs e)
    {
        Button button = (Button)sender;        // the button that was clicked
        Equipment item = (Equipment)button.Tag; // the item we stored in it

        ReserveForm reserveForm = new ReserveForm(app, item);
        if (reserveForm.ShowDialog(this) == DialogResult.OK)
            LoadCards(); // refresh, the item may now show "In use" or the count of bookings changed
    }
}
