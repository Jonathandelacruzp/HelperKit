using System;
using System.Web;

namespace HelperKit.Web.Extensions
{
    public static partial class Extensions
    {
        /// <summary>
        /// CACHE_EXPIRATION
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="key"></param>
        /// <param name="expirationMinutes">CacheExpirationMinutes WebConfig Key</param>
        public static void Add<T>(T t, string key, int expirationMinutes = 1440) where T : class
        {
            var cacheTime = System.Configuration.ConfigurationManager.AppSettings.Get("CACHE_EXPIRATION").ToInteger(expirationMinutes);
            HttpContext.Current.Cache.Insert(key, t, null, DateTime.Now.AddMinutes(cacheTime), System.Web.Caching.Cache.NoSlidingExpiration);
        }

        public static void Clear(string key)
        {
            try
            {
                HttpContext.Current.Cache.Remove(key);
            }
            catch
            {
                return;
            }
        }

        public static void Replace<T>(T t, string key) where T : class
        {
            Clear(key);
            Add<T>(t, key);
        }

        public static bool Exists(string key) => HttpContext.Current.Cache[key] != null;

        public static T Get<T>(string key) where T : class
        {
            try
            {
                return (T)HttpContext.Current.Cache[key];
            }
            catch
            {
                return null;
            }
        }
    }
}