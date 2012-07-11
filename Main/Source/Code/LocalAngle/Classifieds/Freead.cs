using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Globalization;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using LocalAngle.Net;
using System.IO;

namespace LocalAngle.Classifieds
{
    /// <summary>
    /// Represents a classified advert
    /// </summary>
    [DataContract]
    [Table]
    public class Freead : BindableBase, IGeoLocation
    {
        #region Public Properties

        private int _advertId;
        /// <summary>
        /// Gets or sets a unique identifier for the classified advert.
        /// </summary>
        /// <value>
        /// The classified advert id.
        /// </value>
        [DataMember]
        [Column(IsPrimaryKey = true)]
        public int AdvertId
        {
            get
            {
                return _advertId;
            }
            set
            {
                OnPropertyChanged("AdvertId", ref _advertId, value);
            }
        }

        private AdvertType _advertType = AdvertType.ForSale;
        /// <summary>
        /// Gets or sets the type of the advert.
        /// </summary>
        /// <value>
        /// The type of the advert.
        /// </value>
        [Column(DbType = "INT", UpdateCheck = UpdateCheck.Never)]
        public AdvertType AdvertType
        {
            get
            {
                return _advertType;
            }
            set
            {
                OnPropertyChanged("AdvertType", ref _advertType, value);
            }
        }

        private string _contact;
        /// <summary>
        /// Gets or sets the contact details.
        /// </summary>
        /// <value>
        /// The contact details.
        /// </value>
        [DataMember]
        [Column(DbType = "NVARCHAR(255)", UpdateCheck = UpdateCheck.Never)]
        public string ContactDetails
        {
            get
            {
                return _contact;
            }
            set
            {
                OnPropertyChanged("ContactDetails", ref _contact, value);
            }
        }

