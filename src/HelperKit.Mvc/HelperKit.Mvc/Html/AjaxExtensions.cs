using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

namespace HelperKit.Mvc.Html
{
    public static class AjaxExtensions
    {
        #region Ajax

        /// <summary>
        /// Returns an html element that contains the URL to the specified action method;
        /// when the action link is clicked, the action method is invoked asynchronously
        /// by using JavaScript.
        /// </summary>
        /// <param name="ajax"></param>
        /// <param name="innerHtml"></param>
        /// <param name="actionName"></param>
        /// <param name="controllerName"></param>
        /// <param name="routeValues"></param>
        /// <param name="ajaxOptions"></param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public static IHtmlString RawActionLink(this AjaxHelper ajax, string innerHtml, string actionName, string controllerName, object routeValues, AjaxOptions ajaxOptions, object htmlAttributes = null)
        {
            var repID = Guid.NewGuid().ToString();
            var actionLink = ajax.ActionLink(repID, actionName, controllerName, routeValues, ajaxOptions, htmlAttributes);
            return MvcHtmlString.Create(actionLink.ToString().Replace(repID, innerHtml));
        }

        /// <summary>
        /// Returns an html element that contains the URL to the specified action method;
        /// when the action link is clicked, the action method is invoked asynchronously
        /// by using JavaScript.
        /// </summary>
        /// <param name="ajax"></param>
        /// <param name="innerHtml"></param>
        /// <param name="actionName"></param>
        /// <param name="routeValues"></param>
        /// <param name="ajaxOptions"></param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public static IHtmlString RawActionLink(this AjaxHelper ajax, string innerHtml, string actionName, object routeValues, AjaxOptions ajaxOptions, object htmlAttributes = null)
        {
            var repID = Guid.NewGuid().ToString();
            var actionLink = ajax.ActionLink(repID, actionName, routeValues, ajaxOptions, htmlAttributes);
            return MvcHtmlString.Create(actionLink.ToString().Replace(repID, innerHtml));
        }

        #endregion
    }
}