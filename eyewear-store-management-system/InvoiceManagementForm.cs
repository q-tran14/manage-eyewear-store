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
using BLL;
using DAL;
using DAL.Object;
using eyewear_store_management_system.Utils;

namespace eyewear_store_management_system
{
    public partial class InvoiceManagementForm : Form
    {
        private List<KeyValuePair<string, char>> _listType = new List<KeyValuePair<string, char>>{
                                                                new KeyValuePair<string, char>("Option", 'O'),
                                                                new KeyValuePair<string, char>("Sale", 'S'),
                                                                new KeyValuePair<string, char>("Warranty", 'W'),
                                                                new KeyValuePair<string, char>("Return", 'R')
                                                            };

        private List<KeyValuePair<string, string>> _listWorkShift = new List<KeyValuePair<string, string>>()
                                                                    {
                                                                        new KeyValuePair<string, string>("Shift 1 - 8:30 AM to 1:00 PM", "8:30 AM to 1:00 PM"),
                                                                        new KeyValuePair<string, string>("Shift 2 - 1:00 PM to 4:30 PM", "1:00 PM to 4:30 PM"),
                                                                        new KeyValuePair<string, string>("Shift 3 - 4:30 PM to 9:30 PM", "4:30 PM to 9:30 PM"),
                                                                        new KeyValuePair<string, string>("Shift 4 - After Hours", "Outside Working Hours"),
                                                                    };

        private List<KeyValuePair<string, string>> _listSearchWorkShift = new List<KeyValuePair<string, string>>()
                                                                    {
                                                                        new KeyValuePair<string, string>("Option", ""),
                                                                        new KeyValuePair<string, string>("Shift 1", "8:30 AM to 1:00 PM"),
                                                                        new KeyValuePair<string, string>("Shift 2", "1:00 PM to 4:30 PM"),
                                                                        new KeyValuePair<string, string>("Shift 3", "4:30 PM to 9:30 PM"),
                                                                        new KeyValuePair<string, string>("Shift 4", "Outside Working Hours"),
                                                                    };
        private MainForm _parentForm;
        
        private int _action = -1; // -1: Default | 1: New | 2: Update 
        
        private Invoice? _invoice;
        
        public InvoiceManagementForm(MainForm parent)
        {
            InitializeComponent();
            _parentForm = parent;
            OtherUtilities.SetUpComboBox(cbInvoiceType, _listType);
            OtherUtilities.SetUpComboBox(cbWorkShift, _listWorkShift);
            OtherUtilities.SetUpComboBox(cbSearchInvoiceType, _listType);
            OtherUtilities.SetUpComboBox(cbSearchShift, _listSearchWorkShift);
        }

        private void InvoiceManagementForm_Load(object sender, EventArgs e)
        {
            _parentForm.FocusExit();
            _action = -1;
            _invoice = null;

            // Data Grid View Invoice List
            dgvInvoiceList.DataSource = InvoiceBLL.GetAllInvoices();
            dgvInvoiceList.Enabled = true;

            // Data Grid View Product List
            dgvProductList.DataSource = null;
            dgvProductList.Rows.Clear();
            dgvProductList.Columns.Clear();

            // Group Box Invoice Information
            txtbInvoiceID.Texts = "";
            OtherUtilities.SetUpComboBox(cbCustomerName, GetAllCustomer());
            txtbCustomerPhone.Texts = "";
            OtherUtilities.SetUpComboBox(cbStaffName, GetAllEmployee());
            cbWorkShift.SelectedIndex = 0;
            cbInvoiceType.SelectedIndex = 0;
            txtbVoucher.Texts = "";
            txtbTotalBill.Texts = "";
            labelVoucherDescription.Visible = false;

            gBInvoiceInformation.Enabled = false;

            // Group Box Add to Invoice
            txtbProductID.Texts = "";
            OtherUtilities.SetUpComboBox(cbProductName, GetAllProduct());
            txtbUnitPrice.Texts = "";
            txtbTotalPrice.Texts = "";
            txtbQuanity.Texts = "";
            labelAvailableStockTitle.Visible = false;
            labelAvailableStock.Visible = false;

            gBAddToInvoice.Enabled = false;

            OtherUtilities.ButtonDisable(btnAddProduct);
            OtherUtilities.ButtonDisable(btnRemoveProduct);

            // Group box Tools
            OtherUtilities.ButtonEnable(btnNewInvoice);

            OtherUtilities.ButtonDisable(btnPrintInvoice);
            OtherUtilities.ButtonDisable(btnModifyInvoice);
            OtherUtilities.ButtonDisable(btnSaveInvoice);
            OtherUtilities.ButtonDisable(btnCancelInvoice);

            // Group box Search & Filter
            SetUpSearchGroupBox();
        }

