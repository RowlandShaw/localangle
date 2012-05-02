using System;

namespace LocalAngle
{
    /// <summary>
    /// To denote how frequently something happens
    /// </summary>
    /// <remarks>
    /// For english locales, ToString() should return a value that can be used in a sentence of the form "per [x]"
    /// </remarks>
    public enum Recurrence
    {
        /// <summary>
        /// Recurs every hour
        /// </summary>
        Hour,
        /// <summary>
        /// Recurs every day
        /// </summary>
        Day,
        /// <summary>
        /// Recurs every week day
        /// </summary>
        WeekDay,
        /// <summary>
        /// Recurs every seven days
        /// </summary>
        Week,
        /// <summary>
        /// Recurs every fourteen days
        /// </summary>
        Fortnight,
        /// <summary>
        /// Recurs once a calendar month
        /// </summary>
        Month,
        /// <summary>
        /// Recurs once a year
        /// </summary>
        Year
    }
}
