﻿using HelperKit.Interfaces;
using HelperKit.Test.Extensions;
using System;

namespace HelperKit.Test.Models
{
    public class UtcTimeProvider : ITimeProvider
    {
        public DateTime Now => DateTime.UtcNow;
        public TimeZoneInfo TimeZoneInfo => TimeZoneInfo.Utc;
    }

    public class LocalTimeProvider : ITimeProvider
    {
        public DateTime Now => DateTime.Now;
        public TimeZoneInfo TimeZoneInfo => TimeZoneInfo.Local;
    }

    public class CustomTimeProvider : ITimeProvider
    {
        private const string Name = "Custom Provider";
        private const int TimeDifference = DateTimeExtensionsUnitTest.Hours;
        private TimeZoneInfo _timeZoneInfo;

        public DateTime Now => DateTime.UtcNow.Add(_timeZoneInfo.BaseUtcOffset);
        public TimeZoneInfo TimeZoneInfo => _timeZoneInfo ??= TimeZoneInfo.CreateCustomTimeZone(Name, TimeSpan.FromHours(TimeDifference), Name, Name);
    }
}