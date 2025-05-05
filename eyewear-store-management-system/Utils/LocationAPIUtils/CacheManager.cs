using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace eyewear_store_management_system.Utils
{
    public class CacheManager
    {
        private static readonly string _cacheFilePath = GetCacheFilePath();
        private static readonly TimeSpan _cacheLifeSpan = TimeSpan.FromMinutes(5); // Hết hạn sau 5 phút
        private static Dictionary<string, (JArray Data, DateTime Expiry)> _cache = new();

        public CacheManager()
        {
            LoadCacheFromFile();
        }

        private static string GetCacheFilePath()
        {
            var (envPath, _) = Program.FindEnvPathNearExe(); // Cập nhật đúng namespace/class
            string otherDir = Path.GetDirectoryName(envPath); // Lấy thư mục chứa .env (chính là "Others")

            if (!Directory.Exists(otherDir))
                Directory.CreateDirectory(otherDir);

            return Path.Combine(otherDir, "cache.json");
        }

        private void LoadCacheFromFile()
        {
            if (File.Exists(_cacheFilePath))
            {
                try
                {
                    string json = File.ReadAllText(_cacheFilePath);
                    var storedCache = JsonConvert.DeserializeObject<Dictionary<string, (JArray Data, DateTime Expiry)>>(json);

                    if (storedCache != null) _cache = storedCache;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Lỗi khi đọc cache: {ex.Message}");
                }
            }
        }

        public void SetCache(string key, JArray data)
        {
            _cache[key] = (data, DateTime.Now.Add(_cacheLifeSpan));
            SaveCacheToFile();
        }

        private void SaveCacheToFile()
        {
            try
            {
                string json = JsonConvert.SerializeObject(_cache, Formatting.Indented);
                File.WriteAllText(_cacheFilePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi lưu cache: {ex.Message}");
            }
        }

        public bool IsCacheValid(string key)
        {
            return _cache.ContainsKey(key) && _cache[key].Expiry > DateTime.Now;
        }

        public JArray GetCache(string key)
        {
            return IsCacheValid(key) ? _cache[key].Data : null;
        }

        public void InvalidateCache()
        {
            _cache.Clear();
            SaveCacheToFile();
        }
    }
}
