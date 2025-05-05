using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
namespace DAL.Utils
{
    public class UtilityDatabase
    {
        private static UtilityDatabase? _instance;
        private static readonly object _lock = new object();
        private SqlConnection _conn;
        private static string? _connectMethod;
        private UtilityDatabase()
        {
            _connectMethod = SelectConnectionMethod();
            string connString = ConfigurationManager.ConnectionStrings[_connectMethod].ConnectionString;
            Debug.WriteLine(connString);
            try
            {
                _conn = new SqlConnection(connString);
                _conn.Open(); //Kết nối database ngay khi app chạy
                Debug.WriteLine("Kết nối SQL Server thành công!");
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public static UtilityDatabase Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null) _instance = new UtilityDatabase();
                    }
                }
                return _instance;
            }
        }
        public void CloseConnection()
        {
            if (_conn != null && _conn.State == ConnectionState.Open)
            {
                _conn.Close();
                Debug.WriteLine("Đóng kết nối - " + _conn.State.ToString());
            } 
        }

        private static string SelectConnectionMethod()
        {
            string? connectionType = ConfigurationManager.AppSettings["ConnectionType"];

            if (!string.IsNullOrEmpty(connectionType)) return connectionType;

            return "WinAuthConnection";
        }

        // Dành cho SELECT query
        // string query = "SELECT * FROM Users WHERE Username = @username";
        // SqlParameter[] parameters = { new SqlParameter("@username", "admin") };
        // DataTable dt = Database.Instance.ExecuteQuery(query, parameters);
        public DataTable ExecuteQuery(string query, SqlParameter[]? parameters = null)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand(query, _conn))
                {
                    if (parameters != null) command.Parameters.AddRange(parameters);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi thực thi truy vấn: - {DateTime.Now}\n{ex.Message}\n");
            }
            return dt;
        }

        // Dành cho INSERT / UPDATE / DELETE query
        // string query = "DELETE FROM Users WHERE Username = @username";
        // SqlParameter[] parameters = { new SqlParameter("@username", "testuser") };
        // int rowsAffected = Database.Instance.ExecuteNonQuery(query, parameters);
        public int ExecuteNonQuery(string query, SqlParameter[]? parameters = null)
        {
            try
            {
                using (SqlCommand command = new SqlCommand(query, _conn))
                {
                    if (parameters != null) command.Parameters.AddRange(parameters);
                    return command.ExecuteNonQuery(); // Trả về số dòng bị ảnh hưởng
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi thực thi truy vấn: - {DateTime.Now}\n{ex.Message}\n");
                // return -1; // Trả về -1 nếu lỗi
            }
        }

    }
}
