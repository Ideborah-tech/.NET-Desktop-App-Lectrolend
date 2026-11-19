using System.Drawing.Drawing2D;

namespace LectroLend.Desktop.Views.UI;

// A normal button, but with round corners.
// It turns a little lighter when the mouse is over it.
public class RoundButton : Button
{
    private bool mouseIsOver = false;

    public RoundButton()
    {
        FlatStyle = FlatStyle.Flat;
        FlatAppearance.BorderSize = 0;
        Cursor = Cursors.Hand;

        // We draw the button ourselves (in OnPaint below). This line also stops flickering.
        SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
    }

    // The mouse moved onto the button.
    protected override void OnMouseEnter(EventArgs e)
    {
        mouseIsOver = true;
        Invalidate(); // "Invalidate" means: please draw me again
        base.OnMouseEnter(e);
    }

    // The mouse left the button.
    protected override void OnMouseLeave(EventArgs e)
    {
        mouseIsOver = false;
        Invalidate();
        base.OnMouseLeave(e);
    }

    // Draws the button.
    protected override void OnPaint(PaintEventArgs e)
    {
        Graphics g = e.Graphics;
        g.SmoothingMode = SmoothingMode.AntiAlias; // smooth edges

        // 1. Fill everything with the colour behind the button (this is what shows in the corners).
        if (Parent != null)
            g.Clear(Parent.BackColor);
        else
            g.Clear(Color.White);

        // 2. Choose the colour of the button.
        Color fillColor = BackColor;
        if (mouseIsOver)
        {
            if (BackColor == Color.White)
                fillColor = Ui.LightBlue;                        // white buttons turn light blue
            else
                fillColor = ControlPaint.Light(BackColor, 0.25f); // other buttons get lighter
        }

        // 3. Draw the rounded shape.
        Rectangle area = new Rectangle(0, 0, Width - 1, Height - 1);
        GraphicsPath shape = Shapes.RoundedRectangle(area, 10);
        SolidBrush brush = new SolidBrush(fillColor);
        g.FillPath(brush, shape);
        brush.Dispose();
        shape.Dispose();

        // 4. Write the text: in the middle, or on the left for the menu buttons.
        if (TextAlign == ContentAlignment.MiddleLeft)
        {
            Rectangle textArea = new Rectangle(14, 0, Width - 14, Height);
            TextRenderer.DrawText(g, Text, Font, textArea, ForeColor, TextFormatFlags.Left | TextFormatFlags.VerticalCenter);
        }
        else
        {
            TextRenderer.DrawText(g, Text, Font, ClientRectangle, ForeColor,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }
    }
}

// Shapes used by the round button, the cards and the input boxes.
public static class Shapes
{
    // A rectangle with round corners: four quarter-circles joined by straight lines.
    public static GraphicsPath RoundedRectangle(Rectangle area, int radius)
    {
        int size = radius * 2;
        GraphicsPath path = new GraphicsPath();
        path.AddArc(area.X, area.Y, size, size, 180, 90);                       // top-left corner
        path.AddArc(area.Right - size, area.Y, size, size, 270, 90);            // top-right corner
        path.AddArc(area.Right - size, area.Bottom - size, size, size, 0, 90);  // bottom-right corner
        path.AddArc(area.X, area.Bottom - size, size, size, 90, 90);            // bottom-left corner
        path.CloseFigure();
        return path;
    }
}
