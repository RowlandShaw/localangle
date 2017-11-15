using System;
using System.Collections.Generic;

namespace LocalAngle
{
    /// <summary>
    /// Internal helper used for getting the correct path for API calls, etc.
    /// </summary>
    public static class ApiHelper
    {
        private static Uri configBaseUri = new Uri("http://api.angle.uk.com/oauth/1.0/");
        /// <summary>
        /// Gets the base URI for API requests.
        /// </summary>
        /// <value>
        /// The base URI.
        /// </value>
        /// <remarks>
        /// Can be overriden by setting it at runtime
        /// </remarks>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        public static Uri BaseUri
        {
            get
            {
                return configBaseUri;
            }
            set
            {
                configBaseUri = value;
            }
        }
    }
}
