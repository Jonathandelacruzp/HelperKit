using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace HelperKit.Mvc.DataAnnotation
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class AtLeastOnePropertyAttribute : ValidationAttribute
    {
        private string[] PropertyList { get; }

        public AtLeastOnePropertyAttribute(params string[] propertyList)
        {
            this.PropertyList = propertyList;
        }

        //See http://stackoverflow.com/a/1365669
        public override object TypeId
        {
            get
            {
                return this;
            }
        }

        public override bool IsValid(object value)
        {
            PropertyInfo propertyInfo;
            foreach (var propertyName in PropertyList)
            {
                propertyInfo = value.GetType().GetProperty(propertyName);

                if (propertyInfo?.GetValue(value, null) != null)
                {
                    return true;
                }
            }

            return false;
        }
    }
}