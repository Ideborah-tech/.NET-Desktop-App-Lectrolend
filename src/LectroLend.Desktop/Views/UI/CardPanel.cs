using System.Drawing.Drawing2D;

namespace LectroLend.Desktop.Views.UI;

// A white box with round corners and a thin grey border.
// Use it like a normal Panel: put labels and buttons inside it.
public class CardPanel : Panel
{
    public CardPanel()
    {
        BackColor = Color.White;

        // We draw the background ourselves. "ResizeRedraw" draws it again when the size changes.
        SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint
                 | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
    }

    protected override void OnPaintBackground(PaintEventArgs e)
    {
        Graphics g = e.Graphics;
        g.SmoothingMode = SmoothingMode.AntiAlias;

        // The corners show the colour behind the card.
        if (Parent != null)
            g.Clear(Parent.BackColor);

        Rectangle area = new Rectangle(0, 0, Width - 1, Height - 1);
        GraphicsPath shape = Shapes.RoundedRectangle(area, 14);

        SolidBrush fill = new SolidBrush(BackColor);
        g.FillPath(fill, shape);
        fill.Dispose();

        Pen border = new Pen(Color.FromArgb(226, 232, 240));
        g.DrawPath(border, shape);
        border.Dispose();

        shape.Dispose();
    }
}
