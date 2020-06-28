using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace HelperKit.Mvc
{
    public class DataHelper<TModel>
    {
        public DataHelper(ViewContext viewContext, UrlHelper urlHelper, HtmlHelper<TModel> htmlHelper, IViewDataContainer viewDataContainer) : this(viewContext, viewDataContainer, RouteTable.Routes)
        {
            this.Url = urlHelper;
            this.Html = htmlHelper;
        }

        private DataHelper(ViewContext viewContext, IViewDataContainer viewDataContainer, RouteCollection routeCollection)
        {
            RouteCollection = routeCollection;
            ViewContext = viewContext;
            ViewData = new ViewDataDictionary(viewDataContainer.ViewData);
        }

        public RouteCollection RouteCollection { get; private set; }
        public ViewDataDictionary ViewData { get; private set; }
        public ViewContext ViewContext { get; private set; }
        public UrlHelper Url { get; private set; }
        public HtmlHelper<TModel> Html { get; private set; }

        #region Modal Helper

        public virtual IHtmlString ModalLink(string actionName) => ModalLink(actionName, null, null);

        public virtual IHtmlString ModalLink(string actionName, ModalSize size) => ModalLink(actionName, null, null, size);

        public virtual IHtmlString ModalLink(string actionName, object routeValues) => ModalLink(actionName, null, routeValues);

        public virtual IHtmlString ModalLink(string actionName, object routeValues, ModalSize size) => ModalLink(actionName, null, routeValues, size);

        public virtual IHtmlString ModalLink(string actionName, string controllerName) => ModalLink(actionName, controllerName, null, new ModalSize());

        public virtual IHtmlString ModalLink(string actionName, string controllerName, object routeValues) => ModalLink(actionName, controllerName, routeValues, new ModalSize());

        public virtual IHtmlString ModalLink(string actionName, string controllerName, ModalSize size) => ModalLink(actionName, controllerName, null, size);

        public virtual IHtmlString ModalLink(string actionName, string controllerName, object routeValues, ModalSize size)
        {
            var modalSize = string.Empty;
            switch (size)
            {
                case ModalSize.Small:
                    modalSize = "modal-sm";
                    break;

                case ModalSize.Normal:
                    modalSize = "modal-md";
                    break;

                case ModalSize.Large:
                    modalSize = "modal-lg";
                    break;

                default:
                    break;
            }
            var result = $"href=\"#\" data-type=\"modal-link\" data-url=\"{ Url.Action(actionName, controllerName, routeValues) }\" data-modal-size=\"{ modalSize }\"";
            return new HtmlString(result);
        }

        #endregion

        #region Data Tooltip

        public virtual IHtmlString Tooltip(string title, DataPlacement placement, bool isHtml = false)
        {
            var placementStr = string.Empty;
            switch (placement)
            {
                case DataPlacement.right:
                    placementStr = "data-placement=\"right\"";
                    break;

                case DataPlacement.bottom:
                    placementStr = "data-placement=\"bottom\"";
                    break;

                case DataPlacement.left:
                    placementStr = "data-placement=\"left\"";
                    break;

                default:
                    break;
            }
            var result = $"data-toggle=\"tooltip\" {(isHtml ? "data-html=\"true\"" : string.Empty)} {placementStr} title={title}";
            return new HtmlString(result);
        }

        public virtual IHtmlString Tooltip(string title, bool isHtml = false) => Tooltip(title, DataPlacement.top, isHtml);

        #endregion

        #region CascadeDropDown

        public IHtmlString CascadeDropdownListFor<TProperty>(Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, string optionLabel, object htmlAttributes, string sourceUrl, string updateTarget, string filter, string aditionamParams = null, string valueMember = null, string textMember = null)
        {
            var dictAttributes = htmlAttributes.ToDictionary();
            var result = new ExpandoObject();
            var dictionary = result as IDictionary<string, object>;
            if (dictAttributes != null)
            {
                foreach (var pair in dictAttributes)
                {
                    dictionary[pair.Key] = pair.Value;
                }
            }

            if (!string.IsNullOrEmpty(sourceUrl))
            {
                dictionary.Add(new KeyValuePair<string, object>("data-type", "cascade-select"));
                dictionary.Add(new KeyValuePair<string, object>("data-source-url", sourceUrl));
                dictionary.Add(new KeyValuePair<string, object>("data-target", updateTarget));
                dictionary.Add(new KeyValuePair<string, object>("data-filter-param", filter));
            }

            if (!string.IsNullOrEmpty(aditionamParams))
                dictionary.Add(new KeyValuePair<string, object>("data-aditional-param", aditionamParams));

            dictionary.Add(new KeyValuePair<string, object>("data-value-member", valueMember ?? "Value"));
            dictionary.Add(new KeyValuePair<string, object>("data-text-member", textMember ?? "Text"));

            var stringBuilder = new StringBuilder();
            if (string.IsNullOrEmpty(optionLabel))
                stringBuilder.Append(Html.DropDownListFor(expression, selectList, result));
            else
                stringBuilder.Append(Html.DropDownListFor(expression, selectList, optionLabel, result));
            return new MvcHtmlString(stringBuilder.ToString());
        }

        public IHtmlString CascadeDropdownListFor<TProperty>(Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, string optionLabel, object htmlAttributes) => CascadeDropdownListFor(expression, selectList, optionLabel, htmlAttributes, null, null, null, null, null, null);

        public IHtmlString CascadeDropdownListFor<TProperty>(Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, string optionLabel, object htmlAttributes, string valueMember, string textMember) => CascadeDropdownListFor(expression, selectList, optionLabel, htmlAttributes, null, null, null, null, valueMember, textMember);

        public IHtmlString CascadeDropdownListFor<TProperty>(Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, string optionLabel, object htmlAttributes, string sourceUrl, string updateTarget, string filter) => CascadeDropdownListFor(expression, selectList, optionLabel, htmlAttributes, sourceUrl, updateTarget, filter, null, null, null);

        public IHtmlString CascadeDropdownListFor<TProperty>(Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, string optionLabel, object htmlAttributes, string sourceUrl, string updateTarget, string filter, string aditionamParams) => CascadeDropdownListFor(expression, selectList, optionLabel, htmlAttributes, sourceUrl, updateTarget, filter, aditionamParams, null, null);

        public IHtmlString CascadeDropdownListFor<TProperty>(Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, string optionLabel, object htmlAttributes, string sourceUrl, string updateTarget, string filter, string valueMember, string textMember) => CascadeDropdownListFor(expression, selectList, optionLabel, htmlAttributes, sourceUrl, updateTarget, filter, null, valueMember, textMember);

        #endregion
    }
}