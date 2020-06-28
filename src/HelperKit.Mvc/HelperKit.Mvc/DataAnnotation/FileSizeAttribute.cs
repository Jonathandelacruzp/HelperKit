using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace HelperKit.Mvc.DataAnnotation
{
    /// <summary>
    /// Validate file size of HttpPostedFileBase
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class FileSizeAttribute : ValidationAttribute
    {
        private readonly int _maxSize;

        public FileSizeAttribute(int maxSize)
        {
            _maxSize = maxSize;
            ErrorMessage = string.Format("The file size should not exceed {0}", _maxSize);
        }

        public override bool IsValid(object value)
        {
            return value == null || _maxSize > (value as HttpPostedFileBase)?.ContentLength;
        }
    }
}