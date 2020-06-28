using System.Web.Mvc;
using System.Web.Routing;

namespace HelperKit.Mvc.Filters
{
    /// <summary>
    /// redirects Modal Errors to /Error/_Index
    /// </summary>
    public class ModalViewExceptionAttribute : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            var isPartialView = filterContext.RouteData.Values["action"].ToString().StartsWith("_");
            filterContext.Controller.TempData["ModalExceptionDetail"] = filterContext.Exception;
            if (isPartialView)
            {
                filterContext.ExceptionHandled = true;
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Error", action = "_Index", Area = string.Empty }));
            }
        }
    }
}