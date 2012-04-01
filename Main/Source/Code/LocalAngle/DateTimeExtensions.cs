using System;

namespace LocalAngle
{
    /// <summary>
    /// Extensions to the framework's DateTime class
    /// </summary>
    public static class DateTimeExtensions
    {
        private static DateTime EpochStart = new DateTime(1970, 1, 1);

        /// <summary>
        /// Converts the datetime to a unix time.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <remarks>This implementation is year 2038 compliant, as it uses a 64bit integer to represent the number of seconds since 1st Jan 1970</remarks>
        public static Int64 ToUnixTime(this DateTime value)
        {
            TimeSpan diff = value - EpochStart;
            return Convert.ToInt64(diff.TotalSeconds);
        }
    }
}
