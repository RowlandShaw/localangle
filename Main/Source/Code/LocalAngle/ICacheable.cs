namespace LocalAngle
{
    /// <summary>
    /// Interface to indicate that an object is can be loaded and used in a cache
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICacheable<T>
    {
        /// <summary>
        /// Gets a value indicating whether this instance is cacheable.
        /// </summary>
        /// <value>
        /// <see langword="true" /> if this instance is cacheable; otherwise, <see langword="false" />.
        /// </value>
        bool IsCacheable
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
        /// <remarks>It would be 'better' if this could be defined on the interface as readonly, but VB sucks at its implementation of interfaces, and this would later break XML Serialisation</remarks>
        T Id
        {
            get;
            set;
        }

        /// <summary>
        /// Loads the item with the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        void Load(T id);
    }
}
