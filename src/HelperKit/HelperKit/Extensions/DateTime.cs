using System;
using System.Globalization;

namespace HelperKit
{
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
        /// <param name="val"></param>
        /// <param name="def"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this object val, DateTime def)
        {
            return DateTime.TryParse(val.ToString(), out var result) ? result : def;
        }

        /// <summary>
        /// Converts a value to DateTime
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this object val)
        {
            return ToDateTime(val, DateTime.MinValue);
        }

        /// <summary>
        /// Get the full calendar format string
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string ToFullCalendarDate(this DateTime val)
        {
            return val.ToString("yyyy-MM-dd hh:mm:ss");
        }

        /// <summary>
        /// Get the short calendar format string
        /// </summary>
        /// <param name="val"></param>
        /// <param name="cultureInfo"></param>
        /// <returns></returns>
        public static string ToDateTimeFormatByCulture(this DateTime val, CultureInfo cultureInfo = null)
        {
            cultureInfo ??= CultureInfo.CurrentCulture;
            return val.ToString(cultureInfo.DateTimeFormat.ShortDatePattern);
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
        /// <param name="year"></param>
        /// <param name="weekOfYear"></param>
        /// <param name="cultureInfo"></param>
        /// <returns></returns>
        public static DateTime FirstDateOfWeek(int year, int weekOfYear, CultureInfo cultureInfo = null)
        {
            cultureInfo ??= CultureInfo.CurrentCulture;
            var jan1 = new DateTime(year, 1, 1);
            var daysOffset = (int) cultureInfo.DateTimeFormat.FirstDayOfWeek - (int) jan1.DayOfWeek;
            var firstWeekDay = jan1.AddDays(daysOffset);
            var firstWeek = cultureInfo.Calendar.GetWeekOfYear(jan1, cultureInfo.DateTimeFormat.CalendarWeekRule, cultureInfo.DateTimeFormat.FirstDayOfWeek);
            if (firstWeek <= 1 || firstWeek > 50)
                weekOfYear--;

            return firstWeekDay.AddDays(weekOfYear * 7);
        }

        #endregion
    }
}