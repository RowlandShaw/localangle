using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
using Windows.Storage.Streams;

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

        /// <summary>
        /// Initializes a new instance of the <see cref="OAuthWebRequest"/> class.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <param name="consumerKey">The consumer key.</param>
        /// <param name="consumerSecret">The consumer secret.</param>
        public OAuthWebRequest(Uri uri, string consumerKey, string consumerSecret)
            : this(uri, new OAuthCredentials(consumerKey, consumerSecret, string.Empty, string.Empty))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OAuthWebRequest"/> class.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <param name="consumerKey">The consumer key.</param>
        /// <param name="consumerSecret">The consumer secret.</param>
        /// <param name="token">The token.</param>
        /// <param name="tokenSecret">The token secret.</param>
        public OAuthWebRequest(Uri uri, string consumerKey, string consumerSecret, string token, string tokenSecret)
            : this(uri, new OAuthCredentials(consumerKey, consumerSecret, token, tokenSecret))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OAuthWebRequest"/> class.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <param name="credentials">The credentials.</param>
        [SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase", Justification = "OAuth requires the protocol be normalised in lower case.")]
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
            bob.Append(uri.Host.ToLowerInvariant());
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

        /// <summary>
        /// Gets or sets the OAuth credentials.
        /// </summary>
        /// <value>
        /// The OAuth credentials.
        /// </value>
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
        /// You must not use the same nonce for the same <see cref="Timestamp" /> to avoid replay attacks; you may choose to store both "just in case"
        /// </remarks>
        /// <value>The nonce.</value>
        /// <todo>TODO: Consider making this a little better</todo>
        public string Nonce
        {
            get
            {
                if (string.IsNullOrEmpty(_nonce))
                {
                    _nonce = NonceGenerator.Next(100000, 99999999).ToString(CultureInfo.InvariantCulture);
                }

                return _nonce;
            }
            set
            {
                _nonce = value;
            }
        }

        /// <summary>
        /// Gets the request parameters.
        /// </summary>
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
        public OAuthSignatureMethod SignatureMethod
        {
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
                    _timeStamp = DateTime.UtcNow.ToUnixTime().ToString(CultureInfo.InvariantCulture);
                }

                return _timeStamp;
            }
        }

        #endregion

        #region Protected Properties

        /// <summary>
        /// Gets or sets the encapsulated request.
        /// </summary>
        /// <value>
        /// The request.
        /// </value>
        protected HttpWebRequest Request { get; set; }

        /// <summary>
        /// Gets or sets the post body.
        /// </summary>
        /// <value>
        /// The post body.
        /// </value>
        protected string PostBody { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Aborts the Request.
        /// </summary>
        /// <exception cref="T:System.NotImplementedException">Any attempt is made to access the method, when the method is not overridden in a descendant class. </exception>
        public override void Abort()
        {
            Request.Abort();
        }

        /// <summary>
        /// When overridden in a descendant class, provides an asynchronous version of the <see cref="M:System.Net.WebRequest.GetRequestStream"/> method.
        /// </summary>
        /// <param name="callback">The <see cref="T:System.AsyncCallback"/> delegate.</param>
        /// <param name="state">An object containing state information for this asynchronous request.</param>
        /// <returns>
        /// An <see cref="T:System.IAsyncResult"/> that references the asynchronous request.
        /// </returns>
        public override IAsyncResult BeginGetRequestStream(AsyncCallback callback, object state)
        {
            return Request.BeginGetRequestStream(callback, state);
        }

        public override IAsyncResult BeginGetResponse(AsyncCallback callback, object state)
        {
            Sign();
            return Request.BeginGetResponse(callback, state);
        }

        /// <summary>
        /// When overridden in a descendant class, returns a <see cref="T:System.IO.Stream"/> for writing data to the Internet resource.
        /// </summary>
        /// <param name="asyncResult">An <see cref="T:System.IAsyncResult"/> that references a pending request for a stream.</param>
        /// <returns>
        /// A <see cref="T:System.IO.Stream"/> to write data to.
        /// </returns>
        public override Stream EndGetRequestStream(IAsyncResult asyncResult)
        {
            return Request.EndGetRequestStream(asyncResult);
        }

        public override WebResponse EndGetResponse(IAsyncResult asyncResult)
        {
            return Request.EndGetResponse(asyncResult);
        }

        /// <summary>
        /// Returns a response to an OAuth request.
        /// </summary>
        /// <returns>
        /// A <see cref="WebResponse"/> containing the response to the OAuth request.
        /// </returns>
        /// <PermissionSet>
        ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/>
        /// </PermissionSet>
        /// <remarks>
        /// Just wraps the IAsync version
        /// </remarks>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        public WebResponse GetResponse()
        {
            IAsyncResult res = BeginGetResponse(callback => { }, null);
            SpinWait sw = new SpinWait();

            while (!res.IsCompleted)
            {
                sw.SpinOnce();
            }
            return EndGetResponse(res);
        }

        /// <summary>
        /// Returns a response to an OAuth request.
        /// </summary>
        /// <returns>
        /// A <see cref="WebResponse"/> containing the response to the OAuth request.
        /// </returns>
        /// <PermissionSet>
        ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/>
        /// </PermissionSet>
        public async Task<WebResponse> GetResponseAsync()
        {
            Sign();
            return await Request.GetResponseAsync();
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Performs the OAuth signing for the request
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times")]
        protected void Sign()
        {
            if (OAuthCredentials == null)
            {
                throw new UnauthorizedAccessException("You must specify some credentials to sign a request");
            }

            // Validate bits
            if (string.IsNullOrEmpty(OAuthCredentials.ConsumerKey))
            {
                throw new InvalidOperationException("Unable to sign a request without a consumer key");
            }

            // Decouple from externally visible parameters
            // TODO: consider changing to use a list that maintains order.
            // TODO: If we support multipart uploads, Twitter reckons you don't include anything other than the oauth fields in the signature
            List<RequestParameter> signingParameters = new List<RequestParameter>(RequestParameters);

            // Add in OAuth parameters
            signingParameters.Add(new RequestParameter("oauth_consumer_key", EscapeDataString(OAuthCredentials.ConsumerKey)));

            // For Plain text these aren't required (after all, the secrets are transferred in plain text)
            signingParameters.Add(new RequestParameter("oauth_nonce", EscapeDataString(Nonce)));
            signingParameters.Add(new RequestParameter("oauth_timestamp", EscapeDataString(Timestamp)));

            switch (SignatureMethod)
            {
                // TODO: consider using an attribute on the enum 
                case OAuthSignatureMethod.HmacSha1:
                    signingParameters.Add(new RequestParameter("oauth_signature_method", "HMAC-SHA1"));
                    break;

                case OAuthSignatureMethod.Plaintext:
                    signingParameters.Add(new RequestParameter("oauth_signature_method", "PLAINTEXT"));
                    break;

                case OAuthSignatureMethod.RsaSha1:
                    signingParameters.Add(new RequestParameter("oauth_signature_method", "RSA-SHA1"));
                    break;
            }

            if (!string.IsNullOrEmpty(OAuthCredentials.Token))
            {
                // User token is specified, so let the server know (so it can look up the user secret and verify the request)
                signingParameters.Add(new RequestParameter("oauth_token", EscapeDataString(OAuthCredentials.Token)));
            }

            signingParameters.Add(new RequestParameter("oauth_version", "1.0"));

            // Build the digest
            string baseString = GenerateBaseString(Method, Request.RequestUri, signingParameters);
            signingParameters.Add(new RequestParameter("oauth_signature", GenerateSignature(OAuthCredentials.ConsumerSecret, OAuthCredentials.TokenSecret, baseString, SignatureMethod)));

            // Righty, rebuild the request
            // No need to UriEncode anything as the things we've added have been URI encoded as we went along :)
            if (string.Compare(Method, "GET", StringComparison.OrdinalIgnoreCase) == 0)
            {
                string uri = string.Format(CultureInfo.InvariantCulture, "{0}?{1}", Request.RequestUri, NormalizeParameters(signingParameters));
                Uri targetUri = new Uri(uri);
                HttpWebRequest newRequest = WebRequest.Create(targetUri) as HttpWebRequest;
                newRequest.Method = "GET";
                // TODO: If we expose more properties from the encapsulated HttpWebRequest, we'll need to copy them across here.
                Request = newRequest;
            }
            else if (string.Compare(Method, "POST", StringComparison.OrdinalIgnoreCase) == 0)
            {
                // TODO: Would need to do something magical if we were to support multi-part uploads here.
                Request.ContentType = "application/x-www-form-urlencoded";

                // Convert the string into a byte array.
                string postData = NormalizeParameters(signingParameters);
                byte[] byteArray = Encoding.UTF8.GetBytes(postData);

                ManualResetEvent wh = new ManualResetEvent(false);
                BeginGetRequestStream(callback =>
                {
                    using (Stream req = EndGetRequestStream(callback))
                    {
                        // Write to the request stream.
                        req.Write(byteArray, 0, postData.Length);
                    }
                    wh.Set();
                }, Request);

                wh.WaitOne();
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
        /// <param name="credentials">An object that can provide OAuth credentials.</param>
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

        private const string UriSafeCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_.~";
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
            byte[] utf8bytes = System.Text.Encoding.UTF8.GetBytes(value);

            for (int Index = 0; Index < utf8bytes.Length; Index++)
            {
                byte symbol = utf8bytes[Index];
                if ((symbol >= 'A' && symbol <= 'Z') ||
                    (symbol >= 'a' && symbol <= 'z') ||
                    (symbol >= '0' && symbol <= '9') ||
                    symbol == '-' ||
                    symbol == '_' ||
                    symbol == '.' ||
                    symbol == '~')
                {
                    result.Append((char)symbol);
                }
                else
                {
                    result.Append('%' + String.Format(CultureInfo.InvariantCulture, "{0:X2}", (byte)symbol));
                }
            }
            return result.ToString();
        }

        /// <summary>
        /// Generates the base string.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <param name="uri">The URI.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        protected static string GenerateBaseString(string method, Uri uri, IEnumerable<RequestParameter> parameters)
        {
            if (string.IsNullOrEmpty(method))
            {
                throw new ArgumentOutOfRangeException("method", "There must be a HTTP verb specified");
            }

            if (uri == null)
            {
                throw new ArgumentNullException("uri", "A URI must be specified");
            }

            if (parameters == null)
            {
                throw new ArgumentNullException("parameters", "There must be some parameters specified to generate the base string");
            }

            return string.Format(CultureInfo.InvariantCulture, "{0}&{1}&{2}", method.ToUpperInvariant(), EscapeDataString(uri.AbsoluteUri), EscapeDataString(NormalizeParameters(parameters))); // Yes, really escape the data twice
        }

        /// <summary>
        /// Generates the signature for the given consumer secret, token secret and base string using the specified method.
        /// </summary>
        /// <param name="consumerSecret">The consumer secret.</param>
        /// <param name="tokenSecret">The token secret.</param>
        /// <param name="baseString">The base string.</param>
        /// <param name="method">The method.</param>
        /// <returns></returns>
        /// <remarks>Shouldn't escape its output, as the token will most likely be normalised again.</remarks>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "string")]
        protected static string GenerateSignature(string consumerSecret, string tokenSecret, string baseString, OAuthSignatureMethod method)
        {
            switch (method)
            {
                case OAuthSignatureMethod.HmacSha1:
                    return GenerateHmacSignature(consumerSecret, tokenSecret, baseString);

                case OAuthSignatureMethod.Plaintext:
                    return GeneratePlaintextSignature(consumerSecret, tokenSecret);

                case OAuthSignatureMethod.RsaSha1:
                    throw new NotImplementedException();

                default:
                    throw new ArgumentOutOfRangeException("method", "Unexpected signature method");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="consumerSecret"></param>
        /// <param name="tokenSecret"></param>
        /// <param name="baseString"></param>
        /// <returns></returns>
        /// <remarks>The HMAC-SHA1 signature method uses the HMAC-SHA1 signature algorithm as defined in [RFC2104] where the Signature Base String is the text and the key is the concatenated values (each first encoded per Parameter Encoding) of the Consumer Secret and Token Secret, separated by an '&amp;' character (ASCII code 38) even if empty.</remarks>
        private static string GenerateHmacSignature(string consumerSecret, string tokenSecret, string baseString)
        {
            IBuffer KeyMaterial = CryptographicBuffer.ConvertStringToBinary(string.Format(CultureInfo.InvariantCulture, "{0}&{1}", EscapeDataString(consumerSecret), EscapeDataString(tokenSecret)), BinaryStringEncoding.Utf8);
            MacAlgorithmProvider HmacSha1Provider = MacAlgorithmProvider.OpenAlgorithm("HMAC_SHA1");
            CryptographicKey MacKey = HmacSha1Provider.CreateKey(KeyMaterial);
            IBuffer DataToBeSigned = CryptographicBuffer.ConvertStringToBinary(baseString, BinaryStringEncoding.Utf8);
            IBuffer SignatureBuffer = CryptographicEngine.Sign(MacKey, DataToBeSigned);

            return CryptographicBuffer.EncodeToBase64String(SignatureBuffer);
        }

        private static string GeneratePlaintextSignature(string consumerSecret, string tokenSecret)
        {
            return string.Format(CultureInfo.InvariantCulture, "{0}&{1}", EscapeDataString(consumerSecret), EscapeDataString(tokenSecret));
        }

        /// <summary>
        /// Normalise parameters into order required for OAuth
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        protected static string NormalizeParameters(IEnumerable<RequestParameter> parameters)
        {
            List<RequestParameter> sortedParameters = new List<RequestParameter>(parameters);
            sortedParameters.Sort(ParameterComparer);
            return string.Join("&", (from RequestParameter p in sortedParameters select string.Format(CultureInfo.InvariantCulture, "{0}={1}", EscapeDataString(p.Name), EscapeDataString(p.Value))).ToArray());
        }

        private static IComparer<RequestParameter> ParameterComparer = new Comparer<RequestParameter>();

        #endregion
    }
}

