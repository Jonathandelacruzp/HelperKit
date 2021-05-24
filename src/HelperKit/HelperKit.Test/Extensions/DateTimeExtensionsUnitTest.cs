using NUnit.Framework;
using System;
using System.Globalization;
using HelperKit.Interfaces;
using HelperKit.Test.Models;

namespace HelperKit.Test.Extensions
{
    public class DateTimeExtensionsUnitTest
    {
        internal const int Hours = 14;
        private readonly ITimeProvider _utcTimeProvider = new UtcTimeProvider();
        private readonly ITimeProvider _localTimeProvider = new LocalTimeProvider();
        private readonly ITimeProvider _customTimeProvider = new CustomTimeProvider();

        [Test]
        public void VerifyToDateTimeExternsion_ReturnsCorrectValue()
        {
            var dateNow = DateTime.Now;

            var dateStr = dateNow.ToFullCalendarDate();
            var dateStrShort = dateNow.ToString(CultureInfo.InvariantCulture);
            Assert.AreNotEqual(dateStr, dateStrShort);
        }

        [Test]
        public void VerifyDateOffsetBetweenTimeProviders_ReturnsCorrectValue()
        {
            var dates = _localTimeProvider.Now.AddHours(3);
            var dateTime = DateTime.SpecifyKind(dates, DateTimeKind.Unspecified);
            var tz = _customTimeProvider.TimeZoneInfo;
            var dateTimeUtc = TimeZoneInfo.ConvertTimeFromUtc(dateTime, tz);

            var resultHour = Math.Abs((dateTime.Hour + Hours) % 24);

            Assert.AreEqual(resultHour, dateTimeUtc.Hour);
        }

        [Test]
        public void VerifyITimeProvider_ReturnUtcDate()
        {
            var dateFromTimeProvider = _utcTimeProvider.Now;
            var dateFromDateTime = DateTime.UtcNow;

            Assert.AreEqual(dateFromDateTime.Day, dateFromTimeProvider.Day);
            Assert.AreEqual(dateFromDateTime.Hour, dateFromTimeProvider.Hour);
        }

        [Test]
        public void VerifyITimeProviderConvet_ReturnsCorrectValue()
        {
            var hours = Math.Abs(_localTimeProvider.TimeZoneInfo.BaseUtcOffset.TotalHours);
            var result = _localTimeProvider.ConvertTime(_utcTimeProvider);
            var ticksResult = result.Ticks - _localTimeProvider.Now.Ticks;
            int resultHour = new DateTime(Math.Abs(ticksResult)).Hour + (ticksResult > 0 ? 1 : 0);
            // + 1 because the ticks on the provider keeps advancing and is close to the time difference

            Assert.AreEqual((int)hours, resultHour);
        }
    }
}