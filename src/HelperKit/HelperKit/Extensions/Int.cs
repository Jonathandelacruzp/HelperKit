namespace HelperKit
{
    public static partial class Extensions
    {
        #region int Convert Helper

        /// <summary>
        /// Converts an object to int
        /// </summary>
        /// <param name="value"></param>
        /// <param name="def"></param>
        /// <returns>Int32</returns>
        public static int ToInteger(this object value, int def = 0)
        {
            return int.TryParse(value?.ToString(), out var result) ? result : def;
        }

        #endregion
    }
}