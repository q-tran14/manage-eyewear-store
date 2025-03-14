using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DAL.Utils
{
    public class UtilityDatabase
    {
        private static UtilityDatabase _instance;
        private static readonly object _lock = new object();
        private SqlConnection conn;
        private static string _connectMethod;
        private UtilityDatabase()
        {
            try
            {
                _connectMethod = SelectConnectionMethod();
                string connString = ConfigurationManager.ConnectionStrings[_connectMethod].ConnectionString;
                conn = new SqlConnection(connString);
                conn.Open(); //Kết nối database ngay khi app chạy
                Console.WriteLine("Kết nối SQL Server thành công!");
                File.AppendAllText("log.txt", "Kết nối SQL Server thành công!\n");
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi kết nối cơ sở dữ liệu: " + ex.Message);
                //Environment.Exit(1);
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
            if (conn != null && conn.State == ConnectionState.Open) conn.Close();
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
                using (SqlCommand command = new SqlCommand(query, conn))
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
                throw new Exception("Lỗi khi thực thi truy vấn: " + ex.Message);
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
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    if (parameters != null) command.Parameters.AddRange(parameters);

                    return command.ExecuteNonQuery(); // Trả về số dòng bị ảnh hưởng
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi thực thi truy vấn: " + ex.Message);
                return -1; // Trả về -1 nếu lỗi
            }
        }

    }
}
