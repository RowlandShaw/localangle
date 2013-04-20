using System;

namespace System.Data.Linq
{
    /// <summary>
    /// Specifies when concurrency conflicts should be reported.
    /// </summary>
    /// <remarks>
    /// This is mostly for source compatibility with other platforms, until Linq to SQL becomes an option for Windows Store apps 
    /// *grumble*
    /// </remarks>
    public enum ConflictMode
    {
        /// <summary>
        /// Specifies that attempts to update should stop immediately when the first concurrency conflict error is detected.
        /// </summary>
        /// <remarks>
        /// Retained for source compatibility with the real System.Data.Linq.ConflictMode - in reality, our Metro style wrapper
        /// won't initially even look for conflicts, as it is a very naive impletation
        /// </remarks>
        FailOnFirstConflict = 0,
        /// <summary>
        /// Specifies that all updates should be tried, and that concurrency conflicts should be accumulated and returned at 
        /// the end of the process.
        /// </summary>
        /// <remarks>
        /// Retained for source compatibility with the real System.Data.Linq.ConflictMode - in reality, our Metro style wrapper
        /// won't initially even look for conflicts, as it is a very naive impletation
        /// </remarks>
        ContinueOnConflict = 1,
    }
}