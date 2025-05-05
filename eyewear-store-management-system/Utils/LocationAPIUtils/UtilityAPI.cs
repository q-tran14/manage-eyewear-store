using System;
using System.IO;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Security.Authentication;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace eyewear_store_management_system.Utils
{
    public class UtilityAPI
    {
        public static async Task<JArray> FetchDataFromApi(string apiUrl)
        {
            // Kiểm tra URL hợp lệ
            if (string.IsNullOrEmpty(apiUrl))
            {
                ShowError("Lỗi: URL API không hợp lệ!");
                return null;
            }

            // Kiểm tra kết nối Internet trước khi gọi API
            if (!IsInternetConnected())
            {
                ShowError("Không thể kết nối Internet.");
                return null;
            }

            try
            {
                // Cấu hình HttpClient để hỗ trợ TLS 1.2 và TLS 1.3
                HttpClientHandler handler = new HttpClientHandler{ SslProtocols = SslProtocols.Tls12 | SslProtocols.Tls13 };

                using (HttpClient client = new HttpClient(handler))
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    response.EnsureSuccessStatusCode();
                    string json = await response.Content.ReadAsStringAsync();

                    JObject jsonResponse = JObject.Parse(json);

                    // Kiểm tra API có trả về dữ liệu hợp lệ không
                    if (!jsonResponse.ContainsKey("data") || jsonResponse["data"] == null || !jsonResponse["data"].HasValues)
                    {
                        ShowError("Lỗi: API không chứa dữ liệu hợp lệ!");
                        return null;
                    }

                    JToken dataToken = jsonResponse["data"]["data"];

                    if (dataToken == null || !dataToken.HasValues)
                    {
                        ShowError("Lỗi: Không tìm thấy dữ liệu!");
                        return null;
                    }

                    return (JArray)dataToken;
                }
            }
            catch (HttpRequestException ex)
            {
                ShowError($"Lỗi HTTP: {ex.Message}");
                return null;
            }
            catch (Exception ex)
            {
                ShowError($"Lỗi hệ thống: {ex.Message}");
                return null;
            }
        }

        // Kiểm tra kết nối Internet
        private static bool IsInternetConnected()
        {
            try
            {
                return NetworkInterface.GetIsNetworkAvailable();
            }
            catch
            {
                return false;
            }
        }

        // Hiển thị thông báo lỗi và ghi log
        private static void ShowError(string message)
        {
            File.AppendAllText("log.txt", $"{message} - {DateTime.Now}\n");
            throw new Exception(message);
        }
    }
}
