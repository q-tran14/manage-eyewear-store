using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using eyewear_store_management_system.Utils;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Sprache;
using DAL.Utils;


namespace eyewear_store_management_system
{
    public class BaseResponsiveForm: Form
    {
        private static readonly HttpClient client = new HttpClient();
        public Form parentForm;
        private bool _isLoaded = false;
        protected int horiMargin = 10;
        protected int vertMargin = 23;
        // Dùng để fill vô các khoảng trống
        private PictureBox logoPictureBox;
        private Label _titleLabel;
        protected string TitleText { get; set; } = "";
        // tên cột = giá trị (có thẻ như vầy: from value, to value) | [from / to] tên cột = giá trị
        protected Dictionary<string, string> searchQueryPart = new Dictionary<string, string>();
        protected int editAction = 0; // 0 = Not edit, 1 = Modify 2 = New
        // Các vị trí và kích thước ban đầu
        protected Dictionary<string, (Point location, Size size)> controlLayouts = new Dictionary<string, (Point, Size)>();

        public BaseResponsiveForm() { }
        public BaseResponsiveForm(Form f)
        {
            this.parentForm = f;
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            SaveOriginalLayout();
            _isLoaded = true; // Đánh dấu đã load xong
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            // Gọi hàm tạo logo và title sau khi form đã hiển thị hoàn toàn
            ArrangeLogoAndTitle();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (_isLoaded) 
            {
                HandleResize();
            }
        }
        #region Responsive UI
        private void HandleResize()
        {
            if (this.Size.Width == ScreenSizeConfig.Min.Width) AdjustLayoutForSmall();
            else if (this.Size.Width == ScreenSizeConfig.Medium.Width) AdjustLayoutForMedium();
            else AdjustLayoutForFull();

            ArrangeLogoAndTitle();
        }

        protected virtual void SaveOriginalLayout()
        {
            // Luôn lưu hai group box quan trọng
            SaveControlLayout("searchGrb");
            SaveControlLayout("actionGrb");

        }

        protected virtual void AdjustLayoutForSmall()
        {

            if (controlLayouts.Count() == 0) return;
            foreach (var pair in controlLayouts)
            {
                string controlName = pair.Key;
                RestoreControlLayout(controlName);
            }
        }

        protected virtual void AdjustLayoutForMedium() 
        {
            AdjustLayoutForSmall();
            int formWidth = this.Width;
            int formHeight = this.Height;
            var actionGrb = GetControl("actionGrb");
            var searchGrb = GetControl("searchGrb");
            if (actionGrb == null || searchGrb == null) return;

            int actionRightEdge = actionGrb.Location.X + actionGrb.Width;
            int availableWidth = formWidth - actionRightEdge;

            if (availableWidth == 300) return;
            int deltaWidth = availableWidth - 300;

            // Tìm InfoGrb (chỉ có 1 cái)
            string infoGrbKey = controlLayouts.Keys.FirstOrDefault(key => key.Contains("InfoGrb"));
            if (infoGrbKey == null) return; // Không có InfoGrb, thoát luôn

            int infoGrbIncrease = (deltaWidth * 2) / 3;
            int searchGrbIncrease = deltaWidth - infoGrbIncrease;

            // === Cập nhật kích thước InfoGrb ===
            var (infoLocation, infoSize) = controlLayouts[infoGrbKey];
            Size newInfoSize = new Size(infoSize.Width + infoGrbIncrease, infoSize.Height);
            SetControlSize(infoGrbKey, newInfoSize);
            SetControlLocation(infoGrbKey, infoLocation);

            int newRightEdge = infoLocation.X + newInfoSize.Width; // Cạnh phải của InfoGrb

            // === Cập nhật vị trí & kích thước searchGrb ===
            var (searchLocation, searchSize) = controlLayouts["searchGrb"];
            Point newSearLoc = new Point(newRightEdge + horiMargin, searchLocation.Y);
            Size newSearSize = new Size(searchSize.Width + searchGrbIncrease, searchSize.Height);
            SetControlLocation("searchGrb", newSearLoc);
            SetControlSize("searchGrb", newSearSize);

            int searchRightEdge = newSearLoc.X + newSearSize.Width;

            // === Cập nhật vị trí actionGrb (giữ nguyên kích thước) ===
            var (actionLocation, actionSize) = controlLayouts["actionGrb"];
            Point newActLoc = new Point(searchRightEdge + horiMargin, actionLocation.Y);
            SetControlLocation("actionGrb", newActLoc);

            if (controlLayouts.ContainsKey("listGrb"))
            {
                int usedFormHeight = horiMargin + actionGrb.Location.Y + actionGrb.Size.Height;
                int listHeight = formHeight - usedFormHeight;
                Point listLocation = new Point(0, usedFormHeight);
                Size listSize = new Size(formWidth, listHeight);

                SetControlSize("listGrb", listSize);
                SetControlLocation("listGrb", listLocation);
            }
        }

