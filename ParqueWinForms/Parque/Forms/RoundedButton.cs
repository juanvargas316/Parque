using System.Drawing.Drawing2D;

namespace Parque.Forms
{
    public class RoundedButton : Button
    {
        private bool _isHovered = false;

        protected override void OnMouseEnter(EventArgs e)
        {
            _isHovered = true;
            Invalidate(); // redibuja el botón
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            _isHovered = false;
            Invalidate(); // redibuja el botón
            base.OnMouseLeave(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            using var path = new GraphicsPath();
            var rect = new Rectangle(0, 0, Width - 1, Height - 1);
            int r = 20;

            path.AddArc(rect.X, rect.Y, r, r, 180, 90);
            path.AddArc(rect.Right - r, rect.Y, r, r, 270, 90);
            path.AddArc(rect.Right - r, rect.Bottom - r, r, r, 0, 90);
            path.AddArc(rect.X, rect.Bottom - r, r, r, 90, 90);
            path.CloseFigure();

            // Si está hover oscurece un poco el color
            Color colorFondo = _isHovered
                ? ControlPaint.Dark(BackColor, 0.15f)
                : BackColor;

            using var brush = new SolidBrush(colorFondo);
            e.Graphics.FillPath(brush, path);

            using var pen = new Pen(colorFondo, 1);
            e.Graphics.DrawPath(pen, path);

            var textRect = new Rectangle(0, 0, Width, Height);
            var sf = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            using var textBrush = new SolidBrush(ForeColor);
            e.Graphics.DrawString(Text, Font, textBrush, textRect, sf);

            this.Region = new Region(path);
        }
    }
}