using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LocalAngle.Net
{
    /// <summary>
    /// The different signature methods for use with OAuth
    /// </summary>
    public enum OAuthSignatureMethod
    {
        /// <summary>
        /// HMAC-SHA1
        /// </summary>
        HmacSha1,
        /// <summary>
        /// 
        /// </summary>
        Plaintext,
        /// <summary>
        /// RSA-SHA1
        /// </summary>
        RsaSha1
    }
}