        #region DATA GRID VIEW ACTION
        private void dgvInvoiceList_Click(object sender, EventArgs e)
        {
            DataGridViewRow? row = dgvInvoiceList.CurrentRow;
            if (row != null && row.Index != dgvInvoiceList.Rows.Count - 1)
            {
                string id = row.Cells["ID"].Value.ToString();
                string customerName = row.Cells["Customer Name"].Value.ToString();
                string employeeName = row.Cells["Employee Name"].Value.ToString();

                string voucherId = "";
                if (row.Cells["VoucherID"].Value != null && !string.IsNullOrWhiteSpace(row.Cells["VoucherID"].Value.ToString()))
                {
                    voucherId = row.Cells["VoucherID"].Value.ToString();
                }
                
                char invoiceType = Convert.ToChar(row.Cells["Invoice Type"].Value);
                int totalAmount = Convert.ToInt32(row.Cells["Total Amount"].Value);

                txtbInvoiceID.Texts = id;
                cbCustomerName.SelectedIndex = cbCustomerName.Items.Cast<KeyValuePair<string, Customer>>().ToList().FindIndex(item => item.Value != null && item.Key == customerName);
                cbStaffName.SelectedIndex = cbStaffName.Items.Cast<KeyValuePair<string, Employee>>().ToList().FindIndex(item => item.Value != null && item.Key == employeeName);
                
                if (!string.IsNullOrEmpty(voucherId))
                {
                    Voucher voucher = new VoucherBLL(voucherId, "", 0, 0, 0, DateTime.Now, DateTime.Now, true).GetVoucherByID();
                    txtbVoucher.Texts = voucher.ID;
                    txtbVoucher.Tag = voucher;
                    labelVoucherDescription.Text = voucher.Description;
                    labelVoucherDescription.Visible = true;
                } else
                {
                    txtbVoucher.Texts = "";
                    txtbVoucher.Tag = null;
                    labelVoucherDescription.Text = "";
                    labelVoucherDescription.Visible = false;
                }
                cbInvoiceType.SelectedIndex = cbInvoiceType.Items.Cast<KeyValuePair<string, char>>().ToList().FindIndex(item => item.Value != null && item.Value == invoiceType);

                txtbTotalBill.Texts = totalAmount.ToString();

                LoadDataGridViewProductList(id);

                OtherUtilities.ButtonEnable(btnModifyInvoice);
                OtherUtilities.ButtonEnable(btnPrintInvoice);
                OtherUtilities.ButtonEnable(btnCancelInvoice);
            }
        }

        private void dgvProductList_Click(object sender, EventArgs e)
        {
            DataGridViewRow? row = dgvProductList.CurrentRow;
            if (row != null && row.Index != dgvProductList.Rows.Count - 1)
            {
                string id = row.Cells["ID"].Value.ToString();
                string name = row.Cells["Product Name"].Value.ToString();
                int quanity = Convert.ToInt32(row.Cells["Quantity"].Value);
                int unitPrice = Convert.ToInt32(row.Cells["Unit Price"].Value);
                int totalPrice = Convert.ToInt32(row.Cells["Total Price"].Value);

                cbProductName.SelectedIndex = cbProductName.Items.Cast<KeyValuePair<string, Product>>().ToList().FindIndex(item => item.Value != null && item.Value.Name == name);
                txtbProductID.Texts = ((KeyValuePair<string, Product>)cbProductName.SelectedItem).Value.ID;
                if (cbProductName.SelectedIndex > 0) labelAvailableStock.Text = ((KeyValuePair<string, Product>)cbProductName.SelectedItem).Value.Quantity.ToString();
                txtbUnitPrice.Texts = unitPrice.ToString();
                txtbQuanity.Texts = quanity.ToString();
                txtbTotalPrice.Texts = totalPrice.ToString();

                if (_action != -1)
                {
                    labelAvailableStockTitle.Visible = true;
                    labelAvailableStock.Visible = true;
                    OtherUtilities.ButtonEnable(btnRemoveProduct);
                }
                else
                {
                    labelAvailableStockTitle.Visible = false;
                    labelAvailableStock.Visible = false;
                    OtherUtilities.ButtonDisable(btnRemoveProduct);
                    OtherUtilities.ButtonDisable(btnAddProduct);
                }
            }
        }
        #endregion

