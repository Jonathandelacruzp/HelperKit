using PagedList.Mvc;
using System.Web.Mvc.Ajax;

namespace HelperKit.Mvc.Html
{
    public static class PaginationExtensions
    {
        #region PAGINATION

        /// <summary>
        /// Bootstrap3Pager
        /// </summary>
        public static PagedListRenderOptions Bootstrap3Pager => new PagedListRenderOptions()
        {
            DisplayLinkToFirstPage = PagedListDisplayMode.IfNeeded,
            DisplayLinkToLastPage = PagedListDisplayMode.IfNeeded,
            DisplayLinkToPreviousPage = PagedListDisplayMode.Always,
            DisplayLinkToNextPage = PagedListDisplayMode.Always,
            DisplayLinkToIndividualPages = true,
            ContainerDivClasses = null,
            UlElementClasses = new[] { "pagination" },
            ClassToApplyToFirstListItemInPager = "first",
            ClassToApplyToLastListItemInPager = "last",
            LinkToFirstPageFormat = "<i class=\"fas fa-ellipsis-h\"></i>",
            LinkToLastPageFormat = "<i class=\"fas fa-ellipsis-h\"></i>",
            LinkToPreviousPageFormat = "<<i class=\"fas fa-chevron-left\"></i>",
            LinkToNextPageFormat = "<i class=\"fas fa-chevron-right\"></i>",
            LinkToIndividualPageFormat = "{0}"
        };

        /// <summary>
        /// Bootstrap 3 pager with zmdi
        /// </summary>
        public static PagedListRenderOptions Bootstrap3PagerZmdi => new PagedListRenderOptions()
        {
            DisplayLinkToFirstPage = PagedListDisplayMode.IfNeeded,
            DisplayLinkToLastPage = PagedListDisplayMode.IfNeeded,
            DisplayLinkToPreviousPage = PagedListDisplayMode.Always,
            DisplayLinkToNextPage = PagedListDisplayMode.Always,
            DisplayLinkToIndividualPages = true,
            ContainerDivClasses = null,
            UlElementClasses = new[] { "pagination" },
            ClassToApplyToFirstListItemInPager = "first",
            ClassToApplyToLastListItemInPager = "last",
            LinkToFirstPageFormat = "<i class=\"zmdi zmdi-more-horiz\"><i>",
            LinkToLastPageFormat = "<i class=\"zmdi zmdi-more-horiz\"></i>",
            LinkToPreviousPageFormat = "<i class=\"zmdi zmdi-chevron-left\"></i>",
            LinkToNextPageFormat = "<i class=\"zmdi zmdi-chevron-right\"></i>",
            LinkToIndividualPageFormat = "{0}"
        };

        /// <summary>
        /// Bootstrap4Pager
        /// </summary>
        public static PagedListRenderOptions Bootstrap4Pager => new PagedListRenderOptions()
        {
            LiElementClasses = new[] { "page-item" },
            UlElementClasses = new[] { "pagination", "justify-content-center" },
            ContainerDivClasses = new[] { "nav" },
            //ContainerDivClasses = new[] { "justify-content-center" },
            FunctionToTransformEachPageLink = (liTag, aTag) =>
            {
                aTag.Attributes.Add("class", "page-link");
                liTag.InnerHtml = aTag.ToString();
                return liTag;
            },
            LinkToPreviousPageFormat = "&laquo;",
            LinkToNextPageFormat = "&raquo;",
            ClassToApplyToFirstListItemInPager = "first",
            ClassToApplyToLastListItemInPager = "last",
            DisplayLinkToFirstPage = PagedListDisplayMode.IfNeeded,
            DisplayLinkToLastPage = PagedListDisplayMode.IfNeeded,
            DisplayLinkToPreviousPage = PagedListDisplayMode.Always,
            DisplayLinkToNextPage = PagedListDisplayMode.Always
        };

        /// <summary>
        ///
        /// </summary>
        /// <param name="id">Tag Id</param>
        /// <param name="onBegin">Begin Function name</param>
        /// <param name="onComplete"> Complete Funcion name</param>
        /// <returns></returns>
        public static PagedListRenderOptions AjaxPagerOptions(string id, string onBegin, string onComplete) => PagedListRenderOptions.EnableUnobtrusiveAjaxReplacing(Bootstrap3Pager, new AjaxOptions() { HttpMethod = "GET", UpdateTargetId = id, OnBegin = onBegin, OnComplete = onComplete });

        /// <summary>
        ///
        /// </summary>
        /// <param name="id">Tag Id</param>
        /// <returns></returns>
        public static PagedListRenderOptions AjaxPagerOptionsById(string id) => AjaxPagerOptions(id, string.Empty, string.Empty);

        /// <summary>
        /// UpdateTargetId = "paged-section", OnBegin = "pagedLoading", OnFailure = "pageFailure", OnComplete = "pagedLoaded"
        /// </summary>
        public static PagedListRenderOptions AjaxPagerOptionsDefault => PagedListRenderOptions.EnableUnobtrusiveAjaxReplacing(Bootstrap3Pager, new AjaxOptions() { HttpMethod = "GET", UpdateTargetId = "paged-section", OnBegin = "pagedLoading", OnFailure = "pageFailure", OnComplete = "pagedLoaded" });

        #endregion
    }
}