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
        /// <param name="items"></param>
        /// <param name="param"></param>
        /// <returns></returns>
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
        public static bool IsContainedOn<T>(this T value, IEnumerable<T> items)
        {
            _ = value ?? throw new ArgumentNullException(nameof(value));

            return typeof(T).GetInterface("IEnumerable") != null
                ? throw new ArgumentException("Requested value could not be an Enumerable.")
                : items.Any(x => x.Equals(value));
        }

        /// <summary>
        /// Validates if at least one item exist on other collection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static bool IsContainedOn<T>(this T value, params T[] param)
        {
            return IsContainedOn(value, param as IEnumerable<T>);
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
    }
}