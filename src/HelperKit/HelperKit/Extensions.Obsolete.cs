namespace HelperKit;

public static partial class Extensions
{
    #region String

    ///  <summary>
    /// Converts to string UTF
    ///  </summary>
    ///  <param name="str"></param>
    ///  <exception cref="ArgumentNullException"></exception>
    ///  <returns></returns>
    [Obsolete]
    [ExcludeFromCodeCoverage]
    public static string ToStringUtf8(this string str)
    {
        _ = str ?? throw new ArgumentNullException(nameof(str));
        return Encoding.UTF8.GetString(Encoding.GetEncoding(1252).GetBytes(str));
    }

    //[Obsolete]
    //public static string ToStringJavaScript(this string val) => HttpUtility.JavaScriptStringEncode(val ?? string.Empty);

    //public static string ToJson(this object value)
    //  => JsonConvert.SerializeObject(value, Formatting.Indented, new JsonSerializerSettings() { PreserveReferencesHandling = PreserveReferencesHandling.None, ReferenceLoopHandling = ReferenceLoopHandling.Ignore, ObjectCreationHandling = ObjectCreationHandling.Reuse });

    /// <summary>
    /// Deletes all slash / y backslash \ from string
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    [Obsolete("Use carefully, the stack limit is 256")]
    internal static string DeleteSlashAndBackslash(this ReadOnlySpan<char> value)
    {
        var length = value.Length;
        var result = length <= MaxStackLimit
            ? stackalloc char[length]
            : new char[length];
        var j = 0;

        for (var i = 0; i < length; i++)
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
    [Obsolete("Use carefully, the stack limit is 256")]
    internal static string DeleteDotAndComma(this ReadOnlySpan<char> value)
    {
        var length = value.Length;
        var result = length <= MaxStackLimit
            ? stackalloc char[length]
            : new char[length];
        var j = 0;

        for (var i = 0; i < length; i++)
        {
            if (value[i] is Dot or Comma)
                continue;

            result[j] = value[i];
            j++;
        }

        return result.Slice(0, j).ToString();
    }

    #endregion
}
