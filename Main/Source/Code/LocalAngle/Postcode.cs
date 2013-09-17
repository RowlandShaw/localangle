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
#if !NETFX_CORE
    [TypeConverter(typeof(PostcodeConverter))]
#endif
    public class Postcode : IComparable, IComparable<Postcode>, IEquatable<Postcode>
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
                throw new FormatException(string.Format(CultureInfo.InvariantCulture, "'{0}' is not recognised as a valid UK postcode", value));
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
        /// Compares the current instance with another object of the same type and returns an integer that indicates whether the current instance precedes, follows, or occurs in the same position in the sort order as the other object.
        /// </summary>
        /// <param name="obj">An object to compare with this instance.</param>
        /// <returns>
        /// A value that indicates the relative order of the objects being compared. The return value has these meanings: Value Meaning Less than zero This instance precedes <paramref name="obj" /> in the sort order. Zero This instance occurs in the same position in the sort order as <paramref name="obj" />. Greater than zero This instance follows <paramref name="obj" /> in the sort order.
        /// </returns>
        public int CompareTo(object obj)
        {
            if (obj == null)
            {
                return -1;
            }

            Postcode other = obj as Postcode;
            if (other != null)
            {
                return CompareTo(other);
            }

            string str = obj.ToString();
            if (Postcode.IsValid(str))
            {
                return CompareTo(new Postcode(str));
            }

            throw new ArgumentOutOfRangeException("obj","Unable to compare a non-postcode to a postcode");
        }

        /// <summary>
        /// Compares the current object with another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// A 32-bit signed integer that indicates the relative order of the objects being compared. The return value has the following meanings: Value Meaning Less than zero This object is less than the other parameter.Zero This object is equal to other. Greater than zero This object is greater than other.
        /// </returns>
        public int CompareTo(Postcode other)
        {
            if (other == null)
            {
                return -1;
            }

            return string.Compare(ToString(), other.ToString(), StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Equalses the specified other.
        /// </summary>
        /// <param name="obj">The other.</param>
        /// <returns></returns>
        public override bool Equals(Object obj)
        {
            return Equals(obj as Postcode);
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the other parameter; otherwise, false.
        /// </returns>
        public bool Equals(Postcode other)
        {
            if (other == null)
            {
                return false;
            }

            return (CompareTo(other) == 0);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

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
        /// Comparison to see if the left is greater than the right.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns></returns>
        public static bool operator >( Postcode left, Postcode right)
        {
            if (left == null)
            {
                if (right == null)
                {
                    return false;
                }
                else
                {
                    return right.CompareTo(left) < 0;
                }
            }
            else
            {
                return left.CompareTo(right) > 0;
            }
        }

        /// <summary>
        /// Comparison to see if the left is less than than the right.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns></returns>
        public static bool operator <(Postcode left, Postcode right)
        {
            if (left == null)
            {
                if (right == null)
                {
                    return false;
                }
                else
                {
                    return right.CompareTo(left) > 0;
                }
            }
            else
            {
                return left.CompareTo(right) < 0;
            }
        }

        /// <summary>
        /// Comparison to see if the left is greater than or equal to the right.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns></returns>
        public static bool operator >=(Postcode left, Postcode right)
        {
            if (left == null)
            {
                if (right == null)
                {
                    return true;
                }
                else
                {
                    return right.CompareTo(left) <= 0;
                }
            }
            else
            {
                return left.CompareTo(right) >= 0;
            }
        }

        /// <summary>
        /// Comparison to see if the left is less than than or equal to the right.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns></returns>
        public static bool operator <=(Postcode left, Postcode right)
        {
            if (left == null)
            {
                if (right == null)
                {
                    return true;
                }
                else
                {
                    return right.CompareTo(left) >= 0;
                }
            }
            else
            {
                return left.CompareTo(right) <= 0;
            }
        }

        /// <summary>
        /// Comparison to see if the left is equal to the right.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns></returns>
        public static bool operator ==(Postcode left, Postcode right)
        {
            if (object.ReferenceEquals(left, right))
            {
                return true;
            }

            if (!object.ReferenceEquals(left, null))
            {
                return left.Equals(right);
            }
            else if (!object.ReferenceEquals(right, null))
            {
                return right.Equals(left);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Comparison to see if the left is different to the right.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns></returns>
        public static bool operator !=(Postcode left, Postcode right)
        {
            if (left == null)
            {
                return !(right == null);
            }
            else
            {
                return left.CompareTo(right) != 0;
            }
        }

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

        /// <summary>
        /// Determines whether the specified value is valid.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        ///   <c>true</c> if the specified value is valid; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsValid( string value )
        {
            var results = Postcode.Parse.Match(value);
            return results.Success;
        }

        #endregion

        #region Private Static Properties

        private static Regex Parse = new Regex(@"^\s*(((A[BL]|B[ABDHLNRSTX]?|C[ABFHMORTVW]|D[ADEGHLNTY]|E[HNX]?|F[KY]|G[LUY]?|H[ADGPRSUX]|I[GMPV]|JE|K[ATWY]|L[ADELNSU]?|M[EKL]?|N[EGNPRW]?|O[LX]|P[AEHLOR]|R[GHM]|S[AEGKLMNOPRSTY]?|T[ADFNQRSW]|UB|W[ADFNRSV]|YO|ZE)([1-9]?[0-9])|(((E|N|NW|SE|SW|W)(1)|(EC)([1-4])|(WC)([12]))([A-HJKMNPR-Y])|(SW|W)([2-9]|[1-9][0-9])|(EC)([1-9][0-9])))\s*([0-9])([ABD-HJLNP-UW-Z]{2}))\s*$", RegexOptions.IgnoreCase);

        #endregion
    }
}
