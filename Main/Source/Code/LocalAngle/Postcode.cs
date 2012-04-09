using System;
using System.ComponentModel;
using System.Globalization;
using System.Text.RegularExpressions;

namespace LocalAngle
{
    /// <summary>
    /// Represents a UK Postcode
    /// </summary>
    /// <todo>TODO: Implment implicit conversion to a string, and explicit conversion from a string</todo>
    [TypeConverter(typeof(PostcodeConverter))]
    public class Postcode
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Postcode"/> class.
        /// </summary>
        public Postcode() : this( string.Empty) {}

        /// <summary>
        /// Initializes a new instance of the <see cref="Postcode"/> class.
        /// </summary>
        /// <param name="postcode">The postcode.</param>
        public Postcode(string postcode)
        {
            _area = string.Empty;
            _district = string.Empty;
            _sector = ' ';
            _unit = string.Empty;

            if (string.IsNullOrEmpty(postcode))
            {
                return;
            }

            string value = postcode.ToUpperInvariant();
            var results = Postcode.Parse.Match(value);
            if (results.Success)
            {
                if ( !string.IsNullOrEmpty(results.Groups[11].Value) || !string.IsNullOrEmpty(results.Groups[9].Value) || !string.IsNullOrEmpty(results.Groups[7].Value))
                {
                    _area = results.Groups[7].Value + results.Groups[9].Value + results.Groups[11].Value;
                    _district = results.Groups[8].Value + results.Groups[10].Value + results.Groups[12].Value + results.Groups[13].Value;
                }
                else if (!string.IsNullOrEmpty(results.Groups[14].Value))
                {
                    _area = results.Groups[14].Value;
                    _district = results.Groups[15].Value;
                }
                else if (!string.IsNullOrEmpty(results.Groups[16].Value))
                {
                    _area = results.Groups[16].Value;
                    _district = results.Groups[17].Value;
                }
                else
                {
                    _area = results.Groups[3].Value;
                    _district = results.Groups[4].Value;
                }

                _sector = results.Groups[18].Value[0];
                _unit = results.Groups[19].Value;
            }
            else
            {
                throw new FormatException();
            }
        }

        #endregion

        #region Public Properties

        private string _area;
        /// <summary>
        /// Gets the area.
        /// </summary>
        /// <remarks>The Area is the 'OX' bit in OX11 0BT</remarks>
        public string Area { get { return _area; } }

        private string _district;
        /// <summary>
        /// Gets the district.
        /// </summary>
        /// <remarks>The District is the 'OX11' bit in OX11 0BT</remarks>
        public string District { get { return this.Area + _district; } }

        private char _sector;
        /// <summary>
        /// Gets the sector.
        /// </summary>
        /// <remarks>The Sector is the 'OX11 0' bit in OX11 0BT</remarks>
        public string Sector { get { return (this.District + " " + _sector).Trim(); } }

        private string _unit;
        /// <summary>
        /// Gets the unit (full postcode).
        /// </summary>
        public string Unit { get { return (this.Sector + _unit).Trim(); } }

        #endregion

        #region Public Methods

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return this.Unit;
        }

        #endregion

        #region Public Static Methods

        /// <summary>
        /// Performs an implicit conversion from <see cref="LocalAngle.Postcode"/> to <see cref="System.String"/>.
        /// </summary>
        /// <param name="postcode">The post code.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator string(Postcode postcode)
        {
            if (postcode == null)
            {
                return string.Empty;
            }

            return postcode.ToString();
        }

        #endregion

        #region Private Static Properties

        private static Regex Parse = new Regex(@"^\s*(((A[BL]|B[ABDHLNRSTX]?|C[ABFHMORTVW]|D[ADEGHLNTY]|E[HNX]?|F[KY]|G[LUY]?|H[ADGPRSUX]|I[GMPV]|JE|K[ATWY]|L[ADELNSU]?|M[EKL]?|N[EGNPRW]?|O[LX]|P[AEHLOR]|R[GHM]|S[AEGKLMNOPRSTY]?|T[ADFNQRSW]|UB|W[ADFNRSV]|YO|ZE)([1-9]?[0-9])|(((E|N|NW|SE|SW|W)(1)|(EC)([1-4])|(WC)([12]))([A-HJKMNPR-Y])|(SW|W)([2-9]|[1-9][0-9])|(EC)([1-9][0-9])))\s*([0-9])([ABD-HJLNP-UW-Z]{2}))\s*$", RegexOptions.IgnoreCase);

        #endregion
    }

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
