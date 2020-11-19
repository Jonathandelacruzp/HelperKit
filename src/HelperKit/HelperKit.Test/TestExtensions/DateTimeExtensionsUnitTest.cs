using System;
using NUnit.Framework;

namespace HelperKit.Test.TestExtensions
{
    public class DateTimeExtensionsUnitTest
    {
        [Test]
        public void ToDateTimeExtension_ConvertsAValidObject()
        {
            var expectedDateTime = new DateTime(2020, 2, 2);
            object dateObjectString = "2020-02-02";
            var result = dateObjectString.ToDateTime();
            Assert.AreEqual(expectedDateTime, result);
        }

        [Test]
        public void ToDateTimeExtension_ConvertsAndReturnsDefaultValueFromInvalidObject()
        {
            object dateObjectString = "2020-02-02AnyDateInvalid";
            var result = dateObjectString.ToDateTime();
            Assert.AreEqual(DateTime.MinValue, result);
        }

        [Test]
        public void IsBetweenExtension_ReturnsCorrectValue()
        {
            var dateToday = DateTime.Now;
            var dateYesterday = dateToday.AddDays(-1);
            var dateFuture = dateToday.AddDays(9);

            Assert.IsTrue(dateToday.IsBetween(dateYesterday, dateFuture));
        }
    }
}