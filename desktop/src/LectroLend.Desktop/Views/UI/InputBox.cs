using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace LectroLend.Desktop.Views.UI;

// A tall text box with round corners. The border turns blue while you type in it.
// For passwords it also has a "Show" link to see what you typed.
//
// You can drag it from the Toolbox onto a form (build the project first).
// In the Properties window, set "Hint" (the grey text) and "IsPassword".
// In code, read or change "Text", like a normal TextBox.
public class InputBox : UserControl
{
    private TextBox textBox = new TextBox();      // the real text box inside
    private LinkLabel showLink = new LinkLabel(); // "Show" / "Hide", only for passwords
    private bool hasFocus = false;

    public InputBox()
    {
        BackColor = Color.White;
        Size = new Size(300, 44);
        Padding = new Padding(14, 11, 12, 8); // space between the border and the text
        SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint
                 | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);

        // The inner text box has no border; we draw a nicer one around it.
        textBox.BorderStyle = BorderStyle.None;
        textBox.Dock = DockStyle.Fill;
        textBox.Font = new Font("Segoe UI", 11f);
        textBox.Enter += TextBox_Enter;
        textBox.Leave += TextBox_Leave;
        textBox.TextChanged += TextBox_TextChanged;

        // The "Show" link is hidden unless IsPassword is true.
        showLink.Text = "Show";
        showLink.Dock = DockStyle.Right;
        showLink.AutoSize = true;
        showLink.LinkColor = Ui.Blue;
        showLink.LinkBehavior = LinkBehavior.NeverUnderline;
        showLink.Visible = false;
        showLink.LinkClicked += ShowLink_LinkClicked;

        Controls.Add(textBox);
        Controls.Add(showLink);
    }

    // The grey hint shown while the box is empty, for example "you@auca.ac.rw".
    [Category("LectroLend")]
    [DefaultValue("")]
    public string Hint
    {
        get { return textBox.PlaceholderText; }
        set { textBox.PlaceholderText = value; }
    }

    // True for a password box: show dots instead of letters, and a "Show" link.
    [Category("LectroLend")]
    [DefaultValue(false)]
    public bool IsPassword
    {
        get { return showLink.Visible; }
        set
        {
            showLink.Visible = value;
            textBox.UseSystemPasswordChar = value;
        }
    }

    // The text typed in the box.
    [Browsable(true)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public override string Text
    {
        get { return textBox.Text; }
        set { textBox.Text = value; }
    }

    // Makes the box read-only (the user cannot type). It turns light grey.
    public void SetReadOnly()
    {
        textBox.ReadOnly = true;
        BackColor = Color.FromArgb(241, 245, 249);
        textBox.BackColor = BackColor;
    }

    // "Show" / "Hide" was clicked: switch between dots and letters.
    private void ShowLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        textBox.UseSystemPasswordChar = !textBox.UseSystemPasswordChar;
        if (textBox.UseSystemPasswordChar)
            showLink.Text = "Show";
        else
            showLink.Text = "Hide";
    }

    // When the text changes, tell whoever is listening to this box's TextChanged event.
    private void TextBox_TextChanged(object sender, EventArgs e)
    {
        OnTextChanged(e);
    }

    // The user clicked into the box: draw the blue border.
    private void TextBox_Enter(object sender, EventArgs e)
    {
        hasFocus = true;
        Invalidate();
    }

    // The user left the box: draw the grey border again.
    private void TextBox_Leave(object sender, EventArgs e)
    {
        hasFocus = false;
        Invalidate();
    }

    // Clicking anywhere on the box puts the cursor in the text.
    protected override void OnClick(EventArgs e)
    {
        textBox.Focus();
        base.OnClick(e);
    }

    protected override void OnPaintBackground(PaintEventArgs e)
    {
        Graphics g = e.Graphics;
        g.SmoothingMode = SmoothingMode.AntiAlias;
        if (Parent != null)
            g.Clear(Parent.BackColor);

        // Blue and thicker while typing, light grey otherwise.
        Color borderColor = Color.FromArgb(203, 213, 225);
        float borderWidth = 1;
        if (hasFocus)
        {
            borderColor = Ui.Blue;
            borderWidth = 2;
        }

        Rectangle area = new Rectangle(1, 1, Width - 3, Height - 3);
        GraphicsPath shape = Shapes.RoundedRectangle(area, 10);

        SolidBrush fill = new SolidBrush(BackColor);
        g.FillPath(fill, shape);
        fill.Dispose();

        Pen border = new Pen(borderColor, borderWidth);
        g.DrawPath(border, shape);
        border.Dispose();

        shape.Dispose();
    }
}
