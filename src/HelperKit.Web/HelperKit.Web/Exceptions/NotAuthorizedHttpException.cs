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
    }
}