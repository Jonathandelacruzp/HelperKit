using System.Globalization;
using System.Text;

namespace HelperKit
{
    public static partial class Extensions
    {
        #region String

        /// <summary>
        /// Removes diacritics
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string RemoveDiacritics(this string val)
        {
            var normalizedString = val.Normalize(NormalizationForm.FormD);
            var strBuilder = new StringBuilder();
            foreach (var character in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(character);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                    strBuilder.Append(character);
            }
            return strBuilder.ToString().Normalize(NormalizationForm.FormC);
        }

        /// <summary>
        /// Replace all NonBreaking Space (char 160) to a simple space
        /// </summary>
        /// <param name="val"></param>
        /// <param name="def"></param>
        /// <returns></returns>
        public static string ReplaceNoBbreakingSpace(this string val, string def = " ")
        {
            var nbsp = System.Convert.ToChar(160).ToString();
            return val.Replace(nbsp, def);
        }

        /// <summary>
        /// Deletes all slash / y backslash \ from string
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string DeleteSlashAndBackslash(this string val) => val.Replace("/", string.Empty).Replace(@"\", string.Empty);

        /// <summary>
        /// Deletes all dot and commas of a string
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string DeleteDotAndComma(this string val)
        {
            val = val.Replace(".", string.Empty);
            val = val.Replace(",", string.Empty);
            return val;
        }

        #endregion

        #region String Convert Helper

        /// <summary>
        /// Returns a string with safe mode
        /// </summary>
        /// <param name="val"></param>
        /// <param name="def"></param>
        /// <returns>string</returns>
        public static string ToSafeString(this object val, string def = "") => (val ?? def).ToString();

        /// <summary>
        ///Converts o string UTF
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string TostringUTF(this string val) => Encoding.UTF8.GetString(Encoding.GetEncoding(1252).GetBytes(val ?? string.Empty));

        //public static string ToStringJavaScript(this string val) => HttpUtility.JavaScriptStringEncode(val ?? string.Empty);

        #endregion

        //public static string ToJson(this object val) => JsonConvert.SerializeObject(val, Formatting.Indented, new JsonSerializerSettings() { PreserveReferencesHandling = PreserveReferencesHandling.None, ReferenceLoopHandling = ReferenceLoopHandling.Ignore, ObjectCreationHandling = ObjectCreationHandling.Reuse });
    }
}