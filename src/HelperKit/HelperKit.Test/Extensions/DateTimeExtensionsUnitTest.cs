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
        public void VerifyToDateTimeExtension_ReturnsCorrectValue()
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
        public void VerifyITimeProviderConvert_ReturnsCorrectValue()
        {
            var hours = Math.Abs(_localTimeProvider.TimeZoneInfo.BaseUtcOffset.TotalHours);
            var result = _localTimeProvider.ConvertTime(_utcTimeProvider);
            var ticksResult = result.Ticks - _localTimeProvider.Now.Ticks;
            var resultHour = new DateTime(Math.Abs(ticksResult)).Hour + (ticksResult > 0 ? 1 : 0);
            // + 1 because the ticks on the provider keeps advancing and is close to the time difference

            Assert.AreEqual((int)hours, resultHour);
        }

        [Test]
        public void VerifyITimeProviderConvertTime_ReturnsCorrectValue()
        {
            var inputDateTime = new DateTime(2021, 07, 15);
            var convertedDateTime = inputDateTime.ConvertTime(_localTimeProvider, _utcTimeProvider);

            var timeOffsetBetweenProviders = _utcTimeProvider.TimeZoneInfo.BaseUtcOffset - _localTimeProvider.TimeZoneInfo.BaseUtcOffset;

            var hourDifference = Math.Abs(timeOffsetBetweenProviders.TotalHours);
            var hoursDifferenceResult = Math.Abs(inputDateTime.Hour - convertedDateTime.Hour);

            Assert.AreEqual(hourDifference, hoursDifferenceResult);
        }

        [Test]
        public void VerifyToDateTime_ReturnsCorrectValue()
        {
            var inputDateTime = new DateTime(2021, 07, 15);

            var dateStr = inputDateTime.ToString();

            var resultDate = dateStr.ToDateTime();

            Assert.AreEqual(inputDateTime, resultDate);
        }

        [Test]
        public void VerifyFirstDateOfWeek_ReturnsCorrectValue()
        {
            var expected = DateTime.Parse("2021-01-03"); //Fisrt sunday and second week of the year
            var inputDateTime = new DateTime(2021, 1, 1);

            var date = inputDateTime.FirstDateOfWeek(2);
            Assert.AreEqual(expected, date);
        }

        [Test]
        public void IsBetween_ReturnsSuccess()
        {
            var minDateTime = new DateTime(2021, 1, 1);
            var inputDateTime = new DateTime(2021, 2, 1);
            var maxDateTime = new DateTime(2022, 1, 1);

            var result = inputDateTime.IsBetween(minDateTime, maxDateTime);
            Assert.IsTrue(result);
        }
    }
}