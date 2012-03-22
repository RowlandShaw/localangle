using System;
using System.Net;

namespace LocalAngle
{
    public interface IOAuthCredentials
    {
        /// <summary>
        /// Gets or sets the consumer (application) key.
        /// </summary>
        /// <value>The consumer key.</value>
        string ConsumerKey { get; set; }

        /// <summary>
        /// Gets or sets the consumer secret.
        /// </summary>
        /// <value>The consumer secret.</value>
        string ConsumerSecret { get; set; }

        /// <summary>
        /// Gets or sets the token (user key).
        /// </summary>
        /// <value>The token.</value>
        string Token { get; set; }

        /// <summary>
        /// Gets or sets the token secret.
        /// </summary>
        /// <value>The token secret.</value>
        string TokenSecret { get; set; }
    }
}
