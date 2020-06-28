using System;
using System.Web;
using System.Web.Mvc;

namespace HelperKit.Mvc.Filters
{
    public class RequireSecureConnectionFilterAttribute : RequireHttpsAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException(nameof(filterContext));
            }

            if (filterContext.HttpContext.Request.IsLocal || HttpContext.Current.IsDebuggingEnabled)
            {
                // when connection to the application is local or debuging , don't do any HTTPS stuff
                return;
            }

            base.OnAuthorization(filterContext);
        }
    }
}