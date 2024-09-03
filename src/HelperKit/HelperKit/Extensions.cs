namespace HelperKit;

/// <summary>
/// Extension functions
/// </summary>
public static partial class Extensions
{
    #region T

    /// <summary>
    /// Validates if an item exist
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="items"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static bool HasAny<T>(this IEnumerable<T> items, params T[] param)
    {
        _ = items ?? throw new ArgumentNullException(nameof(items));
        return Array.Exists(param, items.Contains);
    }

    #endregion

    /// <summary>
    /// Saves an object as xml file
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="fileName"></param>
    /// <exception cref="ArgumentNullException">Thrown when obj is null</exception>
    public static void SaveAsXml(this object obj, string fileName)
    {
        _ = obj ?? throw new ArgumentNullException(nameof(obj));
        _ = fileName ?? throw new ArgumentNullException(nameof(fileName));
        using var xmlRequest = new StreamWriter(fileName);
        var xmlFileRequest = new XmlSerializer(obj.GetType());
        xmlFileRequest.Serialize(xmlRequest, obj);
        xmlRequest.Close();
    }

    /// <summary>
    /// Creates a new Directory validating if the directory exist
    /// </summary>
    /// <param name="directory"></param>
    public static void CreateDirectory(this DirectoryInfo directory)
    {
        if (!directory.Exists)
            directory.Create();
    }

    public static bool TryGetValue<T>(this IResult<T> value, out T result)
    {
        if (value.Value is null)
        {
            result = default;
            return false;
        }

        result = value.Value;
        return true;
    }
}
