using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace eyewear_store_management_system.Utils
{
    public static class LocationByAPI
    {
        private static readonly CacheManager _cacheManager = new CacheManager();

        public static async Task<JArray> GetCities(bool forceRefresh = false)
        {
            string cacheKey = "cities";
            if (!forceRefresh && _cacheManager.IsCacheValid(cacheKey)) return _cacheManager.GetCache(cacheKey);

            string apiUrl = ConfigurationManager.AppSettings["City"];
            JArray cities = await UtilityAPI.FetchDataFromApi(apiUrl);

            if (cities != null) _cacheManager.SetCache(cacheKey, cities);

            return cities;
        }

        public static async Task<JArray> GetDistricts(string cityCode, bool forceRefresh = false)
        {
            string cacheKey = $"districts_{cityCode}";
            
            if (!forceRefresh && _cacheManager.IsCacheValid(cacheKey)) return _cacheManager.GetCache(cacheKey);

            string apiTemplate = ConfigurationManager.AppSettings["District"];
            string apiUrl = string.Format(apiTemplate, cityCode);
            JArray districts = await UtilityAPI.FetchDataFromApi(apiUrl);

            if (districts != null) _cacheManager.SetCache(cacheKey, districts);

            return districts;
        }

        public static async Task<JArray> GetWards(string districtCode, bool forceRefresh = false)
        {
            string cacheKey = $"wards_{districtCode}";

            if (!forceRefresh && _cacheManager.IsCacheValid(cacheKey)) return _cacheManager.GetCache(cacheKey);

            string apiTemplate = ConfigurationManager.AppSettings["Ward"];
            string apiUrl = string.Format(apiTemplate, districtCode);
            JArray wards = await UtilityAPI.FetchDataFromApi(apiUrl);

            if (wards != null) _cacheManager.SetCache(cacheKey, wards);

            return wards;
        }

        // Invalidate toàn bộ cache nếu cần
        public static void InvalidateCache()
        {
            _cacheManager.InvalidateCache();
        }
    }
}
