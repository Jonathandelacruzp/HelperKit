using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace HelperKit.Mvc
{
    /// <summary>
    /// Base Controller
    /// </summary>
    public abstract class HelperController : Controller
    {
        protected readonly string CurrentCulture;

        protected HelperController()
        {
            if (string.IsNullOrEmpty(CurrentCulture))
                this.CurrentCulture = System.Threading.Thread.CurrentThread.CurrentCulture.ToString();
        }

        public abstract void InvalidateContext();

        /// <summary>
        /// Generate Alerts
        /// </summary>
        /// <param name="alertMessage"></param>
        public void PostMessage(IAlertMessage alertMessage)
        {
            if (TempData["AlertMessage"] == null)
                TempData["AlertMessage"] = new List<IAlertMessage>();
            TempData.ToValue<List<IAlertMessage>>("AlertMessage").Add(alertMessage);
        }

        /// <summary>
        /// Generate Alert
        /// </summary>
        /// <param name="messageType"></param>
        public virtual void PostMessage(MessageType messageType)
        {
            var body = string.Empty;
            switch (messageType)
            {
                case MessageType.Danger: body = "Ha ocurrido un error al procesar la solicitud."; break;
                case MessageType.Info: body = "."; break;
                case MessageType.Success: body = "Los datos se guardaron exitosamente."; break;
                case MessageType.Warning: body = "."; break;
            }
            PostMessage(messageType, body);
        }

        /// <summary>
        /// Generate Alert
        /// </summary>
        /// <param name="messageType"></param>
        /// <param name="body"></param>
        public virtual void PostMessage(MessageType messageType, string body)
        {
            var title = string.Empty;

            switch (messageType)
            {
                case MessageType.Danger: title = "Error!"; break;
                case MessageType.Info: title = "Ojo."; break;
                case MessageType.Success: title = "Éxito!"; break;
                case MessageType.Warning: title = "Atención!"; break;
            }
            PostMessage(new AlertMessage { Title = title, Body = body, MessageType = messageType });
        }

        /// <summary>
        /// Generate Alert
        /// </summary>
        /// <param name="messageType"></param>
        /// <param name="title"></param>
        /// <param name="body"></param>
        public void PostMessage(MessageType messageType, string title, string body) => PostMessage(new AlertMessage { Title = title, Body = body, MessageType = messageType });

        /// <summary>
        /// Generate Alert
        /// </summary>
        /// <param name="ex"></param>
        public void PostMessage(Exception ex) => PostMessage(MessageType.Danger, ex.Message.ToSafeString());
    }
}