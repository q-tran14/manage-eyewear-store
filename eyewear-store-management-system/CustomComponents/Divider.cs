using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace eyewear_store_management_system.CustomComponents
{
    public class Divider : Control
    {
        // Fields
        private int thickness = 2;
        private Color lineColor = Color.Gray;
        private bool isVertical = false;

        // Properties
        [Category("Divider Properties")]
        public int Thickness
        {
            get { return thickness; }
            set { thickness = value; Invalidate(); }
        }

        [Category("Divider Properties")]
        public Color LineColor
        {
            get { return lineColor; }
            set { lineColor = value; Invalidate(); }
        }

        [Category("Divider Properties")]
        public bool IsVertical
        {
            get { return isVertical; }
            set { isVertical = value; Invalidate(); }
        }

        // Constructor
        public Divider()
        {
            this.Size = new Size(200, 2);
            this.Paint += Divider_Paint;
        }

        // Paint event
        private void Divider_Paint(object sender, PaintEventArgs e)
        {
            using (Pen pen = new Pen(lineColor, thickness))
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                if (isVertical)
                {
                    e.Graphics.DrawLine(pen, this.Width / 2, 0, this.Width / 2, this.Height);
                }
                else
                {
                    e.Graphics.DrawLine(pen, 0, this.Height / 2, this.Width, this.Height / 2);
                }
            }
        }
    }
}