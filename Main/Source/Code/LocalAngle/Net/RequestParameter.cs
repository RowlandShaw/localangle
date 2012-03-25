using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Globalization;

namespace LocalAngle.Net
{
    /// <summary>
    /// Represents a request parmeter used in HttpWebRequests, either on the URI (for a GET query), or as part of the request body (for POST requests)
    /// </summary>
    public struct RequestParameter : IComparable<RequestParameter>, IEquatable<RequestParameter>
    {
        public RequestParameter(string name, string value)
        {
            _name = name;
            _value = value;
        }

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
                return _value;
            }
        }

        public int CompareTo(RequestParameter other)
        {
            int retval = string.Compare(this.Name, other.Name, StringComparison.Ordinal);

            if (retval == 0)
            {
                retval = string.Compare(this.Value, other.Value, StringComparison.Ordinal);
            }

            return retval;
        }

        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "{0}={1}", Name, Value);
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
            string raw = uri.Query;
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
                        retval.Add(new RequestParameter(pair, string.Empty));
                    }
                    else
                    {
                        retval.Add(new RequestParameter(pair.Substring(0,index), pair.Substring(index + 1,pair.Length - (index + 1))));
                    }
                }
            }

            return retval;
        }
    }
}
