using BLL;
using eyewear_store_management_system.Utils;
using System.Diagnostics;
using System.Net.Mail;
using System.Net;
using System.Data;

namespace eyewear_store_management_system
{
    public partial class StaffManagementForm : Form
    {
        private List<KeyValuePair<string, string>> _listRole = new List<KeyValuePair<string, string>>{ new KeyValuePair<string, string>("Staff", "Staff"), new KeyValuePair<string, string>("Manager", "Manager") };
        private List<KeyValuePair<string, string>> _listSearchRole = new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>("Choose Option", ""), new KeyValuePair<string, string>("Staff", "Staff"), new KeyValuePair<string, string>("Manager", "Manager") };
        private List<KeyValuePair<string, string>> _listDefault = new List<KeyValuePair<string, string>>{ new KeyValuePair<string, string>("Choose Option", "") };
        public event EventHandler OnCityLoadFinished;
        public event EventHandler OnDistrictLoadFinished;

        private MainForm _parentForm;
        private int _action; // -1: Default | 1: New | 2: Modify

        public StaffManagementForm(MainForm parent)
        {
            _parentForm = parent;
            InitializeComponent();
            OtherUtilities.SetUpComboBox(cbRole, _listRole);
            OtherUtilities.SetUpComboBox(cbSearchRole, _listSearchRole);
            OtherUtilities.SetUpComboBox(cbSearchCity, Program.cities);
            SetUpSearchFilter();
            OtherUtilities.SetUpComboBox(cbCity, Program.cities);
        }
        private void StaffManagementForm_Load(object sender, EventArgs e)
        {
            this.Enabled = false;
            this.SuspendLayout();
            _parentForm.FocusExit();
            _action = -1;

            // Data Grid View Staff List
            dgvStaffList.DataSource = EmployeeBLL.GetAllEmployee();
            dgvStaffList.Enabled = true;

            // Group box Tools
            OtherUtilities.ButtonEnable(btnNew);
            OtherUtilities.ButtonEnable(btnPrint);

            OtherUtilities.ButtonDisable(btnModify);
            OtherUtilities.ButtonDisable(btnRemove);
            OtherUtilities.ButtonDisable(btnSave);
            OtherUtilities.ButtonDisable(btnCancel);

            // Group box Search & Filter
            OtherUtilities.ButtonEnable(btnFind);
            OtherUtilities.ButtonDisable(btnClear);

            // Group Box Staff Information
            txtbStaffID.Texts = "";
            txtbStaffName.Texts = "";
            txtbStaffEmail.Texts = "";
            txtbStaffPhone.Texts = "";
            txtbSalary.Texts = "";
            txtbStreetAddress.Texts = "";
            
            cbRole.SelectedIndex = 0;
            cbCity.SelectedIndex = 0;

            OtherUtilities.SetUpComboBox(cbDistrict, _listDefault);
            OtherUtilities.SetUpComboBox(cbWard, _listDefault);

            gBStaffInformation.Enabled = false;

            this.ResumeLayout();
            this.Enabled = true;
        }

        #region DATA GRID VIEW ACTION
        private void dgvStaffList_Click(object sender, EventArgs e)
        {
            DataGridViewRow? row = dgvStaffList.CurrentRow;
            if (row != null && row.Index != dgvStaffList.Rows.Count - 1)
            {
                string id = row.Cells["ID"].Value.ToString();
                string name = row.Cells["Name"].Value.ToString();
                string email = row.Cells["Email"].Value.ToString();
                string streetAddress = row.Cells["Street Address"].Value.ToString();
                string ward = row.Cells["Ward"].Value.ToString();
                string district = row.Cells["District"].Value.ToString();
                string city = row.Cells["City"].Value.ToString();
                string phone = row.Cells["Phone"].Value.ToString();
                int salary = Convert.ToInt32(row.Cells["Salary"].Value);
                string role = row.Cells["Role"].Value.ToString();

                txtbStaffID.Texts = id;
                txtbStaffName.Texts = name;
                txtbStaffEmail.Texts = email;
                txtbStaffPhone.Texts = phone;
                txtbSalary.Texts = salary.ToString();
                txtbStreetAddress.Texts = streetAddress;

                cbRole.SelectedIndex = cbRole.Items.Cast<KeyValuePair<string, string>>().ToList().FindIndex(item => item.Key == role);
                
                cbCity.SelectedIndex = cbCity.Items.Cast<KeyValuePair<string, string>>().ToList().FindIndex(item => item.Key == city);

                this.OnCityLoadFinished += (s, e) =>
                {
                    Debug.WriteLine("Load City Finish");
                    cbDistrict.SelectedIndex = cbDistrict.Items.Cast<KeyValuePair<string, string>>().ToList().FindIndex(item => item.Key == district);
                };

                this.OnDistrictLoadFinished += (s, e) =>
                {
                    Debug.WriteLine("Load District Finish");
                    cbWard.SelectedIndex = cbWard.Items.Cast<KeyValuePair<string, string>>().ToList().FindIndex(item => item.Key == ward);
                };

                OtherUtilities.ButtonEnable(btnModify);
                OtherUtilities.ButtonEnable(btnRemove);
            }
        }
        #endregion

