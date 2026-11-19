using LectroLend.Core.Models;

namespace LectroLend.Desktop.Views.UI;

// Colours, fonts and small helper methods that every window uses.
// Change a colour here and the whole app changes.
public static class Ui
{
    // ---- Colours ----
    public static readonly Color Navy = Color.FromArgb(3, 52, 110);       // main colour
    public static readonly Color Blue = Color.FromArgb(44, 116, 179);     // buttons and links
    public static readonly Color Yellow = Color.FromArgb(255, 201, 60);   // highlights
    public static readonly Color LightBlue = Color.FromArgb(232, 240, 250);
    public static readonly Color Background = Color.FromArgb(240, 244, 249);
    public static readonly Color White = Color.White;
    public static readonly Color Gray = Color.FromArgb(100, 116, 139);
    public static readonly Color Green = Color.FromArgb(22, 128, 61);
    public static readonly Color Red = Color.FromArgb(200, 40, 40);

    // ---- Fonts ----
    public static readonly Font NormalFont = new Font("Segoe UI", 10f);
    public static readonly Font BoldFont = new Font("Segoe UI", 10f, FontStyle.Bold);

    // A coloured button with round corners, made in code.
    // (Used for buttons that are created while the app runs, like the menu and the card buttons.)
    public static Button MakeButton(string text, Color color)
    {
        RoundButton button = new RoundButton();
        button.Text = text;
        button.BackColor = color;
        button.Font = BoldFont;
        button.Size = new Size(140, 38);
        button.Margin = new Padding(0, 0, 10, 0);

        // Dark text on light buttons, white text on dark buttons.
        if (color == Yellow || color == LightBlue)
            button.ForeColor = Navy;
        else
            button.ForeColor = Color.White;

        return button;
    }

    // Gives a table (grid) the app's look: light header, striped rows, coloured status words.
    // Call it once in the window's constructor, after InitializeComponent().
    public static void StyleGrid(DataGridView grid)
    {
        grid.Font = NormalFont;
        grid.BackgroundColor = White;
        grid.BorderStyle = BorderStyle.None;

        // The user can only look and select a row, not type in the table.
        grid.ReadOnly = true;
        grid.AllowUserToAddRows = false;
        grid.AllowUserToDeleteRows = false;
        grid.AllowUserToResizeRows = false;
        grid.MultiSelect = false;
        grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        grid.RowHeadersVisible = false;

        // The columns share the full width.
        grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        // Thin lines between rows only.
        grid.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
        grid.GridColor = Color.FromArgb(229, 234, 240);

        // The header row: light blue-grey with navy bold text.
        grid.EnableHeadersVisualStyles = false;
        grid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
        grid.ColumnHeadersHeight = 42;
        grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        grid.ColumnHeadersDefaultCellStyle.BackColor = LightBlue;
        grid.ColumnHeadersDefaultCellStyle.SelectionBackColor = LightBlue;
        grid.ColumnHeadersDefaultCellStyle.ForeColor = Navy;
        grid.ColumnHeadersDefaultCellStyle.Font = BoldFont;
        grid.ColumnHeadersDefaultCellStyle.Padding = new Padding(8, 0, 0, 0);

        // The rows: taller, with space on the left, and every second row a little grey.
        grid.RowTemplate.Height = 38;
        grid.DefaultCellStyle.Padding = new Padding(8, 0, 0, 0);
        grid.DefaultCellStyle.ForeColor = Color.FromArgb(30, 41, 59);
        grid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(214, 230, 247);
        grid.DefaultCellStyle.SelectionForeColor = Color.Black;
        grid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 253);

        // Colour the words in the "Status" and "Account" columns.
        grid.CellFormatting += Grid_CellFormatting;
    }

    // Runs for every cell before it is drawn. We colour the status words.
    private static void Grid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
    {
        DataGridView grid = (DataGridView)sender;
        if (e.RowIndex < 0 || e.Value == null)
            return;

        string columnName = grid.Columns[e.ColumnIndex].Name;
        if (columnName == "Status" || columnName == "Account")
        {
            Color color = StatusColor(e.Value.ToString());
            e.CellStyle.ForeColor = color;
            e.CellStyle.SelectionForeColor = color;
            e.CellStyle.Font = BoldFont;
        }
    }

    // Green = good, blue = in progress, red = problem, grey = finished.
    public static Color StatusColor(string status)
    {
        if (status == "Available" || status == "Returned" || status == "Active")
            return Green;
        if (status == "Booked" || status == "In use" || status == "Issued")
            return Blue;
        if (status == "Overdue" || status == "Maintenance" || status == "Deactivated")
            return Red;
        return Gray; // Cancelled, Expired, Retired
    }

    // ---- Message boxes ----

    // Shows an error. A broken rule (like "already booked") is shown as a simple warning.
    public static void ShowError(Exception ex)
    {
        if (ex is AppException || ex is InvalidOperationException)
        {
            MessageBox.Show(ex.Message, "LectroLend", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        else
        {
            // An unexpected problem (for example the database stopped). Show the details.
            string details = ex.Message;
            if (ex.InnerException != null)
                details = ex.InnerException.Message;
            MessageBox.Show("Something went wrong:\n\n" + details, "LectroLend", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    public static void ShowInfo(string message)
    {
        MessageBox.Show(message, "LectroLend", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    // Asks a yes/no question. Returns true if the user clicks "Yes".
    public static bool Confirm(string question)
    {
        DialogResult answer = MessageBox.Show(question, "Please confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        return answer == DialogResult.Yes;
    }

    // Loads a picture from the Assets folder, for example Ui.LoadPicture("auca-logo.png").
    // Returns null (no picture) if the file is missing, so the app still opens.
    public static Image LoadPicture(string fileName)
    {
        string path = Path.Combine(AppContext.BaseDirectory, "Assets", fileName);
        if (!File.Exists(path))
            return null;

        // Read the file into memory first, so the file is not locked while the app is open.
        byte[] bytes = File.ReadAllBytes(path);
        return Image.FromStream(new MemoryStream(bytes));
    }

    // Writes a date and time like "Mon 16 Nov, 14:30".
    public static string FormatTime(DateTime time)
    {
        return time.ToString("ddd d MMM, HH:mm");
    }
}
