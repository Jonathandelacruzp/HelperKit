using System.Web.Mvc;

namespace HelperKit.Mvc.Html
{
    public static class UrlExtensions
    {
        #region ContentUrl Helpers

        public static string ContentUrl(this UrlHelper url, string contentRoute, string areaName = null)
        {
            var contentBase = "~/" + (!string.IsNullOrEmpty(areaName) ? $"Areas/{areaName}/Content/" : "Content/");
            return url.Content(contentBase + contentRoute);
        }

        public static string ContentUrl(this UrlHelper url, string contentRoute) => ContentUrl(url, contentRoute, null);

        #endregion

        #region AbsoluteAction Helper

        /// <summary>
        /// Genera la url ansoluta del la accion
        /// especificando el controller y atributos adicionales.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="actionName">Nombre de la accion</param>
        /// <param name="controllerName">Nombre del controlador</param>
        /// <param name="routeValues">route values</param>
        /// <returns>URL Absoluta</returns>
        public static string AbsoluteAction(this UrlHelper url, string actionName, string controllerName, object routeValues = null)
        {
            var scheme = url.RequestContext.HttpContext.Request.Url.Scheme;
            return url.Action(actionName, controllerName, routeValues, scheme);
        }

        /// <summary>
        /// Genera la url ansoluta del la accion
        /// especificando el controller y atributos adicionales.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="actionName"></param>
        /// <param name="routeValues"></param>
        /// <returns></returns>
        public static string AbsoluteAction(this UrlHelper url, string actionName, object routeValues = null) => AbsoluteAction(url, actionName, null, routeValues);

        /// <summary>
        /// Genera la url ansoluta del la accion
        /// especificando el controller y atributos adicionales.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="actionName"></param>
        /// <param name="controllerName"></param>
        /// <returns></returns>
        public static string AbsoluteAction(this UrlHelper url, string actionName, string controllerName) => AbsoluteAction(url, actionName, controllerName, null);

        #endregion

        #region Conten With Cache

        /// <summary>
        ///
        /// </summary>
        /// <param name="url"></param>
        /// <param name="contentPath"></param>
        /// <param name="hasCache"></param>
        /// <returns></returns>
        public static string Content(this UrlHelper url, string contentPath, bool hasCache = false)
        {
            if (hasCache)
                contentPath = HtmlExtensions.CacheTag(contentPath);
            return url.Content(contentPath);
        }

        #endregion
    }
}