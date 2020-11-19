using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;

namespace HelperKit
{
    public static partial class Extensions
    {
        #region Enum Convert Helper

        /// <summary>
        /// Converts to Enum
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="def"></param>
        /// <returns></returns>
        public static T ToEnum<T>(this string value, T def = default) where T : struct, IConvertible
        {
            try
            {
                return (T) Enum.Parse(typeof(T), value, true);
            }
            catch (Exception)
            {
                return def;
            }
        }

        /// <summary>
        /// Gets the enums values and names as Dictionary
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IDictionary<int, string> EnumNamedValues<T>() where T : struct, IConvertible
        {
            var values = Enum.GetValues(typeof(T));
            return values.Cast<int>().ToDictionary(item => item, item => Enum.GetName(typeof(T), item));
        }

        #endregion

        #region Dictionary Convert Helper

        /// <summary>
        /// Returns the object of type T
        /// </summary>
        /// <param name="dictionary"></param>
        /// <param name="key"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T ToValue<T>(this IDictionary<string, object> dictionary, string key)
        {
            _ = key ?? throw new ArgumentNullException(nameof(key));

            var temp = dictionary[key];
            return temp != null ? (T) temp : (T) Activator.CreateInstance(typeof(T));
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
        /// <param name="t"></param>
        /// <returns></returns>
        public static NameValueCollection ToNameValueCollection<T>(this T t)
        {
            var nameValueCollection = new NameValueCollection();
            foreach (PropertyDescriptor propertyDescriptor in TypeDescriptor.GetProperties(t))
            {
                var value = propertyDescriptor.GetValue(t)?.ToString();
                if (value != null)
                    nameValueCollection.Add(propertyDescriptor.Name, value);
            }

            return nameValueCollection;
        }

        /// <summary>
        /// Converts to List of key value Pair
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static ICollection<KeyValuePair<string, string>> ToKeyValuePair<T>(this T t)
        {
            var keyPair = new List<KeyValuePair<string, string>>();
            foreach (PropertyDescriptor propertyDescriptor in TypeDescriptor.GetProperties(t))
            {
                var value = propertyDescriptor.GetValue(t)?.ToString();
                if (value != null)
                    keyPair.Add(new KeyValuePair<string, string>(propertyDescriptor.Name, value));
            }

            return keyPair;
        }

        /// <summary>
        /// Convert any object to dictionary
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static IDictionary<string, object> ToDictionary(this object obj)
        {
            return obj?.GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                .Where(x => x.CanRead || x.CanWrite)
                .ToDictionary(x => x.Name, x => x.GetValue(obj, null));
        }

        /// <summary>
        /// Convert any object to dictionary with Type
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static IDictionary<string, Tuple<Type, object>> ToDictionaryWithType(this object obj)
        {
            return obj?.GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                .Where(x => x.CanRead || x.CanWrite)
                .ToDictionary(x => x.Name, x => new Tuple<Type, object>(x.PropertyType, x.GetValue(obj, null)));
        }

        #endregion

        #region XML

        /// <summary>
        /// Serialize an object to xml
        /// </summary>
        /// <param name="t"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static string SerializeObjectToXml<T>(this T t)
        {
            var xmlDoc = new XmlDocument();
            var xmlSerializer = new XmlSerializer(t.GetType());

            using var ms = new MemoryStream();
            var xmlns = new XmlSerializerNamespaces();
            xmlns.Add(string.Empty, string.Empty);

            xmlSerializer.Serialize(ms, t, xmlns);
            ms.Position = 0;
            xmlDoc.Load(ms);
            return xmlDoc.InnerXml.Replace("<?xml version=\"1.0\"?>", string.Empty);
        }

        /// <summary>
        /// Deserialize an string to and object
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static T DeserializeXmlToObject<T>(this string xml)
        {
            var xmlSerializer = new XmlSerializer(typeof(T));
            using var stringReader = new StringReader(xml);
            var instance = (T) xmlSerializer.Deserialize(stringReader);

            return instance;
        }

        // /// <summary>
        // /// Converts an object to xml text
        // /// </summary>
        // /// <param name="t"></param>
        // /// <typeparam name="T"></typeparam>
        // /// <returns></returns>
        // public static string ConvertObjectToXmlString<T>(this T t)
        // {
        //     var typeName = t.GetType().Name;
        //     var propertyInfos = t.GetType().GetProperties();
        //
        //     var sb = new StringBuilder();
        //     sb.Append("<").Append(typeName).AppendLine(">");
        //     foreach (var propertyInfo in propertyInfos)
        //     {
        //         string startTag = $"<{propertyInfo.Name}>", endTag = $"</{propertyInfo.Name}>";
        //         sb.Append(startTag).Append(propertyInfo.GetValue(t, null)).AppendLine(endTag);
        //     }
        //
        //     sb.Append("</").Append(typeName).Append(">");
        //     return sb.ToString();
        // }

        #endregion
    }
}