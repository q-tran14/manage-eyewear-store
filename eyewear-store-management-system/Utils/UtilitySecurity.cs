using System;
using System.Security.Cryptography;
using System.Text;


namespace eyewear_store_management_system.Utils
{
    public static class UtilitySecurity
    {
        public static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                // Chuyển đổi chuỗi thành mảng byte
                byte[] bytes = Encoding.UTF8.GetBytes(password);

                // Băm
                byte[] hash = sha256.ComputeHash(bytes);

                // Chuyển byte[] thành chuỗi Hex
                StringBuilder sb = new StringBuilder();
                foreach (byte b in hash)
                {
                    sb.Append(b.ToString("x2")); // x2 => hex format
                }
                return sb.ToString();
            }
        }
    }
}
