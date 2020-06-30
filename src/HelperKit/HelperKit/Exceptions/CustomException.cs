using System;
using System.Runtime.Serialization;

namespace HelperKit.Exceptions
{
    public class CustomException<T> : Exception where T : Exception
    {
        public T Exception { get; set; }

        /// <summary>
        /// constructor
        /// </summary>
        public CustomException()
        {
        }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="message"></param>
        public CustomException(string message) : base(message)
        {
        }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public CustomException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        public CustomException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}