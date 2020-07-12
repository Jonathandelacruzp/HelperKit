using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace HelperKit
{
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
        /// <param name="array"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static bool HasAny<T>(this IEnumerable<T> array, params T[] values)
        {
            _ = array ?? throw new ArgumentNullException(nameof(array));
            return values.Any(item => array?.Contains(item) == true);
        }

        /// <summary>
        /// Validates if at least one item exist on other collection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static bool IsContainedOn<T>(this T value, params T[] values)
        {
            _ = value ?? throw new ArgumentNullException(nameof(value));

            return typeof(T).GetInterface("IEnumerable") != null
                ? throw new ArgumentException("Requested value could not be an Enumerable.")
                : values.Contains(value);
        }

        #endregion

        /// <summary>
        /// Saves an object as xml file
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="fileName"></param>
        public static void SaveAsXml(this object obj, string fileName)
        {
            _ = fileName ?? throw new ArgumentNullException(nameof(fileName));
            var xmlRequest = new StreamWriter(fileName);
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
    }
}