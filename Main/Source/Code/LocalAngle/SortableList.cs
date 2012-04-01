using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace LocalAngle
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <remarks>Loosely based on code from http://msdn.microsoft.com/en-us/library/ms993236.aspx </remarks>
    public class SortableCollection<T> : BindingList<T>
    {
        #region Constructors

        public SortableCollection() : this( null ) { }

        public SortableCollection(IEnumerable<T> items)
            : base()
        {
            if (items != null)
            {
                foreach (T thisItem in items)
                {
                    Add(thisItem);
                }
            }
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets a value indicating whether to use a stable sort.
        /// </summary>
        /// <value>
        /// 	<see langword="true"/> if sorting should be stable; otherwise, <see langword="false"/>.
        /// </value>
        /// <remarks>
        /// Stable sorting can be useful if sorting by multiple properties in order, as it maintians order of values that share the same value for the property you're sorting on.
        /// </remarks>
        public bool UseStableSort { get; set; }

        #endregion

        #region Protected Properties

        /// <summary>
        /// Gets a value indicating whether the list is sorted.
        /// </summary>
        /// <returns>true if the list is sorted; otherwise, false. The default is false.
        ///   </returns>
        protected override bool IsSortedCore
        {
            get
            {
                return (this._sortProperty != null);
            }
        }

        private ListSortDirection _sortDirection;
        /// <summary>
        /// Gets the direction of the sort.
        /// </summary>
        /// <returns>
        /// One of the <see cref="T:System.ComponentModel.ListSortDirection"/> values. The default is <see cref="F:System.ComponentModel.ListSortDirection.Ascending"/>.
        ///   </returns>
        protected override ListSortDirection SortDirectionCore
        {
            get
            {
                return this._sortDirection;
            }
        }

        private PropertyDescriptor _sortProperty;
        /// <summary>
        /// Gets the property descriptor that is used for sort.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.ComponentModel.PropertyDescriptor"/> used for sorting the list.
        ///   </returns>
        protected override PropertyDescriptor SortPropertyCore
        {
            get
            {
                return this._sortProperty;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the list supports sorting.
        /// </summary>
        /// <returns>true</returns>
        protected override bool SupportsSortingCore
        {
            get
            {
                return true;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Sorts the items if overridden in a derived class; otherwise, throws a <see cref="T:System.NotSupportedException"/>.
        /// </summary>
        /// <param name="prop">A <see cref="T:System.ComponentModel.PropertyDescriptor"/> that specifies the property to sort on.</param>
        /// <param name="direction">One of the <see cref="T:System.ComponentModel.ListSortDirection"/>  values.</param>
        protected override void ApplySortCore(PropertyDescriptor prop, ListSortDirection direction)
        {
            if ((this.Items != null) && (this.Items is List<T>) && (prop != null))
            {
                List<T> items = (List<T>)this.Items;
                this._sortDirection = direction;
                this._sortProperty = prop;

                if (UseStableSort)
                {
                    // Stable sort => easy to sort by multiple columns.
                    MergeSort(items, 0, items.Count - 1, new PropertyComparer<T>(prop.Name));
                }
                else
                {
                    items.Sort(new PropertyComparer<T>(prop.Name));
                }

                if (direction == ListSortDirection.Descending)
                {
                    items.Reverse();
                }

                this.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, 0));
            }
        }

        /// <summary>
        /// Removes any sort applied with <see cref="M:System.ComponentModel.BindingList`1.ApplySortCore(System.ComponentModel.PropertyDescriptor,System.ComponentModel.ListSortDirection)"/> if sorting is implemented in a derived class; otherwise, raises <see cref="T:System.NotSupportedException"/>.
        /// </summary>
        protected override void RemoveSortCore()
        {
            this._sortProperty = null;
        }

        #endregion

        #region Private Static Methods

        /// <summary>
        /// MergeSort merge implementation
        /// </summary>
        /// <param name="items">The sz array.</param>
        /// <param name="lower">The n lower.</param>
        /// <param name="middle">The n middle.</param>
        /// <param name="upper">The n upper.</param>
        /// <param name="comp">The comp.</param>
        private static void Merge(ref List<T> items, int lower, int middle, int upper, PropertyComparer<T> comp)
        {
            List<T> buffer = new List<T>();

            // Merge two arrays
            int i = lower;
            int j = middle;
            while ((i < middle) & (j <= upper))
            {
                // Choose the smaller item - ties take from first to preseve stability.
                if (comp.Compare(items[i], items[j]) <= 0)
                {
                    buffer.Add(items[i]);
                    i++;
                }
                else
                {
                    buffer.Add(items[j]);
                    j++;
                }
            }

            // Merge remaining items
            while (i < middle)
            {
                buffer.Add(items[i]);
                i++;
            }
            while (j <= upper)
            {
                buffer.Add(items[j]);
                j++;
            }

            // Replace original array
            int index = 0;
            for (i = lower; i <= upper; i++)
            {
                items[i] = buffer[index];
                index++;
            }
        }

        /// <summary>
        /// MergeSort implementation
        /// </summary>
        /// <param name="items">The sz array.</param>
        /// <param name="lower">The n lower.</param>
        /// <param name="upper">The n upper.</param>
        /// <param name="comp">The comp.</param>
        private static void MergeSort(List<T> items, int lower, int upper, PropertyComparer<T> comp)
        {
            if ((upper - lower) == 1)
            {
                // Swap items if necessary
                if (comp.Compare(items[lower], items[upper]) > 0)
                {
                    T local = items[lower];
                    items[lower] = items[upper];
                    items[upper] = local;
                }
            }
            else if ((upper - lower) > 1)
            {
                // Sort each half and merge
                MergeSort(items, ((lower + upper) / 2) + 1, upper, comp);
                MergeSort(items, lower, (lower + upper) / 2, comp);
                Merge(ref items, lower, ((lower + upper) / 2) + 1, upper, comp);
            }
        }

        #endregion
    }
}
