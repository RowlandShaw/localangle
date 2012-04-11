namespace LocalAngle
{
    /// <summary>
    /// Interface to denote that an object can provide credentials for OAuth
    /// </summary>
    public interface IOAuthCredentials
    {
        /// <summary>
        /// Gets or sets the consumer (application) key.
        /// </summary>
        /// <value>The consumer key.</value>
        string ConsumerKey { get; }

        /// <summary>
        /// Gets or sets the consumer secret.
        /// </summary>
        /// <value>The consumer secret.</value>
        string ConsumerSecret { get; }

        /// <summary>
        /// Gets or sets the token (user key).
        /// </summary>
        /// <value>The token.</value>
        string Token { get; }

        /// <summary>
        /// Gets or sets the token secret.
        /// </summary>
        /// <value>The token secret.</value>
        string TokenSecret { get; }
    }
}
