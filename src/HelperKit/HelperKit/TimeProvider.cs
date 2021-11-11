namespace HelperKit;

public static partial class Extensions
{
    /// <summary>
    /// Convert time between TimeProviders
    /// </summary>
    /// <param name="fromProvider"></param>
    /// <param name="toProvider"></param>
    /// <returns></returns>
    public static DateTime ConvertTime(this ITimeProvider fromProvider, ITimeProvider toProvider)
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
    public static DateTime ConvertTime(this DateTime date, ITimeProvider fromProvider, ITimeProvider toProvider)
    {
        var dateUnspecified = DateTime.SpecifyKind(date, DateTimeKind.Unspecified);
        return TimeZoneInfo.ConvertTime(dateUnspecified, fromProvider.TimeZoneInfo, toProvider.TimeZoneInfo);
    }
}

public sealed class TimeProvider : ITimeProvider
{
    public string Id => TimeZoneInfo.Local.Id;
    public DateTime Now => DateTime.UtcNow.Add(TimeZoneInfo.BaseUtcOffset);
    public TimeZoneInfo TimeZoneInfo { get; }

    private TimeProvider(TimeZoneInfo timeZoneInfo) => TimeZoneInfo = timeZoneInfo;

    public static class Factory
    {
        public static ITimeProvider Create(TimeZoneInfo timeZoneInfo)
            => new TimeProvider(timeZoneInfo);

        public static ITimeProvider Create(string id, TimeSpan baseUtcOffset, string standarName, string displayStandarName = null)
            => new TimeProvider(TimeZoneInfo.CreateCustomTimeZone(id, baseUtcOffset, standarName, displayStandarName ?? standarName));
    }

    private static readonly IDictionary<string, ITimeProvider> _dictionary = new ConcurrentDictionary<string, ITimeProvider>();
    public static IDictionary<string, ITimeProvider> GetSystemTimeProviders()
    {
        if (_dictionary.Count > 0)
            return _dictionary;

        foreach (var item in TimeZoneInfo.GetSystemTimeZones())
            _dictionary.Add(item.Id, new TimeProvider(item));

        return _dictionary;
    }
}
