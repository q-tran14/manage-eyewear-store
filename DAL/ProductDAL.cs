using DAL.Object;
using DAL.Utils;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;

namespace DAL
{
    public class ProductDAL
    {
        private Product _product;

        public ProductDAL(string id, string name, int price, int quantity,int stockEntryQuantity, DateTime stockEntryDate, DateTime? soldOutDate = null)
        {
            this._product = new Product(id, name, price, quantity, stockEntryQuantity, stockEntryDate, soldOutDate);
        }

        public static DataTable GetAllProducts()
        {
            string query = "SELECT * FROM Product";
            return UtilityDatabase.Instance.ExecuteQuery(query);
        }

        public DataTable GetProductByID()
        {
            string query = "SELECT * FROM Product WHERE ID = @ID";
            SqlParameter[] parameters = { new SqlParameter("@ID", _product.ID) };
            return UtilityDatabase.Instance.ExecuteQuery(query, parameters);
        }

        public bool InsertProduct()
        {
            string query = "INSERT INTO Product (ID, Name, Price, Quantity, [Stock Entry Quantity], [Stock Entry Date]) VALUES (@ID, @Name, @Price, @Quantity, @StockEntryQuantity, @StockEntryDate)";
            SqlParameter[] parameters = {
                new SqlParameter("@ID", _product.ID),
                new SqlParameter("@Name", _product.Name),
                new SqlParameter("@Price", _product.Price),
                new SqlParameter("@Quantity", _product.Quantity),
                new SqlParameter("@StockEntryQuantity", _product.StockEntryQuantity),
                new SqlParameter("@StockEntryDate", _product.StockEntryDate)
            };
            int rows = -1;
            try
            {
                rows = UtilityDatabase.Instance.ExecuteNonQuery(query, parameters);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return rows > 0;
        }

        public bool DeleteProduct()
        {
            string query = "DELETE FROM Product WHERE ID = @ID";
            SqlParameter[] parameters = { new SqlParameter("@ID", _product.ID) };
            return UtilityDatabase.Instance.ExecuteNonQuery(query, parameters) > 0;
        }

        public bool UpdateProduct()
        {
            string query = "UPDATE Product SET Name = @Name, Price = @Price, Quantity = @Quantity, [Stock Entry Quantity] = @Stock Entry Quantity, [Stock Entry Date] = @StockEntryDate WHERE ID = @ID";
            SqlParameter[] parameters = {
                new SqlParameter("@Name", _product.Name),
                new SqlParameter("@Price", _product.Price),
                new SqlParameter("@Quantity", _product.Quantity),
                new SqlParameter("@StockEntryQuantity", _product.StockEntryQuantity),
                new SqlParameter("@StockEntryDate", _product.StockEntryDate),
                new SqlParameter("@ID", _product.ID)
            };
            int rows = -1;
            try
            {
                rows = UtilityDatabase.Instance.ExecuteNonQuery(query, parameters);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return rows > 0;
        }

        public bool SoldOut()
        {
            string query = "UPDATE Product SET [Sold Out Date] = GETDATE() WHERE ID = @ID";
            SqlParameter[] parameters = {
                new SqlParameter("@ID", _product.ID)
            };
            return UtilityDatabase.Instance.ExecuteNonQuery(query, parameters) > 0;
        }

        public bool ProductEntry()
        {
            string query = "UPDATE Product SET Quantity = @Quantity, [Stock Entry Quantity] = @StockEntryQuantity, [Stock Entry Date] = GETDATE() WHERE ID = @ID";
            SqlParameter[] parameters = {
                new SqlParameter("@Quantity", _product.Quantity),
                new SqlParameter("@StockEntryQuantity", _product.StockEntryQuantity),
                new SqlParameter("@ID", _product.ID)
            };
            return UtilityDatabase.Instance.ExecuteNonQuery(query, parameters) > 0;
        }

        public static string GenerateProductID()
        {
            string prefix = "P";

            // Câu lệnh SQL để đếm tổng số khách hàng hiện có
            string query = "SELECT COUNT(*) FROM Product";

            // Thực thi query và lấy kết quả
            int count = Convert.ToInt32(UtilityDatabase.Instance.ExecuteQuery(query).Rows[0][0]);

            // Tăng số lượng lên 1 và định dạng số thứ tự 3 chữ số
            string serial = (count + 1).ToString("D3");

            // Trả về ID mới
            return prefix + serial;
        }
    }
}
