using System;

namespace LocalAngle
{
    public static class DateTimeExtensions
    {
        private static DateTime EpochStart = new DateTime(1970, 1, 1);

        public static Int64 ToUnixTime(this DateTime value)
        {
            TimeSpan diff = value - EpochStart;
            return Convert.ToInt64(diff.TotalSeconds);
        }
    }
}
