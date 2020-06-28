using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace HelperKit.Mvc
{
    public abstract class BaseFormBuilder : IBaseFormBuilder
    {
        protected string ActionName { get; set; }
        protected string ControllerName { get; set; }
        protected object RouteValues { get; set; }
        protected FormMethod FormMethod { get; set; }
        protected object HtmlAttributes { get; set; }

        public abstract MvcForm BeginForm(object obj);
    }
}