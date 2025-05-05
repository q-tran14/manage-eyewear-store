using BLL;
using System;
using System.Diagnostics;
using eyewear_store_management_system.Utils;
using System.Xml.Linq;
using System.Data;

namespace eyewear_store_management_system
{
    public partial class StockManagementForm : Form
    {
        private MainForm _parentForm;
        private int _action; // -1: Default | 1: New | 2: Modify
        public StockManagementForm(MainForm parent)
        {
            _parentForm = parent;
            InitializeComponent();
            SetUpSearchFilter();
        }
        private void StockManagementForm_Load(object sender, EventArgs e)
        {
            _parentForm.FocusExit();
            _action = -1;

            // Data Grid View Product List
            dgvProductList.DataSource = ProductBLL.GetAllProducts();
            dgvProductList.Enabled = true;

            // Group Box Product Information
            txtbProductID.Texts = "";
            txtbProductName.Texts = "";
            txtbPrice.Texts = "";
            txtbEntryQuantity.Texts = "";
            txtbQuantity.Texts = "";
            dPEntryDate.Value = dPEntryDate.MinDate;

            gBProductInformation.Enabled = false;
            
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
        }

        #region DATA GRID VIEW ACTION
        private void dgvProductList_Click(object sender, EventArgs e)
        {
            DataGridViewRow? row = dgvProductList.CurrentRow;
            if (row != null && row.Index != dgvProductList.Rows.Count - 1)
            {
                string id = row.Cells["ID"].Value.ToString();
                string name = row.Cells["Name"].Value.ToString();
                int price = Convert.ToInt32(row.Cells["Price"].Value);
                int quantity = Convert.ToInt32(row.Cells["Quantity"].Value);
                int stockEntryQuantity = Convert.ToInt32(row.Cells["Stock Entry Quantity"].Value);
                DateTime entryDate = Convert.ToDateTime(row.Cells["Stock Entry Date"].Value);

                txtbProductID.Texts = id;
                txtbProductName.Texts = name;
                txtbPrice.Texts = price.ToString();
                txtbQuantity.Texts = quantity.ToString();
                txtbEntryQuantity.Texts = stockEntryQuantity.ToString();
                dPEntryDate.Value = entryDate;

                OtherUtilities.ButtonEnable(btnModify);
                OtherUtilities.ButtonEnable(btnRemove);
            }
        }
        #endregion

        #region TOOLS ACTION
        private void btnNew_Click(object sender, EventArgs e)
        {
            _action = 1;

            // Data Grid View Product List
            dgvProductList.Enabled = false;

            // Group box Product Information
            txtbProductID.Texts = ProductBLL.GenerateProductID();
            txtbProductName.Texts = "";
            txtbProductName.Focus();
            txtbPrice.Texts = "";
            txtbQuantity.Texts = "";
            txtbEntryQuantity.Texts = "";
            dPEntryDate.Value = DateTime.Today;

            gBProductInformation.Enabled = true;
            txtbProductID.Enabled = false;
            dPEntryDate.Enabled = false;
            txtbQuantity.Enabled = false;
            txtbEntryQuantity.Enabled = true;

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
            dgvProductList.Enabled = false;

            // Group box Product Information
            gBProductInformation.Enabled = true;
            txtbProductName.Focus();
            txtbProductID.Enabled = false;
            dPEntryDate.Enabled = false;
            txtbEntryQuantity.Enabled = false;
            txtbQuantity.Enabled = true;

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
                "Are you sure you want to delete this product?",
                "Delete Confirmation",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (r != DialogResult.Yes) return;

            try
            {
                bool success = new ProductBLL(txtbProductID.Texts.Trim(), "", 0, 0, 0, DateTime.Now).DeleteProduct();

                // Turn to default form state 
                StockManagementForm_Load(null, EventArgs.Empty);

                if (success == true)
                {
                    ToastManager.ShowToastNotification("Remove Success", "The product has been successfully deleted", "success", _parentForm);
                    return;
                }
            }catch (Exception ex)
            {
                ToastManager.ShowToastNotification("Remove Fail", ex.Message, "error", this._parentForm);
                return;
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            // Turn to default form state 
            StockManagementForm_Load(null, EventArgs.Empty);
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

            string id = txtbProductID.Texts.Trim();
            string name = txtbProductName.Texts.Trim();
            int currentQuantity = Convert.ToInt32(txtbQuantity.Texts.Trim());
            int entryQuantity = Convert.ToInt32(txtbEntryQuantity.Texts.Trim());
            int price = Convert.ToInt32(txtbPrice.Texts.Trim());
            DateTime entryDate = dPEntryDate.Value;
            ProductBLL productBLL = new ProductBLL(id,name,price,currentQuantity,entryQuantity,entryDate);

            try
            {
                bool success = false;
                if (_action == 1) success = productBLL.AddProduct();  // Create New Product

                if (_action == 2) success = productBLL.UpdateProduct();  // Update Product Information

                // Turn to default form state 
                StockManagementForm_Load(null, EventArgs.Empty);

                if (success) 
                {
                    ToastManager.ShowToastNotification($"{(_action == 1 ? "Create" : "Update")} Product Succes", $"Product has been {(_action == 1 ? "added" : "updated")} successfully", "success", _parentForm);
                    return;
                } 
                else
                {
                    ToastManager.ShowToastNotification($"{(_action == 1 ? "Create" : "Update")} Product Fail", $"Product could not be {(_action == 1 ? "added" : "updated")}", "error", _parentForm);
                    return;
                }
            }
            catch (Exception ex)
            {
                ToastManager.ShowToastNotification($"{(_action == 1 ? "Create" : "Update")} Product Fail", ex.Message,"error", _parentForm);
                return;
            }
        }
        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (dgvProductList.Rows.Count > 0)
            {
                DataTable dt = OtherUtilities.GetDataTableFromDGV(dgvProductList);
                OtherUtilities.ExportToExcel(dt);
            }
        }
        #endregion

