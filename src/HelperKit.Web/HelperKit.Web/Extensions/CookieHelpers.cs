using System;
using System.Web;

namespace HelperKit.Web.Extensions
{
    public class CookieHelpers
    {
        protected static HttpContext Context => HttpContext.Current;
        protected static int CookieTime => System.Configuration.ConfigurationManager.AppSettings.Get("COOKIE_TIME").ToInteger(24);

        #region Get & Set

        public static void Set<T>(T key, object value, int? cookieTime = null)
        {
            if (value == null)
            {
                value = string.Empty;
            }
            var cookie = new HttpCookie(key.ToString())
            {
                Value = value.ToString(),
                Expires = DateTime.Now.AddHours(cookieTime ?? CookieTime),
                HttpOnly = true
            };
            Context.Response.Cookies.Remove(key.ToString());
            Context.Response.Cookies.Add(cookie);
        }

        public static HttpCookie GetCookie<T>(T key)
        {
            if (Exists(key))
            {
                return Context.Request.Cookies.Get(key.ToString());
            }
            return null;
        }

        public static string GetValue<T>(T key)
        {
            try
            {
                var cookie = GetCookie(key.ToString());
                return cookie.Value;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public static Boolean Exists<T>(T key)
        {
            return Context.Request.Cookies[key.ToString()] != null;
        }

        #endregion

        #region Delete

        public static void Delete<T>(T key)
        {
            if (Exists(key))
            {
                var cookie = new HttpCookie(key.ToString()) { Expires = DateTime.Now.AddDays(-1) };
                Context.Response.Cookies.Remove(key.ToString());
                Context.Response.Cookies.Add(cookie);
            }
        }

        public static void DeleteAll()
        {
            for (int i = 0; i <= Context.Request.Cookies.Count - 1; i++)
            {
                var name = Context.Request.Cookies[i].Name;
                if (!String.IsNullOrEmpty(Context.Request.Cookies[i].Value))
                {
                    Delete(name);
                }
            }
        }

        #endregion
    }
}