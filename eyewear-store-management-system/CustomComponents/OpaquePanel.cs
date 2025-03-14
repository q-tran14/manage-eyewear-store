using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace eyewear_store_management_system.CustomComponents
{
    public class OpaquePanel : Panel
    {
        private const int WS_EX_TRANSPARENT = 0x20;

        public OpaquePanel()
        {
            SetStyle(ControlStyles.Opaque | ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint, true);
            BackColor = Color.Black;
            Visible = false; // Mặc định ẩn khi khởi tạo
        }

        private int opacity = 50;
        [DefaultValue(50)]
        public int Opacity
        {
            get { return this.opacity; }
            set
            {
                if (value < 0 || value > 100)
                    throw new ArgumentException("Opacity must be between 0 and 100");
                this.opacity = value;
                Invalidate();
            }
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                //cp.ExStyle |= WS_EX_TRANSPARENT; // Duy trì hiệu ứng trong suốt
                return cp;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            using (SolidBrush brush = new SolidBrush(Color.FromArgb(opacity * 255 / 100, BackColor)))
            {
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }
        }
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            // Không gọi base.OnPaintBackground để tránh flickering (nhấp nháy)
        }

        protected override void WndProc(ref Message m)
        {
            const int WM_LBUTTONDOWN = 0x0201;
            if (m.Msg == WM_LBUTTONDOWN)
            {
                OnClick(EventArgs.Empty);
                return;
            }
            base.WndProc(ref m);
        }
    }
}
