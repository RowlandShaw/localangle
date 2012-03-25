using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace LocalAngle
{
    public class Cache<TItem, TId> where TItem : ICacheable<TId>, new()
    {
        #region Constructors 

        public Cache()
        {
        }

        public Cache(CacheOptions options)
        {
            this._options = options;
        }

        public Cache(TimeSpan maxAge)
        {
            this._maxage = maxAge;
        }

        public Cache(CacheOptions options, TimeSpan maxAge)
        {
            this._maxage = maxAge;
            this._options = options;
        }

        #endregion

        #region Public Properties

        private TimeSpan _maxage = new TimeSpan(0, 10, 0);
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

        public void Preload(IEnumerable<TItem> items)
        {
            this.Preload(items, this.Options, this.MaxAge);
        }

        public void Preload(IEnumerable<TItem> items, CacheOptions options)
        {
            this.Preload(items, options, this.MaxAge);
        }

        public void Preload(IEnumerable<TItem> items, TimeSpan maxAge)
        {
            this.Preload(items, this.Options, maxAge);
        }

        public virtual void Preload(IEnumerable<TItem> items, CacheOptions options, TimeSpan maxAge)
        {
            foreach( TItem current in items)
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

        #endregion
    }
}
