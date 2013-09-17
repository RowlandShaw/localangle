using System;

namespace LocalAngle
{
    /// <summary>
    /// Internal helper used for getting the correct path for API calls, etc.
    /// </summary>
    internal static class ApiHelper
    {
        private static Uri configBaseUri = new Uri("http://api.angle.uk.com/oauth/1.0/");
        /// <summary>
        /// Gets the base URI for API requests.
        /// </summary>
        /// <value>
        /// The base URI.
        /// </value>
        /// <remarks>
        /// TODO: Add ability to override, if required.
        /// </remarks>
        public static Uri BaseUri
        {
            get
            {
                return configBaseUri;
            }
        }
    }
}
