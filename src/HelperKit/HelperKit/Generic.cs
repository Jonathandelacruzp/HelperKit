using System.ComponentModel;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;

namespace HelperKit;

public static partial class Extensions
{
    #region Enum Convert Helper

    /// <summary>
    /// Converts to Enum
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    public static T ToEnum<T>(this string value) where T : Enum
    {
        return (T)Enum.Parse(typeof(T), value, true);
    }

    /// <summary>
    /// Gets the enums values and names as Dictionary
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static Dictionary<int, string> EnumNamedValues<T>() where T : Enum
    {
        var values = Enum.GetValues(typeof(T));
        return values.Cast<int>().ToDictionary(x => x, x => Enum.GetName(typeof(T), x));
    }

    #endregion

    private static T CloneSerializableObject<T>(this T value) where T : class
    {
        using var stream = new MemoryStream();
        var formatter = new BinaryFormatter();
        formatter.Serialize(stream, value);
        stream.Position = 0;
        return (T)formatter.Deserialize(stream);
    }

    #region Dictionary Convert Helper

    /// <summary>
    /// Returns the object of type T from a dictionary
    /// </summary>
    /// <param name="dictionary"></param>
    /// <param name="key"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static T ToValue<T>(this IDictionary<string, object> dictionary, string key)
    {
        _ = key ?? throw new ArgumentNullException(nameof(key));

        return dictionary.TryGetValue(key, out var temp)
            ? (T)temp
            : (T)Activator.CreateInstance(typeof(T));
    }

    /// <summary>
    /// Converts an object to named value collection
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    public static NameValueCollection ToNameValueCollection<T>(this T value) where T : class
    {
        var nameValueCollection = new NameValueCollection();
        foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(value))
        {
            var propertyValue = prop.GetValue(value)?.ToString();
            if (propertyValue is not null)
                nameValueCollection.Add(prop.Name, propertyValue);
        }

        return nameValueCollection;
    }

    /// <summary>
    /// Converts object to List of key value Pair
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    public static List<KeyValuePair<string, string>> ToKeyValuePair<T>(this T value) where T : class
    {
        var keyPairs = new List<KeyValuePair<string, string>>();
        foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(value))
        {
            var objValue = prop.GetValue(value)?.ToString();
            if (objValue is not null)
                keyPairs.Add(new KeyValuePair<string, string>(prop.Name, objValue));
        }

        return keyPairs;
    }

    /// <summary>
    /// Converts a class object to dictionary
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static Dictionary<string, object> ToDictionary<T>(this T value) where T : class
    {
        return value?.GetType()
            .GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
            .Where(x => x.CanRead || x.CanWrite)
            .ToDictionary(x => x.Name, x => x.GetValue(value, null));
    }

    #endregion

    #region XML

    /// <summary>
    /// Serialize an object to xml
    /// </summary>
    /// <param name="value"></param>
    /// <param name="includeHeader"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static string SerializeObjectToXml<T>(this T value, bool includeHeader = false) where T : class
    {
        var xmlDoc = new XmlDocument();
        var xmlSerializer = new XmlSerializer(value.GetType());

        using var xmlStream = new MemoryStream();
        var xmlns = new XmlSerializerNamespaces();

        xmlSerializer.Serialize(xmlStream, value, xmlns);
        xmlStream.Position = 0;
        xmlDoc.Load(xmlStream);
        return includeHeader
            ? xmlDoc.InnerXml
            : xmlDoc.InnerXml.Replace("<?xml version=\"1.0\"?>", string.Empty);
    }

    /// <summary>
    /// Deserialize a string to and object
    /// </summary>
    /// <param name="xmlString"></param>
    /// <returns></returns>
    public static T DeserializeXmlToObject<T>(this string xmlString) where T : class
    {
        var xmlSerializer = new XmlSerializer(typeof(T));
        using var reader = new StringReader(xmlString);
        return (T)xmlSerializer.Deserialize(reader);
    }

    #endregion
}
