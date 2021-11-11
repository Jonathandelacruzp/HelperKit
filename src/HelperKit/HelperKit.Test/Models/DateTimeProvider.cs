using HelperKit.Test.Extensions;

namespace HelperKit.Test.Models
{
    public class UtcTimeProvider : ITimeProvider
    {
        public DateTime Now => DateTime.UtcNow;
        public TimeZoneInfo TimeZoneInfo => TimeZoneInfo.Utc;
        public string Id => TimeZoneInfo.Utc.Id;
    }

    public class LocalTimeProvider : ITimeProvider
    {
        public DateTime Now => DateTime.Now;
        public TimeZoneInfo TimeZoneInfo => TimeZoneInfo.Local;
        public string Id => TimeZoneInfo.Local.Id;
    }

    public class CustomTimeProvider : ITimeProvider
    {
        private const string Name = "Custom Provider";
        private const int TimeDifference = DateTimeExtensionsUnitTest.Hours;
        private TimeZoneInfo _timeZoneInfo;

        public string Id => TimeZoneInfo.Id;
        public DateTime Now => DateTime.UtcNow.Add(_timeZoneInfo.BaseUtcOffset);
        public TimeZoneInfo TimeZoneInfo => _timeZoneInfo ??= TimeZoneInfo.CreateCustomTimeZone(Name, TimeSpan.FromHours(TimeDifference), Name, Name);
    }

}