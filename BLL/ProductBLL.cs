using DAL;
using DAL.Object;
using System.Data;

namespace BLL
{
    public class ProductBLL
    {
        private ProductDAL _product;

        public ProductBLL(string id, string name, int price, int quantity, int stockEntryQuantity, DateTime stockEntryDate, DateTime? soldOutDate = null)
        {
            _product = new ProductDAL(id, name, price, quantity, stockEntryQuantity, stockEntryDate, soldOutDate);
        }

        public static DataTable GetAllProducts() => ProductDAL.GetAllProducts();

        public Product GetProductByID() 
        {
            DataTable dt = _product.GetProductByID(); // Gọi xuống DAL
            if (dt.Rows.Count == 0) return null;

            DataRow row = dt.Rows[0];

            return new Product(
                row["ID"].ToString(),
                row["Name"].ToString(),
                Convert.ToInt32(row["Price"]),
                Convert.ToInt32(row["Quantity"]),
                Convert.ToInt32(row["Stock Entry Quantity"]),
                Convert.ToDateTime(row["Stock Entry Date"]),
                row["Sold Out Date"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(row["Sold Out Date"])
            );
        } 

        public bool AddProduct() => _product.InsertProduct();

        public bool DeleteProduct() => _product.DeleteProduct();

        public bool UpdateProduct() => _product.UpdateProduct();

        public bool SoldOut() => _product.SoldOut();

        public bool RestockProduct() => _product.ProductEntry();
        public static string GenerateProductID() => ProductDAL.GenerateProductID();
    }
}
