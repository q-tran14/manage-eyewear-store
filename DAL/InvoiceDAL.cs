using DAL.Object;
using DAL.Utils;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class InvoiceDAL
    {
        private Invoice _invoice;

        public InvoiceDAL(string id, DateTime createDate, char invoiceType, int totalAmount,
                          string customerID, string employeeID, string? voucherID)
        {
            _invoice = new Invoice(
                id, createDate, invoiceType, totalAmount,
                customerID, employeeID, voucherID,
                null, null, null, null
            );
        }
        public void SetEmployee(Employee e) => _invoice.Employee = e;
        public void SetCustomer(Customer c) => _invoice.Customer = c;
        public void SetVoucher(Voucher? v) => _invoice.Voucher = v;
        public void SetListInvoiceLine(List<InvoiceLine> invoiceLines) => _invoice.InvoiceLines = invoiceLines;

        public static DataTable GetAllInvoices()
        {
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
            return UtilityDatabase.Instance.ExecuteQuery(query);
        }

        public DataTable GetInvoiceByID()
        {
            string query = "SELECT * FROM Invoice WHERE ID = @ID";
            SqlParameter[] parameters = {
                new SqlParameter("@ID", _invoice.ID)
            };
            return UtilityDatabase.Instance.ExecuteQuery(query, parameters);
        }

        public bool InsertInvoice()
        {
            string query = @"INSERT INTO Invoice 
                             (ID, CustomerID, EmployeeID, VoucherID , [Invoice Type], [Total Amount], [Create Date])
                             VALUES 
                             (@ID, @CustomerID, @EmployeeID, @VoucherID, @InvoiceType, @TotalAmount, @CreateDate)";
            SqlParameter[] parameters = {
                new SqlParameter("@ID", _invoice.ID),
                new SqlParameter("@CreateDate", _invoice.CreateDate),
                new SqlParameter("@InvoiceType", _invoice.InvoiceType),
                new SqlParameter("@TotalAmount", _invoice.TotalAmount),
                new SqlParameter("@CustomerID", _invoice.CustomerID),
                new SqlParameter("@EmployeeID", _invoice.EmployeeID),
                new SqlParameter("@VoucherID", string.IsNullOrEmpty(_invoice.VoucherID) ? (object)DBNull.Value : _invoice.VoucherID)
            };
            return UtilityDatabase.Instance.ExecuteNonQuery(query, parameters) > 0;
        }

        public bool DeleteInvoice()
        {
            string query = "DELETE FROM Invoice WHERE ID = @ID";
            SqlParameter[] parameters = {
                new SqlParameter("@ID", _invoice.ID)
            };
            return UtilityDatabase.Instance.ExecuteNonQuery(query, parameters) > 0;
        }

        public bool UpdateInvoice()
        {
            string query = @"UPDATE Invoice SET 
                             [Create Date] = @CreateDate,
                             [Invoice Type] = @InvoiceType,
                             [Total Amount] = @TotalAmount,
                             CustomerID = @CustomerID,
                             EmployeeID = @EmployeeID,
                             VoucherID = @VoucherID
                             WHERE ID = @ID";
            SqlParameter[] parameters = {
                new SqlParameter("@CreateDate", _invoice.CreateDate),
                new SqlParameter("@InvoiceType", _invoice.InvoiceType),
                new SqlParameter("@TotalAmount", _invoice.TotalAmount),
                new SqlParameter("@CustomerID", _invoice.CustomerID),
                new SqlParameter("@EmployeeID", _invoice.EmployeeID),
                new SqlParameter("@VoucherID", string.IsNullOrEmpty(_invoice.VoucherID) ? (object)DBNull.Value : _invoice.VoucherID),
                new SqlParameter("@ID", _invoice.ID)
            };
            return UtilityDatabase.Instance.ExecuteNonQuery(query, parameters) > 0;
        }

        public static string GenerateInvoiceID(char type, DateTime date, int shiftNumber, string employeeID)
        {
            // Tạo các thành phần ngày
            string day = date.Day.ToString("D2");
            string month = date.Month.ToString("D2");
            string year = date.Year.ToString();

            // Tạo mã ca làm việc (Shift Number)
            string shiftCode = "S" + shiftNumber;

            // Câu lệnh SQL: Đếm số hóa đơn trong cùng ngày & cùng ca
            string query = @"SELECT COUNT(*) 
                             FROM Invoice 
                             WHERE CAST([Create Date] AS DATE) = @DateCreated";

            SqlParameter[] parameters = {
                new SqlParameter("@DateCreated", date.Date),
                new SqlParameter("@ShiftNumber", shiftNumber)
            };

            int count = Convert.ToInt32(UtilityDatabase.Instance.ExecuteQuery(query, parameters).Rows[0][0]);

            // Số thứ tự trong ca (tăng lên 1)
            string serial = (count + 1).ToString("D4");

            // Gộp tất cả các thành phần thành OrderID
            string orderID = $"{type}{day}{month}{year}{serial}{shiftCode}{employeeID}";

            return orderID;
        }
    }
}
