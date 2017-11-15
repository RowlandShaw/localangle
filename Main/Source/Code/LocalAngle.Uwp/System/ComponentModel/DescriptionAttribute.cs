using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.ComponentModel
{
    /// <summary>
    /// Specifies a description for a property or event.
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    public class DescriptionAttribute : Attribute
    {
        /// <summary>
        ///     Specifies the default value for the System.ComponentModel.DescriptionAttribute,
        ///     which is an empty string (""). This static field is read-only.
        /// </summary>
        public static readonly DescriptionAttribute Default;

        /// <summary>
        ///     Initializes a new instance of the System.ComponentModel.DescriptionAttribute
        ///     class with no parameters.
        /// </summary>
        public DescriptionAttribute() : this(string.Empty) { }

        /// <summary>
        /// Initializes a new instance of the System.ComponentModel.DescriptionAttribute
        /// class with a description.
        /// </summary>
        /// <param name="description">The description text.</param>
        public DescriptionAttribute(string description) { DescriptionValue = description; }

        /// <summary>
        ///     Gets the description stored in this attribute.
        /// </summary>
        /// <returns>
        ///     The description stored in this attribute.
        /// </returns>
        public virtual string Description { get { return DescriptionValue; } }

        /// <summary>
        /// Gets or sets the string stored as the description.
        /// </summary>
        /// <value>
        /// The description value.
        /// </value>
        /// <returns>
        /// The string stored as the description. The default value is an empty string ("").        
        /// </returns>
        protected string DescriptionValue { get; set; }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to the current System.ComponentModel.DescriptionAttribute; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            DescriptionAttribute other = obj as DescriptionAttribute;
            if (object.ReferenceEquals(other, null))
            {
                return false;
            }

            return string.Equals(Description, other.Description);
        }

        public override int GetHashCode()
        {
            return Description.GetHashCode();
        }
    }
}