using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LocalAngle
{
    [Flags] public enum CacheOptions
    {
        None = 0,
        /// <summary>
        /// Use WeakReference objects to hold the cached objects
        /// </summary>
        /// <remarks>Use this option where memory constraints are tight</remarks>
        UseWeakReferences = 1
    }
}
