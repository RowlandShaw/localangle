using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;

namespace LocalAngle
{
    /// <summary>
    /// Really naive type converter
    /// </summary>
    public class PostcodeConverter : TypeConverter
    {
        /// <summary>
        /// Returns whether the type converter can convert an object from the specified type to the type of this converter.
        /// </summary>
        /// <param name="context">An object that provides a format context.</param>
        /// <param name="sourceType">The type you want to convert from.</param>
        /// <returns>
        /// true if this converter can perform the conversion; otherwise, false.
        /// </returns>
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return (sourceType == typeof(string));
        }

        /// <summary>
        /// Returns whether the type converter can convert an object to the specified type.
        /// </summary>
        /// <param name="context">An object that provides a format context.</param>
        /// <param name="destinationType">The type you want to convert to.</param>
        /// <returns>
        /// true if this converter can perform the conversion; otherwise, false.
        /// </returns>
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return (destinationType == typeof(string));
        }

        /// <summary>
        /// Converts from the specified value to the intended conversion type of the converter.
        /// </summary>
        /// <param name="context">An object that provides a format context.</param>
        /// <param name="culture">The <see cref="T:System.Globalization.CultureInfo"/> to use as the current culture.</param>
        /// <param name="value">The value to convert to the type of this converter.</param>
        /// <returns>
        /// The converted value.
        /// </returns>
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value == null)
            {
                return null;
            }

            if (CanConvertFrom(context, value.GetType()))
            {
                return new Postcode(value as string);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        /// <summary>
        /// Converts the specified value object to the specified type.
        /// </summary>
        /// <param name="context">An object that provides a format context.</param>
        /// <param name="culture">The <see cref="T:System.Globalization.CultureInfo"/> to use as the current culture.</param>
        /// <param name="value">The object to convert.</param>
        /// <param name="destinationType">The type to convert the object to.</param>
        /// <returns>
        /// The converted object.
        /// </returns>
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (value == null)
            {
                return null;
            }

            if (CanConvertTo(context, value.GetType()))
            {
                return value.ToString();
            }
            else
            {
                throw new InvalidOperationException();
            }
        }
    }
}
