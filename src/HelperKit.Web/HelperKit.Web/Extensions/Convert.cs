using System.Web;

namespace HelperKit.Web
{
    public static partial class RazorExtensions
    {
        public static string ToStringJavaScript(this string val) => HttpUtility.JavaScriptStringEncode(val ?? string.Empty);
    }
}