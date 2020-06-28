using System;
using System.Web;
using System.Web.Mvc;

namespace HelperKit.Mvc.Models
{
    public abstract class BaseDataContext
    {
        public abstract string CurrentCulture { get; set; }
        public abstract HttpContextBase HttpContext { get; set; }
        public abstract HttpSessionStateBase Session { get; set; }
        public IController Controller { get; set; }
        public IDisposable Context { get; set; }
    }
}