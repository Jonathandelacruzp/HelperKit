namespace HelperKit;

public static partial class Extensions
{
    #region bool Convert Helper

    /// <summary>
    /// Converts to Boolean
    /// </summary>
    /// <param name="value"></param>
    /// <param name="def"></param>
    /// <returns></returns>
    public static bool ToBoolean(this string value, bool def = false)
    {
        return bool.TryParse(value, out var result) ? result : def;
    }

    #endregion
}
