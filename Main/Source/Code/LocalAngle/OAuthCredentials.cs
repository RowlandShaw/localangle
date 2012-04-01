using System;

namespace LocalAngle
{
    /// <summary>
    /// A simple OAuth credential provider
    /// </summary>
    /// <remarks>
    /// Does not handle persistance at all. 
    /// </remarks>
    public struct OAuthCredentials : IOAuthCredentials, IEquatable<IOAuthCredentials>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="OAuthCredentials"/> struct.
        /// </summary>
        /// <param name="consumerKey">The consumer key.</param>
        /// <param name="consumerSecret">The consumer secret.</param>
        public OAuthCredentials(string consumerKey, string consumerSecret) : this( consumerKey, consumerSecret, string.Empty, string.Empty ) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="OAuthCredentials"/> struct.
        /// </summary>
        /// <param name="consumerKey">The consumer key.</param>
        /// <param name="consumerSecret">The consumer secret.</param>
        /// <param name="token">The token.</param>
        /// <param name="tokenSecret">The token secret.</param>
        public OAuthCredentials(string consumerKey, string consumerSecret, string token, string tokenSecret)
        {
            _consumerKey = consumerKey;
            _consumerSecret = consumerSecret;
            _token = token;
            _tokenSecret = tokenSecret;
        }

        #endregion

        #region Public Properties

        private string _consumerKey;
        /// <summary>
        /// Gets or sets the consumer (application) key.
        /// </summary>
        /// <value>The consumer key.</value>
        public string ConsumerKey { 
            get
            {
                return _consumerKey;
            }
            set
            {
                _consumerKey = value;
            }
        }

        private string _consumerSecret;
        /// <summary>
        /// Gets or sets the consumer secret.
        /// </summary>
        /// <value>The consumer secret.</value>
        public string ConsumerSecret
        {
            get
            {
                return _consumerSecret;
            }
            set
            {
                _consumerSecret = value;
            }
        }

        private string _token;
        /// <summary>
        /// Gets or sets the token (user key).
        /// </summary>
        /// <value>The token.</value>
        public string Token
        {
            get
            {
                return _token;
            }
            set
            {
                _token = value;
            }
        }

        private string _tokenSecret;
        /// <summary>
        /// Gets or sets the token secret.
        /// </summary>
        /// <value>The token secret.</value>
        public string TokenSecret
        {
            get
            {
               return _tokenSecret;
            }
            set
            {
                _tokenSecret = value;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object"/> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            IOAuthCredentials other = obj as IOAuthCredentials;
            return Equals(other);
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the other parameter; otherwise, false.
        /// </returns>
        public bool Equals(IOAuthCredentials other)
        {
            if (other == null)
            {
                return false;
            }
            else
            {
                return other.ConsumerKey == this.ConsumerKey &&
                    other.ConsumerSecret == this.ConsumerSecret &&
                    other.Token == this.Token &&
                    other.TokenSecret == this.TokenSecret;
            }
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Concat(this.ConsumerKey, ":", this.Token);
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
        public static bool operator ==(OAuthCredentials left, IOAuthCredentials right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator !=(OAuthCredentials left, IOAuthCredentials right)
        {
            return !left.Equals(right);
        }

        #endregion
    }
}
