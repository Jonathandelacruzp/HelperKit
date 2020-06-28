using System;
using System.Web.Mvc;

namespace HelperKit.Mvc
{
    public static class ViewDataExtensions
    {
        public static T Get<T>(this ViewDataDictionary viewData, string key)
        {
            if (viewData[key] is T)
            {
                var viewDataItem = (T)viewData[key];
                return viewDataItem;
            }
            throw new InvalidCastException(string.Format("View Data does not contain type {0} for key {1}", typeof(T), key));
        }

        public static void Set<T>(this ViewDataDictionary viewData, string key, T value)
        {
            viewData[key] = value;
        }
    }
}