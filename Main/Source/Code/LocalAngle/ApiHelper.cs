using System;
using System.Collections.Generic;
using System.Configuration;

namespace LocalAngle
{
    /// <summary>
    /// Internal helper used for getting the correct path for API calls, etc.
    /// </summary>
    internal static class ApiHelper
    {
        private static string configBaseUri;
        /// <summary>
        /// Gets the base URI for API requests.
        /// </summary>
        /// <value>
        /// The base URI.
        /// </value>
        /// <remarks>
        /// Can be overriden in the app.config/web.config if you want to point at a different server for testing purposes.
        /// <code>
        /// &lt;configuration&gt;
        /// &lt;appSettings&gt;
        /// &lt;add key="LocalAngle.BaseUri" value="http://api.ipswich-angle.com/oauth/1.0/" /&gt;
        /// &lt;!-- along with any other settings --&gt;
        /// &lt;/appSettings&gt;
        /// &lt;!-- along with any other sections --&gt;
        /// &lt;/configuration&gt;
        /// </code>
        /// </remarks>
        public static Uri BaseUri
        {
            get
            {
                if (string.IsNullOrEmpty(configBaseUri))
                {
                    configBaseUri = ConfigurationManager.AppSettings["LocalAngle.BaseUri"];

                    if (string.IsNullOrEmpty(configBaseUri))
                    {
                        configBaseUri = "http://api.angle.uk.com/oauth/1.0/";
                    }
                }
                return new Uri(configBaseUri);
            }
        }
    }
}
