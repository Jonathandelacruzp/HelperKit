using HelperKit;
using System;
using System.Web.Mvc;

namespace $rootnamespace$.Controllers
{
    public class ErrorController : BaseController
    {
        // GET: Error
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult _Index()
        {
            var ex = TempData.ToValue<Exception>("ModalExceptionDetail");
            return View(ex);
        }
    }
}
