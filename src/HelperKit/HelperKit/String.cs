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
    /// <param name="value"></param>
    /// <returns></returns>
    public static string RemoveDiacritics(this string value)
    {
        var normalizedString = value.Normalize(NormalizationForm.FormD);
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
    /// <param name="value"></param>
    /// <param name="def"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static string ReplaceNonBreakingSpace(this string value, string def = " ")
    {
        _ = value ?? throw new ArgumentNullException(nameof(value));
        return value.Replace(Nbsp, def);
    }

    /// <summary>
    /// Deletes all slash / y backslash \ from string
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string DeleteSlashAndBackslash(this string value)
    {
        return value?.Replace("/", string.Empty).Replace(@"\", string.Empty);
    }


    /// <summary>
    /// Deletes all dot and commas of a string
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string DeleteDotAndComma(this string value)
    {
        return value?.Replace(".", string.Empty).Replace(",", string.Empty);
    }

    /// <summary>
    /// Replace all the values set it on param with an empty string
    /// </summary>
    /// <param name="value"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public static string CustomReplaceOn(this string value, params string[] param)
    {
        return param.Aggregate(value, (current, item) => current.Replace(item, string.Empty));
    }

    #endregion

    #region String Convert Helper

    /// <summary>
    /// Returns a string with safe mode
    /// </summary>
    /// <param name="value"></param>
    /// <param name="def"></param>
    /// <returns>string</returns>
    public static string ToSafeString(this object value, string def = "")
    {
        return (value ?? def).ToString();
    }

    #endregion

    /// <summary>
    /// Returns a string with safe mode
    /// </summary>
    /// <param name="value"></param>
    /// <param name="find"></param>
    /// <returns>bool</returns>
    public static bool EqualsIgnoreCase(this string value, string find)
    {
        if (value is null && find is null)
            return true;

        return (value ?? "").Equals(find, StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    /// Returns a string with safe mode
    /// </summary>
    /// <param name="value"></param>
    /// <param name="find"></param>
    /// <param name="sc"></param>
    /// <returns>bool</returns>
    public static bool ContainsValue(this string value, string find, StringComparison sc = StringComparison.OrdinalIgnoreCase)
    {
        if (value is null || find is null)
            return false;

        return value.IndexOf(find, sc) is not -1;
    }
}
