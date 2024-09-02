namespace HelperKit;

public static partial class Extensions
{
    private const char Slash = '/';
    private const char BackSlash = '\\';
    private const char Dot = '.';
    private const char Comma = ',';
    private const string Nbsp = @"\u00A0";
    private const int MaxStackLimit = 256;

    #region String

    /// <summary>
    /// Removes diacritics
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string RemoveDiacritics(this string str)
    {
        var normalizedString = str.Normalize(NormalizationForm.FormD);
        var sb = new StringBuilder();
        foreach (var c in normalizedString)
        {
            var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
            if (unicodeCategory is not UnicodeCategory.NonSpacingMark)
                sb.Append(c);
        }

        return sb.ToString().Normalize(NormalizationForm.FormC);
    }

    /// <summary>
    /// Replace all NonBreaking Space (char 160) to a simple space
    /// </summary>
    /// <param name="str"></param>
    /// <param name="def"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static string ReplaceNonBreakingSpace(this string str, string def = " ")
    {
        _ = str ?? throw new ArgumentNullException(nameof(str));
        return str.Replace(Nbsp, def);
    }

    /// <summary>
    /// Deletes all slash / y backslash \ from string
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string DeleteSlashAndBackslash(this string str)
    {
        return str?.Replace("/", string.Empty).Replace(@"\", string.Empty);
    }

    /// <summary>
    /// Deletes all dot and commas of a string
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string DeleteDotAndComma(this string str)
    {
        return str?.Replace(".", string.Empty).Replace(",", string.Empty);
    }

    /// <summary>
    /// Replace all the values set it on param with an empty string
    /// </summary>
    /// <param name="str"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public static string CustomReplaceOn(this string str, params string[] param)
    {
        return param.Aggregate(str, (current, item) => current.Replace(item, string.Empty));
    }

    #endregion

    #region String Convert Helper

    /// <summary>
    /// Returns a string with safe mode
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="def"></param>
    /// <returns>string</returns>
    public static string ToSafeString(this object obj, string def = "")
    {
        return (obj ?? def).ToString();
    }

    #endregion

    /// <summary>
    /// Determine if a specified value have the same value ignoring the casing
    /// </summary>
    /// <param name="str"></param>
    /// <param name="find"></param>
    /// <returns>bool</returns>
    public static bool EqualsIgnoreCase(this string str, string find)
    {
        if (str is null && find is null)
            return true;

        return (str ?? "").Equals(find, StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    /// Determine if a specified value contains has any occurence in a string value
    /// </summary>
    /// <param name="str"></param>
    /// <param name="find"></param>
    /// <param name="sc"></param>
    /// <returns>bool</returns>
    public static bool ContainsValue(this string str, string find, StringComparison sc = StringComparison.OrdinalIgnoreCase)
    {
        if (string.IsNullOrEmpty(str) || string.IsNullOrEmpty(find))
            return false;

        return str.IndexOf(find, sc) >= 0;
    }
}
