using System.Web;

namespace HelperKit.Mvc
{
    public static partial class ConvertHelper
    {
        public static string ToStringJavaScript(this string val) => HttpUtility.JavaScriptStringEncode(val ?? string.Empty);
    }
}