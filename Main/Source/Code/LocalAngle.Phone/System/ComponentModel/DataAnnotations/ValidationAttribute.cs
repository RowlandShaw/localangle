using System;

namespace System.ComponentModel.DataAnnotations
{
    /// <summary>
    /// Serves as the base class for all validation attributes.
    /// </summary>
    public abstract class ValidationAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationAttribute"/> class.
        /// </summary>
        protected ValidationAttribute() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationAttribute"/> class by using the error message to associate with a validation control.
        /// </summary>
        /// <param name="errorMessage">The error message to associate with a validation control.</param>
        protected ValidationAttribute(string errorMessage) { this.ErrorMessage = errorMessage; }

        /// <summary>
        /// Gets or sets an error message to associate with a validation control if validation fails
        /// </summary>
        /// <value>
        /// The error message.
        /// </value>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Gets or sets the name of the error message resource.
        /// </summary>
        /// <value>
        /// The name of the error message resource.
        /// </value>
        public string ErrorMessageResourceName { get; set; }

        /// <summary>
        /// Gets or sets the type of the error message resource.
        /// </summary>
        /// <value>
        /// The type of the error message resource.
        /// </value>
        public Type ErrorMessageResourceType { get; set; }

        /// <summary>
        /// Formats the error message.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public virtual string FormatErrorMessage(string name) { throw new NotImplementedException(); }

        /// <summary>
        /// Checks whether the specified value is valid with respect to the current validation.
        /// </summary>
        /// <param name="value">The value to validate.</param>
        /// <param name="validationContext">The context information about the validation operation.</param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "value"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "validationContext")]
        public ValidationResult GetValidationResult(object value, ValidationContext validationContext) { throw new NotImplementedException(); }

        /// <summary>
        /// Determines whether the current value is valid.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        ///   <see langword="true"/> if the specified value is valid; otherwise, <see langword="false"/>.
        /// </returns>
        public virtual bool IsValid(object value) { throw new NotImplementedException(); }

        /// <summary>
        /// Validates the specified value.
        /// </summary>
        /// <param name="value">The value of the object to validate.</param>
        /// <param name="name">The name to include in the error message.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "value"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "name")]
        public void Validate(object value, string name) { throw new NotImplementedException(); }
        
        /// <summary>
        /// Validates the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="validationContext">The validation context.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "value"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "validationContext")]
        public void Validate(object value, ValidationContext validationContext) { throw new NotImplementedException(); }
    }
}