        #region TOOLS ACTION
        private void btnNew_Click(object sender, EventArgs e)
        {
            _action = 1;

            // Data Grid View Staff List
            dgvStaffList.Enabled = false;

            // Group box Staff Information
            txtbStaffID.Texts = EmployeeBLL.AutoGenerationID(cbRole.Texts.Trim());
            txtbStaffName.Texts = "";
            txtbStaffName.Focus();
            txtbStaffEmail.Texts = "";
            txtbStaffPhone.Texts = "";
            txtbSalary.Texts = "";
            txtbStreetAddress.Texts = "";

            cbCity.SelectedIndex = 0;

            gBStaffInformation.Enabled = true;
            txtbStaffID.Enabled = false;

            // Group box Tools
            OtherUtilities.ButtonEnable(btnSave);
            OtherUtilities.ButtonEnable(btnCancel);

            OtherUtilities.ButtonDisable(btnNew);
            OtherUtilities.ButtonDisable(btnModify);
            OtherUtilities.ButtonDisable(btnRemove);
        }
        private void btnModify_Click(object sender, EventArgs e)
        {
            _action = 2;

            // Data Grid View Product List
            dgvStaffList.Enabled = false;

            // Group box Product Information
            gBStaffInformation.Enabled = true;
            txtbStaffName.Focus();
            txtbStaffID.Enabled = false;

            // Group box Tools
            OtherUtilities.ButtonEnable(btnSave);
            OtherUtilities.ButtonEnable(btnCancel);

            OtherUtilities.ButtonDisable(btnNew);
            OtherUtilities.ButtonDisable(btnModify);
            OtherUtilities.ButtonDisable(btnRemove);
        }
        private void btnRemove_Click(object sender, EventArgs e)
        {
            // Remove Logic
            DialogResult r = MessageBox.Show(
                "Are you sure you want to delete this staff?",
                "Delete Confirmation",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (r != DialogResult.Yes) return;

            try
            {
                bool success = new EmployeeBLL(txtbStaffID.Texts.Trim(),"","","","","","","","",0,"").DeleteEmployee();

                // Turn to default form state 
                StaffManagementForm_Load(null, EventArgs.Empty);

                if (success == true)
                {
                    ToastManager.ShowToastNotification("Remove Success", "The staff has been successfully deleted", "success", _parentForm);
                    return;
                }
            }
            catch (Exception ex)
            {
                ToastManager.ShowToastNotification("Remove Fail", ex.Message, "error", this._parentForm);
                return;
            }
        }
        private async void btnSave_Click(object sender, EventArgs e)
        {
            // Save Logic
            DialogResult r = MessageBox.Show(
                "Are you sure you want to save your changes?",
                "Save Confirmation",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (r != DialogResult.Yes) return;

            string id = txtbStaffID.Texts.Trim();
            string name = txtbStaffName.Texts.Trim();
            string email = txtbStaffEmail.Texts.Trim();
            string streetAddress = txtbStreetAddress.Texts.Trim();
            string ward = cbWard.Texts.Trim();
            string district = cbDistrict.Texts.Trim();
            string city = cbCity.Texts.Trim();
            string phone = txtbStaffPhone.Texts.Trim();
            int salary = Convert.ToInt32(txtbSalary.Texts.Trim());
            string role = cbRole.Texts.Trim();

            string password = "";
            if (_action == 1) password = await SendPasswordToMail(email);

            EmployeeBLL employeeBLL = new EmployeeBLL(id,name,email,password,streetAddress,ward,district,city,phone,salary,role);

            try
            {
                bool success = false;
                if (_action == 1) success = employeeBLL.AddEmployee();  // Create New Product

                if (_action == 2) success = employeeBLL.UpdateEmployeeInformation();  // Update Product Information

                // Turn to default form state 
                StaffManagementForm_Load(null, EventArgs.Empty);

                if (success)
                {
                    ToastManager.ShowToastNotification($"{(_action == 1 ? "Create" : "Update")} Staff Succes", $"Staff has been {(_action == 1 ? "added" : "updated")} successfully", "success", _parentForm);
                    return;
                }
                else
                {
                    ToastManager.ShowToastNotification($"{(_action == 1 ? "Create" : "Update")} Staff Fail", $"Staff could not be {(_action == 1 ? "added" : "updated")}", "error", _parentForm);
                    return;
                }
            }
            catch (Exception ex)
            {
                ToastManager.ShowToastNotification($"{(_action == 1 ? "Create" : "Update")} Staff Fail", ex.Message, "error", _parentForm);
                return;
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            // Turn to default form state 
            StaffManagementForm_Load(null, EventArgs.Empty);
        }
        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (dgvStaffList.Rows.Count > 0)
            {
                DataTable dt = OtherUtilities.GetDataTableFromDGV(dgvStaffList);
                OtherUtilities.ExportToExcel(dt);
            }
        }
        #endregion

        #region SEARCH ACTION
        private void btnFind_Click(object sender, EventArgs e)
        {
            // Search Logic
            string idOrNameOrEmailOrPhone = txtbSearchBox.Texts.Trim();

            int salaryMin = Convert.ToInt32(string.IsNullOrEmpty(txtbSalaryMin.Texts.Trim()) ? "-1" : txtbSalaryMin.Texts.Trim());
            int salaryMax = Convert.ToInt32(string.IsNullOrEmpty(txtbSalaryMax.Texts.Trim()) ? "-1" : txtbSalaryMax.Texts.Trim());

            List<string> conditionList = new List<string>();
            bool searchAvailable = false;

            // Search by ID, Name, Email, Phone
            string columnName = "";
            if ((idOrNameOrEmailOrPhone.StartsWith("S") || idOrNameOrEmailOrPhone.StartsWith("M")) && idOrNameOrEmailOrPhone.Length == 9) columnName = "ID";
            else if (idOrNameOrEmailOrPhone.StartsWith("0") && idOrNameOrEmailOrPhone.Length == 10 && idOrNameOrEmailOrPhone.All(char.IsDigit)) columnName = "Phone";
            else if (idOrNameOrEmailOrPhone.Contains("@")) columnName = "Email";
            else columnName = "Name";

            if (columnName != "") 
            {
                conditionList.Add(columnName == Name ? $"{columnName} LIKE N'%{idOrNameOrEmailOrPhone}%'" : $"{columnName} LIKE {idOrNameOrEmailOrPhone}");
                searchAvailable = true;
            }

            // Search by Role
            if (cbSearchRole.SelectedIndex > 0) 
            { 
                conditionList.Add($"ROLE = {cbSearchRole.Texts.Trim()}");
                searchAvailable = true;
            }

            // Search by Salary
            string searchBySalary = "";
            if (salaryMin >= 0 && salaryMax < 0) searchBySalary = $"Salary >= {salaryMin}";
            else if (salaryMin < 0 && salaryMax >= 0) searchBySalary = $"Salary <= {salaryMax}";
            else if (salaryMin >= 0 && salaryMax >= 0) searchBySalary = $"(Salary BETWEEN {salaryMin} AND {salaryMax})";

            if (!string.IsNullOrEmpty(searchBySalary))
            {
                conditionList.Add(searchBySalary);
                searchAvailable = true;
            }

            // Search by Date Hired
            string searchByDateHired = "";
            if (dPMin.Value > dPMin.MinDate && dPMax.Value == dPMax.MinDate) searchByDateHired = $"[Date Employed] >= '{dPMin.Value:yyyy-MM-dd}'";
            else if (dPMin.Value == dPMin.MinDate && dPMax.Value > dPMax.MinDate) searchByDateHired = $"[Date Employed] <= '{dPMax.Value:yyyy-MM-dd}'";
            else if (dPMin.Value > dPMin.MinDate && dPMax.Value > dPMax.MinDate) searchByDateHired = $"([Date Employed] BETWEEN '{dPMin.Value:yyyy-MM-dd}' AND '{dPMax.Value:yyyy-MM-dd}')";

            if (!string.IsNullOrEmpty(searchByDateHired))
            {
                conditionList.Add(searchByDateHired);
                searchAvailable = true;
            }

            // Saerch by Location
            if (cbSearchCity.SelectedIndex > 0)
            {
                conditionList.Add($"City = {cbSearchCity.Texts.Trim()}");
                searchAvailable = true;
            }

            if (cbSearchDistrict.SelectedIndex > 0)
            {
                conditionList.Add($"District = {cbSearchDistrict.Texts.Trim()}");
                searchAvailable = true;
            }

            if (cbSearchWard.SelectedIndex > 0)
            {
                conditionList.Add($"Ward = {cbSearchWard.Texts.Trim()}");
                searchAvailable = true;
            }

            if (searchAvailable == false) return;

            string query = "SELECT ID, [Name], Email, [Street Address], Ward, District, City, Phone, Salary, [Role], [Date Employed] FROM Employee";
            query += " WHERE " + string.Join(" AND ", conditionList);

            DataTable result = OtherUtilities.Search(query, _parentForm);
            if (result != null && result.Rows.Count > 0)
            {
                dgvStaffList.DataSource = result;
                OtherUtilities.ButtonEnable(btnClear);
            }
            else
            {
                ToastManager.ShowToastNotification("No Results Found", "No matching results found", "error", _parentForm);
                return;
            }
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            // Clear Logic
            SetUpSearchFilter();
            // Turn to default form state 
            StaffManagementForm_Load(null, EventArgs.Empty);
        }
        private void txtbSearchBox__TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtbSearchBox.Texts)) return;
            if (btnClear.Enabled == false) OtherUtilities.ButtonEnable(btnClear);
        }
        private void txtbSalaryMin__TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtbSalaryMin.Texts)) return;
            if (btnClear.Enabled == false) OtherUtilities.ButtonEnable(btnClear);
        }
        private void txtbSalaryMax__TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtbSalaryMax.Texts)) return;
            if (btnClear.Enabled == false) OtherUtilities.ButtonEnable(btnClear);
        }
        private void dPMin_ValueChanged(object sender, EventArgs e)
        {
            if (dPMin.Value == dPMin.MinDate) return;
            if (btnClear.Enabled == false) OtherUtilities.ButtonEnable(btnClear);
        }
        private void dPMax_ValueChanged(object sender, EventArgs e)
        {
            if (dPMax.Value == dPMax.MinDate) return;
            if (btnClear.Enabled == false) OtherUtilities.ButtonEnable(btnClear);
        }
        private void txtbSalaryMin_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtbSalaryMin.Texts.Trim())) return;
            int salaryMin = Convert.ToInt32(txtbSalaryMin.Texts.Trim());
            if (salaryMin < 0)
            {
                ToastManager.ShowToastNotification("Invalid Salary", "Salary must be positive number", "error", _parentForm);
                return;
            }
            if (!string.IsNullOrEmpty(txtbSalaryMax.Texts.Trim()))
            {
                int salaryMax = Convert.ToInt32(txtbSalaryMax.Texts.Trim());
                if (salaryMax < salaryMin)
                {
                    txtbSalaryMin.Texts = salaryMax.ToString();
                    txtbSalaryMax.Texts = salaryMin.ToString();
                }
            }
        }
        private void txtbSalaryMax_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtbSalaryMax.Texts.Trim())) return;
            int salaryMax = Convert.ToInt32(txtbSalaryMax.Texts.Trim());
            if (salaryMax < 0)
            {
                ToastManager.ShowToastNotification("Invalid Salary", "Salary must be positive number", "error", _parentForm);
                return;
            }
            if (!string.IsNullOrEmpty(txtbSalaryMin.Texts.Trim()))
            {
                int salaryMin = Convert.ToInt32(txtbSalaryMin.Texts.Trim());
                if (salaryMax < salaryMin)
                {
                    txtbSalaryMin.Texts = salaryMax.ToString();
                    txtbSalaryMax.Texts = salaryMin.ToString();
                }
            }
        }
        private void dPMin_Leave(object sender, EventArgs e)
        {
            if (dPMin.Value == dPMin.MinDate) return;
            if (dPMax.Value == dPMax.MinDate) return;
            DateTime fromDate = dPMin.Value;
            DateTime toDate = dPMax.Value;
            if (fromDate > toDate)
            {
                dPMin.Value = toDate;
                dPMax.Value = fromDate;
            }
        }
        private void dPMax_Leave(object sender, EventArgs e)
        {
            if (dPMax.Value == dPMax.MinDate) return;
            if (dPMin.Value == dPMin.MinDate) return;
            DateTime fromDate = dPMin.Value;
            DateTime toDate = dPMax.Value;
            if (fromDate > toDate)
            {
                dPMin.Value = toDate;
                dPMax.Value = fromDate;
            }
        }
        private void cbSearchRole_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbSearchRole.SelectedIndex == 0) return;
            if (btnClear.Enabled == false) OtherUtilities.ButtonEnable(btnClear);
        }
        private async void cbSearchCity_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbSearchCity.SelectedIndex > 0)
            {
                string cityCode = "";
                if (cbSearchCity.SelectedItem is KeyValuePair<string, string> selectedCity) cityCode = selectedCity.Value;

                if (string.IsNullOrEmpty(cityCode)) return;

                if (cbWard.Items.Count > 0) OtherUtilities.SetUpComboBox(cbSearchWard, _listDefault);

                await OtherUtilities.LoadDistricts(cbSearchDistrict, cityCode);

                if (btnClear.Enabled == false) OtherUtilities.ButtonEnable(btnClear);
            }
        }
        private async void cbSearchDistrict_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbSearchDistrict.SelectedIndex > 0)
            {
                string districtCode = "";
                if (cbSearchDistrict.SelectedItem is KeyValuePair<string, string> selectedDistrict) districtCode = selectedDistrict.Value;

                if (string.IsNullOrEmpty(districtCode)) return;

                await OtherUtilities.LoadWards(cbSearchWard, districtCode);

                if (btnClear.Enabled == false) OtherUtilities.ButtonEnable(btnClear);
            }
        }
        private void cbSearchWard_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbSearchWard.SelectedIndex > 0)
            {
                if (btnClear.Enabled == false) OtherUtilities.ButtonEnable(btnClear);
            }
        }
        private void SetUpSearchFilter()
        {
            txtbSearchBox.Texts = "";
            txtbSalaryMin.Texts = "";
            txtbSalaryMax.Texts = "";
            dPMin.Value = dPMin.MinDate;
            dPMax.Value = dPMax.MinDate;

            cbSearchRole.SelectedIndex = 0;
            cbSearchCity.SelectedIndex = 0;

            if (cbSearchDistrict.Items.Count > 0) OtherUtilities.SetUpComboBox(cbSearchDistrict, _listDefault);

            if (cbSearchWard.Items.Count > 0) OtherUtilities.SetUpComboBox(cbSearchWard, _listDefault);
        }
        #endregion

        #region GROUP BOX STAFF INFORMATION
        private void txtbStaffName_Leave(object sender, EventArgs e)
        {
            string name = txtbStaffName.Texts.Trim();
            if (string.IsNullOrEmpty(name))
            {
                ToastManager.ShowToastNotification("Missing Name", "Please enter staff name", "error", _parentForm);
                return;
            }
        }
        private void txtbStaffEmail_Leave(object sender, EventArgs e)
        {
            string email = txtbStaffEmail.Texts.Trim();
            if (string.IsNullOrEmpty(email))
            {
                ToastManager.ShowToastNotification("Missing Email", "Please enter staff email", "error", _parentForm);
                return;
            }
            if (EmployeeBLL.CheckExistedEmail(email))
            {
                ToastManager.ShowToastNotification("Existed Email", "This email existed", "error", _parentForm);
                return;
            }
        }
        private void txtbStaffPhone_Leave(object sender, EventArgs e)
        {
            string phone = txtbStaffPhone.Texts.Trim();
            if (string.IsNullOrEmpty(phone))
            {
                ToastManager.ShowToastNotification("Missing Phone", "Please enter staff phone", "error", _parentForm);
                return;
            }
            if (EmployeeBLL.CheckExistedPhone(phone)) 
            {
                ToastManager.ShowToastNotification("Existed Phone", "This number existed", "error", _parentForm);
                return;
            }
            txtbStaffID.Texts = EmployeeBLL.AutoGenerationID(cbRole.Texts.Trim(), txtbStaffPhone.Texts.Trim());
        }
        private void txtbSalary_Leave(object sender, EventArgs e)
        {
            string salary = txtbSalary.Texts.Trim();
            if (string.IsNullOrEmpty(salary))
            {
                ToastManager.ShowToastNotification("Missing Salary", "Please enter staff salary", "error", _parentForm);
                return;
            }
            if (Convert.ToInt32(salary) < 0)
            {
                ToastManager.ShowToastNotification("Invalid Value", "Salary must greater than 0", "error", _parentForm);
                return;
            }
        }
        private void txtbStreetAddress_Leave(object sender, EventArgs e)
        {
            string streetAddress = txtbStreetAddress.Texts.Trim();
            if (string.IsNullOrEmpty(streetAddress))
            {
                ToastManager.ShowToastNotification("Missing Address", "Please enter staff address", "error", _parentForm);
                return;
            }
        }
        private async void cbCity_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCity.SelectedIndex > 0)
            {
                string cityCode = "";
                if (cbCity.SelectedItem is KeyValuePair<string, string> selectedCity) cityCode = selectedCity.Value;

                if (string.IsNullOrEmpty(cityCode)) return;

                if (cbWard.Items.Count > 0) OtherUtilities.SetUpComboBox(cbWard, _listDefault);

                await OtherUtilities.LoadDistricts(cbDistrict, cityCode);
                
                OnCityLoadFinished?.Invoke(this, EventArgs.Empty);
            }
        }
        private async void cbDistrict_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbDistrict.SelectedIndex > 0)
            {
                string districtCode = "";
                if (cbDistrict.SelectedItem is KeyValuePair<string, string> selectedDistrict) districtCode = selectedDistrict.Value;

                if (string.IsNullOrEmpty(districtCode)) return;
                
                await OtherUtilities.LoadWards(cbWard, districtCode);

                OnDistrictLoadFinished?.Invoke(this, EventArgs.Empty);
            }
        }
        private void cbCity_Leave(object sender, EventArgs e)
        {
            if (cbCity.SelectedIndex > 0) return;
            ToastManager.ShowToastNotification("Missing Staff Living Location", "Please choose staff living city", "error", _parentForm);
            return;
        }
        private void cbDistrict_Leave(object sender, EventArgs e)
        {
            if (cbDistrict.SelectedIndex > 0) return;
            ToastManager.ShowToastNotification("Missing Staff Living Location", "Please choose staff living district", "error", _parentForm);
            return;
        }
        private void cbWard_Leave(object sender, EventArgs e)
        {
            if (cbWard.SelectedIndex > 0) return;
            ToastManager.ShowToastNotification("Missing Staff Living Location", "Please choose staff living ward", "error", _parentForm);
            return;
        }
        private void cbRole_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (_action == 1) txtbStaffID.Texts = EmployeeBLL.AutoGenerationID(cbRole.Texts.Trim(), txtbStaffPhone.Texts.Trim());
        }
        #endregion

        private async Task<string> SendPasswordToMail(string email)
        {
            string tempPassword = new Random().Next(100000, 1000000).ToString();
            using (MailMessage mail = new MailMessage("dev.creation.dream.stu@gmail.com", email, "You’re Hired!", $"Congratulations, you have been hired!\r\nHere is the temporary password for your employee account: {tempPassword}.\r\nPlease keep it confidential. When you start working, make sure to change this password to one of your choice."))
            using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
            {
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("dev.creation.dream.stu@gmail.com", "syvo hstq kade hjah");
                smtp.EnableSsl = true;
                await smtp.SendMailAsync(mail);
                ToastManager.ShowToastNotification("Mail Sent", "Email has been sent to the new employee", "success", this);
            }
            return tempPassword;
        }
    }
}
