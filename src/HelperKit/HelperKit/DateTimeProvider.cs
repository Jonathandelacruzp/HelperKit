namespace HelperKit;

public static partial class Extensions
{
    /// <summary>
    /// Convert time between TimeProviders
    /// </summary>
    /// <param name="fromProvider"></param>
    /// <param name="toProvider"></param>
    /// <returns></returns>
    public static DateTime ConvertTime(this IDateTimeProvider fromProvider, IDateTimeProvider toProvider)
    {
        var dateUnspecified = DateTime.SpecifyKind(fromProvider.Now, DateTimeKind.Unspecified);
        return TimeZoneInfo.ConvertTime(dateUnspecified, fromProvider.TimeZoneInfo, toProvider.TimeZoneInfo);
    }

    /// <summary>
    /// Convert time between TimeProviders
    /// </summary>
    /// <param name="date"></param>
    /// <param name="fromProvider"></param>
    /// <param name="toProvider"></param>
    /// <returns></returns>
    public static DateTime ConvertTime(this DateTime date, IDateTimeProvider fromProvider, IDateTimeProvider toProvider)
    {
        var dateUnspecified = DateTime.SpecifyKind(date, DateTimeKind.Unspecified);
        return TimeZoneInfo.ConvertTime(dateUnspecified, fromProvider.TimeZoneInfo, toProvider.TimeZoneInfo);
    }
}

public sealed class DateTimeProvider : IDateTimeProvider
{
    public DateTime Now => DateTime.UtcNow.Add(TimeZoneInfo.BaseUtcOffset);
    public TimeZoneInfo TimeZoneInfo { get; }

    private DateTimeProvider(TimeZoneInfo timeZoneInfo) => TimeZoneInfo = timeZoneInfo;

    public static class Factory
    {
        public static IDateTimeProvider Create(TimeZoneInfo timeZoneInfo)
            => new DateTimeProvider(timeZoneInfo);

        public static IDateTimeProvider Create(string id, TimeSpan baseUtcOffset, string standardName = null, string displayStandardName = null)
            => new DateTimeProvider(TimeZoneInfo.CreateCustomTimeZone(id, baseUtcOffset, standardName ?? id, displayStandardName ?? standardName ?? id));

        public static IDateTimeProvider Create(string timeZoneId)
            => _systemDateTimeProviders.TryGetValue(timeZoneId, out var value)
                ? value
                : throw new Exception($"The value provided is not a valid timeZoneId: {timeZoneId}");
    }

    private static readonly ReadOnlyDictionary<string, IDateTimeProvider> _systemDateTimeProviders = new(TimeZoneInfo.GetSystemTimeZones().ToDictionary<TimeZoneInfo, string, IDateTimeProvider>(t => t.Id, t => new DateTimeProvider(t)));

    public static IDictionary<string, IDateTimeProvider> GetSystemDateTimeProviders() => _systemDateTimeProviders;
}