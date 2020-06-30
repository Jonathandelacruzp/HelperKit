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
        public static int ToInteger(this object val, int def = 0)
        {
            return int.TryParse(val?.ToString(), out var result) ? result : def;
        }

        #endregion
    }
}