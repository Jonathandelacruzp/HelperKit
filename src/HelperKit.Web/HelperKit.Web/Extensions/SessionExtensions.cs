using System.Web;
using System.Web.SessionState;

namespace HelperKit.Web
{
    public static class SessionExtensions
    {
        #region Getters setters for T

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static object Get<T>(this HttpSessionState session, T key) where T : struct => session[key.ToString()];

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static object Get<T>(this HttpSessionStateBase session, T key) where T : struct => session[key.ToString()];

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool Exists<T>(this HttpSessionState session, T key) where T : struct => session[key.ToString()] != null;

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool Exists<T>(this HttpSessionStateBase session, T key) where T : struct => session[key.ToString()] != null;

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void Set<T>(this HttpSessionState session, T key, object value) where T : struct
        {
            session[key.ToString()] = value;
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void Set<T>(this HttpSessionStateBase session, T key, object value) where T : struct
        {
            session[key.ToString()] = value;
        }

        #endregion
    }
}