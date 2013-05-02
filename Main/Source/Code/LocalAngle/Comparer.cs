using System;
using System.Collections.Generic;

namespace LocalAngle
{
    /// <summary>
    /// A simple comparer that can compare anything implementing <see cref="IComparable{T}"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Comparer<T> : Comparer<T> where T : IComparable<T>
    {
        /// <summary>
        /// Performs a comparison of two objects of the same type and returns a value indicating whether one object is less than, equal to, or greater than the other.
        /// </summary>
        /// <param name="x">The first object to compare.</param>
        /// <param name="y">The second object to compare.</param>
        /// <returns>
        /// Value Condition Less than zero x is less than y.Zero x equals y.Greater than zero x is greater than y.
        /// </returns>
        public override int Compare(T x, T y)
        {
            return x.CompareTo(y);
        }
    }
}
