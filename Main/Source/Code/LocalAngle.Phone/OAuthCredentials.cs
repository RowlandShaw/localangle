using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LocalAngle
{
    public struct OAuthCredentials : IOAuthCredentials
    {
        #region Constructors

        public OAuthCredentials(string consumerKey, string consumerSecret) : this( consumerKey, consumerSecret, string.Empty, string.Empty ) { }

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
    }
}