        #region SEARCH ACTION
        private void btnFind_Click(object sender, EventArgs e)
        {
            // Search Logic
            string idOrName = txtbSearchBox.Texts.Trim();
            bool soldOut = btnToggleSoldOut.IsOn;

            bool importQuantity = btnToggleImportQuantity.IsOn;

            int priceMin = Convert.ToInt32(string.IsNullOrEmpty(txtbPriceMin.Texts.Trim()) ? "-1" : txtbPriceMin.Texts.Trim());
            int priceMax = Convert.ToInt32(string.IsNullOrEmpty(txtbPriceMax.Texts.Trim()) ? "-1" : txtbPriceMax.Texts.Trim());
            int quantityMin = Convert.ToInt32(string.IsNullOrEmpty(txtbQuantityMin.Texts.Trim()) ? "-1" : txtbQuantityMin.Texts.Trim());
            int quantityMax = Convert.ToInt32(string.IsNullOrEmpty(txtbQuantityMax.Texts.Trim()) ? "-1" : txtbQuantityMax.Texts.Trim());

            List<string> conditionList = new List<string>();
            bool searchAvailable = false;

            // Search by ID or Name
            if (!string.IsNullOrEmpty(idOrName))
            {
                string columnName = (idOrName.StartsWith('P') && idOrName.Length < 5) ? "ID" : "Name";
                if (columnName == "ID") conditionList.Add($"{columnName} LIKE '{idOrName}'");
                else conditionList.Add($"{columnName} LIKE N'%{idOrName}%'");
                searchAvailable = true;
            }

            // Search by Sold Out Date
            if (soldOut == true) 
            { 
                conditionList.Add("SoldOutDate != NULL");
                searchAvailable = true;
            }

            // Search by entry date
            string searchByEntryDate = "";
            if (dPMin.Value > dPMin.MinDate && dPMax.Value == dPMax.MinDate) searchByEntryDate = $"[Stock Entry Date] >= '{dPMin.Value:yyyy-MM-dd}'";
            else if (dPMin.Value == dPMin.MinDate && dPMax.Value > dPMax.MinDate) searchByEntryDate = $"[Stock Entry Date] <= '{dPMax.Value:yyyy-MM-dd}'";
            else if (dPMin.Value > dPMin.MinDate && dPMax.Value > dPMax.MinDate) searchByEntryDate = $"([Stock Entry Date] BETWEEN '{dPMin.Value:yyyy-MM-dd}' AND '{dPMax.Value:yyyy-MM-dd}')";

            if (!string.IsNullOrEmpty(searchByEntryDate))
            {
                conditionList.Add(searchByEntryDate);
                searchAvailable = true;
            }

            // Search by price
            string searchByPrice = "";
            if (priceMin >= 0 && priceMax < 0) searchByPrice = $"Price >= {priceMin}";
            else if (priceMin < 0 && priceMax >= 0) searchByPrice = $"Price <= {priceMax}";
            else if (priceMin >= 0 && priceMax >= 0) searchByPrice = $"(Price BETWEEN {priceMin} AND {priceMax})";

            if (!string.IsNullOrEmpty(searchByPrice))
            { 
                conditionList.Add(searchByPrice);
                searchAvailable = true;
            }

            // Search by quantity
            if (quantityMin > 0 || quantityMax > 0)
            {
                string columnQuantity = "";
                if (btnToggleImportQuantity.IsOn == true) columnQuantity = "[Stock Entry Quantity]";
                else columnQuantity = "Quantity";

                string condition = "";
                if (quantityMin >= 0 && quantityMax < 0) condition = $"{columnQuantity} >= {quantityMin}";
                else if (quantityMin < 0 && quantityMax >= 0) condition = $"{columnQuantity} <= {quantityMax}";
                else if (quantityMin >= 0 && quantityMax >= 0) condition = $"({columnQuantity} BEWTEEN {quantityMin} AND {quantityMin})";
                conditionList.Add(condition);
                searchAvailable = true;
            }

            if (searchAvailable == false) return;

            string query = "SELECT * FROM Product";
            query += " WHERE " + string.Join(" AND ", conditionList);

            DataTable result = OtherUtilities.Search(query, _parentForm);
            if (result != null && result.Rows.Count > 0)
            {
                dgvProductList.DataSource = result;
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
            StockManagementForm_Load(null, EventArgs.Empty);
        }
        private void txtbSearchBox__TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtbSearchBox.Texts)) return;
            if (btnClear.Enabled == false) OtherUtilities.ButtonEnable(btnClear);
        }
        private void dPMin_ValueChanged(object sender, EventArgs e)
        {
            if (dPMin.Value == dPMin.MinDate) return;
            if (btnClear.Enabled == false) OtherUtilities.ButtonEnable(btnClear);
        }
        private void dPMax_ValueChanged(object sender, EventArgs e)
        {
            if (dPMax.Value == dPMin.MinDate) return;
            if (btnClear.Enabled == false) OtherUtilities.ButtonEnable(btnClear);
        }
        private void txtbQuantityMin__TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtbQuantityMin.Texts)) return;
            if (btnClear.Enabled == false) OtherUtilities.ButtonEnable(btnClear);
        }
        private void txtbQuantityMax__TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtbQuantityMax.Texts)) return;
            if (btnClear.Enabled == false) OtherUtilities.ButtonEnable(btnClear);
        }
        private void txtbPriceMax__TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtbPriceMax.Texts)) return;
            if (btnClear.Enabled == false) OtherUtilities.ButtonEnable(btnClear);
        }
        private void txtbPriceMin__TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtbPriceMin.Texts)) return;
            if (btnClear.Enabled == false) OtherUtilities.ButtonEnable(btnClear);
        }
        private void btnToggleImportQuantity_Click(object sender, EventArgs e)
        {
            btnToggleImportQuantity.Toggle();
            if (btnToggleImportQuantity.IsOn == false) return;
            if (btnClear.Enabled == false) OtherUtilities.ButtonEnable(btnClear);
        }
        private void btnToggleSoldOut_Click(object sender, EventArgs e)
        {
            btnToggleSoldOut.Toggle();
            if (btnToggleSoldOut.IsOn == false) return;
            if (btnClear.Enabled == false) OtherUtilities.ButtonEnable(btnClear);
        }
        private void txtbPriceMin_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtbPriceMin.Texts)) return;
            int priceMin = Convert.ToInt32(txtbPriceMin.Texts.Trim());
            if (priceMin < 0)
            {
                ToastManager.ShowToastNotification("Invalid Price", "Price must be positive number", "error", _parentForm);
                return;
            }
            if (!string.IsNullOrEmpty(txtbPriceMax.Texts.Trim()))
            {
                int priceMax = Convert.ToInt32(txtbPriceMax.Texts.Trim());
                if (priceMax < priceMin)
                {
                    txtbPriceMin.Texts = priceMax.ToString();
                    txtbPriceMax.Texts = priceMin.ToString();
                }
            }
        }
        private void txtbPriceMax_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtbPriceMax.Texts)) return;
            int priceMax = Convert.ToInt32(txtbPriceMax.Texts.Trim());
            if (priceMax < 0)
            {
                ToastManager.ShowToastNotification("Invalid Price", "Price must be positive number", "error", _parentForm);
                return;
            }
            if (!string.IsNullOrEmpty(txtbPriceMin.Texts.Trim()))
            {
                int priceMin = Convert.ToInt32(txtbPriceMin.Texts.Trim());
                if (priceMax < priceMin)
                {
                    txtbPriceMin.Texts = priceMax.ToString();
                    txtbPriceMax.Texts = priceMin.ToString();
                }
            }
        }
        private void txtbQuantityMin_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtbQuantityMin.Texts)) return;
            int quantityMin = Convert.ToInt32(txtbQuantityMin.Texts.Trim());
            if (quantityMin < 0)
            {
                ToastManager.ShowToastNotification("Invalid Quantity", "Quantity must be positive number", "error", _parentForm);
                return;
            }
            if (!string.IsNullOrEmpty(txtbQuantityMax.Texts.Trim()))
            {
                int quantityMax = Convert.ToInt32(txtbQuantityMax.Texts.Trim());
                if (quantityMax < quantityMin)
                {
                    txtbQuantityMin.Texts = quantityMax.ToString();
                    txtbQuantityMax.Texts = quantityMin.ToString();
                }
            }
        }
        private void txtbQuantityMax_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtbQuantityMax.Texts)) return;
            int quantityMax = Convert.ToInt32(txtbQuantityMax.Texts.Trim());
            if (quantityMax < 0)
            {
                ToastManager.ShowToastNotification("Invalid Quantity", "Quantity must be positive number", "error", _parentForm);
                return;
            }
            if (!string.IsNullOrEmpty(txtbQuantityMin.Texts.Trim()))
            {
                int quantityMin = Convert.ToInt32(txtbQuantityMin.Texts.Trim());
                if (quantityMax < quantityMin)
                {
                    txtbQuantityMin.Texts = quantityMax.ToString();
                    txtbQuantityMax.Texts = quantityMin.ToString();
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
        private void SetUpSearchFilter()
        {
            txtbSearchBox.Texts = "";
            if (btnToggleSoldOut.IsOn == true) btnToggleSoldOut.Toggle();
            if (btnToggleImportQuantity.IsOn == true) btnToggleImportQuantity.Toggle();
            dPMin.Value = dPMin.MinDate;
            dPMax.Value = dPMax.MinDate;
            txtbPriceMin.Texts = "";
            txtbPriceMax.Texts = "";
            txtbQuantityMin.Texts = "";
            txtbQuantityMax.Texts = "";
        }
        #endregion

        #region GROUP BOX PRODUCT INFORMATION
        private void txtbQuantity_Leave(object sender, EventArgs e)
        {
            if (_action != 2) return;
            string quantity = txtbQuantity.Texts;
            if (string.IsNullOrEmpty(quantity))
            {
                ToastManager.ShowToastNotification("Missing Quantity", "Please enter the quantity you want", "error",_parentForm);
                return;
            }
            if (Convert.ToInt32(quantity) < 0)
            {
                ToastManager.ShowToastNotification("Invalid Value", "Quantity must greater than 0", "error", _parentForm);
                return;
            }
            txtbEntryQuantity.Texts = quantity;
            dPEntryDate.Value = DateTime.Today;
        }
        private void txtbEntryQuantity_Leave(object sender, EventArgs e)
        {
            if (_action != 1) return;
            string entryQuantity = txtbEntryQuantity.Texts;
            if (string.IsNullOrEmpty(entryQuantity))
            {
                ToastManager.ShowToastNotification("Missing Entry Quantity", "Please enter the entry quantity you want", "error", _parentForm);
                return;
            }
            if (Convert.ToInt32(entryQuantity) < 0)
            {
                ToastManager.ShowToastNotification("Invalid Value", "Entry quantity must greater than 0", "error", _parentForm);
                return;
            }
            txtbQuantity.Texts = entryQuantity;
        }
        private void txtbPrice_Leave(object sender, EventArgs e)
        {
            string price = txtbPrice.Texts;
            if (string.IsNullOrEmpty(price))
            {
                ToastManager.ShowToastNotification("Missing Price", "Please enter product price you want", "error", _parentForm);
                return;
            }
            if (Convert.ToInt32(price) < 0)
            {
                ToastManager.ShowToastNotification("Invalid Value", "Price must greater than 0", "error", _parentForm);
                return;
            }
        }
        private void txtbProductName_Leave(object sender, EventArgs e)
        {
            string name = txtbProductName.Texts;
            if (string.IsNullOrEmpty(name))
            {
                ToastManager.ShowToastNotification("Missing Name", "Please enter product name you want", "error", _parentForm);
                return;
            }
        }
        #endregion
    }
}
