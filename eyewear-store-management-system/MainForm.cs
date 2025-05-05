using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAL.Object;
using eyewear_store_management_system.CustomComponents;
using eyewear_store_management_system.Utils;
namespace eyewear_store_management_system
{
    public partial class MainForm : Form
    {
        // Main Panel Size = W: 1362, H: 1002
        public Employee employee;
        private CustomButton? _currentButton;
        private Form _currentForm;
        private bool _isManager;
        public MainForm(Employee e)
        {
            employee = e;
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Bounds = Screen.PrimaryScreen.WorkingArea;
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            labelStaffName.Text = employee.Name;
            labelStaffName.Location = new Point((panel1.Width - labelStaffName.Width)/2,labelStaffName.Location.Y);
            if (employee.Role == "Staff") _isManager = false;
            else _isManager = true;
            AccessControl();
        }
        private void btnExit_Click(object sender, EventArgs e) => Environment.Exit(1);
        private void btnLogout_Click(object sender, EventArgs e)
        {
            _currentButton = null;
            this.Close();
        } 

        private void btnStockManagement_Click(object sender, EventArgs e)
        {
            ActiveButton(sender as CustomButton);
            LoadForm(new StockManagementForm(this));
        }

        private void btnInvoiceManagement_Click(object sender, EventArgs e)
        {
            ActiveButton(sender as CustomButton);
            LoadForm(new InvoiceManagementForm(this));
        }

        private void btnStaffManagement_Click(object sender, EventArgs e)
        {
            ActiveButton(sender as CustomButton);
            LoadForm(new StaffManagementForm(this));
        }

        private void btnRevenueAnalysis_Click(object sender, EventArgs e)
        {
            ActiveButton(sender as CustomButton);
            LoadForm(new RevenueAnalysisForm());
        }
        private void btnVoucherManagement_Click(object sender, EventArgs e)
        {
            ActiveButton(sender as CustomButton);
            LoadForm(new VoucherManagementForm(this));
        }
        public void ActiveButton(CustomButton? btn)
        {
            if (_currentButton != null && btn != _currentButton)
            {
                _currentButton.BackColor = Color.LightGray;
                _currentButton.ForeColor = Color.Black;
            }
            if (btn != null && btn != _currentButton)
            {
                _currentButton = btn;
                _currentButton.BackColor = Color.DimGray;
                _currentButton.ForeColor = Color.White;
            }
        }
        private void LoadForm(object targetForm)
        {
            Form f = targetForm as Form;
            if (_currentForm != null && f.GetType() == _currentForm.GetType()) return;
            if (this.mainPanel.Controls.Count > 0) 
            {
                Form oldForm = this.mainPanel.Controls[0] as Form;
                this.mainPanel.Controls.RemoveAt(0);
                oldForm?.Dispose();
            }
            _currentForm = f!;
            _currentForm.TopLevel = false;
            _currentForm.Dock = DockStyle.Fill;
            this.mainPanel.Controls.Add(_currentForm);
            this.mainPanel.Tag = _currentForm;
            _currentForm.Show();
            FocusExit();
        }
        private void MainForm_Shown(object sender, EventArgs e) => btnInvoiceManagement_Click(btnInvoiceManagement, EventArgs.Empty);
        public void AccessControl()
        {
            btnStaffManagement.Enabled = _isManager;
            btnStaffManagement.Visible = _isManager;

            btnVoucherManagement.Enabled = _isManager;
            btnVoucherManagement.Visible = _isManager;

            btnRevenueAnalysis.Enabled = false;
            btnRevenueAnalysis.Visible = false;
        }
        public void FocusExit()
        {
            labelStaffName.Focus();
        }
    }
}
