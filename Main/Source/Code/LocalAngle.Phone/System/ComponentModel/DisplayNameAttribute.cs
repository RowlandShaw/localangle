using System;
using System.Runtime;

namespace System.ComponentModel
{
    /// <summary>
    /// Represents a Display name attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Event)]
    public class DisplayNameAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DisplayNameAttribute"/> class.
        /// </summary>
        public DisplayNameAttribute() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="DisplayNameAttribute"/> class using the display name.
        /// </summary>
        /// <param name="displayName">The display name.</param>
        public DisplayNameAttribute(string displayName) { this.DisplayName = displayName; }

        /// <summary>
        /// Gets the display name.
        /// </summary>
        public virtual string DisplayName { get; private set; }
    }
}