using System;

namespace HelperKit.Interfaces
{
    public interface ITimeProvider
    {
        public DateTime Now { get; }
        public TimeZoneInfo TimeZoneInfo { get; }
    }
}