using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HelperKit.Mvc.DataAnnotation
{
    /// <summary>
    /// Validate for filetype of HttpPostedFileBase
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class FileTypesAttribute : ValidationAttribute, IClientValidatable
    {
        private readonly IEnumerable<string> _validTypes;
        private const string _defaultErrorMessage = "Invalid file type. Only the following file types are allowed: {0}";

        public FileTypesAttribute(string types)
        {
            _validTypes = types.Split(',').ToList();
            ErrorMessage = string.Format(_defaultErrorMessage, string.Join(", ", _validTypes));
        }

        public override bool IsValid(object value)
        {
            if (value == null)
                return true;
            var fileExt = System.IO.Path.GetExtension((value as HttpPostedFileBase)?.FileName).Substring(1);
            return _validTypes.Contains(fileExt, StringComparer.OrdinalIgnoreCase);
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule
            {
                ValidationType = "filetype",
                ErrorMessage = ErrorMessageString
            };
            rule.ValidationParameters.Add("validtypes", string.Join(",", _validTypes));
            yield return rule;
        }
    }
}