using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;

namespace LocalAngle
{
    public class PropertyComparer<T> : IComparer<T>
    {
        #region Constructors

        public PropertyComparer(string propertyToSort)
            : this(propertyToSort, ListSortDirection.Ascending)
        {
            PropertyToSort = propertyToSort;
        }

        public PropertyComparer(string propertyToSort, ListSortDirection sortOrder)
        {
            PropertyToSort = propertyToSort;
            SortOrder = sortOrder;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// The property to use to sort on
        /// </summary>
        /// <value>
        /// The property to sort.
        /// </value>
        [DefaultValue("")]
        public string PropertyToSort { get; set; }

        /// <summary>
        /// Gets or sets the sort order.
        /// </summary>
        /// <value>
        /// The sort order.
        /// </value>
        [DefaultValue(0)]
        public ListSortDirection SortOrder { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Performs a comparison of two objects of the same type and returns a value indicating whether one is less than, equal to, or greater than the other
        /// </summary>
        /// <param name="x">The first object to compare.</param>
        /// <param name="y">The second object to compare.</param>
        /// <returns><list>
        /// <item><term>Less than zero</term><description><paramref name="x" /> is less than <paramref name="y" /></description></item>
        /// <item><term>Zero</term><description><paramref name="x" /> equals <paramref name="y" /></description></item>
        /// <item><term>Less than zero</term><description><paramref name="x" /> is greater than <paramref name="y" /></description></item>
        /// </list></returns>
        /// <remarks>If <paramref name="x" /> and <paramref name="y" /> are of the same type, and it implements <see cref="System.IComparable" />, then <paramref name="x" />.CompareTo(<paramref name="y" />) is returned; Otherwise a string comparison is performed.</remarks>
        public int Compare(T x, T y)
        {
            PropertyInfo property = x.GetType().GetProperty(this.PropertyToSort);
            object xvalue = property.GetValue(x, null);
            object yvalue = property.GetValue(y, null);
            bool isAscending = (this.SortOrder == ListSortDirection.Ascending);
            bool isDescending = (this.SortOrder == ListSortDirection.Descending);
            if (!isAscending && !isDescending)
            {
                return 0;
            }
            if ((xvalue == null) || (yvalue == null))
            {
                if (xvalue == null)
                {
                    xvalue = string.Empty;
                }
                if (yvalue == null)
                {
                    yvalue = string.Empty;
                }
                if (xvalue.ToString() == yvalue.ToString())
                {
                    return 0;
                }
                if (String.Compare(xvalue.ToString(), yvalue.ToString(), false, CultureInfo.CurrentCulture) > 0)
                {
                    if (isAscending)
                    {
                        return 1;
                    }
                    return -1;
                }
                if (isAscending)
                {
                    return -1;
                }
                return 1;
            }
            IComparable xcomp = xvalue as IComparable;
            if (xcomp != null)
            {
                // See if this is IComparable
                if (isAscending)
                {
                    return xcomp.CompareTo(yvalue);
                }
                return (-1 * xcomp.CompareTo(yvalue));
            }

            // otherwise fallback to lexiographical sort
            if (xvalue == null)
            {
                xvalue = string.Empty;
            }
            if (yvalue == null)
            {
                yvalue = string.Empty;
            }
            if (xvalue.ToString() == yvalue.ToString())
            {
                return 0;
            }
            if (String.Compare(xvalue.ToString(), yvalue.ToString(), false, CultureInfo.CurrentCulture) > 0)
            {
                if (isAscending)
                {
                    return 1;
                }
                return -1;
            }
            if (isAscending)
            {
                return -1;
            }
            return 1;
        }

        #endregion
    }
}
