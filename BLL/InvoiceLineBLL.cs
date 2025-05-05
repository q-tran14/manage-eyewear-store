using DAL.Object;
using DAL;
using System.Data;

namespace BLL
{
    public class InvoiceLineBLL
    {
        private InvoiceLineDAL _invoiceLine;

        public InvoiceLineBLL(int id, string invoiceID, string productID, int quantity, int unitPrice)
        {
            _invoiceLine = new InvoiceLineDAL(id, invoiceID, productID, quantity, unitPrice);
        }

        public void SetInvoice(Invoice invoice) => _invoiceLine.SetInvoice(invoice);

        public void SetProduct(Product product) => _invoiceLine.SetProduct(product);
        public string GetInvoiceLineProductID() => _invoiceLine.GetProductID();
        public int GetTotalPrice() => _invoiceLine.GetTotalPrice();
        public void SetQuantity(int quantity) => _invoiceLine.SetQuantity(quantity);
        public int GetQuantity() => _invoiceLine.GetQuantity();
        public InvoiceLine GetInvoiceLineByID() 
        {
            DataTable dt = _invoiceLine.GetInvoiceLineByID(); // Gọi xuống DAL để lấy theo ID
            if (dt.Rows.Count == 0) return null;

            DataRow row = dt.Rows[0];
            Product product = new ProductBLL(row["ProductID"].ToString(),"",0,0,0,DateTime.Now).GetProductByID(); 

            return new InvoiceLine(
                Convert.ToInt32(row["ID"]),
                row["InvoiceID"].ToString(),
                product.ID,
                Convert.ToInt32(row["Quantity"]),
                product.Price,
                null, // Hoặc bạn có thể truyền đối tượng Invoice nếu có
                product  // Hoặc bạn có thể truyền đối tượng Product nếu có
            );
        }

        public DataTable GetInvoiceLinesByInvoiceID() => _invoiceLine.GetInvoiceLinesByInvoiceID();

        public bool AddInvoiceLine() => _invoiceLine.InsertInvoiceLine();

        public bool DeleteInvoiceLine() => _invoiceLine.DeleteInvoiceLine();

        public bool UpdateInvoiceLine() => _invoiceLine.UpdateInvoiceLine();
    }

}
