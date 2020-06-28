using System;
using System.Web;

namespace HelperKit.Web.Exceptions
{
    /// <summary>
    ///
    /// </summary>
    public class NotAuthorizedHttpException : HttpException
    {
        /// <summary>
        ///
        /// </summary>
        public NotAuthorizedHttpException() : base(401, "Unauthorized")
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="message"></param>
        public NotAuthorizedHttpException(string message) : base(401, message)
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public NotAuthorizedHttpException(string message, Exception innerException) : base(401, message, innerException)
        {
        }

        public NotAuthorizedHttpException(string message, int hr) : base(message, hr)
        {
        }

        public NotAuthorizedHttpException(int httpCode, string message, Exception innerException) : base(httpCode, message, innerException)
        {
        }

        public NotAuthorizedHttpException(int httpCode, string message) : base(httpCode, message)
        {
        }

        public NotAuthorizedHttpException(int httpCode, string message, int hr) : base(httpCode, message, hr)
        {
        }
    }
}