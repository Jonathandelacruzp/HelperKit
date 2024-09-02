namespace HelperKit;

public static partial class Extensions
{
    #region bool Convert Helper

    /// <summary>
    /// Converts to Boolean
    /// </summary>
    /// <param name="str"></param>
    /// <param name="def"></param>
    /// <returns></returns>
    public static bool ToBoolean(this string str, bool def = false)
    {
        return bool.TryParse(str, out var result) ? result : def;
    }

    #endregion
}
