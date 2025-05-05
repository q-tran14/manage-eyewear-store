using DAL;
using DAL.Object;
using System.Data;
namespace BLL
{
    public class CustomerBLL
    {
        private CustomerDAL _customer;
        public CustomerBLL(string id, string name, string phone)
        {
            _customer = new CustomerDAL(id, name, phone);
        }
        public static DataTable GetAllCustomers() => CustomerDAL.GetAllCustomers();
        public Customer GetCustomerByID()
        {

            DataTable dt = _customer.GetCustomerByID(); // Gọi xuống DAL để lấy theo ID
            if (dt.Rows.Count == 0) return null;

            DataRow row = dt.Rows[0];

            return new Customer(
                row["ID"].ToString(),
                row["Name"].ToString(),
                row["Phone"].ToString()
            );
        }
        public bool AddCustomer() => _customer.InsertCustomer();
        public bool DeleteCustomer() => _customer.DeleteCustomer();
        public bool UpdateCustomer() => _customer.UpdateCustomer();
        public static string AutoGenerateCustomerID() => CustomerDAL.GenerateCustomerID();
    }
}
