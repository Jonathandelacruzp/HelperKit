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
        /// <param name="Start"></param>
        /// <param name="End"></param>
        /// <returns></returns>
        public static Boolean IsBetween(this DateTime val, DateTime Start, DateTime End) => val >= Start && val <= End;

        #endregion

        #region DateTime Convert

        public static DateTime ToDateTime(this object val, DateTime def)
        {
            if (DateTime.TryParse(val.ToString(), out DateTime reval))
                return reval;

            return def;
        }

        public static DateTime ToDateTime(this object val) => ToDateTime(val, DateTime.MinValue);

        public static string ToFullCallendarDate(this DateTime val) => val.ToString("yyyy-MM-dd hh:mm:ss");

        public static string ToDateTimeFormatByCulture(this DateTime val, CultureInfo culture)
        {
            var format = culture.DateTimeFormat.ShortDatePattern;
            return val.ToString(format);
        }

        public static int GetWeekNumber(this DateTime date)
        {
            CultureInfo ci = CultureInfo.CurrentCulture;
            var weekNum = ci.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
            return weekNum;
        }

        public static DateTime FirstDateOfWeek(int year, int weekOfYear, CultureInfo culture = null)
        {
            culture ??= CultureInfo.CurrentCulture;
            var jan1 = new DateTime(year, 1, 1);
            var daysOffset = (int)culture.DateTimeFormat.FirstDayOfWeek - (int)jan1.DayOfWeek;
            var firstWeekDay = jan1.AddDays(daysOffset);
            var firstWeek = culture.Calendar.GetWeekOfYear(jan1, culture.DateTimeFormat.CalendarWeekRule, culture.DateTimeFormat.FirstDayOfWeek);
            if (firstWeek <= 1 || firstWeek > 50)
                weekOfYear--;

            return firstWeekDay.AddDays(weekOfYear * 7);
        }

        #endregion
    }
}