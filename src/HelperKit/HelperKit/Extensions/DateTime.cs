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

        public static DateTime ToDateTime(this object val, DateTime def)
        {
            return DateTime.TryParse(val.ToString(), out var result) ? result : def;
        }

        public static DateTime ToDateTime(this object val)
        {
            return ToDateTime(val, DateTime.MinValue);
        }

        public static string ToFullCalendarDate(this DateTime val)
        {
            return val.ToString("yyyy-MM-dd hh:mm:ss");
        }

        public static string ToDateTimeFormatByCulture(this DateTime val, CultureInfo culture)
        {
            var format = culture.DateTimeFormat.ShortDatePattern;
            return val.ToString(format);
        }

        public static int GetWeekNumber(this DateTime date)
        {
            var ci = CultureInfo.CurrentCulture;
            var weekNum = ci.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
            return weekNum;
        }

        public static DateTime FirstDateOfWeek(int year, int weekOfYear, CultureInfo culture = null)
        {
            culture ??= CultureInfo.CurrentCulture;
            var jan1 = new DateTime(year, 1, 1);
            var daysOffset = (int) culture.DateTimeFormat.FirstDayOfWeek - (int) jan1.DayOfWeek;
            var firstWeekDay = jan1.AddDays(daysOffset);
            var firstWeek = culture.Calendar.GetWeekOfYear(jan1, culture.DateTimeFormat.CalendarWeekRule, culture.DateTimeFormat.FirstDayOfWeek);
            if (firstWeek <= 1 || firstWeek > 50)
                weekOfYear--;

            return firstWeekDay.AddDays(weekOfYear * 7);
        }

        #endregion
    }
}