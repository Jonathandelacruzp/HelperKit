using System;
using System.ComponentModel.DataAnnotations;

namespace HelperKit.Mvc.DataAnnotation
{
    [AttributeUsage(AttributeTargets.Property)]
    public class HelpTextAttribute : ValidationAttribute
    {
        public string Text { get; set; }

        public HelpTextAttribute(string helpText)
        {
            this.Text = helpText;
        }
    }
}