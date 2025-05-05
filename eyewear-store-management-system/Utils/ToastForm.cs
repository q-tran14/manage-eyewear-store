using System.Windows.Forms;
using System.Diagnostics;
namespace eyewear_store_management_system.Utils
{
    public partial class ToastForm : Form
    {
        private System.Windows.Forms.Timer closeTimer;
        private System.Windows.Forms.Timer fadeTimer;

        private string _title;
        private string _message;
        private Form _form;
        public ToastForm(string title, string message, string type, Form parentForm)
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.Manual;
            this.Opacity = 0; // Bắt đầu trong suốt
            this.Size = new Size(285, 70); // Kích thước toast
            this.ShowInTaskbar = false;

            // Chọn màu title theo loại thông báo
            this.BackColor = type == "error" ? Color.FromArgb(255, 173, 173) : Color.FromArgb(199, 230, 199); // Đỏ pastel hoặc xanh pastel
            Image iconImage = type == "error" ? global::eyewear_store_management_system.Properties.Resources.error
                                              : global::eyewear_store_management_system.Properties.Resources.success;

            _title = title;
            _message = message;
            _form = parentForm;

            // Load icon từ Resources
            PictureBox icon = new PictureBox
            {
                Image = iconImage,
                SizeMode = PictureBoxSizeMode.StretchImage,
                Size = new Size(40, 40),
                Location = new Point(12, (this.Height - 40) / 2) // Căn giữa chiều dọc
            };
            icon.Click += ToastForm_Click;

            // Cấu hình ban đầu
            Font titleFont = new Font("Arial", 12, FontStyle.Regular);
            int maxWidth = this.Width - 70 - 10; // Tổng khoảng trống từ trái và phải (tuỳ padding)

            string trimmedTitle = TrimMessageToFit(title, titleFont, maxWidth);

            // Label Title
            Label lblTitle = new Label
            {
                Text = trimmedTitle,
                ForeColor = Color.Black,
                Font = new Font("Arial", 12, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(60, 15) // Cách icon 20px
            };
            lblTitle.Click += ToastForm_Click;

            Font messageFont = new Font("Arial", 10, FontStyle.Regular);
            // Cắt chuỗi phù hợp độ rộng
            string trimmedMessage = TrimMessageToFit(message, messageFont, maxWidth);

            // Label Message
            Label lblMessage = new Label
            {
                Text = trimmedMessage,
                ForeColor = Color.Black,
                Font = new Font("Arial", 10, FontStyle.Regular),
                AutoSize = true,
                Location = new Point(60, 40) // Cách title một chút
            };
            lblMessage.Click += ToastForm_Click;
            this.Controls.Add(icon);
            this.Controls.Add(lblTitle);
            this.Controls.Add(lblMessage);


            // Vị trí toast
            Rectangle parentBounds = parentForm.Bounds;
            int x = parentBounds.Right - this.Width - 10;
            int y = parentBounds.Bottom - this.Height - 10;
            this.Location = new Point(x, y);

            // Timer đóng form sau 3 giây
            closeTimer = new System.Windows.Forms.Timer { Interval = 3000 };
            closeTimer.Tick += (s, e) => { this.Close(); };
            closeTimer.Start();

            // Timer hiệu ứng fade-in
            fadeTimer = new System.Windows.Forms.Timer { Interval = 50 };
            fadeTimer.Tick += (s, e) =>
            {
                if (this.Opacity < 1.0)
                    this.Opacity += 0.1;
                else
                    fadeTimer.Stop();
            };
            fadeTimer.Start();
        }

        private string TrimMessageToFit(string message, Font font, int maxWidth)
        {
            using (Bitmap bmp = new Bitmap(1, 1))
            using (Graphics g = Graphics.FromImage(bmp))
            {
                string ellipsis = "...";
                SizeF size = g.MeasureString(message, font);

                if (size.Width <= maxWidth)
                    return message;

                for (int i = message.Length - 1; i >= 0; i--)
                {
                    string trimmed = message.Substring(0, i) + ellipsis;
                    size = g.MeasureString(trimmed, font);
                    if (size.Width <= maxWidth)
                        return trimmed;
                }

                return ellipsis;
            }
        }

        private void ToastForm_Click(object sender, EventArgs e)
        {
           closeTimer.Stop();
           MessageBox.Show(_message, _title);
           this.Close();
        }
    }
}
