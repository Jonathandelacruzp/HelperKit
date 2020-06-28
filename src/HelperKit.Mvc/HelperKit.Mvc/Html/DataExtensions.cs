using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace HelperKit.Mvc.Html
{
    public static class DataExtensions
    {
        #region Google

        /// <summary>
        /// Inserta script de Tag manager con AppSetting Google.Analytics
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static IHtmlString GoogleTagManagerScript<TModel>(this DataHelper<TModel> data)
        {
            if (data is null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            var analytics = System.Configuration.ConfigurationManager.AppSettings.Get("Google.Analytics");
            if (string.IsNullOrEmpty(analytics))
                return MvcHtmlString.Empty;

            var tagScriptDeclaration = new TagBuilder("script");
            tagScriptDeclaration.Attributes.Add("src", $"https://www.googletagmanager.com/gtag/js?id={analytics}");
            tagScriptDeclaration.Attributes.Add("async", null);

            var script = new System.Text.StringBuilder();
            script.Append("window.dataLayer = window.dataLayer || [];");
            script.Append("function gtag() { dataLayer.push(arguments) };");
            script.Append("gtag('js', new Date());");
            script.Append("gtag('config', '").Append(analytics).Append("');");

            var tagScript = new TagBuilder("script")
            {
                InnerHtml = script.ToString()
            };

            var result = $"{tagScriptDeclaration.ToString(TagRenderMode.Normal)}{Environment.NewLine}\t{tagScript.ToString(TagRenderMode.Normal)}";
            return new HtmlString(result);
        }

        /// <summary>
        /// AppSettings Google.Captcha.SiteKey
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static IHtmlString GoogleReCaptcha<TModel>(this DataHelper<TModel> data)
        {
            if (data is null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            var siteKey = System.Web.Configuration.WebConfigurationManager.AppSettings.Get("Google.Captcha.SiteKey");
            var div = new TagBuilder("div");
            div.AddCssClass("g-recaptcha");
            div.Attributes.Add("data-sitekey", siteKey);
            return MvcHtmlString.Create(div.ToString(TagRenderMode.Normal));
        }

        /// <summary>
        /// script de llamado a Google Recaptcha https://www.google.com/recaptcha/api.js
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static IHtmlString GoogleRecaptchaScript<TModel>(this DataHelper<TModel> data)
        {
            if (data is null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            var script = new TagBuilder("script");
            script.Attributes.Add("src", "https://www.google.com/recaptcha/api.js");
            return new HtmlString(script.ToString(TagRenderMode.Normal));
        }

        /// <summary>
        /// Invisible Recaptcha button generator con funcion javascript onSubmit
        /// </summary>
        /// <param name="data"></param>
        /// <param name="innerHtml"></param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public static IHtmlString GoogleInvisibleReCaptcha<TModel>(this DataHelper<TModel> data, string innerHtml, object htmlAttributes = null) => GoogleInvisibleReCaptcha(data, innerHtml, string.Empty, "onSubmit", htmlAttributes);

        /// <summary>
        /// Invisible Recaptcha button generator con funcion javascript onSubmit
        /// </summary>
        /// <param name="data"></param>
        /// <param name="innerHtml"></param>
        /// <param name="classNames"></param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public static IHtmlString GoogleInvisibleReCaptcha<TModel>(this DataHelper<TModel> data, string innerHtml, string classNames, object htmlAttributes = null) => GoogleInvisibleReCaptcha(data, innerHtml, classNames, "onSubmit", htmlAttributes);

        /// <summary>
        /// Invisible Recaptcha button generator
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="data"></param>
        /// <param name="innerHtml"></param>
        /// <param name="classNames"></param>
        /// <param name="callBack"></param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public static IHtmlString GoogleInvisibleReCaptcha<TModel>(this DataHelper<TModel> data, string innerHtml, string classNames, string callBack, object htmlAttributes = null)
        {
            if (data is null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            var siteKey = System.Web.Configuration.WebConfigurationManager.AppSettings["Google.Captcha.SiteKey"];
            var button = new TagBuilder("button");
            button.AddCssClass("g-recaptcha");
            if (!string.IsNullOrEmpty(classNames))
                button.AddCssClass(classNames);

            button.Attributes.Add("data-sitekey", siteKey);
            button.Attributes.Add("data-callback", callBack);
            button.InnerHtml = innerHtml;
            if (htmlAttributes != null)
                button.MergeAttributes(new RouteValueDictionary(htmlAttributes));
            return MvcHtmlString.Create(button.ToString(TagRenderMode.Normal));
        }

        #endregion

        #region BitFilter

        public static IHtmlString BitFilter<TModel>(this DataHelper<TModel> data, string blockId = "", string method = "GET") => BitFilter(data, null, null, null, blockId, method);

        public static IHtmlString BitFilter<TModel>(this DataHelper<TModel> data, string action, string blockId = "", string method = "GET") => BitFilter(data, action, null, null, blockId, method);

        public static IHtmlString BitFilter<TModel>(this DataHelper<TModel> data, string action, string controller, string blockId = "", string method = "GET") => BitFilter(data, action, controller, null, blockId, method);

        public static IHtmlString BitFilter<TModel>(this DataHelper<TModel> data, string action, object routeValues, string blockId = "", string method = "GET") => BitFilter(data, action, null, routeValues, blockId, method);

        public static IHtmlString BitFilter<TModel>(this DataHelper<TModel> data, string action, string controller, object routeValues, string blockId = "", string method = "GET")
        {
            var result = $"data-type=\"bit-filter\" data-bf-source-url=\"{data.Url.Action(action, controller, routeValues)}\"";

            if (!string.Equals(method ?? string.Empty, "GET"))
                result += $" data-bf-method=\"{method.ToUpper()}\"";

            if (!String.IsNullOrEmpty(blockId))
                result += $" data-bf-block-id=\"{blockId}\"";

            return new HtmlString(result);
        }

        #endregion
    }
}