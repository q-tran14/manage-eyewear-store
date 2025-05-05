using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Object
{
    public class Invoice
    {
        public string ID { get; set; }
        public DateTime CreateDate { get; set; }
        public char InvoiceType { get; set; } // 'S', 'R', 'W'
        public int TotalAmount { get; set; }

        public string CustomerID { get; set; }
        public string EmployeeID { get; set; }
        public string? VoucherID { get; set; }

        public Customer Customer { get; set; }
        public Employee Employee { get; set; }
        public Voucher? Voucher { get; set; }

        public List<InvoiceLine> InvoiceLines { get; set; } = new();
        public Invoice(string id, DateTime createDate, char invoiceType, int totalAmount,
               string customerID, string employeeID, string? voucherID,
               Customer customer, Employee employee, Voucher? voucher,
               List<InvoiceLine> invoiceLines)
        {
            ID = id;
            CreateDate = createDate;
            InvoiceType = invoiceType;
            TotalAmount = totalAmount;

            CustomerID = customerID;
            EmployeeID = employeeID;
            VoucherID = voucherID;

            Customer = customer;
            Employee = employee;
            Voucher = voucher;

            InvoiceLines = invoiceLines ?? new List<InvoiceLine>();
        }
    }
}
