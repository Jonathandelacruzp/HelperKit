using TimeZoneConverter;

namespace HelperKit.Tests.Extensions;

public class DateTimeExtensionsUnitTest
{
    private const int Hours = 14;
    private readonly IDateTimeProvider _utcTimeProvider = new UtcDateTimeProvider();
    private readonly IDateTimeProvider _localTimeProvider = new LocalDateTimeProvider();
    private readonly IDateTimeProvider _customTimeProvider = DateTimeProvider.Factory.Create("Custom Provider", TimeSpan.FromHours(Hours));

    [Fact]
    public void VerifyToDateTimeExtension_ReturnsCorrectValue()
    {
        var dateNow = DateTime.Now;

        var dateStr = dateNow.ToFullCalendarDate();
        var dateStrShort = dateNow.ToString(CultureInfo.InvariantCulture);

        dateStrShort.Should().NotBe(dateStr);
    }

    [Fact]
    public void VerifyDateOffsetBetweenTimeProviders_ReturnsCorrectValue()
    {
        var dates = _localTimeProvider.Now.AddHours(3);
        var dateTime = DateTime.SpecifyKind(dates, DateTimeKind.Unspecified);
        var tz = _customTimeProvider.TimeZoneInfo;
        var dateTimeUtc = TimeZoneInfo.ConvertTimeFromUtc(dateTime, tz);

        var resultHour = Math.Abs((dateTime.Hour + Hours) % 24);

        resultHour.Should().Be(dateTimeUtc.Hour);
    }

    [Fact]
    public void VerifyITimeProvider_ReturnUtcDate()
    {
        var dateFromTimeProvider = _utcTimeProvider.Now;
        var dateFromDateTime = DateTime.UtcNow;

        dateFromDateTime.Day.Should().Be(dateFromTimeProvider.Day);
        dateFromDateTime.Hour.Should().Be(dateFromTimeProvider.Hour);
    }

    [Fact]
    public void VerifyITimeProviderConvert_ReturnsCorrectValue()
    {
        var hours = Math.Abs(_localTimeProvider.TimeZoneInfo.BaseUtcOffset.TotalHours);
        var result = _localTimeProvider.ConvertTime(_utcTimeProvider);
        var ticksResult = result.Ticks - _localTimeProvider.Now.Ticks;
        var resultHour = new DateTime(Math.Abs(ticksResult)).Hour + (ticksResult > 0 ? 1 : 0);
        // + 1 because the ticks on the provider keeps advancing and is close to the time difference

        resultHour.Should().Be((int)hours);
    }

    [Fact]
    public void VerifyITimeProviderConvertTime_ReturnsCorrectValue()
    {
        var inputDateTime = new DateTime(2021, 07, 15);
        var convertedDateTime = inputDateTime.ConvertTime(_localTimeProvider, _utcTimeProvider);

        var timeOffsetBetweenProviders = _utcTimeProvider.TimeZoneInfo.BaseUtcOffset - _localTimeProvider.TimeZoneInfo.BaseUtcOffset;

        var hourDifference = Math.Abs(timeOffsetBetweenProviders.TotalHours);
        var hoursDifferenceResult = Math.Abs(inputDateTime.Hour - convertedDateTime.Hour);

        hoursDifferenceResult.Should().Be((int)hourDifference);
    }

    [Fact]
    public void VerifyToDateTime_ReturnsCorrectValue()
    {
        var inputDateTime = new DateTime(2021, 07, 15);

        var dateStr = inputDateTime.ToString(CultureInfo.CurrentCulture);

        var resultDate = dateStr.ToDateTime();

        resultDate.Should().Be(inputDateTime);
    }

    [Fact]
    public void GetWeekNumber_ReturnsCorrectValue()
    {
        var inputDateTime = new DateTime(2021, 07, 15);

        var weekNumber = inputDateTime.GetWeekNumber();

        weekNumber.Should().BeGreaterThan(1);
    }

    [Fact]
    public void VerifyFirstDateOfWeek_ReturnsCorrectValue()
    {
        var expected = DateTime.Parse("2021-01-03"); //First sunday and second week of the year
        var inputDateTime = new DateTime(2021, 1, 1);

        var date = inputDateTime.FirstDateOfWeek(2);

        date.Should().Be(expected);
    }

    [Fact]
    public void IsBetween_ReturnsSuccess()
    {
        var minDateTime = new DateTime(2021, 1, 1);
        var inputDateTime = new DateTime(2021, 2, 1);
        var maxDateTime = new DateTime(2022, 1, 1);

        var result = inputDateTime.IsBetween(minDateTime, maxDateTime);

        result.Should().BeTrue();
    }

    [Fact]
    public void GetSystemTimeProviders()
    {
        var items = DateTimeProvider.DateTimeProviders;

        items.Should().NotBeEmpty();
    }

    [Fact]
    public void Create_FromGetSystemTimeProviders()
    {
        var items = DateTimeProvider.DateTimeProviders;
        var timeZone = TZConvert.GetTimeZoneInfo("Central Standard Time");

        var dateNow = items.TryGetValue(timeZone.Id, out var dateTimeProvider)
            ? dateTimeProvider.Now
            : DateTime.MinValue;

        dateNow.Should().NotBe(DateTime.MinValue);
    }

    [Fact]
    public void CurrentDateOffset_ChangesTimes()
    {
        var timeZoneCst = TZConvert.GetTimeZoneInfo("Central Standard Time");
        var timeZoneSaPst = TZConvert.GetTimeZoneInfo("SA Pacific Standard Time");

        var dateTimeProviderCentral = DateTimeProvider.Factory.Create(timeZoneCst);
        var dateTimeProviderPeru = DateTimeProvider.Factory.Create(timeZoneSaPst);

        var customTimeProvider = new CustomDateTimeProvider(new DateTime(2022, 02, 20), dateTimeProviderPeru.TimeZoneInfo);

        var dateCentral = customTimeProvider.ConvertTime(dateTimeProviderCentral);
        var datePeru = customTimeProvider.ConvertTime(dateTimeProviderPeru);

        var difference = dateTimeProviderPeru.TimeZoneInfo.BaseUtcOffset - dateTimeProviderCentral.TimeZoneInfo.BaseUtcOffset;

        var dateNow = dateTimeProviderCentral.Now;

        dateNow.Should().NotBe(DateTime.MinValue);
        difference.Should().Be(datePeru - dateCentral);
    }
}