        #region TOOLS ACTION
        private void btnNewInvoice_Click(object sender, EventArgs e)
        {
            _action = 1;

            cbInvoiceType.SelectedIndex = 1;
            char invoiceType = Convert.ToChar(((KeyValuePair<string, char>)cbInvoiceType.SelectedItem).Value);
            DateTime invoiceDate = DateTime.Now;
            cbStaffName.SelectedIndex = cbStaffName.Items.Cast<KeyValuePair<string, Employee>>().ToList().FindIndex(item => item.Value != null && item.Value.ID == _parentForm.employee.ID);
            Employee employee = ((KeyValuePair<string, Employee>)cbStaffName.SelectedItem).Value;

            CheckShift();
            int shift = cbWorkShift.SelectedIndex + 1;
            string id = InvoiceBLL.AutoGenerateInvoiceID(invoiceType, invoiceDate, shift, employee.ID);
            _invoice = new Invoice(id,invoiceDate,invoiceType,0,"",employee.ID,null,null,employee,null,new List<InvoiceLine>());
            
            // Data Grid View Invoice List
            dgvInvoiceList.Enabled = false;

            // Data Grid View Product List
            LoadDataGridViewProductList(id);
            dgvInvoiceList.Enabled = true;

            // Group Box Invoice Information
            txtbInvoiceID.Texts = _invoice.ID;
            cbCustomerName.SelectedIndex = 0;
            cbCustomerName.Focus();
            
            txtbCustomerPhone.Texts = "";
            txtbVoucher.Texts = "";
            labelVoucherDescription.Visible = false;
            txtbTotalBill.Texts = "";

            gBInvoiceInformation.Enabled = true;
            txtbInvoiceID.Enabled = false;
            txtbCustomerPhone.Enabled = false;
            cbStaffName.Enabled = false;
            cbWorkShift.Enabled = false;
            txtbTotalBill.Texts = "";

            // Group Box Add to Invoice
            txtbProductID.Texts = "";
            cbProductName.SelectedIndex = 0;
            txtbUnitPrice.Texts = "";
            txtbTotalPrice.Texts = "";
            txtbQuanity.Texts = "";

            gBAddToInvoice.Enabled = true;
            txtbProductID.Enabled = false;
            txtbUnitPrice.Enabled = false;
            txtbTotalPrice.Enabled = false;
            labelAvailableStockTitle.Visible = false;
            labelAvailableStock.Visible = false;

            // Group Box Tools
            OtherUtilities.ButtonEnable(btnSaveInvoice);
            OtherUtilities.ButtonEnable(btnCancelInvoice);

            OtherUtilities.ButtonDisable(btnNewInvoice);
            OtherUtilities.ButtonDisable(btnModifyInvoice);
            OtherUtilities.ButtonDisable(btnPrintInvoice);
        }
        private void btnModifyInvoice_Click(object sender, EventArgs e)
        {
            _action = 2;

            _invoice = new InvoiceBLL(txtbInvoiceID.Texts.ToString(),DateTime.Now,'S',0,"","","").GetInvoiceByID();

            // Data Grid View Invoice List
            dgvInvoiceList.Enabled = false;

            // Data Grid View Product List
            dgvProductList.Enabled = false;

            // Group Box Invoice Information
            gBInvoiceInformation.Enabled = true;
            txtbVoucher.Enabled = true;
            txtbInvoiceID.Enabled = false;
            cbCustomerName.Enabled = false;
            txtbCustomerPhone.Enabled = false;
            cbStaffName.Enabled = false;
            cbWorkShift.Enabled = false;
            cbInvoiceType.Enabled = false;
            txtbTotalBill.Enabled = false;

            // Group Box Add to Invoice
            gBAddToInvoice.Enabled = true;
            cbProductName.Enabled = true;
            txtbQuanity.Enabled = true;

            txtbProductID.Enabled = false;
            txtbUnitPrice.Enabled = false;
            txtbTotalPrice.Enabled = false;

            // Group Box Tools
            OtherUtilities.ButtonEnable(btnSaveInvoice);
            OtherUtilities.ButtonEnable(btnCancelInvoice);

            OtherUtilities.ButtonDisable(btnNewInvoice);
            OtherUtilities.ButtonDisable(btnModifyInvoice);
            OtherUtilities.ButtonDisable(btnPrintInvoice);
        }
        private void btnCancelInvoice_Click(object sender, EventArgs e)
        {
            // Turn to default form state 
            InvoiceManagementForm_Load(null, EventArgs.Empty);
        }
        private void btnSaveInvoice_Click(object sender, EventArgs e)
        {
            // Save Logic
            DialogResult r = MessageBox.Show(
                "Are you sure you want to save your changes?",
                "Save Confirmation",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (r != DialogResult.Yes) return;
            Customer customer = _invoice.Customer;
            if (cbCustomerName.Texts.Contains("(NEW)")) new CustomerBLL(customer.ID,customer.Name,customer.Phone).AddCustomer();

            InvoiceBLL invoiceBLL = new InvoiceBLL(_invoice.ID,_invoice.CreateDate,_invoice.InvoiceType,_invoice.TotalAmount,_invoice.CustomerID,_invoice.EmployeeID,_invoice.VoucherID);

            List<InvoiceLineBLL> invoiceLinesBLL = new List<InvoiceLineBLL>();
            foreach (InvoiceLine i in _invoice.InvoiceLines)
            {
                InvoiceLineBLL invoiceLineBLL = new InvoiceLineBLL(i.ID,i.InvoiceID,i.ProductID,i.Quantity,i.UnitPrice);
                invoiceLinesBLL.Add(invoiceLineBLL);
            }

            try
            {
                bool success = false;
                if (_action == 1) // Create New Product
                {
                    success = invoiceBLL.AddInvoice();
                    foreach (InvoiceLineBLL i in invoiceLinesBLL) 
                    {
                        i.AddInvoiceLine(); 
                    }
                }

                if (_action == 2) // Update Product Information
                {
                    success = invoiceBLL.UpdateInvoice();
                    foreach (InvoiceLineBLL i in invoiceLinesBLL) 
                    {
                        i.UpdateInvoiceLine(); 
                    }
                }  

                // Turn to default form state 
                InvoiceManagementForm_Load(null, EventArgs.Empty);

                if (success)
                {
                    ToastManager.ShowToastNotification($"{(_action == 1 ? "Create" : "Update")} Invoice Succes", $"Invoice has been {(_action == 1 ? "added" : "updated")} successfully", "success", _parentForm);
                    return;
                }
                else
                {
                    ToastManager.ShowToastNotification($"{(_action == 1 ? "Create" : "Update")} Invoice Fail", $"Invoice could not be {(_action == 1 ? "added" : "updated")}", "error", _parentForm);
                    return;
                }
            }
            catch (Exception ex)
            {
                ToastManager.ShowToastNotification($"{(_action == 1 ? "Create" : "Update")} Invoice Fail", ex.Message, "error", _parentForm);
                return;
            }
        }
        private void btnPrintInvoice_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtbInvoiceID.Texts.ToString())) return;
            _invoice = new InvoiceBLL(txtbInvoiceID.Texts.ToString(), DateTime.Now, 'S', 0, "", "", "").GetInvoiceByID();
            if (dgvProductList.Rows.Count > 0)
            {
                DataTable dt = OtherUtilities.GetDataTableFromDGV(dgvProductList);
                OtherUtilities.Print("Payment Invoice", _invoice, dt);
            }
            
        }
        #endregion

        #region SEARCH ACTION
        private void btnFind_Click(object sender, EventArgs e)
        {
            // Search Logic
            string id = txtbSearchBox.Texts.Trim();

            int totalBillMin = Convert.ToInt32(string.IsNullOrEmpty(txtbMin.Texts.Trim()) ? "-1" : txtbMin.Texts.Trim());
            int totalBillMax = Convert.ToInt32(string.IsNullOrEmpty(txtbMax.Texts.Trim()) ? "-1" : txtbMax.Texts.Trim());

            List<string> conditionList = new List<string>();
            bool searchAvailable = false;

            // Search by ID
            if (!string.IsNullOrEmpty(id))
            {
                conditionList.Add($"I.ID = '{id}'");
                searchAvailable = true;
            }

            // Search by Invoice Type
            if (cbSearchInvoiceType.SelectedIndex > 0)
            {
                char invoiceType = ((KeyValuePair<string, char>)cbSearchInvoiceType.SelectedItem).Value;
                conditionList.Add($"I.[Invoice Type] LIKE '{invoiceType}'");
                searchAvailable = true;
            }

            // Search by Customer
            if (cbSearchCustomer.SelectedIndex > 0)
            {
                Customer customer = ((KeyValuePair<string, Customer>)cbSearchCustomer.SelectedItem).Value;
                conditionList.Add($"I.CustomerID = '{customer.ID}'");
                searchAvailable = true;
            }

            // Search by Shift
            if (cbSearchShift.SelectedIndex > 0)
            {
                int shiftNumber = cbSearchShift.SelectedIndex;   
                conditionList.Add($"SUBSTRING(I.ID, 14, 2) = 'S{shiftNumber}'");
                searchAvailable = true;
            }

            // Search by Total Bill
            string searchByTotalBill = "";
            if (totalBillMin >= 0 && totalBillMax < 0) searchByTotalBill = $"[Total Amount] >= {totalBillMin}";
            else if (totalBillMin < 0 && totalBillMax >= 0) searchByTotalBill = $"[Total Amount] <= {totalBillMax}";
            else if (totalBillMin >= 0 && totalBillMax >= 0) searchByTotalBill = $"([Total Amount] BETWEEN {totalBillMin} AND {totalBillMax})";

            if (!string.IsNullOrEmpty(searchByTotalBill))
            {
                conditionList.Add(searchByTotalBill);
                searchAvailable = true;
            }

            // Search by Has Use Voucher
            if (btnSearchToggleHasVoucher.IsOn == true)
            {
                conditionList.Add("I.VoucherID IS NOT NULL");
                searchAvailable = true;
            }

            if (searchAvailable == false) return;

            string query = @"SELECT 
                            I.ID,
                            C.Name AS [Customer Name],
                            E.Name AS [Employee Name],
                            I.VoucherID,
                            I.[Invoice Type],
                            I.[Total Amount],
                            I.[Create Date]
                        FROM Invoice I
                        LEFT JOIN Customer C ON I.CustomerID = C.ID
                        LEFT JOIN Employee E ON I.EmployeeID = E.ID";

            query += " WHERE " + string.Join(" AND ", conditionList);
            Debug.WriteLine(query);
            InvoiceManagementForm_Load(null, EventArgs.Empty);

            DataTable result = OtherUtilities.Search(query, _parentForm);
            if (result != null && result.Rows.Count > 0)
            {
                dgvInvoiceList.DataSource = result;
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
            InvoiceManagementForm_Load(null, EventArgs.Empty);
        }
        private void btnSearchToggleHasVoucher_Click(object sender, EventArgs e)
        {
            btnSearchToggleHasVoucher.Toggle();
            if (btnSearchToggleHasVoucher.IsOn == false) return;
            if (btnClear.Enabled == false) OtherUtilities.ButtonEnable(btnClear);
        }
        private void txtbSearchBox__TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtbSearchBox.Texts)) return;
            if (btnClear.Enabled == false) OtherUtilities.ButtonEnable(btnClear);
        }
        private void cbSearchInvoiceType_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbSearchInvoiceType.SelectedIndex == 0) return;
            if (btnClear.Enabled == false) OtherUtilities.ButtonEnable(btnClear);
        }
        private void cbSearchCustomer_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbSearchCustomer.SelectedIndex == 0) return;
            if (btnClear.Enabled == false) OtherUtilities.ButtonEnable(btnClear);
        }
        private void cbSearchShift_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbSearchShift.SelectedIndex == 0) return;
            if (btnClear.Enabled == false) OtherUtilities.ButtonEnable(btnClear);
        }
        private void txtbMin__TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtbMin.Texts)) return;
            if (btnClear.Enabled == false) OtherUtilities.ButtonEnable(btnClear);
        }
        private void txtbMax__TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtbMin.Texts)) return;
            if (btnClear.Enabled == false) OtherUtilities.ButtonEnable(btnClear);
        }
        private void txtbMin_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtbMin.Texts.Trim())) return;
            int totalBillMin = Convert.ToInt32(txtbMin.Texts.Trim());
            if (totalBillMin < 0)
            {
                ToastManager.ShowToastNotification("Invalid Total Bill Amount", "Total bill amount must be positive number", "error", _parentForm);
                return;
            }
            if (!string.IsNullOrEmpty(txtbMax.Texts.Trim()))
            {
                int totalBillMax = Convert.ToInt32(txtbMax.Texts.Trim());
                if (totalBillMax < totalBillMin)
                {
                    txtbMin.Texts = totalBillMax.ToString();
                    txtbMax.Texts = totalBillMin.ToString();
                }
            }
        }
        private void txtbMax_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtbMax.Texts.Trim())) return;
            int totalBillMax = Convert.ToInt32(txtbMax.Texts.Trim());
            if (totalBillMax < 0)
            {
                ToastManager.ShowToastNotification("Invalid Total Bill Amount", "Total bill amount must be positive number", "error", _parentForm);
                return;
            }
            if (!string.IsNullOrEmpty(txtbMin.Texts.Trim()))
            {
                int totalBillMin = Convert.ToInt32(txtbMin.Texts.Trim());
                if (totalBillMax < totalBillMin)
                {
                    txtbMin.Texts = totalBillMax.ToString();
                    txtbMax.Texts = totalBillMin.ToString();
                }
            }
        }
        #endregion

        #region GROUP BOX INVOICE INFORMATION
        private void cbCustomerName_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCustomerName.SelectedIndex > 0)
            {
                string customerName = ((KeyValuePair<string, Customer>)cbCustomerName.SelectedItem).Key;
                Customer customer = ((KeyValuePair<string, Customer>)cbCustomerName.SelectedItem).Value;

                if (customerName.Contains("(NEW)")) 
                { 
                    cbCustomerName.DropDownStyle = ComboBoxStyle.DropDown; 
                    txtbCustomerPhone.Enabled = true;
                }
                else
                { 
                    cbCustomerName.DropDownStyle = ComboBoxStyle.DropDownList;
                    txtbCustomerPhone.Enabled = false;
                }
                if (_invoice != null)
                {
                    _invoice.CustomerID = customer.ID;
                    _invoice.Customer = customer;
                }
                

                if (customer != null && !string.IsNullOrEmpty(customer.Phone)) txtbCustomerPhone.Texts = customer.Phone;
                else txtbCustomerPhone.Focus();
                cbCustomerName.Tag = customer;
                
            }
            else
            {
                cbCustomerName.DropDownStyle = ComboBoxStyle.DropDown;
                txtbCustomerPhone.Texts = "";
                txtbCustomerPhone.Enabled = true;
            }
        }
        private void txtbCustomerPhone_Leave(object sender, EventArgs e)
        {
            txtbCustomerPhone_PreviewKeyDown(null, new PreviewKeyDownEventArgs(Keys.Tab));
        }
        private void txtbCustomerPhone_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab || e.KeyCode == Keys.Enter)
            {
                string phone = txtbCustomerPhone.Texts.Trim();
                if (string.IsNullOrEmpty(phone))
                {
                    ToastManager.ShowToastNotification("Missing Phone", "Please enter new customer phone", "error", _parentForm);
                    return;
                }

                if (!phone.StartsWith('0') || phone.Length != 10)
                {
                    ToastManager.ShowToastNotification("Invalid Phone", "New customer phone is invalid", "error", _parentForm);
                    return;
                }

                Customer customer = new Customer(CustomerBLL.AutoGenerateCustomerID(), cbCustomerName.Texts.Trim(), txtbCustomerPhone.Texts.Trim());
                int index = cbCustomerName.Items.Cast<KeyValuePair<string, Customer>>().ToList().FindIndex(item => item.Value != null && item.Value.ID == customer.ID);

                if (index == -1)
                {
                    cbCustomerName.Items.Add(new KeyValuePair<string, Customer>(customer.Name + "(NEW)", customer));
                    cbCustomerName.SelectedIndex = cbCustomerName.Items.Count - 1;
                }
                else
                {
                    cbCustomerName.Items[index] = new KeyValuePair<string, Customer>(customer.Name.Replace("(NEW)", "") + "(NEW)", customer);
                    cbCustomerName.SelectedIndex = index;
                }
            }
        }
        private void cbStaffName_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbStaffName.SelectedIndex > 0)
            {
                cbStaffName.Tag = ((KeyValuePair<string,Employee>)cbStaffName.SelectedItem).Value;
            }else cbStaffName.Tag = null;
        }
        private void txtbVoucher_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtbVoucher.Texts.Trim())) return;

            Voucher voucher = new VoucherBLL(txtbVoucher.Texts.Trim(),"",0,0,0,DateTime.Now,DateTime.Now,true).GetVoucherByID();

            if (voucher == null)
            {
                ToastManager.ShowToastNotification("Invalid Voucher", "This voucher code is not exist", "error", _parentForm);
                txtbVoucher.Texts = "";
                return;
            }
            labelVoucherDescription.Text = voucher.Description;
            labelVoucherDescription.Visible = true;

            _invoice.VoucherID = voucher.ID;
            _invoice.Voucher = voucher;
            UpdateTotalBill();
        }
        private void cbInvoiceType_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbInvoiceType.SelectedIndex > 0)
            {
                if (_invoice == null) return;
                char invoiceType = Convert.ToChar(((KeyValuePair<string, char>)cbInvoiceType.SelectedItem).Value);
                Debug.WriteLine(invoiceType);
                txtbInvoiceID.Texts = InvoiceBLL.AutoGenerateInvoiceID(invoiceType, _invoice.CreateDate, cbWorkShift.SelectedIndex + 1, _invoice.EmployeeID);
                _invoice.ID = txtbInvoiceID.Texts;
            }
        }
        #endregion

        #region GROUP BOX ADD TO INVOICE
        private void cbProductName_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbProductName.SelectedIndex > 0)
            {
                Product product = ((KeyValuePair<string, Product>)cbProductName.SelectedItem).Value;
                cbProductName.Tag = product;
                txtbProductID.Texts = product.ID;
                txtbUnitPrice.Texts = product.Price.ToString();
                labelAvailableStock.Text = product.Quantity.ToString();
                labelAvailableStockTitle.Visible = true;
                labelAvailableStock.Visible = true;
                txtbTotalPrice.Texts = "";
                txtbQuanity.Texts = "";
                txtbQuanity.Enabled = true;
                txtbQuanity.Focus();
                OtherUtilities.ButtonEnable(btnAddProduct);
            }
            else 
            {
                cbProductName.Tag = null;
                txtbProductID.Texts = "";
                txtbUnitPrice.Texts = "";
                txtbQuanity.Texts = "";
                txtbTotalPrice.Texts = "";
                labelAvailableStockTitle.Visible = false;
                labelAvailableStock.Visible = false;
                txtbQuanity.Enabled = false;
                txtbTotalPrice.Texts = "";
                OtherUtilities.ButtonDisable(btnAddProduct);
                OtherUtilities.ButtonDisable(btnRemoveProduct);
            }
        }
        private void txtbQuanity_Leave(object sender, EventArgs e)
        {
            txtbQuanity_KeyPress(null, new KeyPressEventArgs((char)Keys.Tab));
        }
        private void txtbQuanity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Tab)
            {
                if (string.IsNullOrEmpty(txtbQuanity.Texts.Trim()))
                {
                    ToastManager.ShowToastNotification("Missing Quantity", "Please enter the product quantity you want to add", "error", _parentForm);
                    txtbTotalPrice.Texts = "";
                    txtbQuanity.Focus();
                    return;
                }

                int quantity = Convert.ToInt32(txtbQuanity.Texts.Trim());

                if (quantity < 0)
                {
                    ToastManager.ShowToastNotification("Invalid Quantity", "The product quantity must be a positive number", "error", _parentForm);
                    txtbTotalPrice.Texts = "";
                    txtbQuanity.Focus();
                    return;
                }

                if (quantity > Convert.ToInt32(labelAvailableStock.Text.Trim()))
                {
                    ToastManager.ShowToastNotification("Invalid Quantity", "Not enough stock for this deal", "error", _parentForm);
                    quantity = Convert.ToInt32(labelAvailableStock.Text.Trim());
                    txtbQuanity.Texts = quantity.ToString();
                }
                Product product = cbProductName.Tag as Product;
                txtbTotalPrice.Texts = (product.Price * quantity).ToString();
            }
        }
        private void btnRemoveProduct_Click(object sender, EventArgs e)
        {
            var dt = dgvProductList.DataSource as DataTable;
            string productName = cbProductName.Texts.Trim();
            string productId = (cbProductName.Tag as Product).ID;

            var lineToDelete = _invoice.InvoiceLines.FirstOrDefault(line => line.ProductID == productId);

            if (lineToDelete != null)
            {
                // Xóa khỏi danh sách _invoice.InvoiceLines
                _invoice.InvoiceLines.Remove(lineToDelete);
                
                // Xóa khỏi DataTable
                var rowToDelete = dt.AsEnumerable().FirstOrDefault(row => row.Field<string>("Product Name") == productName);

                if (rowToDelete != null)
                {
                    dt.Rows.Remove(rowToDelete);
                }
            }

            UpdateIDForInvoiceLine();
            UpdateTotalBill();
            cbProductName.SelectedIndex = 0;
        }
        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            var dt = dgvProductList.DataSource as DataTable;
            string productName = cbProductName.Texts.Trim();
            string productId = (cbProductName.Tag as Product).ID;
            int quantity = Convert.ToInt32(txtbQuanity.Texts.Trim());
            int unitPrice = Convert.ToInt32(txtbUnitPrice.Texts.Trim());
            int totalPrice = Convert.ToInt32(txtbTotalPrice.Texts.Trim());

            // Tạo đối tượng InvoiceLine
            InvoiceLine invoiceLine = new InvoiceLine(0, _invoice.ID, productId, quantity, unitPrice, null, null);

            // Kiểm tra xem có dòng với ProductId trong _invoice.InvoiceLines không
            var existingInvoiceLine = _invoice.InvoiceLines.FirstOrDefault(line => line.ProductID == productId);

            if (existingInvoiceLine != null)
            {
                // Nếu tồn tại -> Cập nhật trong InvoiceLines và DataTable
                existingInvoiceLine.Quantity = quantity;

                // Cập nhật trong DataTable
                var existingRow = dt.AsEnumerable().FirstOrDefault(row => row.Field<string>("Product Name") == productName);

                if (existingRow != null)
                {
                    existingRow["Quantity"] = quantity;
                    existingRow["Total Price"] = quantity * unitPrice;
                }
            }
            else
            {
                // Nếu không tồn tại -> Thêm mới vào InvoiceLines và DataTable
                _invoice.InvoiceLines.Add(invoiceLine);
                
                // Thêm vào DataTable
                DataRow newRow = dt.NewRow();
                newRow["ID"] = dt.Rows.Count + 1; // Hoặc giá trị ID thích hợp
                newRow["Product Name"] = productName;
                newRow["Quantity"] = quantity;
                newRow["Unit Price"] = unitPrice;
                newRow["Total Price"] = quantity * unitPrice;
                dt.Rows.Add(newRow);
            }
            UpdateIDForInvoiceLine();
            UpdateTotalBill();
            cbProductName.SelectedIndex = 0;
        }
        #endregion

        #region UTILITIES FUNCTION
        private List<KeyValuePair<string, Customer>> GetAllCustomer()
        {
            List<KeyValuePair<string, Customer>> listCustomer = new List<KeyValuePair<string, Customer>>();
            listCustomer.Add(new KeyValuePair<string, Customer>("New Customer", default(Customer)));
            DataTable customerTable =  CustomerBLL.GetAllCustomers();
            if (customerTable.Rows.Count > 0)
            {
                foreach (DataRow row in customerTable.Rows)
                {
                    string customerName = row["Name"].ToString();
                    Customer customer = new CustomerBLL(row["ID"].ToString(), "", "").GetCustomerByID();
                    listCustomer.Add(new KeyValuePair<string, Customer>(customerName, customer));
                }
            }
            return listCustomer;
        }
        private List<KeyValuePair<string, Product>> GetAllProduct()
        {
            List<KeyValuePair<string, Product>> listProduct = new List<KeyValuePair<string, Product>>();
            listProduct.Add(new KeyValuePair<string, Product>("Choose Option", default(Product)));
            DataTable productTable = ProductBLL.GetAllProducts();
            if (productTable.Rows.Count > 0)
            {
                foreach (DataRow row in productTable.Rows)
                {
                    string productName = row["Name"].ToString();
                    Product product = new ProductBLL(row["ID"].ToString(),"", 0, 0, 0, DateTime.Now).GetProductByID();
                    listProduct.Add(new KeyValuePair<string, Product>(productName, product));
                }
            }
            return listProduct;
        }
        private List<KeyValuePair<string, Employee>> GetAllEmployee()
        {
            List<KeyValuePair<string, Employee>> listEmployee = new List<KeyValuePair<string, Employee>>();
            listEmployee.Add(new KeyValuePair<string, Employee>("Choose Option", default(Employee)));
            DataTable empTable = EmployeeBLL.GetAllEmployee();
            if (empTable.Rows.Count > 0)
            {
                foreach (DataRow row in empTable.Rows)
                {
                    string employeeName = row["Name"].ToString();
                    Employee employee = new EmployeeBLL(row["ID"].ToString(),"","","","","","","","",0,"").GetEmployeeByID();
                    listEmployee.Add(new KeyValuePair<string, Employee>(employeeName, employee));
                }
            }
            return listEmployee;
        }
        private void CheckShift()
        {
            // Lấy thời gian hiện tại
            DateTime currentTime = DateTime.Now;

            // Định nghĩa các mốc thời gian cho các ca làm việc
            DateTime shift1Start = DateTime.Today.AddHours(8).AddMinutes(30);  // 8:30 AM
            DateTime shift1End = DateTime.Today.AddHours(13);  // 1:00 PM
            DateTime shift2Start = DateTime.Today.AddHours(13);  // 1:00 PM
            DateTime shift2End = DateTime.Today.AddHours(16).AddMinutes(30);  // 4:30 PM
            DateTime shift3Start = DateTime.Today.AddHours(16).AddMinutes(30);  // 4:30 PM
            DateTime shift3End = DateTime.Today.AddHours(21).AddMinutes(30);  // 9:30 PM

            // Kiểm tra ca làm việc hiện tại và chọn đúng index
            if (currentTime >= shift1Start && currentTime < shift1End)
            {
                cbWorkShift.SelectedIndex = 0;  // Chọn ca 1
            }
            else if (currentTime >= shift2Start && currentTime < shift2End)
            {
                cbWorkShift.SelectedIndex = 1;  // Chọn ca 2
            }
            else if (currentTime >= shift3Start && currentTime < shift3End)
            {
                cbWorkShift.SelectedIndex = 2;  // Chọn ca 3
            }
            else
            {
                cbWorkShift.SelectedIndex = 3;  // Không có ca làm việc phù hợp
            }
        }
        private void LoadDataGridViewProductList(string invoiceID)
        {
            dgvProductList.DataSource = null;
            dgvProductList.Rows.Clear();
            dgvProductList.Columns.Clear();

            DataTable dt;

            if (!string.IsNullOrWhiteSpace(invoiceID))
            {
                dt = new InvoiceLineBLL(-1, invoiceID, "", 0, 0).GetInvoiceLinesByInvoiceID();
                dgvInvoiceList.Enabled = true;
            }
            else
            {
                dt = CreateEmptyInvoiceLineTable(); // tạo bảng rỗng để bind
                dgvProductList.Enabled = false;
            }

            dgvProductList.DataSource = dt;
            dgvProductList.Tag = invoiceID;
        }
        private DataTable CreateEmptyInvoiceLineTable()
        {
            var dt = new DataTable();
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Product Name", typeof(string));
            dt.Columns.Add("Quantity", typeof(int));
            dt.Columns.Add("Unit Price", typeof(int));
            dt.Columns.Add("Total Price", typeof(int));
            return dt;
        }
        private void UpdateTotalBill()
        {
            int totalBill = 0;
            Voucher v = null;
            if (_invoice != null && _invoice.InvoiceLines.Count > 0) _invoice.InvoiceLines.ForEach(item => totalBill += item.TotalPrice);

            if (_invoice.Voucher != null)
            {
                Debug.WriteLine("Has voucher");
                v = _invoice.Voucher;

                if (totalBill > v.MinimumOrderPrice)
                {
                    int discountAmount = 0;
                    if (v.DiscountAmount < 100)
                    {
                        discountAmount = (int)(totalBill * (v.DiscountAmount / 100f));
                        if (v.DiscountMaximumAmount > 0 && discountAmount > v.DiscountMaximumAmount) discountAmount = v.DiscountMaximumAmount;
                    }
                    else discountAmount = v.DiscountAmount;
                    if (discountAmount > v.DiscountMaximumAmount) discountAmount = v.DiscountMaximumAmount;
                    else totalBill -= discountAmount;
                }
            }
            txtbTotalBill.Texts = totalBill.ToString();
            _invoice.TotalAmount = totalBill;
        }
        private void UpdateIDForInvoiceLine()
        {
            int id = 1;
            foreach (InvoiceLine inL in _invoice.InvoiceLines)
            {
                inL.ID = id;
                id++;
            }
        }
        private void SetUpSearchGroupBox()
        {
            txtbSearchBox.Texts = "";
            cbSearchInvoiceType.SelectedIndex = 0;
            OtherUtilities.SetUpComboBox(cbSearchCustomer, GetAllCustomer());
            cbSearchShift.SelectedIndex = 0;
            txtbMin.Texts = "";
            txtbMax.Texts = "";
            if (btnSearchToggleHasVoucher.IsOn != false) btnSearchToggleHasVoucher.Toggle();
            OtherUtilities.ButtonDisable(btnClear);
            OtherUtilities.ButtonEnable(btnFind);
        } 
        #endregion
    }
}
