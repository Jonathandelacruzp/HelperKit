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

public class CustomDateTimeProvider(DateTime date, TimeZoneInfo timeZoneInfo)
    : IDateTimeProvider
{
    public DateTime Now { get; } = DateTime.SpecifyKind(date, DateTimeKind.Unspecified);
    public TimeZoneInfo TimeZoneInfo { get; } = timeZoneInfo;
}