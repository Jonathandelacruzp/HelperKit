using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Mvc.Html;

namespace HelperKit.Mvc
{
    /// <summary>
    /// Modal Ajax Form
    /// </summary>
    public class ModalAjaxFormBuilder : BaseFormBuilder
    {
        /// <summary>
        /// AjaxOptions
        /// </summary>
        public AjaxOptions AjaxOptions { get; set; }

        /// <summary>
        ///
        /// </summary>
        public ModalAjaxFormBuilder()
        {
            AjaxOptions = new AjaxOptions();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="ajaxOptions"></param>
        public ModalAjaxFormBuilder(AjaxOptions ajaxOptions)
        {
            this.AjaxOptions = ajaxOptions;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="routeValues"></param>
        /// <param name="ajaxOptions"></param>
        public ModalAjaxFormBuilder(object routeValues, AjaxOptions ajaxOptions)
        {
            this.RouteValues = routeValues;
            this.AjaxOptions = ajaxOptions;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="actionName"></param>
        public ModalAjaxFormBuilder(string actionName)
        {
            this.ActionName = actionName;
            this.AjaxOptions = new AjaxOptions();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="actionName"></param>
        /// <param name="htmlAttributes"></param>
        public ModalAjaxFormBuilder(string actionName, object htmlAttributes)
        {
            this.ActionName = actionName;
            this.HtmlAttributes = htmlAttributes;
            this.AjaxOptions = new AjaxOptions();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="actionName"></param>
        // <param name="ajaxOptions"></param>
        public ModalAjaxFormBuilder(string actionName, AjaxOptions ajaxOptions)
        {
            this.ActionName = actionName;
            this.AjaxOptions = ajaxOptions;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="actionName"></param>
        /// <param name="htmlAttributes"></param>
        /// <param name="ajaxOptions"></param>
        public ModalAjaxFormBuilder(string actionName, object htmlAttributes, AjaxOptions ajaxOptions)
        {
            this.ActionName = actionName;
            this.HtmlAttributes = htmlAttributes;
            this.AjaxOptions = ajaxOptions;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="actionName"></param>
        /// <param name="controllerName"></param>
        public ModalAjaxFormBuilder(string actionName, string controllerName)
        {
            this.ActionName = actionName;
            this.ControllerName = controllerName;
            this.AjaxOptions = new AjaxOptions();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="actionName"></param>
        /// <param name="controllerName"></param>
        /// <param name="htmlAttributes"></param>
        public ModalAjaxFormBuilder(string actionName, string controllerName, object htmlAttributes)
        {
            this.ActionName = actionName;
            this.ControllerName = controllerName;
            this.AjaxOptions = new AjaxOptions();
            this.HtmlAttributes = htmlAttributes;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="actionName"></param>
        /// <param name="controllerName"></param>
        /// <param name="ajaxOptions"></param>
        public ModalAjaxFormBuilder(string actionName, string controllerName, AjaxOptions ajaxOptions)
        {
            this.ActionName = actionName;
            this.ControllerName = controllerName;
            this.AjaxOptions = ajaxOptions;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="actionName"></param>
        /// <param name="controllerName"></param>
        /// <param name="htmlAttributes"></param>
        /// <param name="ajaxOptions"></param>
        public ModalAjaxFormBuilder(string actionName, string controllerName, object htmlAttributes, AjaxOptions ajaxOptions)
        {
            this.ActionName = actionName;
            this.ControllerName = controllerName;
            this.HtmlAttributes = htmlAttributes;
            this.AjaxOptions = ajaxOptions;
        }

        /// <summary>
        /// </summary>
        /// <param name="obj">Type AjaxHelper</param>
        /// <returns></returns>
        public override MvcForm BeginForm(object obj) => (obj as AjaxHelper).BeginForm(ActionName, ControllerName, RouteValues, AjaxOptions, HtmlAttributes);
    }
}