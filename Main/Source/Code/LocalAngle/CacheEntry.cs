using System;
using System.ComponentModel;

namespace LocalAngle
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class CacheEntry<T>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CacheEntry&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="data">The data.</param>
        public CacheEntry(object data)
        {
            if ((this.Options & CacheOptions.UseWeakReferences) == CacheOptions.UseWeakReferences)
            {
                this._data = new WeakReference(data);
            }
            else
            {
                this._data = data;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CacheEntry&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="options">The options.</param>
        public CacheEntry(object data, CacheOptions options)
        {
            this._options = options;
            if ((options & CacheOptions.UseWeakReferences) == CacheOptions.UseWeakReferences)
            {
                this._data = new WeakReference(data);
            }
            else
            {
                this._data = data;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CacheEntry&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="maxAge">The max age.</param>
        public CacheEntry(object data, TimeSpan maxAge)
        {
            if ((this.Options & CacheOptions.UseWeakReferences) == CacheOptions.UseWeakReferences)
            {
                this._data = new WeakReference(data);
            }
            else
            {
                this._data = data;
            }
            this._maxage = maxAge;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CacheEntry&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="options">The options.</param>
        /// <param name="birth">The birth.</param>
        public CacheEntry(object data, CacheOptions options, DateTime birth)
        {
            this._birth = birth;
            this._options = options;
            if ((options & CacheOptions.UseWeakReferences) == CacheOptions.UseWeakReferences)
            {
                this._data = new WeakReference(data);
            }
            else
            {
                this._data = data;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CacheEntry&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="options">The options.</param>
        /// <param name="maxAge">The max age.</param>
        public CacheEntry(object data, CacheOptions options, TimeSpan maxAge)
        {
            this._maxage = maxAge;
            this._options = options;
            if ((options & CacheOptions.UseWeakReferences) == CacheOptions.UseWeakReferences)
            {
                this._data = new WeakReference(data);
            }
            else
            {
                this._data = data;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CacheEntry&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="options">The options.</param>
        /// <param name="birth">The birth.</param>
        /// <param name="maxAge">The max age.</param>
        public CacheEntry(object data, CacheOptions options, DateTime birth, TimeSpan maxAge)
        {
            this._birth = birth;
            this._maxage = maxAge;
            this._options = options;
            if ((options & CacheOptions.UseWeakReferences) == CacheOptions.UseWeakReferences)
            {
                this._data = new WeakReference(data);
            }
            else
            {
                this._data = data;
            }
        }

        #endregion

        #region Public Properties

        private object _data;
        /// <summary>
        /// Gets the data.
        /// </summary>
        public T Data
        {
            get
            {
                if ((this.Options & CacheOptions.UseWeakReferences) == CacheOptions.UseWeakReferences)
                {
                    return (T)((WeakReference)this._data).Target;
                }
                return (T)this._data;
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

        private DateTime _birth = DateTime.Now;
        private TimeSpan _maxage = new TimeSpan(0, 10, 0);
        /// <summary>
        /// Gets a value indicating whether this <see cref="CacheEntry&lt;T&gt;"/> is stale.
        /// </summary>
        /// <value>
        /// 	<see langword="true"/> if stale; otherwise, <see langword="false"/>.
        /// </value>
        public bool Stale
        {
            get
            {
                if ((this.Options & CacheOptions.UseWeakReferences) == CacheOptions.UseWeakReferences)
                {
                    if (this._maxage.TotalSeconds == TimeSpan.MaxValue.TotalSeconds)
                    {
                        if ((this._data != null) && ((WeakReference)this._data).IsAlive)
                        {
                            return false;
                        }
                        return true;
                    }
                    if (((this._data != null) && (DateTime.Compare(this._birth.Add(this._maxage), DateTime.Now) > 0)) && ((WeakReference)this._data).IsAlive)
                    {
                        return false;
                    }
                    return true;
                }
                if (this._maxage.TotalSeconds == TimeSpan.MaxValue.TotalSeconds)
                {
                    return (this._data == null);
                }
                if ((this._data != null) && (DateTime.Compare(this._birth.Add(this._maxage), DateTime.Now) > 0))
                {
                    return false;
                }
                return true;
            }
        }

        #endregion
    }
}
