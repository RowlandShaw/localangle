using System;

namespace System.Data.Linq.Mapping
{
    /// <summary>
    /// Replicates just enough of LINQ to SQL to allow source compatibility with the non-Metro framework to designates a class as an entity class that is associated with a database table.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class TableAttribute : Attribute
    {
        public TableAttribute() { }

        /// <summary>
        /// Gets or sets the name of the table
        /// </summary>
        public string Name { get; set; }
    }
}
