using System;

namespace LocalAngle
{
    /// <summary>
    /// The options for caching
    /// </summary>
    [Flags] public enum CacheOptions
    {
        /// <summary>
        /// Don't use WeakReferences.
        /// </summary>
        /// <remarks>Warning, if combined with a lare maximum age, this can lead to very memory hungry caches</remarks>
        None = 0,
        /// <summary>
        /// Use WeakReference objects to hold the cached objects
        /// </summary>
        /// <remarks>Use this option where memory constraints are tight</remarks>
        UseWeakReferences = 1
    }
}
