using System.Xml;
using System.Configuration;
using System.Diagnostics;
using eyewear_store_management_system.Utils;

namespace eyewear_store_management_system
{
    internal static class Program
    {
        public static List<KeyValuePair<string, string>> cities;

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            ApplicationConfiguration.Initialize();

            var citiesArray = LocationByAPI.GetCities().GetAwaiter().GetResult();

            if (citiesArray != null)
            {
                cities = OtherUtilities.ConvertToKeyValueList(citiesArray);
            }

            Application.Run(new LoginForm());
        }

        public static void UpdateAppConfig(string connectionName, string connectionString)
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

        public static (string path, bool existed) FindEnvPathNearExe()
        {
            // Lấy đường dẫn thư mục chứa file .exe
            string exeDir = AppDomain.CurrentDomain.BaseDirectory;
            // Debug.WriteLine(exeDir);
            // Ghép đường dẫn đến thư mục Other
            string otherDir = Path.Combine(exeDir, "Others");

            // Ghép đường dẫn đến file .env
            string envPath = Path.Combine(otherDir, ".env");

            return File.Exists(envPath)? (envPath, true) : (envPath, false);
        }
    }
}