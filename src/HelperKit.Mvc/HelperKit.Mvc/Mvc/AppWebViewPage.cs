using System.Web.Mvc;

namespace HelperKit.Mvc
{
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public class AppWebViewPage<TModel> : WebViewPage<TModel>
    {
        /// <summary>
        /// Data Helper Instance
        /// </summary>
        public DataHelper<TModel> Data { get; private set; }

        public override void InitHelpers()
        {
            base.InitHelpers();
            this.Data = new DataHelper<TModel>(base.ViewContext, base.Url, base.Html, this);
        }

        public override void Execute()
        {
        }
    }
}