using System;
using System.Web.Mvc;

namespace HelperKit.Mvc
{
    public static class TempDataExtensions
    {
        public static T Get<T>(this TempDataDictionary tempData, string key)
        {
            if (tempData[key] is T)
            {
                return (T)tempData[key];
            }
            throw new InvalidCastException(string.Format("Temp Data does not contain type {0} for key {1}", typeof(T), key));
        }

        public static void Set<T>(this TempDataDictionary tempData, string key, T value)
        {
            tempData[key] = value;
        }
    }
}