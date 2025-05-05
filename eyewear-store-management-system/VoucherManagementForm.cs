using BLL;
using eyewear_store_management_system.Utils;
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

namespace eyewear_store_management_system
{
    public partial class VoucherManagementForm : Form
    {
        private MainForm _parentForm;
        private int _action; // -1: Default | 1: New | 2: Modify
        public VoucherManagementForm(MainForm parent)
        {
            _parentForm = parent;
            InitializeComponent();
            SetUpSearchFilter();
        }

        private void VoucherManagementForm_Load(object sender, EventArgs e)
        {
            _parentForm.FocusExit();
            _action = -1;

            // Data Grid View Voucher List
            dgvVoucherList.DataSource = VoucherBLL.GetAllVouchers();
            dgvVoucherList.Enabled = true;
            dgvVoucherList.Columns[dgvVoucherList.Columns.Count - 1].ReadOnly = true;

            // Group Box Voucher Information
            txtbVoucherID.Texts = "";
            txtbMinimumOrderValue.Texts = "";
            if (btnToggleIsActive.IsOn == true) btnToggleIsActive.Toggle();
            txtbDiscountAmount.Texts = "";
            txtbDiscountMaximumAmount.Texts = "";
            dPStart.Value = dPStart.MinDate;
            dPEnd.Value = dPEnd.MinDate;
            txtbVoucherDescription.Texts = "";
            gBVoucherInformation.Enabled = false;

            // Group box Tools
            OtherUtilities.ButtonEnable(btnNew);
            OtherUtilities.ButtonEnable(btnExport);

            OtherUtilities.ButtonDisable(btnModify);
            OtherUtilities.ButtonDisable(btnRemove);
            OtherUtilities.ButtonDisable(btnSave);
            OtherUtilities.ButtonDisable(btnCancel);

            // Group box Search & Filter
            OtherUtilities.ButtonEnable(btnFind);
            OtherUtilities.ButtonDisable(btnClear);
        }

        #region DATA GRID VIEW ACTION
        private void dgvVoucherList_Click(object sender, EventArgs e)
        {
            DataGridViewRow? row = dgvVoucherList.CurrentRow;
            if (row != null && row.Index != dgvVoucherList.Rows.Count - 1)
            {
                string id = row.Cells["ID"].Value.ToString();
                string description = row.Cells["Description"].Value.ToString();
                int minimumOrderPrice = Convert.ToInt32(row.Cells["Minimum Order Price"].Value);
                int discountAmount = Convert.ToInt32(row.Cells["Discount Amount"].Value);
                int discountMaximumAmount = Convert.ToInt32(row.Cells["Discount Maximum Amount"].Value);
                DateTime startDate = Convert.ToDateTime(row.Cells["Start Date"].Value);
                DateTime endDate = Convert.ToDateTime(row.Cells["End Date"].Value);
                bool isActive = Convert.ToBoolean(row.Cells["Is Active"].Value);

                txtbVoucherID.Texts = id;
                txtbMinimumOrderValue.Texts = minimumOrderPrice.ToString();
                txtbVoucherDescription.Texts = description;
                txtbDiscountAmount.Texts = discountAmount.ToString();
                txtbDiscountMaximumAmount.Texts = discountMaximumAmount.ToString();
                dPStart.Value = startDate;
                dPEnd.Value = endDate;
                if (btnToggleIsActive.IsOn != isActive) btnToggleIsActive.Toggle(); 

                OtherUtilities.ButtonEnable(btnModify);
                OtherUtilities.ButtonEnable(btnRemove);
            }
        }
        #endregion

