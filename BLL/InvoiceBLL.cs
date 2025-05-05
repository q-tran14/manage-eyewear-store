using DAL.Object;
using DAL;
using System.Data;
using System.Diagnostics;

namespace BLL
{
    public class InvoiceBLL
    {
        private InvoiceDAL _invoice;

        public InvoiceBLL(string id, DateTime createDate, char invoiceType, int totalAmount,
                          string customerID, string employeeID, string? voucherID)
        {
            _invoice = new InvoiceDAL(id, createDate, invoiceType, totalAmount, customerID, employeeID, voucherID);
        }

        public static DataTable GetAllInvoices() => InvoiceDAL.GetAllInvoices();

        public Invoice GetInvoiceByID() 
        { 
            DataTable dt = _invoice.GetInvoiceByID();
            if (dt.Rows.Count == 0) return null;

            DataRow row = dt.Rows[0];
            Voucher voucher = null;
            
            if (!string.IsNullOrEmpty(row["VoucherID"].ToString())) voucher = new VoucherBLL(row["VoucherID"].ToString(), "", 0, 0, 0, DateTime.Now, DateTime.Now, true).GetVoucherByID();
            
            Customer customer = new CustomerBLL(row["CustomerID"].ToString(),"","").GetCustomerByID();
            Employee employee = new EmployeeBLL(row["EmployeeID"].ToString(),"","","","","","","","",0,"").GetEmployeeByID();

            List<InvoiceLine> invoiceLines = new List<InvoiceLine>();
            DataTable dtInvoiceLine = new InvoiceLineBLL(0, row["ID"].ToString(),"",0,0).GetInvoiceLinesByInvoiceID();

            if (dtInvoiceLine.Rows.Count > 0)
            {
                foreach (DataRow r in dtInvoiceLine.Rows)
                {
                    InvoiceLine invoiceLine = new InvoiceLineBLL(Convert.ToInt32(r["ID"]), row["ID"].ToString(),"",0,0).GetInvoiceLineByID();
                    invoiceLines.Add(invoiceLine);
                }
            }

            Invoice invoice =new Invoice(
                                row["ID"].ToString(), 
                                Convert.ToDateTime(row["Create Date"]),
                                Convert.ToChar(row["Invoice Type"]),
                                Convert.ToInt32(row["Total Amount"]),
                                customer.ID,
                                employee.ID,
                                (voucher == null ? "" : voucher.ID),
                                customer,
                                employee,
                                voucher,
                                invoiceLines
                            );
            return invoice;
        }

        public bool AddInvoice() => _invoice.InsertInvoice();

        public bool DeleteInvoice() => _invoice.DeleteInvoice();

        public bool UpdateInvoice() => _invoice.UpdateInvoice();

        public static string AutoGenerateInvoiceID(char type,DateTime now, int shiftNumber, string employeeID)
            => InvoiceDAL.GenerateInvoiceID(type,now, shiftNumber, employeeID);

        public void SetCustomer(Customer customer) => _invoice.SetCustomer(customer);

        public void SetEmployee(Employee employee) => _invoice.SetEmployee(employee);

        public void SetVoucher(Voucher? voucher) => _invoice.SetVoucher(voucher);

        public void SetInvoiceLines(List<InvoiceLine> invoiceLines) => _invoice.SetListInvoiceLine(invoiceLines);
    }
}
