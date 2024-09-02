namespace HelperKit;

public static partial class Extensions
{
    #region int Convert Helper

    /// <summary>
    /// Converts an object to int
    /// </summary>
    /// <param name="obj">object value</param>
    /// <param name="def"></param>
    /// <returns>Int32</returns>
    public static int ToInteger(this object obj, int def = 0)
    {
        return obj is null ? def : ToInteger(obj.ToString(), def);
    }

    /// <summary>
    /// Converts a string to int
    /// </summary>
    /// <param name="str">string value</param>
    /// <param name="def"></param>
    /// <returns>Int32</returns>
    public static int ToInteger(this string str, int def = 0)
    {
        return int.TryParse(str, out var result) ? result : def;
    }

    #endregion
}
