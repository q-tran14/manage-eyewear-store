using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Object
{
    public class InvoiceLine
    {
        public int ID { get; set; }

        public string InvoiceID { get; set; }
        public string ProductID { get; set; }

        public int Quantity { get; set; }
        public int UnitPrice { get; set; }

        public int TotalPrice => Quantity * UnitPrice;

        public Invoice Invoice { get; set; }
        public Product Product { get; set; }
        public InvoiceLine(int id, string invoiceID, string productID, int quantity, int unitPrice,
                       Invoice invoice, Product product)
        {
            ID = id;
            InvoiceID = invoiceID;
            ProductID = productID;
            Quantity = quantity;
            UnitPrice = unitPrice;
            Invoice = invoice;
            Product = product;
        }
    }
}
