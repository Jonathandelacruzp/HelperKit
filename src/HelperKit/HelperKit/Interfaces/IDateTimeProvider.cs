namespace HelperKit.Interfaces;

/// <summary>
/// Defines the structure of a time provider, Makes easier the unit testing and value mock
/// </summary>
public interface IDateTimeProvider
{
    public DateTime Now { get; }
    public TimeZoneInfo TimeZoneInfo { get; }
}