        #region TOOLS ACTION
        private void btnNew_Click(object sender, EventArgs e)
        {
            _action = 1;

            // Data Grid View Voucher List
            dgvVoucherList.Enabled = false;

            // Group box Product Information
            txtbVoucherID.Texts = "";
            txtbMinimumOrderValue.Texts = "";
            txtbVoucherDescription.Texts = "";
            txtbVoucherID.Focus();
            txtbDiscountAmount.Texts = "";
            txtbDiscountMaximumAmount.Texts = "";
            dPStart.Value = DateTime.Today;
            dPEnd.Value = DateTime.Today;
            if (btnToggleIsActive.IsOn != true) btnToggleIsActive.Toggle();

            gBVoucherInformation.Enabled = true;

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
            dgvVoucherList.Enabled = false;

            // Group box Product Information
            gBVoucherInformation.Enabled = true;
            txtbVoucherID.Focus();

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
                "Are you sure you want to delete this voucher?",
                "Delete Confirmation",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (r != DialogResult.Yes) return;

            try
            {
                bool success = new VoucherBLL(txtbVoucherID.Texts.Trim(), "",0, 0, 0, DateTime.Now, DateTime.Now,true).DeleteVoucher();

                // Turn to default form state 
                VoucherManagementForm_Load(null, EventArgs.Empty);

                if (success == true)
                {
                    ToastManager.ShowToastNotification("Remove Success", "The voucher has been successfully deleted", "success", _parentForm);
                    return;
                }
            }
            catch (Exception ex)
            {
                ToastManager.ShowToastNotification("Remove Fail", ex.Message, "error", this._parentForm);
                return;
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            // Save Logic
            DialogResult r = MessageBox.Show(
                "Are you sure you want to save your changes?",
                "Save Confirmation",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (r != DialogResult.Yes) return;

            string id = txtbVoucherID.Texts.Trim();
            int minimumOrderPrice = Convert.ToInt32(txtbMinimumOrderValue.Texts.Trim());
            int discountAmount = Convert.ToInt32(txtbDiscountAmount.Texts.Trim());
            int discountMaximumAmount = Convert.ToInt32(txtbDiscountMaximumAmount.Texts.Trim());
            DateTime startDate = dPStart.Value;
            DateTime endDate = dPEnd.Value;
            string description = txtbVoucherDescription.Texts.Trim();
            bool isActive = btnToggleIsActive.IsOn;
            MessageBox.Show(description);
            VoucherBLL voucherBLL = new VoucherBLL(id,description,minimumOrderPrice,discountAmount,discountMaximumAmount,startDate,endDate,isActive);

            try
            {
                bool success = false;
                if (_action == 1) success = voucherBLL.AddVoucher();  // Create New Product

                if (_action == 2) success = voucherBLL.UpdateVoucher();  // Update Product Information

                // Turn to default form state 
                VoucherManagementForm_Load(null, EventArgs.Empty);

                if (success)
                {
                    ToastManager.ShowToastNotification($"{(_action == 1 ? "Create" : "Update")} Voucher Succes", $"Voucher has been {(_action == 1 ? "added" : "updated")} successfully", "success", _parentForm);
                    return;
                }
                else
                {
                    ToastManager.ShowToastNotification($"{(_action == 1 ? "Create" : "Update")} Voucher Fail", $"Voucher could not be {(_action == 1 ? "added" : "updated")}", "error", _parentForm);
                    return;
                }
            }
            catch (Exception ex)
            {
                ToastManager.ShowToastNotification($"{(_action == 1 ? "Create" : "Update")} Voucher Fail", ex.Message, "error", _parentForm);
                return;
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            // Turn to default form state
            VoucherManagementForm_Load(null, EventArgs.Empty);
        }
        private void btnExport_Click(object sender, EventArgs e)
        {
            if (dgvVoucherList.Rows.Count > 0)
            {
                DataTable dt = OtherUtilities.GetDataTableFromDGV(dgvVoucherList);
                OtherUtilities.ExportToExcel(dt);
            }
        }
        #endregion

        #region SEARCH ACTION
        private void btnFind_Click(object sender, EventArgs e)
        {
            // Search Logic
            string id = txtbSearchBox.Texts.Trim();
            bool isActive = btnToggleIsActive.IsOn;

            int discountAmountMin = Convert.ToInt32(string.IsNullOrEmpty(txtbMin.Texts.Trim()) ? "-1" : txtbMin.Texts.Trim());
            int discountAmountMax = Convert.ToInt32(string.IsNullOrEmpty(txtbMax.Texts.Trim()) ? "-1" : txtbMax.Texts.Trim());

            int minimumOrderPriceMin = Convert.ToInt32(string.IsNullOrEmpty(txtbMinimumOrderPriceMin.Texts.Trim()) ? "-1" : txtbMinimumOrderPriceMin.Texts.Trim());
            int minimumOrderPriceMax = Convert.ToInt32(string.IsNullOrEmpty(txtbMinimumOrderPriceMax.Texts.Trim()) ? "-1" : txtbMinimumOrderPriceMax.Texts.Trim());

            List<string> conditionList = new List<string>();
            bool searchAvailable = false;

            // Search by ID
            if (!string.IsNullOrEmpty(id))
            {
                conditionList.Add($"ID LIKE '{id}'");
                searchAvailable = true;
            }

            // Search by Is Active
            if (isActive == true)
            {
                conditionList.Add("IsActive = 1");
                searchAvailable = true;
            }
            else
            {
                conditionList.Add("IsActive != 1");
                searchAvailable = true;
            }

            // Search by Discount Amount
            string searchByDiscountAmount = "";
            if (discountAmountMin >= 0 && discountAmountMax < 0) searchByDiscountAmount = $"[Discount Amount] >= {discountAmountMin}";
            else if (discountAmountMin < 0 && discountAmountMax >= 0) searchByDiscountAmount = $"[Discount Amount] <= {discountAmountMax}";
            else if (discountAmountMin >= 0 && discountAmountMax >= 0) searchByDiscountAmount = $"([Discount Amount] BETWEEN {discountAmountMin} AND {discountAmountMax})";

            if (!string.IsNullOrEmpty(searchByDiscountAmount))
            {
                conditionList.Add(searchByDiscountAmount);
                searchAvailable = true;
            }

            // Search by Minimum Order Price
            string searchByMinimumOrderPrice = "";
            if (minimumOrderPriceMin >= 0 && minimumOrderPriceMax < 0) searchByMinimumOrderPrice = $"[Minimum Order Price] >= {minimumOrderPriceMin}";
            else if (minimumOrderPriceMin < 0 && minimumOrderPriceMax >= 0) searchByMinimumOrderPrice = $"[Minimum Order Price] <= {minimumOrderPriceMax}";
            else if (minimumOrderPriceMin >= 0 && minimumOrderPriceMax >= 0) searchByMinimumOrderPrice = $"([Minimum Order Price] BETWEEN {minimumOrderPriceMin} AND {minimumOrderPriceMax})";

            if (!string.IsNullOrEmpty(searchByMinimumOrderPrice))
            {
                conditionList.Add(searchByMinimumOrderPrice);
                searchAvailable = true;
            }

            // Search by Duration of Validity
            string searchByDurantionDate = "";
            if (dPMin.Value > dPMin.MinDate && dPMax.Value == dPMax.MinDate) searchByDurantionDate = $"[End Date] >= '{dPMin.Value:yyyy-MM-dd}'";
            else if (dPMin.Value == dPMin.MinDate && dPMax.Value > dPMax.MinDate) searchByDurantionDate = $"[End Date] <= '{dPMax.Value:yyyy-MM-dd}'";
            else if (dPMin.Value > dPMin.MinDate && dPMax.Value > dPMax.MinDate) searchByDurantionDate = $"([Start Date] >= '{dPMin.Value:yyyy-MM-dd}' AND [End Date] <='{dPMax.Value:yyyy-MM-dd}')";

            if (!string.IsNullOrEmpty(searchByDurantionDate))
            {
                conditionList.Add(searchByDurantionDate);
                searchAvailable = true;
            }

            if (searchAvailable == false) return;

            string query = "SELECT * FROM Voucher";
            query += " WHERE " + string.Join(" AND ", conditionList);

            DataTable result = OtherUtilities.Search(query, _parentForm);
            if (result != null && result.Rows.Count > 0)
            {
                dgvVoucherList.DataSource = result;
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
            VoucherManagementForm_Load(null, EventArgs.Empty);
        }
        private void txtbSearchBox__TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtbSearchBox.Texts)) return;
            if (btnClear.Enabled == false) OtherUtilities.ButtonEnable(btnClear);
        }
        private void txtbMin__TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtbMin.Texts)) return;
            if (btnClear.Enabled == false) OtherUtilities.ButtonEnable(btnClear);
        }
        private void txtbMax__TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtbMax.Texts)) return;
            if (btnClear.Enabled == false) OtherUtilities.ButtonEnable(btnClear);
        }
        private void txtbMinimumOrderPriceMin__TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtbMinimumOrderPriceMin.Texts)) return;
            if (btnClear.Enabled == false) OtherUtilities.ButtonEnable(btnClear);
        }
        private void txtbMinimumOrderPriceMax__TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtbMinimumOrderPriceMax.Texts)) return;
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
        private void btnToggleSearchIsActive_Click(object sender, EventArgs e)
        {
            btnToggleSearchIsActive.Toggle();
            if (btnToggleSearchIsActive.IsOn == false) return;
            if (btnClear.Enabled == false) OtherUtilities.ButtonEnable(btnClear);
        }
        private void btnToggleSearchPercent_Click(object sender, EventArgs e)
        {
            btnToggleSearchPercent.Toggle();
            if (btnToggleSearchPercent.IsOn == false) return;
            if (btnClear.Enabled == false) OtherUtilities.ButtonEnable(btnClear);
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
        private void txtbMin_Leave(object sender, EventArgs e)
        {
            bool unitPercent = btnToggleSearchPercent.IsOn;
            if (string.IsNullOrEmpty(txtbMin.Texts.Trim())) return;
            int discountAmountMin = Convert.ToInt32(txtbMin.Texts.Trim());
            if (unitPercent == true && discountAmountMin > 100)
            {
                ToastManager.ShowToastNotification("Invalid Discount Amount", "The current scale is set to Percent.\nThe percent value exceeds the acceptable limit (0% - 100%)", "error", _parentForm);
                return;
            }

            if (discountAmountMin < 0)
            {
                ToastManager.ShowToastNotification("Invalid Discount Amount", "Discount Amount must be positive number", "error", _parentForm);
                return;
            }
            if (!string.IsNullOrEmpty(txtbMax.Texts.Trim()))
            {
                int discountAmountMax = Convert.ToInt32(txtbMax.Texts.Trim());
                if (unitPercent == true && discountAmountMax > 100)
                {
                    txtbMax.Focus();
                    ToastManager.ShowToastNotification("Invalid Discount Amount", "The current scale is set to Percent.\nThe percent value exceeds the acceptable limit (0% - 100%)", "error", _parentForm);
                    return;
                }

                if (discountAmountMax < discountAmountMin)
                {
                    txtbMin.Texts = discountAmountMax.ToString();
                    txtbMax.Texts = discountAmountMin.ToString();
                }
            }
        }
        private void txtbMax_Leave(object sender, EventArgs e)
        {
            bool unitPercent = btnToggleSearchPercent.IsOn;
            if (string.IsNullOrEmpty(txtbMax.Texts.Trim())) return;
            int discountAmountMax = Convert.ToInt32(txtbMax.Texts.Trim());
            if (unitPercent == true && discountAmountMax > 100)
            {
                ToastManager.ShowToastNotification("Invalid Discount Amount", "The current scale is set to Percent.\nThe percent value exceeds the acceptable limit (0% - 100%)", "error", _parentForm);
                return;
            }

            if (discountAmountMax < 0)
            {
                ToastManager.ShowToastNotification("Invalid Discount Amount", "Discount Amount must be positive number", "error", _parentForm);
                return;
            }
            if (!string.IsNullOrEmpty(txtbMax.Texts.Trim()))
            {
                int discountAmountMin = Convert.ToInt32(txtbMin.Texts.Trim());
                if (unitPercent == true && discountAmountMin > 100)
                {
                    txtbMin.Focus();
                    ToastManager.ShowToastNotification("Invalid Discount Amount", "The current scale is set to Percent.\nThe percent value exceeds the acceptable limit (0% - 100%)", "error", _parentForm);
                    return;
                }

                if (discountAmountMax < discountAmountMin)
                {
                    txtbMin.Texts = discountAmountMax.ToString();
                    txtbMax.Texts = discountAmountMin.ToString();
                }
            }
        }
        private void txtbMinimumOrderPriceMin_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtbMinimumOrderPriceMin.Texts.Trim())) return;
            int orderPriceMin = Convert.ToInt32(txtbMinimumOrderPriceMin.Texts.Trim());
            if (orderPriceMin < 0)
            {
                ToastManager.ShowToastNotification("Invalid Order Price", "Order price must be positive number", "error", _parentForm);
                return;
            }
            if (!string.IsNullOrEmpty(txtbMinimumOrderPriceMax.Texts.Trim()))
            {
                int orderPriceMax = Convert.ToInt32(txtbMinimumOrderPriceMax.Texts.Trim());
                if (orderPriceMax < orderPriceMin)
                {
                    txtbMinimumOrderPriceMin.Texts = orderPriceMax.ToString();
                    txtbMinimumOrderPriceMax.Texts = orderPriceMin.ToString();
                }
            }
        }
        private void txtbMinimumOrderPriceMax_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtbMinimumOrderPriceMax.Texts.Trim())) return;
            int orderPriceMax = Convert.ToInt32(txtbMinimumOrderPriceMax.Texts.Trim());
            if (orderPriceMax < 0)
            {
                ToastManager.ShowToastNotification("Invalid Order Price", "Order price must be positive number", "error", _parentForm);
                return;
            }
            if (!string.IsNullOrEmpty(txtbMinimumOrderPriceMin.Texts.Trim()))
            {
                int orderPriceMin = Convert.ToInt32(txtbMinimumOrderPriceMin.Texts.Trim());
                if (orderPriceMax < orderPriceMin)
                {
                    txtbMinimumOrderPriceMin.Texts = orderPriceMax.ToString();
                    txtbMinimumOrderPriceMax.Texts = orderPriceMin.ToString();
                }
            }
        }
        private void SetUpSearchFilter()
        {
            txtbSearchBox.Texts = "";
            if (btnToggleSearchIsActive.IsOn == true) btnToggleSearchIsActive.Toggle();
            if (btnToggleSearchPercent.IsOn == true) btnToggleSearchPercent.Toggle();
            dPMin.Value = dPMin.MinDate;
            dPMax.Value = dPMax.MinDate;
            txtbMin.Texts = "";
            txtbMax.Texts = "";
            txtbMinimumOrderPriceMin.Texts = "";
            txtbMinimumOrderPriceMax.Texts = "";
        }
        #endregion

        #region GROUP BOX VOUCHER ACTION
        private void btnToggleIsActive_Click(object sender, EventArgs e)
        {
            btnToggleIsActive.Toggle();
        }
        private void dPStart_Leave(object sender, EventArgs e)
        {
            if (dPStart.Value == dPStart.MinDate) return;
            if (dPEnd.Value == dPEnd.MinDate) return;
            DateTime fromDate = dPStart.Value;
            DateTime toDate = dPEnd.Value;
            if (fromDate > toDate)
            {
                dPStart.Value = toDate;
                dPEnd.Value = fromDate;
            }
        }
        private void dPEnd_Leave(object sender, EventArgs e)
        {
            if (dPEnd.Value == dPEnd.MinDate) return;
            if (dPStart.Value == dPStart.MinDate) return;
            DateTime fromDate = dPStart.Value;
            DateTime toDate = dPEnd.Value;
            if (fromDate > toDate)
            {
                dPStart.Value = toDate;
                dPEnd.Value = fromDate;
            }
        }
        private void txtbDiscountAmount_Leave(object sender, EventArgs e)
        {
            string discountAmount = txtbDiscountAmount.Texts;
            if (string.IsNullOrEmpty(discountAmount))
            {
                ToastManager.ShowToastNotification("Missing Discount Amount", "Please enter the discount amount you want", "error", _parentForm);
                return;
            }
            if (Convert.ToInt32(discountAmount) < 0)
            {
                ToastManager.ShowToastNotification("Invalid Discount Amount", "Discount amount must greater than 0", "error", _parentForm);
                return;
            }
        }
        private void txtbDiscountMaximumAmount_Leave(object sender, EventArgs e)
        {
            string discountMaximumAmount = txtbDiscountMaximumAmount.Texts;
            if (string.IsNullOrEmpty(discountMaximumAmount))
            {
                ToastManager.ShowToastNotification("Missing Discount Maximum Amount", "Please enter the discount maximum amount you want", "error", _parentForm);
                return;
            }
            if (Convert.ToInt32(discountMaximumAmount) < 0)
            {
                ToastManager.ShowToastNotification("Invalid Discount Maximum Amount", "Discount maximum amount must greater than 0", "error", _parentForm);
                return;
            }
        }
        #endregion
    }
}
