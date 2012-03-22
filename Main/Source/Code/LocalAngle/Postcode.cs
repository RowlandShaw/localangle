using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;

namespace LocalAngle
{
    /// <summary>
    /// Represents a UK Postcode
    /// </summary>
    /// <todo>TODO: Implment implicit conversion to a string, and explicit conversion from a string</todo>
    public class Postcode
    {
        #region Constructors

        public Postcode() : this( string.Empty) {}

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

            string value = postcode.ToUpper(CultureInfo.InvariantCulture);
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

        public override string ToString()
        {
            return this.Unit;
        }

        #endregion

        #region Private Static Properties

        private static Regex Parse = new Regex(@"^\s*(((A[BL]|B[ABDHLNRSTX]?|C[ABFHMORTVW]|D[ADEGHLNTY]|E[HNX]?|F[KY]|G[LUY]?|H[ADGPRSUX]|I[GMPV]|JE|K[ATWY]|L[ADELNSU]?|M[EKL]?|N[EGNPRW]?|O[LX]|P[AEHLOR]|R[GHM]|S[AEGKLMNOPRSTY]?|T[ADFNQRSW]|UB|W[ADFNRSV]|YO|ZE)([1-9]?[0-9])|(((E|N|NW|SE|SW|W)(1)|(EC)([1-4])|(WC)([12]))([A-HJKMNPR-Y])|(SW|W)([2-9]|[1-9][0-9])|(EC)([1-9][0-9])))\s*([0-9])([ABD-HJLNP-UW-Z]{2}))\s*$", RegexOptions.IgnoreCase);

        #endregion
    }
}
