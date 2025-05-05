using DAL.Object;
using DAL.Utils;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace DAL
{
    public class InvoiceLineDAL
    {
        private InvoiceLine _invoiceLine;

        public InvoiceLineDAL(int id, string invoiceID, string productID, int quantity, int unitPrice)
        {
            _invoiceLine = new InvoiceLine(id, invoiceID, productID, quantity, unitPrice, null, null);
        }

        public void SetInvoice(Invoice i) => _invoiceLine.Invoice = i;

        public void SetProduct(Product p) => _invoiceLine.Product = p;
        public string GetProductID()
        {
            return _invoiceLine.ProductID;
        }
        public int GetTotalPrice() => _invoiceLine.TotalPrice;
        public void SetQuantity(int extra)
        {
            _invoiceLine.Quantity += extra;
        }
        public DataTable GetAllInvoiceLines()
        {
            string query = "SELECT * FROM InvoiceLine";
            return UtilityDatabase.Instance.ExecuteQuery(query);
        }

        public DataTable GetInvoiceLineByID()
        {
            string query = "SELECT * FROM InvoiceLine WHERE ID = @ID AND InvoiceID = @InvoiceID";
            SqlParameter[] parameters = {
                new SqlParameter("@ID", _invoiceLine.ID),
                new SqlParameter("@InvoiceID", _invoiceLine.InvoiceID)
            };
            return UtilityDatabase.Instance.ExecuteQuery(query, parameters);
        }

        public DataTable GetInvoiceLinesByInvoiceID()
        {
            string query = @"SELECT 
                            IL.ID,
                            P.Name AS [Product Name],
                            IL.Quantity,
                            IL.[Unit Price],
                            IL.[Total Price]
                        FROM InvoiceLine IL
                        JOIN Product P ON IL.ProductID = P.ID
                        WHERE IL.InvoiceID = @InvoiceID";
            SqlParameter[] parameters = {
                new SqlParameter("@InvoiceID", _invoiceLine.InvoiceID)
            };
            return UtilityDatabase.Instance.ExecuteQuery(query, parameters);
        }

        public bool InsertInvoiceLine()
        {
            string query = @"INSERT INTO InvoiceLine (ID, InvoiceID, ProductID, Quantity, [Unit Price])
                             VALUES (@ID, @InvoiceID, @ProductID, @Quantity, @UnitPrice)";
            Debug.WriteLine($"ID: {_invoiceLine.ID} - InvoiceID: {_invoiceLine.InvoiceID} - ProductID: {_invoiceLine.ProductID} - Quanity: {_invoiceLine.Quantity} - Unit Price:{_invoiceLine.UnitPrice}");
            SqlParameter[] parameters = {
                new SqlParameter("@ID", _invoiceLine.ID),
                new SqlParameter("@InvoiceID", _invoiceLine.InvoiceID),
                new SqlParameter("@ProductID", _invoiceLine.ProductID),
                new SqlParameter("@Quantity", _invoiceLine.Quantity),
                new SqlParameter("@UnitPrice", _invoiceLine.UnitPrice)
            };
            return UtilityDatabase.Instance.ExecuteNonQuery(query, parameters) > 0;
        }

        public bool DeleteInvoiceLine()
        {
            string query = "DELETE FROM InvoiceLine WHERE ID = @ID";
            SqlParameter[] parameters = {
                new SqlParameter("@ID", _invoiceLine.ID)
            };
            return UtilityDatabase.Instance.ExecuteNonQuery(query, parameters) > 0;
        }

        public bool UpdateInvoiceLine()
        {
            string query = @"UPDATE InvoiceLine SET
                                Quantity = @Quantity
                             WHERE ID = @ID AND InvoiceID = @InvoiceID ";
            SqlParameter[] parameters = {
                new SqlParameter("@InvoiceID", _invoiceLine.InvoiceID),
                new SqlParameter("@Quantity", _invoiceLine.Quantity),
                new SqlParameter("@ID", _invoiceLine.ID)
            };
            return UtilityDatabase.Instance.ExecuteNonQuery(query, parameters) > 0;
        }

        public int GetQuantity()
        {
            return _invoiceLine.Quantity;
        }
    }
}
