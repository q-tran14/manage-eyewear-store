using DAL.Object;
using DAL.Utils;
using System.Data.SqlClient;
using System.Data;

namespace DAL
{
    public class CustomerDAL
    {
        private Customer _customer;

        public CustomerDAL(string id, string name, string phone)
        {
            this._customer = new Customer(id, name, phone);
        }

        public static DataTable GetAllCustomers()
        {
            string query = "SELECT * FROM Customer";
            return UtilityDatabase.Instance.ExecuteQuery(query);
        }

        public DataTable GetCustomerByID()
        {
            string query = "SELECT * FROM Customer WHERE ID = @ID";
            SqlParameter[] parameters = { new SqlParameter("@ID", _customer.ID) };
            return UtilityDatabase.Instance.ExecuteQuery(query, parameters);
        }
        public static DataTable GetCustomerByName(string username)
        {
            string query = "SELECT * FROM Customer WHERE Name = @Username";
            SqlParameter[] parameters = { new SqlParameter("@Username", username) };
            return UtilityDatabase.Instance.ExecuteQuery(query, parameters);
        }
        public bool InsertCustomer()
        {
            string query = "INSERT INTO Customer (ID, Name, Phone) VALUES (@ID, @Name, @Phone)";
            SqlParameter[] parameters = {
                new SqlParameter("@ID", _customer.ID),
                new SqlParameter("@Name", _customer.Name),
                new SqlParameter("@Phone", _customer.Phone)
            };
            return UtilityDatabase.Instance.ExecuteNonQuery(query, parameters) > 0;
        }

        public bool DeleteCustomer()
        {
            string query = "DELETE FROM Customer WHERE ID = @ID";
            SqlParameter[] parameters = { new SqlParameter("@ID", _customer.ID) };
            return UtilityDatabase.Instance.ExecuteNonQuery(query, parameters) > 0;
        }

        public bool UpdateCustomer()
        {
            string query = "UPDATE Customer SET Name = @Name, Phone = @Phone WHERE ID = @ID";
            SqlParameter[] parameters = {
                new SqlParameter("@Name", _customer.Name),
                new SqlParameter("@Phone", _customer.Phone),
                new SqlParameter("@ID", _customer.ID)
            };
            return UtilityDatabase.Instance.ExecuteNonQuery(query, parameters) > 0;
        }

        public static string GenerateCustomerID()
        {
            string prefix = "C";

            // Câu lệnh SQL để đếm tổng số khách hàng hiện có
            string query = "SELECT COUNT(*) FROM Customer";

            // Thực thi query và lấy kết quả
            int count = Convert.ToInt32(UtilityDatabase.Instance.ExecuteQuery(query).Rows[0][0]);

            // Tăng số lượng lên 1 và định dạng số thứ tự 3 chữ số
            string serial = (count + 1).ToString("D3");

            // Trả về ID mới
            return prefix + serial;
        }
    }
}
