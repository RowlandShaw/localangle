using System;
using System.Collections.Generic;

namespace Microsoft.VisualStudio.TestTools.UnitTesting
{
    /// <summary>
    /// Just enough to allow source compatibility between VS2008 and VS2010
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public sealed class TestCategoryAttribute : Attribute
    {
        /// <summary>
        /// All a bit of a mock
        /// </summary>
        /// <param name="testCategory">VS2010 onwards uses this to categorise tests</param>
        public TestCategoryAttribute(string testCategory) { }

        /// <summary>
        /// If we actually bothered to implement this, this would return the list of categories
        /// </summary>
        public IList<string> TestCategories { get { throw new NotImplementedException(); } }
    }
}
