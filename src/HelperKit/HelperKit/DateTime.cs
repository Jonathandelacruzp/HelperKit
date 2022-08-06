namespace HelperKit;

public static partial class Extensions
{
    #region DateTime

    /// <summary>
    /// Validates if the given date is between 2 dates
    /// </summary>
    /// <param name="val"></param>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <returns></returns>
    public static bool IsBetween(this DateTime val, DateTime start, DateTime end)
    {
        return val >= start && val <= end;
    }

    #endregion

    #region DateTime Convert

    /// <summary>
    /// Converts a value to DateTime
    /// </summary>
    /// <param name="s"></param>
    /// <param name="def"></param>
    /// <returns></returns>
    public static DateTime ToDateTime(this object s, DateTime def = default)
    {
        return DateTime.TryParse(s.ToString(), out var result) ? result : def;
    }

    /// <summary>
    /// Get the full calendar format string
    /// </summary>
    /// <param name="date"></param>
    /// <returns></returns>
    public static string ToFullCalendarDate(this DateTime date)
    {
        return date.ToString("yyyy-MM-dd HH:mm:ss");
    }

    /// <summary>
    /// Gets the week number
    /// </summary>
    /// <param name="date"></param>
    /// <param name="cultureInfo"></param>
    /// <returns></returns>
    public static int GetWeekNumber(this DateTime date, CultureInfo cultureInfo = null)
    {
        cultureInfo ??= CultureInfo.CurrentCulture;
        return cultureInfo.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
    }

    /// <summary>
    /// Gets the default fist day of week
    /// </summary>
    /// <param name="_"></param>
    /// <param name="year"></param>
    /// <param name="weekOfYear"></param>
    /// <param name="cultureInfo"></param>
    /// <returns></returns>
    public static DateTime FirstDateOfWeek(this DateTime _, int year, int weekOfYear, CultureInfo cultureInfo = null)
    {
        cultureInfo ??= CultureInfo.CurrentCulture;
        var jan1 = new DateTime(year, 1, 1);
        var daysOffset = (int)cultureInfo.DateTimeFormat.FirstDayOfWeek - (int)jan1.DayOfWeek;
        var firstWeekDay = jan1.AddDays(daysOffset);
        var firstWeek = cultureInfo.Calendar.GetWeekOfYear(jan1, cultureInfo.DateTimeFormat.CalendarWeekRule, cultureInfo.DateTimeFormat.FirstDayOfWeek);
        if (firstWeek is <= 1 or > 50)
            weekOfYear--;

        return firstWeekDay.AddDays(weekOfYear * 7);
    }

    /// <summary>
    /// Gets the default fist day of week
    /// </summary>
    /// <param name="date">Only gets the year of this parameter</param>
    /// <param name="weekOfYear"></param>
    /// <param name="cultureInfo"></param>
    /// <returns></returns>
    public static DateTime FirstDateOfWeek(this DateTime date, int weekOfYear, CultureInfo cultureInfo = null)
    {
        return FirstDateOfWeek(date, date.Year, weekOfYear, cultureInfo);
    }

    #endregion
}
