using HelperKit.Mvc.DataAnnotation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Caching;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace HelperKit.Mvc.Html
{
    public static class HtmlExtensions
    {
        #region Html.Alert Helper

        /// <summary>
        /// Genera Mensaje de Alerta Html Boostrap Dimissable
        /// </summary>
        /// <param name="html"></param>
        /// <param name="type">Tipo de Alerta</param>
        /// <param name="title"></param>
        /// <param name="message">Mensaje</param>
        /// <param name="hasIcon"></param>
        /// <returns>MvcHtmlString</returns>
        public static IHtmlString Alert(this HtmlHelper html, MessageType type, string title, string message, bool hasIcon = false)
        {
            var span = new TagBuilder("span");
            span.Attributes.Add("aria-hidden", "true");
            span.InnerHtml = "×";

            var buttonDimiss = new TagBuilder("button");
            buttonDimiss.Attributes.Add("type", "button");
            buttonDimiss.Attributes.Add("data-dismiss", "alert");
            buttonDimiss.Attributes.Add("aria-label", "Close");
            buttonDimiss.AddCssClass("close");
            buttonDimiss.InnerHtml = html.Raw(span.ToString(TagRenderMode.Normal)).ToHtmlString();

            var i = new TagBuilder("i");
            var divClass = "alert alert-dismissible";

            switch (type)
            {
                case MessageType.Primary:
                    divClass += " alert-primary";
                    break;

                case MessageType.Secondary:
                    divClass += " alert-secondary";
                    break;

                case MessageType.Success:
                    divClass += " alert-success";
                    break;

                case MessageType.Danger:
                    divClass += " alert-danger";
                    break;

                case MessageType.Warning:
                    divClass += " alert-warning";
                    break;

                case MessageType.Info:
                    divClass += " alert-info";
                    break;

                case MessageType.Light:
                    divClass += " alert-light";
                    break;

                case MessageType.Dark:
                    divClass += " alert-dark";
                    break;

                default:
                    divClass += " alert-default";
                    break;
            }

            var strong = new TagBuilder("strong")
            {
                InnerHtml = title
            };

            var div = new TagBuilder("div");
            div.Attributes.Add("role", "alert");
            div.AddCssClass(divClass);
            div.InnerHtml += hasIcon ? html.Raw(i.ToString(TagRenderMode.Normal) + "&nbsp;").ToHtmlString() : string.Empty;
            div.InnerHtml += html.Raw(buttonDimiss.ToString(TagRenderMode.Normal)).ToHtmlString();
            div.InnerHtml += string.IsNullOrEmpty(title) ? string.Empty : html.Raw(strong.ToString(TagRenderMode.Normal) + "&nbsp;").ToHtmlString();
            div.InnerHtml += html.Raw(message).ToHtmlString();

            return MvcHtmlString.Create(div.ToString(TagRenderMode.Normal));
        }

        /// <summary>
        /// Genera Mensaje de Alerta Html Boostrap Dimissable
        /// </summary>
        /// <param name="html"></param>
        /// <param name="type"></param>
        /// <param name="message"></param>
        /// <param name="hasIcon"></param>
        /// <returns></returns>
        public static IHtmlString Alert(this HtmlHelper html, MessageType type, string message, bool hasIcon = false) => Alert(html, type, null, message, hasIcon);

        /// <summary>
        /// Genera Mensaje de Alerta Html Boostrap Dimissable
        /// </summary>
        /// <param name="html"></param>
        /// <param name="alertMessage"></param>
        /// <param name="hasIcon"></param>
        /// <returns></returns>
        public static IHtmlString Alert(this HtmlHelper html, IAlertMessage alertMessage, bool hasIcon = false) => Alert(html, alertMessage.MessageType, alertMessage.Title, alertMessage.Body, hasIcon);

        /// <summary>
        /// Genera Mensaje de Alerta Html Boostrap Dimissable
        /// </summary>
        /// <param name="html"></param>
        /// <param name="tempData">TempData["AlertMessage"] required</param>
        /// <param name="hasIcon"></param>
        /// <returns></returns>
        public static IHtmlString Alert(this HtmlHelper html, TempDataDictionary tempData, bool hasIcon = false)
        {
            var sb = new StringBuilder();
            foreach (var item in tempData.ToValue<List<IAlertMessage>>("AlertMessage"))
            {
                sb.AppendLine(Alert(html, item, hasIcon).ToHtmlString());
            }
            return MvcHtmlString.Create(sb.ToString());
        }

        #endregion

        #region Html.Badge Helper

        /// <summary>
        ///
        /// </summary>
        /// <param name="html"></param>
        /// <param name="type"></param>
        /// <param name="content"></param>
        /// <param name="isPill"></param>
        /// <returns></returns>
        public static IHtmlString Badge(this HtmlHelper html, MessageType type, string content, bool isPill = false)
        {
            var span = new TagBuilder("span");
            var badgeClass = "badge ";
            if (isPill)
                badgeClass += "badge-pill ";
            switch (type)
            {
                case MessageType.Primary:
                    badgeClass += " badge-primary";
                    break;

                case MessageType.Secondary:
                    badgeClass += " badge-secondary";
                    break;

                case MessageType.Success:
                    badgeClass += " badge-success";
                    break;

                case MessageType.Danger:
                    badgeClass += " badge-danger";
                    break;

                case MessageType.Warning:
                    badgeClass += " badge-warning";
                    break;

                case MessageType.Info:
                    badgeClass += " badge-info";
                    break;

                case MessageType.Light:
                    badgeClass += " badge-light";
                    break;

                case MessageType.Dark:
                    badgeClass += " badge-dark";
                    break;

                default:
                    badgeClass += " badge-default";
                    break;
            }
            span.InnerHtml = html.Raw(content).ToHtmlString();
            return MvcHtmlString.Create(span.ToString(TagRenderMode.Normal));
        }

        #endregion

        #region Html.Image Helper

        /// <summary>
        /// Devuelne un tag html <img />
        /// </summary>
        /// <param name="html"></param>
        /// <param name="src"></param>
        /// <param name="alt"></param>
        /// <param name="Class"></param>
        /// <returns></returns>
        public static IHtmlString Image(this HtmlHelper html, string src, string alt, string Class = null)
        {
            var url = new UrlHelper(html.ViewContext.RequestContext);
            var img = new TagBuilder("img");
            img.MergeAttribute("src", url.Content(src));
            img.MergeAttribute("alt", alt);
            img.AddCssClass(Class);
            return MvcHtmlString.Create(img.ToString(TagRenderMode.SelfClosing));
        }

        /// <summary>
        /// Devuelne un tag html <img />
        /// </summary>
        /// <param name="html"></param>
        /// <param name="src"></param>
        /// <param name="alt"></param>
        /// <returns></returns>
        public static IHtmlString Image(this HtmlHelper html, string src, string alt) => Image(html, src, alt, string.Empty);

        /// <summary>
        /// Genera una imagen para un objeto
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="html"></param>
        /// <param name="expression"></param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public static IHtmlString ImageFor<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression, object htmlAttributes = null)
        {
            var src = expression.Compile()(html.ViewData.Model);
            return BuildImageTag(src.ToString(), htmlAttributes);
        }

        private static IHtmlString BuildImageTag(string src, object htmlAttributes = null)
        {
            var img = new TagBuilder("img");
            img.Attributes.Add("src", src);
            if (htmlAttributes != null)
                img.MergeAttributes(new RouteValueDictionary(htmlAttributes));

            return MvcHtmlString.Create(img.ToString(TagRenderMode.SelfClosing));
        }

        #endregion

        #region Label Helpers

        /// <summary>
        /// Retorna un mvc label con el * de requerido
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="html"></param>
        /// <param name="expression"></param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public static IHtmlString LabelRequiredFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, object htmlAttributes = null)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            var resolvedLabelText = metadata.DisplayName ?? metadata.PropertyName;
            if (metadata.IsRequired)
                resolvedLabelText += "*";

            return html.LabelFor(expression, resolvedLabelText, htmlAttributes);
        }

        #endregion

        #region Html.Content Helper

        /// <summary>
        /// Genera una ruta en la cache de aplicacion para una ruta de archivo
        /// </summary>
        /// <param name="rootRelativePath"></param>
        /// <param name="cacheTime"></param>
        /// <returns></returns>
        public static string CacheTag(string rootRelativePath, int? cacheTime = null)
        {
            try
            {
                if (HttpRuntime.Cache[rootRelativePath] == null)
                {
                    var absolute = HostingEnvironment.MapPath(rootRelativePath);

                    var date = File.GetLastWriteTime(absolute);
                    var index = rootRelativePath.LastIndexOf('/');

                    var result = rootRelativePath.Insert(rootRelativePath.Length, "?v=" + date.Ticks);
                    if (cacheTime.HasValue)
                        HttpRuntime.Cache.Insert(rootRelativePath, result, new CacheDependency(absolute), DateTime.UtcNow.AddSeconds(cacheTime.Value), Cache.NoSlidingExpiration);
                    else
                        HttpRuntime.Cache.Insert(rootRelativePath, result, new CacheDependency(absolute));
                }

                return HttpRuntime.Cache[rootRelativePath] as string;
            }
            catch (Exception)
            {
                return rootRelativePath;
            }
        }

        /// <summary>
        /// Devuelve definiciones Html para definir estilos y scripts
        /// </summary>
        /// <param name="html"></param>
        /// <param name="contentRoute"></param>
        /// <param name="areaName"></param>
        /// <param name="isScripts"></param>
        /// <param name="hasCache"></param>
        /// <returns></returns>
        public static IHtmlString Content(this HtmlHelper html, string contentRoute, string areaName = null, bool isScripts = false, bool hasCache = true)
        {
            TagBuilder tag;
            var url = new UrlHelper(html.ViewContext.RequestContext);
            var contentBase = "~/";
            if (isScripts)
                contentBase += !String.IsNullOrEmpty(areaName) ? $"Areas/{areaName}/Scripts/" : "Scripts/";
            else
                contentBase += !String.IsNullOrEmpty(areaName) ? $"Areas/{areaName}/Content/" : "Content/";

            if (contentRoute.EndsWith(".css"))
            {
                tag = new TagBuilder("link");
                tag.Attributes.Add("rel", "stylesheet");
                tag.Attributes.Add("href", url.Content(contentBase + contentRoute, hasCache));
                return MvcHtmlString.Create(tag.ToString(TagRenderMode.SelfClosing));
            }
            else if (contentRoute.EndsWith(".js"))
            {
                tag = new TagBuilder("script");
                tag.Attributes.Add("type", "text/javascript");
                tag.Attributes.Add("src", url.Content(contentBase + contentRoute, hasCache));
                return MvcHtmlString.Create(tag.ToString(TagRenderMode.Normal));
            }
            return MvcHtmlString.Empty;
        }

        /// <summary>
        /// Devuelve definiciones Html para definir estilos y scripts
        /// </summary>
        /// <param name="html"></param>
        /// <param name="contentName"></param>
        /// <param name="areaName"></param>
        /// <returns></returns>
        public static IHtmlString Content(this HtmlHelper html, string contentName, string areaName) => Content(html, contentName, areaName, false);

        /// <summary>
        /// Devuelve definiciones Html para definir estilos y scripts
        /// </summary>
        /// <param name="html"></param>
        /// <param name="contentName"></param>
        /// <param name="isScripts"></param>
        /// <returns></returns>
        public static IHtmlString Content(this HtmlHelper html, string contentName, bool isScripts) => Content(html, contentName, string.Empty, isScripts);

        /// <summary>
        /// Devuelve definiciones Html para definir estilos y scripts
        /// </summary>
        /// <param name="html"></param>
        /// <param name="contentName"></param>
        /// <returns></returns>
        public static IHtmlString Content(this HtmlHelper html, string contentName) => Content(html, contentName, string.Empty, false);

        #endregion

        #region Bootstraps Helper

        /// <summary>
        /// Devuelve la clásica estructura de un "from group" de bootstrap indicando el label y el control
        /// </summary>
        /// <param name="html"></param>
        /// <param name="LabelText"></param>
        /// <param name="FormControl"></param>
        /// <param name="Inline"></param>
        /// <param name="validateMessage"></param>
        /// <param name="colMd"></param>
        /// <returns></returns>
        public static IHtmlString FormGroup(this HtmlHelper html, string LabelText, MvcHtmlString FormControl, bool Inline = false, MvcHtmlString validateMessage = null, int colMd = 6)
        {
            var div = new TagBuilder("div");
            div.AddCssClass("form-group col-sm-12 col-md-" + colMd);
            div.InnerHtml = string.Empty;

            var label = new TagBuilder("label");
            label.AddCssClass($"col-sm-{ (Inline ? "3" : "12") } control-label");
            label.InnerHtml = html.Raw(LabelText).ToHtmlString();

            div.InnerHtml += html.Raw(label.ToString(TagRenderMode.Normal)).ToHtmlString();

            var divInput = new TagBuilder("div");
            divInput.AddCssClass("col-sm-" + (Inline ? "9" : "12"));
            divInput.InnerHtml = FormControl.ToHtmlString();

            div.InnerHtml += html.Raw(divInput.ToString(TagRenderMode.Normal)).ToHtmlString();

            if (validateMessage != null)
            {
                divInput = new TagBuilder("div");
                divInput.AddCssClass("col-sm-12");
                divInput.InnerHtml = validateMessage.ToHtmlString();

                div.InnerHtml += html.Raw(divInput.ToString(TagRenderMode.Normal)).ToHtmlString();
            }

            return MvcHtmlString.Create(div.ToString(TagRenderMode.Normal));
        }

        #endregion

        #region HelpText

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="html"></param>
        /// <param name="expression"></param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public static IHtmlString HelpTextFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, object htmlAttributes = null)
        {
            if (expression?.Body is MemberExpression memberExpr)
            {
                var helpAttr = memberExpr.Member.GetCustomAttributes(false).OfType<HelpTextAttribute>().SingleOrDefault();
                if (helpAttr != null)
                {
                    var span = new TagBuilder("span");
                    span.Attributes.Add("for", expression.Compile()(html.ViewData.Model).ToString());
                    span.InnerHtml = helpAttr.Text;
                    if (htmlAttributes != null)
                        span.MergeAttributes(new RouteValueDictionary(htmlAttributes));
                    return MvcHtmlString.Create(span.ToString(TagRenderMode.Normal));
                }
            }
            return MvcHtmlString.Empty;
        }

        #endregion

        //#region Json Convert Helper

        ///// <summary>
        /////
        ///// </summary>
        ///// <param name="html"></param>
        ///// <param name="val"></param>
        ///// <returns></returns>
        //public static IHtmlString ToHTMLJson(this HtmlHelper html, object val) => MvcHtmlString.Create(val.ToJson());
        //#endregion

        ///// <summary>
        ///// Get SafeHtml free of Scripts
        ///// </summary>
        ///// <param name="html"></param>
        ///// <param name="value"></param>
        ///// <returns></returns>
        //public static string Sanitize(this HtmlHelper html, string value)
        //{
        //    return Sanitizer.GetSafeHtmlFragment(value);
        //}

        public static MvcHtmlString FilterEditorFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, params Expression<Func<TModel, TValue>>[] otherExpression) => FilterEditorFor(html, expression, null, null, otherExpression);

        public static MvcHtmlString FilterEditorFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, object additionalViewData, params Expression<Func<TModel, TValue>>[] otherExpression) => FilterEditorFor(html, expression, null, additionalViewData);

        public static MvcHtmlString FilterEditorFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, string templateName, params Expression<Func<TModel, TValue>>[] otherExpression) => FilterEditorFor(html, expression, templateName, null);

        public static MvcHtmlString FilterEditorFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, string templateName, object additionalViewData, params Expression<Func<TModel, TValue>>[] otherExpression)
        {
            if (templateName == null)
                templateName = "Filter/" + (Nullable.GetUnderlyingType(expression.ReturnType) ?? expression.ReturnType).Name;
            else
                templateName = "Filter/" + templateName;

            var propiedad = ExpressionHelper.GetExpressionText(expression);
            propiedad = propiedad.Substring(propiedad.IndexOf("]") + 2);

            foreach (var other in otherExpression)
            {
                var otherPropiedad = ExpressionHelper.GetExpressionText(other);
                propiedad += "," + otherPropiedad.Substring(otherPropiedad.IndexOf("]") + 2);
            }

            return html.EditorFor(_ => propiedad, templateName, new { OriginalModel = html.ViewData.Model, Additional = additionalViewData });
        }

        public static IHtmlString BitFilterSubmit(this HtmlHelper htmlHelper)
        {
            const string result = "<span data-toggle=tooltip data-placement=top title=Filtrar><a class=\"btn bn-xs\" data-bit-filter=\"submit\"><i class=\"fa fa-filter\"></i></a></span>&nbsp;<span data-toggle=tooltip data-placement=top title=Limpiar><a class=\"btn bn-xs\" data-bit-filter=\"reset\"><i class=\"fa fa-ban\"></i></a> </span>";
            return new HtmlString(result);
        }

        public static IHtmlString ValidationForCustom<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> propertyExpression)
        {
            var propertyName = html.NameFor(propertyExpression).ToString();
            var metadata = ModelMetadata.FromLambdaExpression(propertyExpression, html.ViewData);
            var attributes = html.GetUnobtrusiveValidationAttributes(propertyName, metadata);
            return new HtmlString(String.Join(" ", attributes.Select(x => String.Format("{0}=\"{1}\"", x.Key, x.Value)).ToArray()));
        }
    }
}