using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
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
        /// <returns></returns>
        public static T ToEnum<T>(this string value) where T : Enum
        {
            return (T) Enum.Parse(typeof(T), value, true);
        }

        /// <summary>
        /// Gets the enums values and names as Dictionary
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IDictionary<int, string> EnumNamedValues<T>() where T : Enum
        {
            var values = Enum.GetValues(typeof(T));
            return values.Cast<int>().ToDictionary(item => item, item => Enum.GetName(typeof(T), item));
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
        public static T ToValue<T>(this IDictionary<string, object> dictionary, string key)
        {
            _ = key ?? throw new ArgumentNullException(nameof(key));

            return dictionary.TryGetValue(key, out var temp)
                ? (T) temp
                : (T) Activator.CreateInstance(typeof(T));
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
        /// <param name="val"></param>
        /// <returns></returns>
        public static NameValueCollection ToNameValueCollection<T>(this T val) where T : class
        {
            var nameValueCollection = new NameValueCollection();
            foreach (PropertyDescriptor propertyDescriptor in TypeDescriptor.GetProperties(val))
            {
                var value = propertyDescriptor.GetValue(val)?.ToString();
                if (value != null)
                    nameValueCollection.Add(propertyDescriptor.Name, value);
            }

            return nameValueCollection;
        }

        /// <summary>
        /// Converts to List of key value Pair
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="val"></param>
        /// <returns></returns>
        public static ICollection<KeyValuePair<string, string>> ToKeyValuePair<T>(this T val) where T : class
        {
            var keyPair = new List<KeyValuePair<string, string>>();
            foreach (PropertyDescriptor propertyDescriptor in TypeDescriptor.GetProperties(val))
            {
                var objValue = propertyDescriptor.GetValue(val)?.ToString();
                if (objValue != null)
                    keyPair.Add(new KeyValuePair<string, string>(propertyDescriptor.Name, objValue));
            }

            return keyPair;
        }

        /// <summary>
        /// Converts a class object to dictionary
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static IDictionary<string, object> ToDictionary<T>(this T val) where T : class
        {
            return val?.GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                .Where(x => x.CanRead || x.CanWrite)
                .ToDictionary(x => x.Name, x => x.GetValue(val, null));
        }

        #endregion

        #region XML

        /// <summary>
        /// Serialize an object to xml
        /// </summary>
        /// <param name="val"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static string SerializeObjectToXml<T>(this T val) where T : class
        {
            var xmlDoc = new XmlDocument();
            var xmlSerializer = new XmlSerializer(val.GetType());

            using var xmlStream = new MemoryStream();
            var xmlns = new XmlSerializerNamespaces();
            xmlns.Add(string.Empty, string.Empty);

            xmlSerializer.Serialize(xmlStream, val, xmlns);
            xmlStream.Position = 0;
            xmlDoc.Load(xmlStream);
            return xmlDoc.InnerXml.Replace("<?xml version=\"1.0\"?>", string.Empty);
        }

        /// <summary>
        /// Deserialize an string to and object
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static T DeserializeXmlToObject<T>(this string xml) where T : class
        {
            var xmlSerializer = new XmlSerializer(typeof(T));
            using var stringReader = new StringReader(xml);
            return (T) xmlSerializer.Deserialize(stringReader);
        }

        /// <summary>
        /// Converts an object to xml text
        /// </summary>
        /// <param name="val"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static string ConvertObjectToXmlString<T>(this T val) where T : class
        {
            var typeName = val.GetType().Name;
            var propertyInfos = val.GetType().GetProperties();

            var strBuilder = new StringBuilder();
            strBuilder.Append('<').Append(typeName).Append('>');
            foreach (var propertyInfo in propertyInfos.Where(x => x.CanRead))
                strBuilder.Append('<').Append(propertyInfo.Name).Append('>')
                    .Append(propertyInfo.GetValue(val, null)?.ToString() ?? string.Empty)
                    .Append("</").Append(propertyInfo.Name).Append('>');

            strBuilder.Append("</").Append(typeName).Append('>');
            return strBuilder.ToString();
        }

        #endregion
    }
}