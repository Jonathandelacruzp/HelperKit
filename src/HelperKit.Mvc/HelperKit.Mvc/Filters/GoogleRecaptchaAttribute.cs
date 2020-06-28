using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using System.Web.Routing;

namespace HelperKit.Mvc.Filters
{
    /// <summary>
    /// Filtro para Google Recaptcha
    /// AppSetting Google.Captcha.SecretKey
    /// </summary>
    public class GoogleRecaptchaAttribute : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
                throw new ArgumentNullException(nameof(filterContext));

            var response = filterContext.HttpContext.Request["g-recaptcha-response"];
            var secretKey = System.Configuration.ConfigurationManager.AppSettings.Get("Google.Captcha.SecretKey");
            string result;
            using (var webClient = new WebClient())
            {
                result = webClient.DownloadString($"https://www.google.com/recaptcha/api/siteverify?secret={secretKey}&response={response}");
            }

            var resultObj = JObject.Parse(result);

            var success = resultObj.SelectToken("success").ToString().ToBoolean();

            if (!success)
            {
                filterContext.Controller.TempData.ToValue<List<IAlertMessage>>("AlertMessage").Add(new AlertMessage() { MessageType = MessageType.Danger, Title = "Error!", Body = "Invalid Captcha" });
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(filterContext.HttpContext.Request.UrlReferrer.PathAndQuery));
            }
        }
    }
}