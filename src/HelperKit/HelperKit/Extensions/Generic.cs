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
        public static T ToEnum<T>(this string value) where T : struct, IConvertible => (T)Enum.Parse(typeof(T), value, true);

        /// <summary>
        /// Gets the enums values and names as Distionary
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IDictionary<int, string> EnumNamedValues<T>() where T : struct, IConvertible
        {
            var result = new Dictionary<int, string>();
            var values = Enum.GetValues(typeof(T));

            foreach (int item in values)
                result.Add(item, Enum.GetName(typeof(T), item));
            return result;
        }

        #endregion

        #region Dictionary Convert Helper

        /// <summary>
        /// Retorna un objeto según su tipo de dato (T) guardado en un diccionario,
        /// en caso el objeto no exista se crea una nueva instancia del mismo
        /// </summary>
        /// <param name="dictionary">Diccionario tipo string-object</param>
        /// <param name="key">Nombre del Key a buscar en el TempData</param>
        /// <returns>Object</returns>
        public static T ToValue<T>(this IDictionary<string, object> dictionary, string key)
        {
            _ = key ?? throw new ArgumentNullException(nameof(key));

            var temp = dictionary[key];
            return temp != null ? (T)temp : (T)Activator.CreateInstance(typeof(T));
        }

        ///// <summary>
        /////
        ///// </summary>
        ///// <param name="namevalueCollection"></param>
        ///// <param name="name"></param>
        ///// <param name="def">Default value</param>
        ///// <returns></returns>
        //public static string GetValue(this NameValueCollection namevalueCollection, string name, string def = "") => namevalueCollection[name] ?? def;
        public static NameValueCollection ToNameValueCollection<T>(this T t)
        {
            var nameValueCollection = new NameValueCollection();
            foreach (PropertyDescriptor propertyDescriptor in TypeDescriptor.GetProperties(t))
            {
                var value = propertyDescriptor.GetValue(t).ToString();
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
                var value = propertyDescriptor.GetValue(t).ToString();
                keyPair.Add(new KeyValuePair<string, string>(propertyDescriptor.Name, value));
            }
            return keyPair;
        }

        /// <summary>
        /// Convert any object to dictionary
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static IDictionary<string, object> ToDictionary(this object obj) =>
            obj?.GetType()
            .GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
            .Where(x => x.CanRead || x.CanWrite)
            .ToDictionary(x => x.Name, x => x.GetValue(obj, null));

        #endregion

        #region XML

        public static string SerializeObjectToXML<T>(this T t)
        {
            var xmlDoc = new XmlDocument();
            var xmlSerializer = new XmlSerializer(t.GetType());

            using MemoryStream xmlStream = new MemoryStream();
            var xmlns = new XmlSerializerNamespaces();
            xmlns.Add(string.Empty, string.Empty);

            xmlSerializer.Serialize(xmlStream, t, xmlns);
            xmlStream.Position = 0;
            xmlDoc.Load(xmlStream);
            return xmlDoc.InnerXml.Replace("<?xml version=\"1.0\"?>", String.Empty);
        }

        public static T DeserializerXMLToObject<T>(this string xml)
        {
            T instance;
            var xmlSerializer = new XmlSerializer(typeof(T));
            using (var stringReader = new StringReader(xml))
            {
                instance = (T)xmlSerializer.Deserialize(stringReader);
            }
            return instance;
        }

        public static string ConvertObjectToXMLText<T>(this T t)
        {
            var typeName = t.GetType().Name;
            var propertyInfos = t.GetType().GetProperties();

            var strBuilder = new StringBuilder();
            strBuilder.Append('<').Append(typeName);
            for (var i = 0; i < propertyInfos.Length; i++)
            {
                PropertyInfo propertyInfo = propertyInfos[i];
                string startTag = $"<{propertyInfo.Name}>", endTag = $"</{propertyInfo.Name}>";
                strBuilder.Append(startTag).Append(propertyInfo.GetValue(t, null)).Append(endTag);
            }
            strBuilder.Append("</").Append(typeName);
            return strBuilder.ToString();
        }

        #endregion
    }
}