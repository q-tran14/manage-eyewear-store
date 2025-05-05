using DAL;
using System.Data;
using DAL.Object;
namespace BLL
{
    public class EmployeeBLL
    {
        private EmployeeDAL _employee;
        public EmployeeBLL(string id, string name, string email, string password, string streetAddress,
                        string ward, string district, string city, string phone, int salary,
                        string role)
        {
            _employee = new EmployeeDAL(id, name, email, password, streetAddress, ward, district, city, phone, salary, role);
        }
        public static DataTable GetAllEmployee() => EmployeeDAL.GetAllEmployees();
        public Employee GetEmployeeByID()
        {
            DataRow emp = _employee.GetEmployeeByID().Rows.Count > 0 ? _employee.GetEmployeeByID().Rows[0] : null;
            if (emp == null) return null;
            return new Employee(
                emp["ID"].ToString(),
                emp["Name"].ToString(),
                emp["Email"].ToString(),
                emp["Password"].ToString(),
                emp["Street Address"].ToString(),
                emp["Ward"].ToString(),
                emp["District"].ToString(),
                emp["City"].ToString(),
                emp["Phone"].ToString(),
                Convert.ToInt32(emp["Salary"]),
                emp["Role"].ToString(),
                Convert.ToDateTime(emp["Date Employed"])
            );
        } 
        public bool AddEmployee() => _employee.InsertEmployee();
        public bool DeleteEmployee() => _employee.DeleteEmployee();
        public bool UpdateEmployeeInformation() => _employee.UpdateInfoEmployee();
        public bool UpdateEmployeePassword() => _employee.UpdatePasswordEmployee();
        public static string AutoGenerationID(string role, string? phone = "") => EmployeeDAL.GenerateEmployeeID(role, phone);
        public static Employee Authentication(string username, string password)
        {
            DataRow empRow = EmployeeDAL.GetEmployeeByMailOrPhone(username).Rows.Count > 0 ? EmployeeDAL.GetEmployeeByMailOrPhone(username).Rows[0] : null;
            if (empRow == null) return null;
            if (empRow["Password"].ToString() != password) return null;

            string iD = empRow["Id"].ToString()!;
            string name = empRow["Name"].ToString()!;
            string email = empRow["Email"].ToString()!;
            string pass = empRow["Password"].ToString()!;
            string streetAddress = empRow["Street Address"].ToString()!;
            string ward = empRow["Ward"].ToString()!;
            string district = empRow["District"].ToString()!;
            string city = empRow["City"].ToString()!;
            string phone = empRow["Phone"].ToString()!;
            int salary = int.Parse(empRow["Salary"].ToString()!);
            string role = empRow["Role"].ToString()!;
            DateTime dateEmployed = DateTime.Parse(empRow["Date Employed"].ToString()!);
            Employee emp = new Employee(iD,name,email,pass,streetAddress,ward,district,city,phone,salary,role,dateEmployed);
            return emp;
        }

        public static bool ChangePassword(string username, string password)
        {
            DataRow empRow = EmployeeDAL.GetEmployeeByMailOrPhone(username).Rows.Count > 0 ? EmployeeDAL.GetEmployeeByMailOrPhone(username).Rows[0] : null;
            if (empRow == null) return false;

            string iD = empRow["Id"].ToString()!;
            string name = empRow["Name"].ToString()!;
            string email = empRow["Email"].ToString()!;
            string pass = password;
            string streetAddress = empRow["Street Address"].ToString()!;
            string ward = empRow["Ward"].ToString()!;
            string district = empRow["District"].ToString()!;
            string city = empRow["City"].ToString()!;
            string phone = empRow["Phone"].ToString()!;
            int salary = int.Parse(empRow["Salary"].ToString()!);
            string role = empRow["Role"].ToString()!;

            bool success = new EmployeeDAL(iD, name, email, pass, streetAddress, ward, district, city, phone, salary, role).UpdatePasswordEmployee();
            if (!success) return false;
            return true;
        }

        public static bool ExistedEmail(string email)
        {
            return EmployeeDAL.GetEmployeeByMailOrPhone(email).Rows.Count > 0;
        }

        public static bool CheckExistedPhone(string phone)
        {
            return EmployeeDAL.GetEmployeeByPhone(phone).Rows.Count > 0;
        }

        public static bool CheckExistedEmail(string email)
        {
            return EmployeeDAL.GetEmployeeByMail(email).Rows.Count > 0;
        }
    }
}