        private string _description;
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        [DataMember]
        [Column(DbType = "NTEXT", UpdateCheck = UpdateCheck.Never)]
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                OnPropertyChanged("Description", ref _description, value);
            }
        }

        /// <summary>
        /// Gets a value indicating whether this advert has expired.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this advert has expired; otherwise, <c>false</c>.
        /// </value>
        public bool IsExpired 
        {
            get
            {
                return (RenewalDate < DateTime.Now);
            }
        }

        private double _latitude = 90;
        /// <summary>
        /// Gets or sets the latitude in decimal degrees.
        /// </summary>
        /// <value>
        /// The latitude.
        /// </value>
        /// <remarks>Uses the WGS84 datum</remarks>
        [DataMember]
        [Column(UpdateCheck= UpdateCheck.Never)]
        public double Latitude
        {
            get
            {
                return _latitude;
            }
            set
            {
                OnPropertyChanged("Latitude", ref _latitude, value);
            }
        }

        private double _longitude = 90;
        /// <summary>
        /// Gets or sets the longitude in decimal degrees.
        /// </summary>
        /// <value>
        /// The longitude.
        /// </value>
        /// <remarks>Uses the WGS84 datum</remarks>
        [DataMember]
        [Column(UpdateCheck= UpdateCheck.Never)]
        public double Longitude
        {
            get
            {
                return _longitude;
            }
            set
            {
                OnPropertyChanged("Longitude", ref _longitude, value);
            }
        }

        private DateTime _lastModified;
        /// <summary>
        /// Gets or sets the last modification time.
        /// </summary>
        /// <value>
        /// The last modified.
        /// </value>
        [DataMember]
        [Column(UpdateCheck= UpdateCheck.Never)]
        public DateTime LastModified
        {
            get
            {
                return _lastModified;
            }
            set
            {
                OnPropertyChanged("LastModified", ref _lastModified, value);
            }
        }

        private string _name;
        /// <summary>
        /// Gets or sets the item being advertised.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [DataMember]
        [Column(DbType = "NVARCHAR(255)", UpdateCheck = UpdateCheck.Never)]
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                OnPropertyChanged("Name", ref _name, value);
            }
        }

        private PublishStatus _publishStatus = PublishStatus.Active;
        /// <summary>
        /// Gets or sets the publish status.
        /// </summary>
        /// <value>
        /// The publish status.
        /// </value>
        [DataMember]
        [Column(UpdateCheck= UpdateCheck.Never)]
        public PublishStatus PublishStatus
        {
            get
            {
                return _publishStatus;
            }
            set
            {
                OnPropertyChanged("PublishStatus", ref _publishStatus, value);
            }
        }

        private DateTime _renewalDate;
        /// <summary>
        /// Gets or sets the renewal time.
        /// </summary>
        /// <value>
        /// The time when the advert will natuarally expire
        /// </value>
        [DataMember]
        [Column(UpdateCheck= UpdateCheck.Never)]
        public DateTime RenewalDate
        {
            get
            {
                return _renewalDate;
            }
            set
            {
                OnPropertyChanged("RenewalDate", ref _renewalDate, value);
            }
        }

        #endregion

        #region Private Properties

        /// <summary>
        /// Gets the image to display.
        /// </summary>
        protected RequestFileParameter ImageToSend { get; private set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds the image.
        /// </summary>
        /// <param name="imageFile">The image file.</param>
        public void AddImage(FileInfo imageFile)
        {
            if (imageFile == null)
            {
                throw new ArgumentNullException("imageFile", "An image file must be specified");
            }

            if (!imageFile.Exists)
            {
                throw new ArgumentOutOfRangeException("imageFile", string.Format("Unable to locate image at '{0}'.", imageFile.FullName));
            }

            if (imageFile.Length > MaximumImageSize)
            {
                throw new ArgumentOutOfRangeException("imageFile", string.Format("Images are not permitted to be larger than {1} bytes. The specifed file was {0} bytes long.", imageFile.Length, MaximumImageSize));
            }

            string fileExtension = Path.GetExtension(imageFile.FullName);
            string mimeType = null;

            // Make a guess on the mime tpye based on the file extensions -- as we're the API only supports a limited set,
            // only check for supported types
            if (string.Compare(".png", fileExtension, StringComparison.InvariantCultureIgnoreCase) == 0)
            {
                mimeType = "image/png";
            }
            else if (string.Compare(".jpg", fileExtension, StringComparison.InvariantCultureIgnoreCase) == 0 || string.Compare(".jpeg", fileExtension, StringComparison.InvariantCultureIgnoreCase) == 0)
            {
                mimeType = "image/jepg";
            }
            else if (string.Compare(".gif", fileExtension, StringComparison.InvariantCultureIgnoreCase) == 0)
            {
                mimeType = "image/gif";
            }

            if (string.IsNullOrEmpty(mimeType))
            {
                throw new ArgumentOutOfRangeException("imageFile", string.Format("'{0}' is not a recognised image file type.", imageFile.FullName));
            }

            AddImage( imageFile.FullName, mimeType, (int)imageFile.Length, imageFile.OpenRead());
        }

        /// <summary>
        /// Adds the image.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="contentType">Type of the content.</param>
        /// <param name="contentLength">Length of the content.</param>
        /// <param name="content">The content.</param>
        public void AddImage(string fileName, string contentType, int contentLength, Stream content)
        {
            ImageToSend = new RequestFileParameter("image", fileName, contentType, contentLength, content);
        }

        /// <summary>
        /// Renews the advert using the specified credentials.
        /// </summary>
        /// <param name="credentials">The credentials.</param>
        /// <remarks>If the advert was not published using the same credentials, the server reserves the right to do nothing.</remarks>
        public void Renew(IOAuthCredentials credentials)
        {
            if (AdvertId == 0)
            {
                // You cannot renew an advert that hasn't been published, but you can publish it instead.
                Save(credentials);
                return;
            }

            if (PublishStatus == PublishStatus.Deleted)
            {
                throw new InvalidOperationException("You cannot renew an advert that has been deleted");
            }

            OAuthWebRequest req = new OAuthWebRequest(new Uri("http://api.angle.uk.com/oauth/1.0/freead/renew"), credentials);
            req.Method = "POST";
            req.RequestParameters.Add(new RequestParameter("advertid", AdvertId.ToString(CultureInfo.InvariantCulture)));
            HttpWebResponse res = req.GetResponse() as HttpWebResponse;

            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Freead));
            Freead retval = (Freead)ser.ReadObject(res.GetResponseStream());
            this.AdvertId = retval.AdvertId;
            this.Name = retval.Name;
            this.Description = retval.Description;
            this.ContactDetails = retval.ContactDetails;
            this.LastModified = retval.LastModified;
            this.PublishStatus = retval.PublishStatus;
            this.RenewalDate = retval.RenewalDate;

            throw new NotImplementedException();
        }

        /// <summary>
        /// Saves changes to the classified advert using the specified credentials.
        /// </summary>
        /// <param name="credentials">The credentials.</param>
        /// <remarks>
        /// This will update the state of the current object to match the server's version after chanegs are commited.
        /// </remarks>
        public void Save(IOAuthCredentials credentials)
        {
            if (credentials == null)
            {
                throw new ArgumentNullException("credentials", "You must supply the credentials to use to save the advert");
            }

            if (string.IsNullOrEmpty(credentials.Token))
            {
                throw new UnauthorizedAccessException("Insufficient details provided to be able to save changes.");
            }

            OAuthWebRequest req = new OAuthWebRequest(new Uri("http://api.angle.uk.com/oauth/1.0/freead/save"), credentials);
            req.Method = "POST";
            // TODO:
            req.RequestParameters.Add(new RequestParameter("name", Name));
            req.RequestParameters.Add(new RequestParameter("description", Description));
            req.RequestParameters.Add(new RequestParameter("contact", ContactDetails));
            HttpWebResponse res = req.GetResponse() as HttpWebResponse;

            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Freead));
            Freead retval = (Freead)ser.ReadObject(res.GetResponseStream());
            this.AdvertId = retval.AdvertId;
            this.Name = retval.Name;
            this.Description = retval.Description;
            this.ContactDetails = retval.ContactDetails;
            this.LastModified = retval.LastModified;
            this.PublishStatus = retval.PublishStatus;
            this.RenewalDate = retval.RenewalDate;

            throw new NotImplementedException();
        }

        #endregion

        #region Public Static Properties

        /// <summary>
        /// The maximum file size for image attachments, in bytes
        /// </summary>
        public const int MaximumImageSize = 1024 * 1024;

        #endregion

        #region Public Static Methods

        /// <summary>
        /// Begins an asynchronous searches for classified adverts near the specified location.
        /// </summary>
        /// <param name="callback">The callback to call when the results are ready to be read</param>
        /// <param name="location">The location to search near.</param>
        /// <param name="range">The range in miles to search for classified adverts in.</param>
        /// <param name="since">The date to bring back changes since.</param>
        /// <param name="credentials">The credentials.</param>
        /// <returns></returns>
        public static IAsyncResult BeginSearchNear(AsyncCallback callback, Postcode location, double range, DateTime since, IOAuthCredentials credentials)
        {
            if (location == null)
            {
                throw new ArgumentNullException("location", "You must specify a location to search near");
            }

            OAuthWebRequest req = new OAuthWebRequest(new Uri("http://api.angle.uk.com/oauth/1.0/freeads/nearby"), credentials);
            req.RequestParameters.Add(new RequestParameter("location", location.ToString()));
            req.RequestParameters.Add(new RequestParameter("range", range.ToString(CultureInfo.InvariantCulture)));
            if (since != default(DateTime))
            {
                req.RequestParameters.Add(new RequestParameter("since", since.ToUnixTime().ToString(CultureInfo.InvariantCulture)));
            }

            return req.BeginGetResponse(callback, req);
        }

        /// <summary>
        /// Begins an asynchronous searches for classified adverts near the specified location.
        /// </summary>
        /// <param name="callback">The callback to call when the results are ready to be read</param>
        /// <param name="location">The location to search near.</param>
        /// <param name="range">The range in miles to search for classified adverts in.</param>
        /// <param name="since">The date to bring back changes since.</param>
        /// <param name="credentials">The credentials.</param>
        /// <returns></returns>
        public static IAsyncResult BeginSearchNear(AsyncCallback callback, IGeoLocation location, double range, DateTime since, IOAuthCredentials credentials)
        {
            if (location == null)
            {
                throw new ArgumentNullException("location", "You must specify a location to search near");
            }

            OAuthWebRequest req = new OAuthWebRequest(new Uri("http://api.angle.uk.com/oauth/1.0/freeads/nearby"), credentials);
            req.RequestParameters.Add(new RequestParameter("latitude", location.Latitude.ToString(CultureInfo.InvariantCulture)));
            req.RequestParameters.Add(new RequestParameter("longitude", location.Longitude.ToString(CultureInfo.InvariantCulture)));
            req.RequestParameters.Add(new RequestParameter("range", range.ToString(CultureInfo.InvariantCulture)));
            if (since != default(DateTime))
            {
                req.RequestParameters.Add(new RequestParameter("since", since.ToUnixTime().ToString(CultureInfo.InvariantCulture)));
            }

            return req.BeginGetResponse(callback, req);
        }

        /// <summary>
        /// Ends an asynchronous search for classified adverts near a location.
        /// </summary>
        /// <param name="asyncResult">The async result.</param>
        /// <returns></returns>
        public static IEnumerable<Freead> EndSearchNear(IAsyncResult asyncResult)
        {
            if (asyncResult == null)
            {
                throw new ArgumentNullException("asyncResult");
            }

            OAuthWebRequest req = (OAuthWebRequest)asyncResult.AsyncState;
            HttpWebResponse res;
            try
            {
                res = req.EndGetResponse(asyncResult) as HttpWebResponse;
            }
            catch (WebException)
            {
                // Sometimes we're seeing a 404, even though the server is not reporting one.
                return null;
            }

            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(IEnumerable<Freead>));

            try
            {
                IEnumerable<Freead> retval = (IEnumerable<Freead>)ser.ReadObject(res.GetResponseStream());
                return retval;
            }
            catch (ArgumentNullException)
            {
                // Sometimes we're seeing an ArgumentNullException, even though ser and the stream returned are not null
            }

            return null;
        }

        /// <summary>
        /// Searches for classified adverts near the specified location.
        /// </summary>
        /// <param name="location">The location to search near.</param>
        /// <param name="range">The range in miles to search for classified adverts in.</param>
        /// <param name="credentials">The credentials.</param>
        /// <returns></returns>
        public static IEnumerable<Freead> SearchNear(Postcode location, double range, IOAuthCredentials credentials)
        {
            return SearchNear(location, range, default(DateTime), credentials);
        }

        /// <summary>
        /// Searches for classified adverts near the specified location.
        /// </summary>
        /// <param name="location">The location to search near.</param>
        /// <param name="range">The range in miles to search for classified adverts in.</param>
        /// <param name="since">The date to bring back changes since.</param>
        /// <param name="credentials">The credentials.</param>
        /// <returns></returns>
        public static IEnumerable<Freead> SearchNear(Postcode location, double range, DateTime since, IOAuthCredentials credentials)
        {
            IAsyncResult res = BeginSearchNear(callback => { }, location, range, since, credentials);
            res.AsyncWaitHandle.WaitOne();
            return EndSearchNear(res);
        }

        /// <summary>
        /// Searches for classified adverts near the specified location.
        /// </summary>
        /// <param name="location">The location to search near.</param>
        /// <param name="range">The range in miles to search for classified adverts in.</param>
        /// <param name="credentials">The credentials.</param>
        /// <returns></returns>
        public static IEnumerable<Freead> SearchNear(IGeoLocation location, double range, IOAuthCredentials credentials)
        {
            return SearchNear(location, range, default(DateTime), credentials);
        }

        /// <summary>
        /// Searches for classified adverts near the specified location.
        /// </summary>
        /// <param name="location">The location to search near.</param>
        /// <param name="range">The range in miles to search for classified adverts in.</param>
        /// <param name="since">The date to bring back changes since.</param>
        /// <param name="credentials">The credentials.</param>
        /// <returns></returns>
        public static IEnumerable<Freead> SearchNear(IGeoLocation location, double range, DateTime since, IOAuthCredentials credentials)
        {
            IAsyncResult res = BeginSearchNear(callback => {}, location, range, since, credentials);
            res.AsyncWaitHandle.WaitOne();
            return EndSearchNear(res);
        }

        #endregion
    }
}
