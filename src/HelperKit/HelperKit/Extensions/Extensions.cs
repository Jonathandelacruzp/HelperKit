using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace HelperKit
{
    /// <summary>
    /// Funciones de Extension
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
        public static Boolean HasAny<T>(this IEnumerable<T> array, params T[] values)
        {
            foreach (var item in values)
            {
                if (array.Contains(item))
                    return true;
            }
            return false;
        }

        ///// <summary>
        /////
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="value"></param>
        ///// <param name="values"></param>
        ///// <returns></returns>
        //public static Boolean IsContainedOn<T>(this T value, params T[] values)
        //{
        //    if (typeof(T).GetInterface("IEnumerable") != null)
        //        throw new ArgumentException("Requested value could not be an Enumerable.");

        //    foreach (var item in values)
        //        if (value.Equals((T)item))
        //            return true;

        //    return false;
        //}

        /// <summary>
        /// Validates if at least one item exist on other collection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static bool IsContainedOn<T>(this T value, IEnumerable<T> values)
        {
            _ = value ?? throw new ArgumentNullException(nameof(value));

            if (typeof(T).GetInterface("IEnumerable") != null)
                throw new ArgumentException("Requested value could not be an Enumerable.");

            return values.Contains(value);
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

            if (typeof(T).GetInterface("IEnumerable") != null)
                throw new ArgumentException("Requested value could not be an Enumerable.");

            return values.Contains(value);
        }

        #endregion

        /// <summary>
        /// Guarda archivo xml dependiendo del objeto y el tipo definido.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="fileName"></param>
        public static void SaveAsXML(this object obj, string fileName)
        {
            _ = fileName ?? throw new ArgumentNullException(nameof(fileName));
            var xmlRequest = new StreamWriter(fileName);
            var xmlFileRequest = new XmlSerializer(obj.GetType());
            xmlFileRequest.Serialize(xmlRequest, obj);
            xmlRequest.Close();
        }

        /// <summary>
        /// Crea nuevo directorio
        /// </summary>
        /// <param name="directory"></param>
        public static void CreateDirectory(this DirectoryInfo directory)
        {
            if (!directory.Exists)
                directory.Create();
        }
    }
}