using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace HelperKit.Mvc.Render
{
    public class TemplateRender : IDisposable
    {
        private readonly string _filePath;
        private bool _disposed;

        /// <summary>
        /// Get filepaht from AppSettings RENDER_PATH, default path "~/Templates/"
        /// </summary>
        public TemplateRender()
        {
            this._filePath = System.Configuration.ConfigurationManager.AppSettings.Get("RENDER_PATH");
            this._disposed = false;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="path"></param>
        public TemplateRender(string path)
        {
            this._filePath = path;
            this._disposed = false;
        }

        /// <summary>
        /// Render an external cshtml page without controller
        /// </summary>
        /// <param name="template">Template name </param>
        /// <param name="model"></param>
        /// <returns></returns>
        public virtual string Render<C>(string template, object model = null) where C : ControllerBase, new()
        {
            var controller = CreateController<C>();

            IDictionary<string, object> anonymousDictionary = HtmlHelper.AnonymousObjectToHtmlAttributes(model);
            IDictionary<string, object> expando = new ExpandoObject();
            foreach (var item in anonymousDictionary)
                expando.Add(item);

            controller.ViewData.Model = (ExpandoObject)expando;

            using (var strWriter = new StringWriter())
            {
                var viewPath = this._filePath + template + ".cshtml";
                var razorView = new RazorView(controller.ControllerContext, viewPath, string.Empty, false, new List<string>());
                var viewContext = new ViewContext(controller.ControllerContext, razorView, controller.ViewData, controller.TempData, strWriter);
                viewContext.View.Render(viewContext, strWriter);
                return strWriter.GetStringBuilder().ToString();
            }
        }

        /// <summary>
        /// Simulate a Controller Creation for render a page
        /// </summary>
        /// <typeparam name="C"></typeparam>
        /// <param name="routeData"></param>
        /// <returns></returns>
        protected static C CreateController<C>(RouteData routeData = null) where C : ControllerBase, new()
        {
            // create a disconnected controller instance
            C controller = new C();

            // get context wrapper from HttpContext if available
            HttpContextBase wrapper;
            if (HttpContext.Current != null)
                wrapper = new HttpContextWrapper(HttpContext.Current);
            else
                throw new InvalidOperationException("Can't create Controller Context if no active HttpContext instance is available.");

            if (routeData == null)
                routeData = new RouteData();

            // add the controller routing if not existing
            if (!routeData.Values.ContainsKey("controller") &&
                !routeData.Values.ContainsKey("Controller"))
            {
                routeData.Values.Add("controller", controller.GetType().Name.ToLower().Replace("controller", string.Empty));
            }

            controller.ControllerContext = new ControllerContext(wrapper, routeData, controller);
            return controller;
        }

        public void Dispose()
        {
            this._disposed = true;
            GC.SuppressFinalize(this);
        }
    }
}