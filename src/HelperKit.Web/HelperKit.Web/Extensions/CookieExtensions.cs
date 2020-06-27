using System;
using System.Web;

namespace HelperKit.Web
{
    public static class CookieExtensions
    {
        public static int CookieTime => System.Configuration.ConfigurationManager.AppSettings.Get("COOKIE_TIME").ToInteger(24);

        #region Get & Set

        /// <summary>
        /// Use AppSetting COOKIE_TIME
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cookie"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="cookieTime"></param>
        public static void Set<T>(this HttpCookieCollection cookie, T key, object value, int? cookieTime = null)
        {
            if (value == null)
            {
                value = string.Empty;
            }
            var httpCookie = new HttpCookie(key.ToString())
            {
                Value = value.ToString(),
                Expires = DateTime.Now.AddHours(cookieTime ?? CookieTime),
                HttpOnly = true
            };
            cookie.Remove(key.ToString());
            cookie.Add(httpCookie);
        }

        public static HttpCookie GetCookie<T>(this HttpCookieCollection cookie, T key)
        {
            if (Exists(cookie, key))
            {
                return cookie.Get(key.ToString());
            }
            return null;
        }

        public static string GetValue<T>(this HttpCookieCollection cookie, T key)
        {
            try
            {
                var httpCookie = GetCookie(cookie, key.ToString());
                return httpCookie.Value;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public static Boolean Exists<T>(this HttpCookieCollection cookie, T key)
        {
            return cookie[key.ToString()] != null;
        }

        #endregion

        #region Delete

        public static void Delete<T>(this HttpCookieCollection cookie, T key)
        {
            if (Exists(cookie, key))
            {
                var httpCookie = new HttpCookie(key.ToString()) { Expires = DateTime.Now.AddDays(-1) };
                cookie.Remove(key.ToString());
                cookie.Add(httpCookie);
            }
        }

        public static void DeleteAll(this HttpCookieCollection cookie)
        {
            for (int i = 0; i <= cookie.Count - 1; i++)
            {
                var name = cookie[i].Name;
                if (!String.IsNullOrEmpty(cookie[i].Value))
                {
                    Delete(cookie, name);
                }
            }
        }

        #endregion
    }
}