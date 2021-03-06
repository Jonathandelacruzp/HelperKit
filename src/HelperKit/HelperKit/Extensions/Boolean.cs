﻿namespace HelperKit
{
    public static partial class Extensions
    {
        #region bool Convert Helper

        /// <summary>
        /// Converts to Boolean
        /// </summary>
        /// <param name="val"></param>
        /// <param name="def"></param>
        /// <returns></returns>
        public static bool ToBoolean(this string val, bool def = false)
        {
            return bool.TryParse(val, out var result) ? result : def;
        }

        #endregion
    }
}