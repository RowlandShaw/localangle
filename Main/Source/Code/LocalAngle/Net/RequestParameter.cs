using System;
using System.Collections.Generic;
using System.Globalization;

namespace LocalAngle.Net
{
    /// <summary>
    /// Represents a request parmeter used in HttpWebRequests, either on the URI (for a GET query), or as part of the request body (for POST requests)
    /// </summary>
    public struct RequestParameter : IComparable<RequestParameter>, IEquatable<RequestParameter>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestParameter"/> struct.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        public RequestParameter(string name, string value)
        {
            _name = name;
            _value = value;
        }

        #endregion

        #region Public Properties

        private string _name;
        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name
        {
            get
            {
                return _name;
            }
        }

        private string _value;
        /// <summary>
        /// Gets the value.
        /// </summary>
        public string Value
        {
            get
            {
                return (_value == null ? string.Empty :_value);
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Compares the current object with another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// A 32-bit signed integer that indicates the relative order of the objects being compared. The return value has the following meanings: Value Meaning Less than zero This object is less than the other parameter.Zero This object is equal to other. Greater than zero This object is greater than other.
        /// </returns>
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

        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object"/> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
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

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the other parameter; otherwise, false.
        /// </returns>
        public bool Equals(RequestParameter other)
        {
            return CompareTo(other) == 0;
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
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

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator ==(RequestParameter left, RequestParameter right)
        {
            return object.ReferenceEquals(left, right) || left.Equals(right);
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator !=(RequestParameter left, RequestParameter right)
        {
            return !(left == right);
        }

        /// <summary>
        /// Implements the operator &gt;.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator >(RequestParameter left, RequestParameter right)
        {
            if (left == null)
            {
                return (right == null ? false : true);
            }

            return left.CompareTo(right) > 0;
        }

        /// <summary>
        /// Implements the operator &lt;=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator <=(RequestParameter left, RequestParameter right)
        {
            return !(left > right);
        }

        /// <summary>
        /// Implements the operator &gt;=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator >=(RequestParameter left, RequestParameter right)
        {
            return !(left < right);
        }

        /// <summary>
        /// Implements the operator &lt;.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator <(RequestParameter left, RequestParameter right)
        {
            if (left == null)
            {
                return false;
            }

            return left.CompareTo(right) < 0;
        }

        #endregion
    }

    /// <summary>
    /// Extensions for objects implementing generic interfaces involving <see cref="RequestParameter"/>
    /// </summary>
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
