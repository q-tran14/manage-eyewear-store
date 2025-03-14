using System;
using System.IO;
using DotNetEnv;
using System.Xml;
using System.Configuration;
using DAL.Utils;

namespace eyewear_store_management_system
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Xác định đường dẫn thư mục gốc chứa file .sln
            string solutionDirectory = FindSolutionDirectory();
            string envPath = Path.Combine(solutionDirectory, ".env");

            Env.Load(envPath);
            string server = Env.GetString("DB_SERVER");
            string database = Env.GetString("DB_NAME");
            string username = Env.GetString("DB_USERNAME");
            string password = Env.GetString("DB_PASSWORD");

            string connectMethod = "";
            string connectionString = "";
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                connectMethod = "WinAuthConnection";
                // Connection string cho Windows Authentication
                connectionString = $"Server={server};Database={database};Integrated Security=True;";
            }
            else
            {
                connectMethod = "SqlAuthConnection";
                // Connection string cho SQL Server Authentication
                connectionString = $"Server={server};Database={database};User Id={username};Password={password};";
            }
            // Cập nhật App.config
            UpdateAppConfig(connectMethod, connectionString);

            // Khởi tạo kết nối SQL Server
            UtilityDatabase db = UtilityDatabase.Instance;

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new LoginForm());

            // Đóng kết nối tới server khi app kết thúc
            db.CloseConnection();
        }

        static void UpdateAppConfig(string connectionName, string connectionString)
        {
            string configPath = System.Reflection.Assembly.GetExecutingAssembly().Location + ".config";

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(configPath);

            // Cập nhật Connection String
            XmlNode node = xmlDoc.SelectSingleNode($"//connectionStrings/add[@name='{connectionName}']");
            if (node != null)
            {
                XmlAttribute attr = node.Attributes["connectionString"];
                if (attr != null)
                {
                    attr.Value = connectionString;
                }
            }

            // Cập nhật Connection Type trong <appSettings>
            XmlNode appSettingsNode = xmlDoc.SelectSingleNode("//appSettings");
            if (appSettingsNode != null)
            {
                XmlNode connectionTypeNode = appSettingsNode.SelectSingleNode("add[@key='ConnectionType']");
                if (connectionTypeNode == null)
                {
                    // Key ko tồn tại, thêm mới
                    XmlElement newElem = xmlDoc.CreateElement("add");
                    newElem.SetAttribute("key", "ConnectionType");
                    newElem.SetAttribute("value", connectionName);
                    appSettingsNode.AppendChild(newElem);
                }
                else
                {
                    // Key tồn tai, cập nhật giá trị mới
                    XmlAttribute attr = connectionTypeNode.Attributes["value"];
                    if (attr != null)
                    {
                        attr.Value = connectionName;
                    }
                }
            }

            xmlDoc.Save(configPath);
            ConfigurationManager.RefreshSection("connectionStrings");
            ConfigurationManager.RefreshSection("appSettings"); // Làm mới cấu hình
        }

        static string FindSolutionDirectory()
        {
            string currentDir = AppDomain.CurrentDomain.BaseDirectory;

            while (!Directory.EnumerateFiles(currentDir, "*.sln").Any()) // Tìm file .sln
            {
                DirectoryInfo parent = Directory.GetParent(currentDir);
                if (parent == null) return null;
                currentDir = parent.FullName;
            }
            return currentDir;
        }
    }
}