using System;

namespace LocalAngle
{
    public class Comparer<T> : System.Collections.Generic.Comparer<T> where T : IComparable<T>
    {
        public override int Compare(T x, T y)
        {
            return x.CompareTo(y);
        }
    }
}
