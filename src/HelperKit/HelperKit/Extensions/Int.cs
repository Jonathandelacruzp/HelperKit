using System;

namespace HelperKit
{
    public static partial class Extensions
    {
        #region int Convert Helper

        /// <summary>
        /// Converts an object to int
        /// </summary>
        /// <param name="val"></param>
        /// <param name="def"></param>
        /// <returns>Int32</returns>
        public static int ToInteger(this object val, int def)
        {
            if (Int32.TryParse(val?.ToString(), out var reval))
                return reval;

            return def;
        }

        /// <summary>
        /// Converts an object to int
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static int ToInteger(this object val) => ToInteger(val, 0);

        #endregion
    }
}