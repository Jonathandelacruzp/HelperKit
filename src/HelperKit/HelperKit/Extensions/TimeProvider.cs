using HelperKit.Interfaces;
using System;

namespace HelperKit
{
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
}