        protected virtual void AdjustLayoutForFull() 
        {
            AdjustLayoutForMedium();
        }

        protected void RestoreControlLayout(string controlName)
        {
            if (controlLayouts.TryGetValue(controlName, out var layout))
            {
                SetControlLocation(controlName, layout.location);
                SetControlSize(controlName, layout.size);
            }
        }

        protected void SaveControlLayout(string controlName)
        {
            var control = GetControl(controlName);
            if (control != null)
            {
                controlLayouts[controlName] = (control.Location, control.Size);
            }
        }

        private Control GetControl(string controlName)
        {
            var field = this.GetType().GetField(controlName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            return field?.GetValue(this) as Control;
        }

        private void SetControlLocation(string controlName, Point location)
        {
            var control = GetControl(controlName);
            if (control != null) control.Location = location;
        }

        private void SetControlSize(string controlName, Size size)
        {
            var control = GetControl(controlName);
            if (control != null) control.Size = size;
        }
        #endregion

        #region Logo and page title
        protected void ArrangeLogoAndTitle()
        {
            Control actionGrb = GetControl("actionGrb");
            if (actionGrb == null) return; // Không có actionGrb thì không làm gì cả

            if (this.Name.Contains("Orders") && this.Width == ScreenSizeConfig.Min.Width)
            {
                RemoveLogoAndTitle();
                return;
            }

            int availableSpace = this.Width - (actionGrb.Location.X + actionGrb.Width);
            // Nếu không đủ 320px thì xóa logo và title
            if (availableSpace < 280)
            {
                RemoveLogoAndTitle();
                return;
            }

            int centerX = actionGrb.Location.X + actionGrb.Width + availableSpace / 2;

            // Tạo logo nếu chưa có
            if (logoPictureBox == null)
            {
                logoPictureBox = new PictureBox
                {
                    Image = global::eyewear_store_management_system.Properties.Resources.logo_dark,
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Size = new Size(100, 100) // Kích thước logo mặc định
                };
                logoPictureBox.EnabledChanged += LogoPictureBox_EnabledChanged;
                this.Controls.Add(logoPictureBox);
            }

            // Tạo tiêu đề nếu chưa có
            if (_titleLabel == null)
            {
                _titleLabel = new Label
                {
                    Text = TitleText,
                    Font = new Font("Californian FB", 24F, FontStyle.Bold),
                    AutoSize = false,
                    TextAlign = ContentAlignment.MiddleCenter,
                    MaximumSize = new Size(300, 0), // Giới hạn chiều rộng, cho phép xuống dòng
                    Size = new Size(250, 100) // Kích thước mặc định
                };
                
                this.Controls.Add(_titleLabel);
            }
            _titleLabel.SendToBack();
            logoPictureBox.SendToBack();

            int totalWidth = logoPictureBox.Width - horiMargin + _titleLabel.Width; // Khoảng cách tối thiểu 10px
            int totalHeight = logoPictureBox.Height - horiMargin + _titleLabel.Height;

            int centerY = actionGrb.Location.Y + (actionGrb.Height / 2);
            int startY = centerY - (totalHeight / 2); 

            // Đủ chỗ để xếp ngang
            if (availableSpace >= totalWidth)
            {
                int logoX = centerX - totalWidth / 2;
                int titleX = logoX + logoPictureBox.Width + horiMargin;

                int startYTitle = actionGrb.Location.Y + actionGrb.Height / 2 - _titleLabel.Height/2;

                int startYLogo = actionGrb.Location.Y + actionGrb.Height / 2 - logoPictureBox.Height/2; 

                logoPictureBox.Location = new Point(logoX + horiMargin, startYLogo);
                _titleLabel.Location = new Point(titleX, startYTitle);
            }
            else // Xếp dọc 
            {
                _titleLabel.Size = new Size(_titleLabel.Size.Width, 80);
                int logoX = centerX - logoPictureBox.Width / 2;
                int titleX = centerX - _titleLabel.Width / 2;

                logoPictureBox.Location = new Point(logoX, startY);
                _titleLabel.Location = new Point(titleX, startY + logoPictureBox.Height - horiMargin);
            }
        }

        // Hàm xóa logo và title nếu không đủ khoảng trống
        public void RemoveLogoAndTitle()
        {
            if (logoPictureBox != null)
            {
                this.Controls.Remove(logoPictureBox);
                logoPictureBox.Dispose();
                logoPictureBox = null;
            }

            if (_titleLabel != null)
            {
                this.Controls.Remove(_titleLabel);
                _titleLabel.Dispose();
                _titleLabel = null;
            }
        }

        private void LogoPictureBox_EnabledChanged(object sender, EventArgs e)
        {
            if (!logoPictureBox.Enabled)
            {
                logoPictureBox.Image = UtilityImage.AdjustImage(Properties.Resources.logo_dark, exposure: 0.6f, saturation: -1f, shadows: -50f);
            }
            else
            {
                logoPictureBox.Image = Properties.Resources.logo_dark;
            }
        }
        #endregion

        #region Combo box utility
        // Set up Combo Box thủ công
        protected void SetupComboBox(ComboBox comboBox, string placeholder, List<KeyValuePair<string, string>> values)
        {
            comboBox.Items.Clear();
            comboBox.Items.Add(new KeyValuePair<string, string>("", placeholder)); // Placeholder
            comboBox.SelectedIndex = 0;

            if (values != null)
            {
                foreach (var item in values)
                {
                    comboBox.Items.Add(item); // Thêm từng phần tử thay vì AddRange()
                }
            }

            comboBox.DisplayMember = "Value"; // Hiển thị nội dung
            comboBox.ValueMember = "Key";     // Giá trị thực tế để lấy ra
        }
        // Set up Combo box với sql query không có parameter
        protected void SetupComboBox(ComboBox comboBox, string placeholder, string query)
        {
            SetupComboBox(comboBox, placeholder, query, null);
        }
        // Set up Combo box với sql query có parameter
        protected void SetupComboBox(ComboBox comboBox, string placeholder, string query, SqlParameter[] parameters)
        {
            comboBox.Items.Clear();
            comboBox.Items.Add(placeholder);
            comboBox.SelectedIndex = 0;

            if (!string.IsNullOrEmpty(query))
            {
                try
                {
                    DataTable dt = UtilityDatabase.Instance.ExecuteQuery(query, parameters);
                    foreach (DataRow row in dt.Rows)
                    {
                        comboBox.Items.Add(row[0].ToString()); // Lấy dữ liệu từ cột đầu tiên
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Load dữ liệu từ API và thiết lập ComboBox
        protected async Task SetupComboBoxFromApi(ComboBox comboBox, string placeholder, string apiUrl)
        {
            comboBox.Items.Clear();
            comboBox.Items.Add(new KeyValuePair<string, string>("", "----Can't choose----")); // Placeholder
            comboBox.SelectedIndex = 0;
            comboBox.Enabled = false;

            if (string.IsNullOrEmpty(apiUrl))
            {
                MessageBox.Show("Lỗi: URL API không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (client == null)
            {
                MessageBox.Show("Lỗi: HttpClient chưa được khởi tạo!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                HttpClientHandler handler = new HttpClientHandler();
                handler.SslProtocols = System.Security.Authentication.SslProtocols.Tls12 | System.Security.Authentication.SslProtocols.Tls13;
                HttpClient client = new HttpClient(handler);
                HttpResponseMessage response = await client.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode();
                string json = await response.Content.ReadAsStringAsync();

                JObject jsonResponse = JObject.Parse(json);

                // Kiểm tra nếu response không có dữ liệu hoặc không hợp lệ
                if (!jsonResponse.ContainsKey("data") || jsonResponse["data"] == null || !jsonResponse["data"].HasValues)
                {
                    MessageBox.Show("Lỗi: API không chứa dữ liệu hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                JToken dataToken = jsonResponse["data"]["data"];

                if (dataToken == null || !dataToken.HasValues)
                {
                    MessageBox.Show("Lỗi: Không tìm thấy dữ liệu!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                JArray data = (JArray)dataToken;

                comboBox.Items.Clear();
                comboBox.Items.Add(new KeyValuePair<string, string>("", placeholder)); // Placeholder
                comboBox.SelectedIndex = 0;
                foreach (var item in data)
                {
                    if (item["name"] != null && item["code"] != null)
                    {
                        comboBox.Items.Add(new KeyValuePair<string, string>(item["code"].ToString(), item["name"].ToString()));
                    }
                }

                comboBox.DisplayMember = "Value";
                comboBox.ValueMember = "Key";
                comboBox.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu từ API: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected void BlockComboBox(ComboBox cb)
        {
            cb.Items.Clear();
            cb.Items.Add(new KeyValuePair<string, string>("", "----Can't choose----"));
            cb.DisplayMember = "Value";
            cb.ValueMember = "Key";
            cb.SelectedIndex = 0;
            cb.Enabled = false;
        }
        #endregion

        #region Search group utility
        protected void ResetFilters(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                if (control is ComboBox comboBox)
                {
                    comboBox.SelectedIndex = 0; // Reset ComboBox
                }
                else if (control is CustomComponents.CustomDatePicker datePicker)
                {
                    datePicker.Value = DateTime.Today; // Reset DatePicker
                }
                else if (control is CustomComponents.CustomTextInput textBox) //
                {
                    // Reset TextBox
                    textBox.Texts = string.Empty;
                }
                else if (control is CheckBox checkbox)
                {
                    checkbox.Checked = false;
                }
                // Đệ quy để duyệt tiếp các control con bên trong
                ResetFilters(control);
            }
            searchQueryPart.Clear();
        }

        protected string searchStringProcess(string columnsCreated, string tableName)
        {
            string searchQuery = $"SELECT {columnsCreated} FROM {tableName} WHERE ";

            foreach (var pair in searchQueryPart)
            {
                string key = pair.Key;
                string value = pair.Value;
                // MessageBox.Show($"{key}\n{value}");
                string nextCondition = $"{key} = {value}";
                // Range combo box
                if (value.Contains("from") && value.Contains("to"))
                {
                    string[] tmp = value.Split(',');
                    nextCondition = $"{key} BETWEEN '{tmp[0].Substring("from ".Length)}' AND '{tmp[1].Substring("to ".Length)}' ";
                }
                else if (value.Contains("from") && !value.Contains("to")) // DateTime, Salary, Price, Quantity
                {
                    nextCondition = $"{key} >= {value.Substring("from ".Length)} ";
                }
                else if (!value.Contains("from") && value.Contains("to"))
                {
                    nextCondition = $"{key} <= {value.Substring("to ".Length)} ";
                }

                // Seach text box
                if (key.Contains("searchTxtb"))
                {
                    // Xử lý ID, Name, Phone
                    if (int.TryParse(value, out _))
                    {
                        if (value.StartsWith('0')) // Phone
                        {
                            nextCondition = $"Phone LIKE '%{value}%'";
                        }
                        else // ID
                        {
                            nextCondition = $"ID LIKE '%{value}%'";
                        }
                    }
                    else if (value.Contains("@")) // email
                    {
                        nextCondition = $"Email LIKE '%{value}%'";
                    }
                    else // Name
                    {
                        nextCondition = $"Name LIKE N'%{value}%'";
                    }
                }
                if (key == "City") nextCondition = $"Address LIKE N'%{value}%'";
                if (key == "District") nextCondition = $"Address LIKE N'Quận ' + N'%{value}%'";
                if (key == "Ward") nextCondition = $"Address LIKE N'Phường ' + N'%{value}%'";

                searchQuery += nextCondition;
                searchQuery += "AND ";
            }
            if (searchQuery.Substring(searchQuery.Length - 4) == "AND ") searchQuery = searchQuery.Substring(0, searchQuery.Length - 4);
            return searchQuery;
        }
        #endregion

        #region Other utility
        /// <summary>
        /// Tạo danh sách các khoảng để dùng trong ComboBox.
        /// </summary>
        /// <param name="min">Số tối thiểu.</param>
        /// <param name="max">Số tối đa.</param>
        /// <param name="step">Khoảng cách giữa các số.</param>
        /// <param name="nameVariable">Tên biến trong SQL.</param>
        /// <returns> 
        /// <para>Danh sách các khoảng dưới dạng KeyValuePair (SQL Condition, Display Text).</para>
        /// <para>Vd về 1 item trong list: nameVariable <= giá trị nào đó , 1 đoạn text</para>
        /// <para>Cụ thể hơn: salary > 20 AND salary <= 30, 20 - 30 VND</para>
        /// </returns>
        protected List<KeyValuePair<string, string>> GenerateRanges(int min, int max, int step, string nameVariable, string unit)
        {
            List<KeyValuePair<string, string>> ranges = new List<KeyValuePair<string, string>>();

            // Mức lương nhỏ hơn hoặc bằng min
            ranges.Add(new KeyValuePair<string, string>($"to {min}", $"≤ {min}{unit}"));

            // Tạo các mức lương theo step
            for (int i = min; i < max; i += step)
            {
                int upper = i + step;
                ranges.Add(new KeyValuePair<string, string>($"from {i},to {upper}", $"{i} - {upper}{unit}"));
            }

            // Mức lương lớn hơn max
            ranges.Add(new KeyValuePair<string, string>($"from {max}", $"> {max}{unit}"));

            return ranges;
        }
        #endregion
    }
}
