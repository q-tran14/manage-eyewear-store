using DAL.Utils;
using System.Data.SqlClient;
using System.Data;
using DAL.Object;

namespace DAL
{
    public class EmployeeDAL
    {
        private Employee _emp;

        public EmployeeDAL(string id, string name, string email, string password, string streetAddress,
                        string ward, string district, string city, string phone, int salary,
                        string role)
        {
            this._emp = new Employee(id, name, email, password, streetAddress, ward, district, city, phone, salary, role, DateTime.Now);
        }

        public static DataTable GetAllEmployees()
        {
            string query = "SELECT ID, [Name], Email, [Street Address], Ward, District, City, Phone, Salary, [Role], [Date Employed] FROM Employee";

            return UtilityDatabase.Instance.ExecuteQuery(query);
        }

        public DataTable GetEmployeeByID()
        {
            string query = "SELECT * FROM Employee WHERE ID = @ID";
            SqlParameter[] parameters = { new SqlParameter("@ID", _emp.ID) };
            return UtilityDatabase.Instance.ExecuteQuery(query, parameters);
        }

        public bool InsertEmployee()
        {
            string query = "INSERT INTO Employee (ID, [Name], Email, [Password], [Street Address], Ward, District, City, Phone, Salary, [Role]) VALUES (@ID, @Name, @Email, @Password, @Street, @Ward, @District, @City, @Phone, @Salary, @Role)";
            SqlParameter[] parameters = {
                new SqlParameter("@ID", _emp.ID),
                new SqlParameter("@Name", _emp.Name),
                new SqlParameter("@Email", _emp.Email),
                new SqlParameter("@Password", _emp.Password),
                new SqlParameter("@Street", _emp.StreetAddress),
                new SqlParameter("@Ward", _emp.Ward),
                new SqlParameter("@District", _emp.District),
                new SqlParameter("@City", _emp.City),
                new SqlParameter("@Phone", _emp.Phone),
                new SqlParameter("@Salary", _emp.Salary),
                new SqlParameter("@Role", _emp.Role),
                new SqlParameter("@DateEmployed", _emp.DateEmployed)
            };
            return UtilityDatabase.Instance.ExecuteNonQuery(query, parameters) > 0;
        }

        public bool DeleteEmployee()
        {
            string query = "DELETE FROM Employee WHERE ID = @ID";
            SqlParameter[] parameters = { new SqlParameter("@ID", _emp.ID) };
            return UtilityDatabase.Instance.ExecuteNonQuery(query, parameters) > 0;
        }

        public bool UpdateInfoEmployee()
        {
            string query = "UPDATE Employee SET Name=@Name, Email=@Email, [Street Address]=@Street, Ward=@Ward, District=@District, City=@City, Phone=@Phone, Salary=@Salary, Role=@Role, [Date Employed]=@DateEmployed WHERE ID=@ID";
            SqlParameter[] parameters = {
                new SqlParameter("@Name", _emp.Name),
                new SqlParameter("@Email", _emp.Email),
                new SqlParameter("@Street", _emp.StreetAddress),
                new SqlParameter("@Ward", _emp.Ward),
                new SqlParameter("@District", _emp.District),
                new SqlParameter("@City", _emp.City),
                new SqlParameter("@Phone", _emp.Phone),
                new SqlParameter("@Salary", _emp.Salary),
                new SqlParameter("@Role", _emp.Role),
                new SqlParameter("@DateEmployed", _emp.DateEmployed),
                new SqlParameter("@ID", _emp.ID)
            };
            return UtilityDatabase.Instance.ExecuteNonQuery(query, parameters) > 0;
        }

        public bool UpdatePasswordEmployee()
        {
            string query = "UPDATE Employee SET Password=@Password WHERE ID=@ID";
            SqlParameter[] parameters = {
                new SqlParameter("@Password", _emp.Password),
                new SqlParameter("@ID", _emp.ID)
            };
            return UtilityDatabase.Instance.ExecuteNonQuery(query, parameters) > 0;
        }

        public static string GenerateEmployeeID(string role, string? phone = "")
        {
            // Xác định prefix dựa trên role
            string prefix = role == "Staff" ? "S" : "M";

            // Lấy 4 số cuối số điện thoại
            string last4Digits = phone == "" ? "0000" : phone[^4..];

            // Đếm tổng số nhân viên, không phân biệt role
            string query = "SELECT COUNT(*) FROM Employee";

            // Không cần parameter vì không lọc theo role
            int count = Convert.ToInt32(UtilityDatabase.Instance.ExecuteQuery(query).Rows[0][0]);

            // Tăng số lượng lên 1 và định dạng số thứ tự
            string serial = (count + 1).ToString("D3");

            // Ghép lại thành ID mới
            return prefix + serial + last4Digits;
        }

        public static DataTable GetEmployeeByMailOrPhone(string username)
        {
            string query = "SELECT * FROM Employee WHERE Phone = @Username OR Email = @Username";
            SqlParameter[] parameters = { new SqlParameter("@Username", username) };
            return UtilityDatabase.Instance.ExecuteQuery(query, parameters);
        }

        public static DataTable GetEmployeeByMail(string email)
        {
            string query = "SELECT * FROM Employee WHERE Email = @Username";
            SqlParameter[] parameters = { new SqlParameter("@Username", email) };
            return UtilityDatabase.Instance.ExecuteQuery(query, parameters);
        }

        public static DataTable GetEmployeeByPhone(string phone)
        {
            string query = "SELECT * FROM Employee WHERE Phone = @Username";
            SqlParameter[] parameters = { new SqlParameter("@Username", phone) };
            return UtilityDatabase.Instance.ExecuteQuery(query, parameters);
        }
    }
}