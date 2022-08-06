namespace HelperKit.Tests.Models;

public class UtcDateTimeProvider : IDateTimeProvider
{
    public DateTime Now => DateTime.UtcNow;
    public TimeZoneInfo TimeZoneInfo => TimeZoneInfo.Utc;
}

public class LocalDateTimeProvider : IDateTimeProvider
{
    public DateTime Now => DateTime.Now;
    public TimeZoneInfo TimeZoneInfo => TimeZoneInfo.Local;
}

public class CustomDateTimeProvider : IDateTimeProvider
{
    public DateTime Now { get; }
    public TimeZoneInfo TimeZoneInfo { get; }

    public CustomDateTimeProvider(DateTime date, TimeZoneInfo timeZoneInfo)
    {
        Now = DateTime.SpecifyKind(date, DateTimeKind.Unspecified);
        TimeZoneInfo = timeZoneInfo;
    }
}