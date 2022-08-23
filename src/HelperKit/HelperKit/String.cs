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
        var strBuilder = new StringBuilder();
        for (var i = 0; i < normalizedString.Length; i++)
        {
            var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(normalizedString[i]);
            if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                strBuilder.Append(normalizedString[i]);
        }

        return strBuilder.ToString().Normalize(NormalizationForm.FormC);
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
    /// Replace all the values set it on param with a empty string
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
}
