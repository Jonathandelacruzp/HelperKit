using HelperKit.Interfaces;
using HelperKit.Test.Extensions;
using System;

namespace HelperKit.Test.Models
{
    public class UtcTimeProvider : ITimeProvider
    {
        public DateTime Now => DateTime.UtcNow;
        public TimeZoneInfo TimeZoneInfo => TimeZoneInfo.Utc;
    }

    public class LocalTimeProvider : ITimeProvider
    {
        public DateTime Now => DateTime.Now;
        public TimeZoneInfo TimeZoneInfo => TimeZoneInfo.Local;
    }

    public class CustomTimeProvider : ITimeProvider
    {
        private readonly string _name = "Custom Provider";
        private readonly int _timeDifference = DateTimeExtensionsUnitTest.Hours;
        private TimeZoneInfo _timeZoneInfo;

        public DateTime Now => DateTime.UtcNow.Add(_timeZoneInfo.BaseUtcOffset);
        public TimeZoneInfo TimeZoneInfo => _timeZoneInfo ??= TimeZoneInfo.CreateCustomTimeZone(_name, TimeSpan.FromHours(_timeDifference), _name, _name);
    }
}
