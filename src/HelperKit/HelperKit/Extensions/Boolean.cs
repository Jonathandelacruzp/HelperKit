using System;

namespace HelperKit
{
    public static partial class Extensions
    {
        #region bool Convert Helper

        /// <summary>
        /// Convetrs to Bolean
        /// </summary>
        /// <param name="val"></param>
        /// <param name="def"></param>
        /// <returns></returns>
        public static bool ToBoolean(this string val, bool def = false)
        {
            if (Boolean.TryParse(val, out var reval))
                return reval;

            return def;
        }

        #endregion
    }
}