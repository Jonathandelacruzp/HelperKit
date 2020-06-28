using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace HelperKit.Mvc
{
    /// <summary>
    /// Modal Form
    /// </summary>
    public class ModalFormBuilder : BaseFormBuilder
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="routeValues"></param>
        public ModalFormBuilder(object routeValues)
        {
            this.RouteValues = routeValues;
            this.FormMethod = FormMethod.Post;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="actionName"></param>
        public ModalFormBuilder(string actionName)
        {
            this.ActionName = actionName;
            this.FormMethod = FormMethod.Post;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="actionName"></param>
        /// <param name="method"></param>
        public ModalFormBuilder(string actionName, FormMethod method)
        {
            this.ActionName = actionName;
            this.FormMethod = method;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="actionName"></param>
        /// <param name="htmlAttributes"></param>
        public ModalFormBuilder(string actionName, object htmlAttributes)
        {
            this.ActionName = actionName;
            this.FormMethod = FormMethod.Post;
            this.HtmlAttributes = htmlAttributes;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="actionName"></param>
        /// <param name="controllerName"></param>
        public ModalFormBuilder(string actionName, string controllerName)
        {
            this.ActionName = actionName;
            this.ControllerName = controllerName;
            this.FormMethod = FormMethod.Post;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="actionName"></param>
        /// <param name="controllerName"></param>
        /// <param name="method"></param>
        public ModalFormBuilder(string actionName, string controllerName, FormMethod method)
        {
            this.ActionName = actionName;
            this.ControllerName = controllerName;
            this.FormMethod = method;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="actionName"></param>
        /// <param name="controllerName"></param>
        /// <param name="htmlAttributes"></param>
        public ModalFormBuilder(string actionName, string controllerName, object htmlAttributes)
        {
            this.ActionName = actionName;
            this.ControllerName = controllerName;
            this.FormMethod = FormMethod.Post;
            this.HtmlAttributes = htmlAttributes;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="actionName"></param>
        /// <param name="controllerName"></param>
        /// <param name="method"></param>
        /// <param name="htmlAttributes"></param>
        public ModalFormBuilder(string actionName, string controllerName, FormMethod method, object htmlAttributes)
        {
            this.ActionName = actionName;
            this.ControllerName = controllerName;
            this.FormMethod = method;
            this.HtmlAttributes = htmlAttributes;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="obj">Type HtmlHelper</param>
        /// <returns></returns>
        public override MvcForm BeginForm(object obj) => (obj as HtmlHelper).BeginForm(ActionName, ControllerName, RouteValues, FormMethod, HtmlAttributes);
    }
}