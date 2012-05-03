using System;

namespace System.ComponentModel.DataAnnotations
{
    /// <summary>
    /// Specifies that a data field value is required.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class RequiredAttribute : ValidationAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequiredAttribute"/> class.
        /// </summary>
        public RequiredAttribute() { }

        /// <summary>
        /// Gets or sets a value indicating whether empty strings are allowed.
        /// </summary>
        /// <value>
        /// 	<see langword="true"/> if [allow empty strings]; otherwise, <see langword="false"/>.
        /// </value>
        public bool AllowEmptyStrings { get; set; }
    }
}
