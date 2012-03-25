using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Windows.Security.Authentication.Web;
using Windows.Security.Cryptography.Core;
using Windows.Security.Cryptography;
using Windows.Storage.Streams;
using System.Text;

namespace LocalAngle.Net
{
    /// <summary>
    /// Wrapper for HttpWebRequest to handle OAuth (per http://tools.ietf.org/html/rfc5849)
    /// </summary>
    /// <remarks>
    /// This should greatly simplify clients wanting to make requests, as it'll automatically handle signing of requests.
    /// Currently does not support multipart requests (so you cannot upload media to Twitter with it *yet*.
    /// </remarks>
    public class OAuthWebRequest : WebRequest
    {
        #region Constructors

        public OAuthWebRequest(Uri uri, string consumerKey, string consumerSecret)
            : this(uri, new OAuthCredentials(consumerKey, consumerSecret, string.Empty, string.Empty))
        {
        }

        public OAuthWebRequest(Uri uri, string consumerKey, string consumerSecret, string token, string tokenSecret)
            : this(uri, new OAuthCredentials(consumerKey, consumerSecret, token, tokenSecret))
        {
        }

        [SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase", Justification= "OAuth requires the protocol be normalised in lower case.")]
        public OAuthWebRequest(Uri uri, IOAuthCredentials credentials)
        {
            if (uri == null)
            {
                throw new ArgumentNullException("uri");
            }

            if (string.Compare(uri.Scheme, "http", StringComparison.OrdinalIgnoreCase) != 0 && string.Compare(uri.Scheme, "https", StringComparison.OrdinalIgnoreCase) != 0)
            {
                throw new ArgumentOutOfRangeException("uri", "Only HTTP and HTTPS schemes are supported for OAuth requests.");
            }

            this.OAuthCredentials = credentials;

            // Extract any parameters from the URI
            RequestParameters = uri.GetParameters();

            // Create a new request with the normalised URI
            StringBuilder bob = new StringBuilder();
            bob.Append(uri.Scheme.ToLowerInvariant());
            bob.Append("://");
            bob.Append(uri.Host);
            if ((string.Compare(uri.Scheme, "http", StringComparison.OrdinalIgnoreCase) == 0 & uri.Port != 80) || (string.Compare(uri.Scheme, "https", StringComparison.OrdinalIgnoreCase) == 0 & uri.Port != 443))
            {
                bob.Append(':');
                bob.Append(uri.Port);
            }
            bob.Append(uri.AbsolutePath);

            this.Request = WebRequest.Create(new Uri(bob.ToString())) as HttpWebRequest;
        }

        #endregion

        #region Public Properties

        public override string ContentType
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override WebHeaderCollection Headers
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public IOAuthCredentials OAuthCredentials { get; set; }

        /// <summary>
        /// When overridden in a descendant class, gets or sets the protocol method to use in this request.
        /// </summary>
        /// <value></value>
        /// <returns>
        /// The protocol method to use in this request.
        /// </returns>
        /// <exception cref="T:System.NotImplementedException">
        /// If the property is not overridden in a descendant class, any attempt is made to get or set the property.
        /// </exception>
        /// <PermissionSet>
        /// 	<IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/>
        /// </PermissionSet>
        public override string Method
        {
            get
            {
                return Request.Method;
            }
            set
            {
                Request.Method = value;
            }
        }

        private string _nonce;
        /// <summary>
        /// Gets or sets the nonce to use for this request
        /// </summary>
        /// <remarks>
        /// You must not use the same nonce for the same <see cref="TimeStamp" /> to avoid replay attacks; you may choose to store both "just in case"
        /// </remarks>
        /// <value>The nonce.</value>
        /// <todo>TODO: Consider making this a little better</todo>
        public string Nonce
        {
            get
            {
                if (string.IsNullOrEmpty(_nonce))
                {
                    _nonce = NonceGenerator.Next(100000, 99999999).ToString();
                }

                return _nonce;
            }
            set
            {
                _nonce = value;
            }
        }

        public IList<RequestParameter> RequestParameters { get; private set; }

        /// <summary>
        /// When overridden in a descendant class, gets the URI of the Internet resource associated with the request.
        /// </summary>
        /// <value></value>
        /// <returns>
        /// A <see cref="T:System.Uri"/> representing the resource associated with the request
        /// </returns>
        /// <exception cref="T:System.NotImplementedException">
        /// Any attempt is made to get or set the property, when the property is not overridden in a descendant class.
        /// </exception>
        /// <PermissionSet>
        /// 	<IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/>
        /// </PermissionSet>
        public override Uri RequestUri
        {
            get
            {
                return Request.RequestUri;
            }
        }

        private OAuthSignatureMethod _signatureMethod = OAuthSignatureMethod.HmacSha1;
        /// <summary>
        /// Gets or sets the signature method.
        /// </summary>
        /// <value>The signature method.</value>
        [DefaultValue(OAuthSignatureMethod.HmacSha1)]
        public OAuthSignatureMethod SignatureMethod { 
            get
            {
                return _signatureMethod;
            }
            set 
            {
                switch (value)
                {
                    case OAuthSignatureMethod.Plaintext:
                    case OAuthSignatureMethod.HmacSha1:
                        _signatureMethod = value;
                        break;

                    case OAuthSignatureMethod.RsaSha1:
                        throw new NotSupportedException("Haven't needed to implement RSA-SHA1 hashing yet, so I haven't. Sorry about that.");

                    default:
                        throw new InvalidOperationException("Unexpected signature method");
                }
            }
        }

        private string _timeStamp;
        /// <summary>
        /// Gets the time stamp used for the query.
        /// </summary>
        /// <remarks>
        /// You must not use the same <see cref="Nonce"/> for the same time stamp to avoid replay attacks; you may choose to store both "just in case"
        /// </remarks>
        /// <value>The time stamp.</value>
        public string Timestamp
        {
            get
            {
                if (string.IsNullOrEmpty(_timeStamp))
                {
                    _timeStamp = DateTime.UtcNow.ToUnixTime().ToString();
                }

                return _timeStamp;
            }
        }

        #endregion

        #region Protected Properties

        protected HttpWebRequest Request { get; set; }

        protected string PostBody { get; set; }
        
        #endregion

        #region Public Methods

        public override void Abort()
        {
            Request.Abort();
        }

        public override IAsyncResult BeginGetRequestStream(AsyncCallback callback, object state)
        {
            throw new NotImplementedException();
        }

        public override IAsyncResult BeginGetResponse(AsyncCallback callback, object state)
        {
            Sign();
            return Request.BeginGetResponse(callback, state);
        }

        public override Stream EndGetRequestStream(IAsyncResult asyncResult)
        {
            throw new NotImplementedException();
        }

        public override WebResponse EndGetResponse(IAsyncResult asyncResult)
        {
            return Request.EndGetResponse(asyncResult);
        }

        public WebResponse GetResponse()
        {
            Sign();

            WebResponse resp = null;
            IAsyncResult res = Request.BeginGetResponse(callback =>
                {
                    resp = EndGetResponse(callback);
                }, null);

            res.AsyncWaitHandle.WaitOne();
            return resp;
        }

        #endregion

        #region Protected Methods

        protected void Sign()
        {
            if (OAuthCredentials == null)
            {
                throw new UnauthorizedAccessException("You must specify some credentials to sign an OAuth request");
            }

            // Validate bits
            if (string.IsNullOrEmpty(OAuthCredentials.ConsumerKey))
            {
                throw new InvalidOperationException("Unable to sign a request without a consumer key");
            }

            // Decouple from externally visible parameters
            // TODO: consider changing to use a list that maintains order.
            // TODO: If we support multipart uploads, Twitter reckons you don't include anything other than the oauth fields in the signature
            List<RequestParameter> sortedParameters = new List<RequestParameter>(RequestParameters);

            // Add in OAuth parameters
            sortedParameters.Add(new RequestParameter("oauth_consumer_key", EscapeDataString(OAuthCredentials.ConsumerKey)));

            // For Plain text these aren't required (after all, the secrets are transferred in plain text)
            sortedParameters.Add(new RequestParameter("oauth_nonce", EscapeDataString(Nonce)));
            sortedParameters.Add(new RequestParameter("oauth_timestamp", EscapeDataString(Timestamp)));

            switch (SignatureMethod)
            {
                    // TODO: consider using an attribute on the enum 
                case OAuthSignatureMethod.HmacSha1:
                    sortedParameters.Add(new RequestParameter("oauth_signature_method", "HMAC-SHA1"));
                    break;
                
                case OAuthSignatureMethod.Plaintext:
                    sortedParameters.Add(new RequestParameter("oauth_signature_method", "PLAINTEXT"));
                    break;

                case OAuthSignatureMethod.RsaSha1:
                    sortedParameters.Add(new RequestParameter("oauth_signature_method", "RSA-SHA1"));
                    break;
            }

            if (!string.IsNullOrEmpty(OAuthCredentials.Token))
            {
                // User token is specified, so let the server know (so it can look up the user secret and verify the request)
                sortedParameters.Add(new RequestParameter("oauth_token", EscapeDataString(OAuthCredentials.Token)));
            }

            sortedParameters.Add(new RequestParameter("oauth_version", "1.0"));

            // Force the order to be right (might become obsolete "later")
            sortedParameters.Sort(ParameterComparer);

            string normalisedParameters = string.Join("&", (from RequestParameter p in sortedParameters select p.ToString()).ToArray());

            // Build the digest
            string requestToSign = string.Format(CultureInfo.InvariantCulture, "{0}&{1}&{2}", Method.ToUpperInvariant(), EscapeDataString(Request.RequestUri.AbsoluteUri), EscapeDataString(normalisedParameters));

            switch (SignatureMethod)
            {
                // TODO: consider using an attribute on the enum 
                case OAuthSignatureMethod.HmacSha1:
                    IBuffer KeyMaterial = CryptographicBuffer.ConvertStringToBinary(string.Format(CultureInfo.InvariantCulture, "{0}&{1}", EscapeDataString(OAuthCredentials.ConsumerSecret), EscapeDataString(OAuthCredentials.TokenSecret)), BinaryStringEncoding.Utf8);
                    MacAlgorithmProvider HmacSha1Provider = MacAlgorithmProvider.OpenAlgorithm("HMAC_SHA1");
                    CryptographicKey MacKey = HmacSha1Provider.CreateKey(KeyMaterial);
                    IBuffer DataToBeSigned = CryptographicBuffer.ConvertStringToBinary(requestToSign, BinaryStringEncoding.Utf8);
                    IBuffer SignatureBuffer = CryptographicEngine.Sign(MacKey, DataToBeSigned);

                    sortedParameters.Add(new RequestParameter("oauth_signature", EscapeDataString(CryptographicBuffer.EncodeToBase64String(SignatureBuffer))));
                    break;

                case OAuthSignatureMethod.Plaintext:
                    sortedParameters.Add(new RequestParameter("oauth_signature", EscapeDataString(string.Format(CultureInfo.InvariantCulture, "{0}&{1}", OAuthCredentials.ConsumerSecret, OAuthCredentials.TokenSecret))));
                    break;

                case OAuthSignatureMethod.RsaSha1:
                    throw new NotImplementedException();
            }

            // Righty, rebuild the request
            // No need to UriEncode anything as the things we've added have been URI encoded as we went along :)
            normalisedParameters = string.Join("&", (from RequestParameter p in sortedParameters select p.ToString()).ToArray());
            if (string.Compare(Method, "GET", StringComparison.OrdinalIgnoreCase) == 0)
            {
                Uri targetUri = new Uri(string.Format(CultureInfo.InvariantCulture, "{0}?{1}", Request.RequestUri, normalisedParameters));
                HttpWebRequest newRequest = WebRequest.Create(targetUri) as HttpWebRequest;
                newRequest.Method = "GET";
                // TODO: If we expose more properties from the encapsulated HttpWebRequest, we'll need to copy them across here.
                Request = newRequest;
            }
            else if (string.Compare(Method, "POST", StringComparison.OrdinalIgnoreCase) == 0)
            {
                // TODO: Would need to do something magical if we were to support multi-part uploads here.
                Request.ContentType = "application/x-www-form-urlencoded";

                object Foo = new object();
                Request.BeginGetRequestStream(callback =>
                {
                    using (Stream req = Request.EndGetRequestStream(callback))
                    {
                        using (StreamWriter writer = new StreamWriter(req))
                        {
                            writer.Write(normalisedParameters);
                        }
                    }
                }, Foo);
            }
        }
        
        #endregion

        #region Private Static Properties

        private static Random NonceGenerator = new Random();

        #endregion

        #region Public Static Methods

        /// <summary>
        /// Creates an OAuthWebRequest for the specified URI.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <param name="consumerKey">The consumer (application) key.</param>
        /// <param name="consumerSecret">The consumer secret.</param>
        /// <param name="token">The token (user key).</param>
        /// <param name="tokenSecret">The token secret.</param>
        /// <returns></returns>
        public static OAuthWebRequest Create(Uri uri, IOAuthCredentials credentials)
        {
            if (uri == null)
            {
                throw new ArgumentNullException("uri");
            }

            if (string.Compare(uri.Scheme, "http", StringComparison.OrdinalIgnoreCase) != 0 && string.Compare(uri.Scheme, "https", StringComparison.OrdinalIgnoreCase) != 0)
            {
                throw new ArgumentOutOfRangeException("uri", "Only HTTP and HTTPS schemes are supported for OAuth requests.");
            }

            return new OAuthWebRequest(uri, credentials);
        }

        /// <summary>
        /// Creates an OAuthWebRequest for the specified URI.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <param name="consumerKey">The consumer (application) key.</param>
        /// <param name="consumerSecret">The consumer secret.</param>
        /// <param name="token">The token (user key).</param>
        /// <param name="tokenSecret">The token secret.</param>
        /// <returns></returns>
        public static OAuthWebRequest Create(Uri uri, string consumerKey, string consumerSecret, string token, string tokenSecret)
        {
            OAuthCredentials creds = new OAuthCredentials(consumerKey, consumerSecret, token, tokenSecret);
            return OAuthWebRequest.Create(uri, creds);
        }

        #endregion

        #region Protected Static Methods

        protected const string UriSafeCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_.~";
        /// <summary>
        /// The URI encoding providing by the .Net framework doesn't match the implementation specified by the OAuth spec, so reimplement per OAuth
        /// </summary>
        /// <param name="value">The value to Url encode</param>
        /// <returns>Returns a Url encoded string</returns>
        protected static string EscapeDataString(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return string.Empty;
            }

            StringBuilder result = new StringBuilder();

            for (int Index = 0; Index < value.Length; Index++)
            {
                char symbol = value[Index];
                if ((symbol >= 'A' && symbol <= 'Z') ||
                    (symbol >= 'a' && symbol <= 'z') ||
                    (symbol >= '0' && symbol <= '9') ||
                    symbol == '-' || 
                    symbol == '_' || 
                    symbol == '.' || 
                    symbol == '~')
                {
                    result.Append(symbol);
                }
                else
                {
                    result.Append('%' + String.Format(CultureInfo.InvariantCulture, "{0:X2}", (int)symbol));
                }
            }
            return result.ToString();
        }

        private static IComparer<RequestParameter> ParameterComparer = new Comparer<RequestParameter>();

        #endregion
    }
}

