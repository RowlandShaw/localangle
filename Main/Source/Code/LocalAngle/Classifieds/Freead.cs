using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Globalization;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using LocalAngle.Net;

namespace LocalAngle.Classifieds
{
    /// <summary>
    /// Represents a classified advert
    /// </summary>
    public class Freead : BindableBase, IGeoLocation
    {
        #region Public Properties

        private string _advertId;
        /// <summary>
        /// Gets or sets a unique identifier for the classified advert.
        /// </summary>
        /// <value>
        /// The classified advert id.
        /// </value>
        [DataMember]
        [Column(IsPrimaryKey = true)]
        public string AdvertId
        {
            get
            {
                return _advertId;
            }
            protected set
            {
                OnPropertyChanged("AdvertId", ref _advertId, value);
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
        [Column(DbType = "NVARCHAR(255)")]
        public string ContactDetails
        {
            get
            {
                return _contact;
            }
            protected set
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
        [Column(DbType = "NTEXT")]
        public string Description
        {
            get
            {
                return _description;
            }
            protected set
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
        [Column]
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
        [Column]
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
        [Column]
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
        [Column(DbType = "NVARCHAR(255)")]
        public string Name
        {
            get
            {
                return _name;
            }
            protected set
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
        [Column]
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
        [Column]
        public DateTime RenewalDate
        {
            get
            {
                return _renewalDate;
            }
            protected set
            {
                OnPropertyChanged("RenewalDate", ref _renewalDate, value);
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Renews the advert using the specified credentials.
        /// </summary>
        /// <param name="credentials">The credentials.</param>
        /// <remarks>If the advert was not published using the same credentials, the server reserves the right to do nothing.</remarks>
        public void Renew(IOAuthCredentials credentials)
        {
            if (AdvertId == null)
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
            req.RequestParameters.Add(new RequestParameter("advertid", AdvertId));
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

        #region Public Static Methods

        /// <summary>
        /// Begins an asynchronous searches for classified adverts near the specified location.
        /// </summary>
        /// <param name="location">The location to search near.</param>
        /// <param name="range">The range in miles to search for classified adverts in.</param>
        /// <param name="since">The date to bring back changes since.</param>
        /// <param name="credentials">The credentials.</param>
        /// <returns></returns>
        public static IAsyncResult BeginSearchNear(Postcode location, double range, DateTime since, IOAuthCredentials credentials)
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

            return req.BeginGetResponse(callback => { }, req);
        }

        /// <summary>
        /// Begins an asynchronous searches for classified adverts near the specified location.
        /// </summary>
        /// <param name="location">The location to search near.</param>
        /// <param name="range">The range in miles to search for classified adverts in.</param>
        /// <param name="since">The date to bring back changes since.</param>
        /// <param name="credentials">The credentials.</param>
        /// <returns></returns>
        public static IAsyncResult BeginSearchNear(IGeoLocation location, double range, DateTime since, IOAuthCredentials credentials)
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

            return req.BeginGetResponse(callback => { }, req);
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
            HttpWebResponse res = req.EndGetResponse(asyncResult) as HttpWebResponse;
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(IEnumerable<Freead>));
            IEnumerable<Freead> retval = (IEnumerable<Freead>)ser.ReadObject(res.GetResponseStream());

            return retval;
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
            IAsyncResult res = BeginSearchNear(location, range, since, credentials);
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
            IAsyncResult res = BeginSearchNear(location, range, since, credentials);
            res.AsyncWaitHandle.WaitOne();
            return EndSearchNear(res);
        }

        #endregion
    }
}
