using NUnit.Framework;
using System;
using System.Globalization;

namespace HelperKit.Test.Extensions
{
    public class DateTimeExtensionsUnitTest
    {
        [Test]
        public void Type()
        {
            var dateNow = DateTime.Now;

            var dateStr = dateNow.ToFullCalendarDate();
            var dateStrShort = dateNow.ToString(CultureInfo.InvariantCulture);
            Assert.AreNotEqual(dateStr, dateStrShort);
        }
    }
}