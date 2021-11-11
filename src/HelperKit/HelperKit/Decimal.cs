namespace HelperKit;

public static partial class Extensions
{
    #region Decimal

    /// <summary>
    /// Converts a value to decimal
    /// </summary>
    /// <param name="value"></param>
    /// <param name="def"></param>
    /// <returns>Decimal</returns>
    public static decimal ToDecimal(this object value, decimal def = 0)
    {
        return decimal.TryParse(value?.ToString(), out var result) ? result : def;
    }

    #endregion

    #region Double Convert Helper

    /// <summary>
    /// Converts a value to double
    /// </summary>
    /// <param name="value">Decimal</param>
    /// <param name="def"></param>
    /// <returns>long</returns>
    public static double ToDouble(this object value, long def = 0)
    {
        return double.TryParse(value?.ToString(), out var result) ? result : def;
    }

    #endregion

    #region Long Convert Helper

    /// <summary>
    /// Converts a value to long
    /// </summary>
    /// <param name="value">Decimal</param>
    /// <param name="def"></param>
    /// <returns>long</returns>
    public static long ToLong(this object value, long def = 0)
    {
        return long.TryParse(value?.ToString(), out var result) ? result : def;
    }

    #endregion
}
