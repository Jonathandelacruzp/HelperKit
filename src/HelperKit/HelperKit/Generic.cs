using System.ComponentModel;
using System.Data;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;

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
    public static IDictionary<int, string> EnumNamedValues<T>() where T : Enum
    {
        var values = Enum.GetValues(typeof(T));
        return values.Cast<int>().ToDictionary(x => x, x => Enum.GetName(typeof(T), x));
    }

    #endregion

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

    // /// <summary>
    // ///
    // /// </summary>
    // /// <param name="nameValueCollection"></param>
    // /// <param name="name"></param>
    // /// <param name="def"></param>
    // /// <returns></returns>
    // public static string GetValue(this NameValueCollection nameValueCollection, string name, string def = "")
    // {
    //     return nameValueCollection[name] ?? def;
    // }

    /// <summary>
    /// Converts an object to named value collection
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    public static NameValueCollection ToNameValueCollection<T>(this T value) where T : class
    {
        var nameValueCollection = new NameValueCollection();
        foreach (PropertyDescriptor propertyDescriptor in TypeDescriptor.GetProperties(value))
        {
            var propertyValue = propertyDescriptor.GetValue(value)?.ToString();
            if (propertyValue != null)
                nameValueCollection.Add(propertyDescriptor.Name, propertyValue);
        }

        return nameValueCollection;
    }

    /// <summary>
    /// Converts to List of key value Pair
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    public static ICollection<KeyValuePair<string, string>> ToKeyValuePair<T>(this T value) where T : class
    {
        var keyPair = new List<KeyValuePair<string, string>>();
        foreach (PropertyDescriptor propertyDescriptor in TypeDescriptor.GetProperties(value))
        {
            var objValue = propertyDescriptor.GetValue(value)?.ToString();
            if (objValue != null)
                keyPair.Add(new KeyValuePair<string, string>(propertyDescriptor.Name, objValue));
        }

        return keyPair;
    }

    /// <summary>
    /// Converts a class object to dictionary
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static IDictionary<string, object> ToDictionary<T>(this T value) where T : class
    {
        return value?.GetType()
            .GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
            .Where(x => x.CanRead || x.CanWrite)
            .ToDictionary(x => x.Name, x => x.GetValue(value, null));
    }

    /// <summary>
    /// Converts an Object to Datatable
    /// </summary>
    /// <param name="result"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static DataTable ToDataTable<T>(this object result)
    {
        return result is IEnumerable results
            ? results.ToDataTable<T>()
            : new[] { result }.ToDataTable<T>();
    }

    /// <summary>
    /// Converts an IEnumerable object to Datatable
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="items"></param>
    /// <returns></returns>
    /// <exception cref="MissingFieldException"></exception>
    public static DataTable ToDataTable<T>(this IEnumerable<T> items) where T : class
    {
        var props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
        if (props.Length == 0)
            throw new MissingFieldException("The implemented type doesn't have valid fields");

        var dataTable = new DataTable(typeof(T).Name);
        foreach (var prop in props)
            dataTable.Columns.Add(prop.Name, prop.PropertyType);

        foreach (var item in items)
        {
            var values = new object[props.Length];
            for (var i = 0; i < props.Length; i++)
                values[i] = props[i].GetValue(item, null);

            dataTable.Rows.Add(values);
        }

        return dataTable;
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
        //xmlns.Add(string.Empty, string.Empty);

        xmlSerializer.Serialize(xmlStream, value, xmlns);
        xmlStream.Position = 0;
        xmlDoc.Load(xmlStream);
        return includeHeader
            ? xmlDoc.InnerXml
            : xmlDoc.InnerXml.Replace("<?xml version=\"1.0\"?>", string.Empty);
    }

    /// <summary>
    /// Deserialize an string to and object
    /// </summary>
    /// <param name="xmlString"></param>
    /// <returns></returns>
    public static T DeserializeXmlToObject<T>(this string xmlString) where T : class
    {
        var xmlSerializer = new XmlSerializer(typeof(T));
        using var reader = new StringReader(xmlString);
        return (T)xmlSerializer.Deserialize(reader);
    }

    /// <summary>
    /// Converts an object to xml text
    /// </summary>
    /// <param name="value"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    [Obsolete("Use SerializeObjectToXml<T> instead")]
    public static string ConvertObjectToXmlString<T>(this T value) where T : class
    {
        var typeName = value.GetType().Name;
        var propertyInfos = value.GetType().GetProperties();

        var strBuilder = new StringBuilder();
        strBuilder.Append('<').Append(typeName).Append('>');
        foreach (var propertyInfo in propertyInfos.Where(x => x.CanRead))
        {
            strBuilder.Append('<').Append(propertyInfo.Name).Append('>')
               .Append(propertyInfo.GetValue(value, null)?.ToString() ?? string.Empty)
               .Append("</").Append(propertyInfo.Name).Append('>');
        }

        strBuilder.Append("</").Append(typeName).Append('>');
        return strBuilder.ToString();
    }

    #endregion
}
