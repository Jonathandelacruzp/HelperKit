namespace HelperKit;

public static partial class Extensions
{
    #region int Convert Helper

    /// <summary>
    /// Converts an object to int
    /// </summary>
    /// <param name="s"></param>
    /// <param name="def"></param>
    /// <returns>Int32</returns>
    public static int ToInteger(this object s, int def = 0)
    {
        return int.TryParse(s?.ToString(), out var result) ? result : def;
    }

    #endregion
}
