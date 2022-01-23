using System.Xml.Serialization;

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
        return param.Any(items.Contains);
    }

    /// <summary>
    /// Validates if at least one item exist on other collection
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value"></param>
    /// <param name="items"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">Thrown when value is null</exception>
    /// <exception cref="ArgumentException">Thrown when obj is null</exception>
    public static bool IsContainedIn<T>(this T value, IEnumerable<T> items)
    {
        _ = value ?? throw new ArgumentNullException(nameof(value));

        return typeof(T).GetInterface("IEnumerable") == null
            ? items?.Any(x => x.Equals(value)) == true
            : throw new ArgumentException("Requested value could not be an Enumerable.");
    }

    /// <summary>
    /// Validates if at least one item exist on other collection
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public static bool IsContainedIn<T>(this T value, params T[] param)
    {
        return IsContainedIn(value, param as IEnumerable<T>);
    }

    /// <summary>
    /// Validates if at least one item exist on other collection
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    [Obsolete("Use IsContainedIn intead")]
    public static bool IsContainedOn<T>(this T value, params T[] param)
    {
        return IsContainedIn(value, param as IEnumerable<T>);
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
    /// Creates a new Directory
    /// </summary>
    /// <param name="directory"></param>
    public static void CreateDirectory(this DirectoryInfo directory)
    {
        if (!directory.Exists)
            directory.Create();
    }

    public static bool TryGetResult<T>(this IDataResponse<T> value, out T result)
    {
        if (value.Result is null)
        {
            result = default;
            return false;
        }

        result = value.Result;
        return true;
    }
}
