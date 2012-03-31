using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
    
namespace LocalAngle.Net
{
    /// <summary>
    /// Represents a request parmeter used in HttpWebRequests, either on the URI (for a GET query), or as part of the request body (for POST requests)
    /// </summary>
    public struct RequestParameter : IComparable<RequestParameter>, IEquatable<RequestParameter>
    {
        #region Constructors

        public RequestParameter(string name, string value)
        {
            _name = name;
            _value = value;
        }

        #endregion

        #region Public Properties

        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
        }

        private string _value;
        public string Value
        {
            get
            {
                return (_value == null ? string.Empty :_value);
            }
        }

        #endregion

        #region Public Methods

        public int CompareTo(RequestParameter other)
        {
            if (object.ReferenceEquals(this, other))
            {
                return 0;
            }

            if (object.ReferenceEquals(null, other))
            {
                return -1;
            }

            int retval = string.Compare(this.Name, other.Name, StringComparison.Ordinal);

            if (retval == 0)
            {
                retval = string.Compare(this.Value, other.Value, StringComparison.Ordinal);
            }

            return retval;
        }

        public override bool Equals(object obj)
        {
            if (obj is RequestParameter)
            {
                return CompareTo((RequestParameter)obj) == 0;
            }
            else
            {
                return false;
            }
        }

        public bool Equals(RequestParameter other)
        {
            return CompareTo(other) == 0;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance, escaped ready for use on a URI.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "{0}={1}", Uri.EscapeDataString(Name), Uri.EscapeDataString(Value));
        }

        #endregion

        #region Public Static Methods

        public static bool operator ==(RequestParameter left, RequestParameter right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(RequestParameter left, RequestParameter right)
        {
            return !left.Equals(right);
        }

        public static bool operator >(RequestParameter left, RequestParameter right)
        {
            return left.CompareTo(right) > 0;
        }

        public static bool operator <=(RequestParameter left, RequestParameter right)
        {
            return left.CompareTo(right) <= 0;
        }

        public static bool operator >=(RequestParameter left, RequestParameter right)
        {
            return left.CompareTo(right) >= 0;
        }

        public static bool operator <(RequestParameter left, RequestParameter right)
        {
            return left.CompareTo(right) < 0;
        }

        #endregion
    }

    public static class RequestParameterExtensions
    {
        /// <summary>
        /// Extension method on the Uri class to return a collection of parameters
        /// </summary>
        public static IList<RequestParameter> GetParameters(this Uri uri)
        {
            if (uri == null)
            {
                throw new ArgumentNullException("uri");
            }

            if (string.Compare(uri.Scheme, "http", StringComparison.OrdinalIgnoreCase) != 0 && string.Compare(uri.Scheme, "https", StringComparison.OrdinalIgnoreCase) != 0)
            {
                throw new ArgumentOutOfRangeException("uri", "Only HTTP and HTTPS schemes are supported for retrieving request parameterss.");
            }

            List<RequestParameter> retval = new List<RequestParameter>();
            string raw = uri.Query.Replace('+', ' ');
            if (raw.StartsWith("?", StringComparison.OrdinalIgnoreCase))
            {
                // Remove the first character
                raw = raw.Remove(0, 1);
            }

            if (string.IsNullOrEmpty(raw))
            {
                // Bail early if there's nothing here to see
                return retval;
            }

            string[] pairs = raw.Split('&');
            foreach (string pair in pairs)
            {
                if (!string.IsNullOrEmpty(pair))
                {
                    int index = pair.IndexOf('=');
                    if (index < 0)
                    {
                        retval.Add(new RequestParameter(Uri.UnescapeDataString(pair), string.Empty));
                    }
                    else
                    {
                        retval.Add(new RequestParameter(Uri.UnescapeDataString(pair.Substring(0, index)), Uri.UnescapeDataString(pair.Substring(index + 1, pair.Length - (index + 1)))));
                    }
                }
            }

            return retval;
        }
    }
}
