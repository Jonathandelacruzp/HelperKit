using System;
using System.Web;

namespace HelperKit.Web.Exceptions
{
    /// <summary>
    ///
    /// </summary>
    public class NotFoundHttpException : HttpException
    {
        /// <summary>
        ///
        /// </summary>
        public NotFoundHttpException() : base(400, "The requested query was not found on this server")
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="message"></param>
        public NotFoundHttpException(string message) : base(400, message)
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public NotFoundHttpException(string message, Exception exception) : base(401, message, exception)
        {
        }

        public NotFoundHttpException(string message, int hr) : base(message, hr)
        {
        }

        public NotFoundHttpException(int httpCode, string message, Exception innerException) : base(httpCode, message, innerException)
        {
        }

        public NotFoundHttpException(int httpCode, string message) : base(httpCode, message)
        {
        }

        public NotFoundHttpException(int httpCode, string message, int hr) : base(httpCode, message, hr)
        {
        }
    }
}