namespace HelperKit;

public static partial class Extensions
{
    #region Decimal

    /// <summary>
    /// Converts a value to decimal
    /// </summary>
    /// <param name="obj">object value</param>
    /// <param name="def"></param>
    /// <returns>Decimal</returns>
    public static decimal ToDecimal(this object obj, decimal def = 0)
    {
        return obj is null ? def : ToDecimal(obj.ToString(), def);
    }

    /// <summary>
    /// Converts a string to decimal
    /// </summary>
    /// <param name="str">string value</param>
    /// <param name="def"></param>
    /// <returns>Decimal</returns>
    public static decimal ToDecimal(this string str, decimal def = 0)
    {
        return decimal.TryParse(str, out var result) ? result : def;
    }

    #endregion

    #region Double Convert Helper

    /// <summary>
    /// Converts a value to double
    /// </summary>
    /// <param name="obj">object value</param>
    /// <param name="def"></param>
    /// <returns>double</returns>
    public static double ToDouble(this object obj, double def = 0)
    {
        return obj is null ? def : ToDouble(obj.ToString(), def);
    }

    /// <summary>
    /// Converts a value to double
    /// </summary>
    /// <param name="str">string value</param>
    /// <param name="def"></param>
    /// <returns>double</returns>
    public static double ToDouble(this string str, double def = 0)
    {
        return double.TryParse(str, out var result) ? result : def;
    }

    #endregion

    #region Long Convert Helper

    /// <summary>
    /// Converts a value to long
    /// </summary>
    /// <param name="obj">object value</param>
    /// <param name="def"></param>
    /// <returns>long</returns>
    public static long ToLong(this object obj, long def = 0)
    {
        return obj is null ? def : ToLong(obj.ToString(), def);
    }

    /// <summary>
    /// Converts a string to long
    /// </summary>
    /// <param name="str">string value</param>
    /// <param name="def"></param>
    /// <returns>long</returns>
    public static long ToLong(this string str, long def = 0)
    {
        return long.TryParse(str, out var result) ? result : def;
    }

    #endregion
}
