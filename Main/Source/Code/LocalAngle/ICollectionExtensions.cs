using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LocalAngle
{
    public static class ICollectionExtensions
    {
        /// <summary>
        /// Adds the range of items to the collection.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection">The collection.</param>
        /// <param name="items">The items.</param>
        public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> items)
        {
            if (items != null && collection != null)
            {
                foreach (T item in items)
                {
                    collection.Add(item);
                }
            }
        }
    }
}
