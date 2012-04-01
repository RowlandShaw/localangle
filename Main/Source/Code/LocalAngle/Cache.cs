using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace LocalAngle
{
    /// <summary>
    /// Generic cache of items implementing <see cref="ICacheable{T}"/>
    /// </summary>
    /// <typeparam name="TItem">The type of the item.</typeparam>
    /// <typeparam name="TId">The type of the id, by which items can be loaded.</typeparam>
    public class Cache<TItem, TId> where TItem : ICacheable<TId>, new()
    {
        #region Constructors 

        /// <summary>
        /// Initializes a new instance of the <see cref="Cache&lt;TItem, TId&gt;"/> class.
        /// </summary>
        public Cache()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Cache&lt;TItem, TId&gt;"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public Cache(CacheOptions options)
        {
            this._options = options;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Cache&lt;TItem, TId&gt;"/> class.
        /// </summary>
        /// <param name="maxAge">The max age.</param>
        public Cache(TimeSpan maxAge)
        {
            this._maxage = maxAge;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Cache&lt;TItem, TId&gt;"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="maxAge">The max age.</param>
        public Cache(CacheOptions options, TimeSpan maxAge)
        {
            this._maxage = maxAge;
            this._options = options;
        }

        #endregion

        #region Public Properties

        private TimeSpan _maxage = new TimeSpan(0, 10, 0);
        /// <summary>
        /// Gets or sets the maximum age that an item is held in the cache before being dropped.
        /// </summary>
        /// <value>
        /// The max age.
        /// </value>
        public TimeSpan MaxAge
        {
            get
            {
                return this._maxage;
            }
            set
            {
                this._maxage = value;
            }
        }

        private CacheOptions _options = CacheOptions.UseWeakReferences;
        /// <summary>
        /// Gets the options.
        /// </summary>
        [DefaultValue(CacheOptions.UseWeakReferences)]
        public CacheOptions Options
        {
            get
            {
                return this._options;
            }
        }

        #endregion

        #region Private Properties

        private Dictionary<TId, CacheEntry<TItem>> _cache = new Dictionary<TId, CacheEntry<TItem>>();
        private object _cacheLock = new object();
        private DateTime _lastCacheCheck;

        #endregion

        #region Public Methods

        /// <summary>
        /// Loads the item with the specified id from the cache.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        /// <remarks>If the item is not already in the cache, it will be loaded and added to the cache.</remarks>
        public TItem Load(TId id)
        {
            if ((DateTime.Compare(this._lastCacheCheck, DateTime.MinValue) != 0) && (this._lastCacheCheck.Minute != DateTime.Now.Minute))
            {
                Dictionary<TId, bool> removeKeys = new Dictionary<TId, bool>();
                foreach (TId thisKey in this._cache.Keys)
                {
                    if (this._cache[thisKey].Stale)
                    {
                        removeKeys.Add(thisKey, false);
                    }
                }
                foreach (TId thisKey in removeKeys.Keys)
                {
                    this._cache.Remove(thisKey);
                }
            }
            this._lastCacheCheck = DateTime.Now;
            if (this._cache.ContainsKey(id))
            {
                if (this._cache[id].Stale)
                {
                    this._cache.Remove(id);
                }
                else
                {
                    TItem retval = this._cache[id].Data;
                    if (retval != null)
                    {
                        return retval;
                    }
                }
            }
            TItem data = Activator.CreateInstance<TItem>();
            if (!EqualityComparer<TId>.Default.Equals(id, default(TId)))
            {
                data.Load(id);
            }

            lock (this._cacheLock)
            {
                if (data.IsCacheable && !this._cache.ContainsKey(id))
                {
                    this._cache.Add(id, new CacheEntry<TItem>(data, this._options, this._maxage));
                }
            }
            return data;
        }

        /// <summary>
        /// Preloads the specified items.
        /// </summary>
        /// <param name="items">The items.</param>
        public void Preload(IEnumerable<TItem> items)
        {
            this.Preload(items, this.Options, this.MaxAge);
        }

        /// <summary>
        /// Preloads the specified items.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <param name="options">The options.</param>
        public void Preload(IEnumerable<TItem> items, CacheOptions options)
        {
            this.Preload(items, options, this.MaxAge);
        }

        /// <summary>
        /// Preloads the specified items.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <param name="maxAge">The max age.</param>
        public void Preload(IEnumerable<TItem> items, TimeSpan maxAge)
        {
            this.Preload(items, this.Options, maxAge);
        }

        /// <summary>
        /// Preloads the specified items.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <param name="options">The options.</param>
        /// <param name="maxAge">The max age.</param>
        public virtual void Preload(IEnumerable<TItem> items, CacheOptions options, TimeSpan maxAge)
        {
            if (items != null)
            {
                foreach (TItem current in items)
                {
                    if (current.IsCacheable)
                    {
                        TId id = current.Id;

                        lock (this._cacheLock)
                        {
                            if (!this._cache.ContainsKey(id))
                            {
                                this._cache.Add(id, new CacheEntry<TItem>(current, options, maxAge));
                            }
                        }
                    }
                }
            }
        }

        #endregion
    }
}
