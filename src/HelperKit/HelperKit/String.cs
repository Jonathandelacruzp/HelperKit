﻿namespace HelperKit;

public static partial class Extensions
{
    private const char Slash = '/';
    private const char BackSlash = '\\';
    private const char Dot = '.';
    private const char Comma = ',';
    
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
        foreach (var character in normalizedString)
        {
            var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(character);
            if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                strBuilder.Append(character);
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
        var nbsp = Convert.ToChar(160).ToString();
        return value.Replace(nbsp, def);
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
    /// Deletes all slash / y backslash \ from string
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string DeleteSlashAndBackslash(this ReadOnlySpan<char> value)
    {
        Span<char> result = stackalloc char[value.Length];
        var j = 0;

        for (var i = 0; i < value.Length; i++)
        {
            if (value[i] is Slash or BackSlash)
                continue;

            result[j] = value[i];
            j++;
        }

        return result.Slice(0, j).ToString();
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
    /// Deletes all dot and commas of a string
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string DeleteDotAndComma(this ReadOnlySpan<char> value)
    {
        Span<char> result = stackalloc char[value.Length];
        var j = 0;

        for (var i = 0; i < value.Length; i++)
        {
            if (value[i] is Dot or Comma)
                continue;

            result[j] = value[i];
            j++;
        }

        return result.Slice(0, j).ToString();
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