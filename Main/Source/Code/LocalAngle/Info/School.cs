﻿using LocalAngle.Net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Linq.Mapping;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;

namespace LocalAngle.Info
{
    public class School : BindableBase, IEmployer
    {
        #region Public Properties

        private ICollection<ContactNumber> _contacts;
        /// <summary>
        /// Gets the contact numbers.
        /// </summary>
        /// <value>
        /// The contact numbers.
        /// </value>
        public ICollection<ContactNumber> ContactNumbers
        {
            get
            {
                if (_contacts == null)
                {
                    _contacts = new ObservableCollection<ContactNumber>();
                }
                return _contacts;
            }
        }

        private string _deliveryPoint;
        /// <summary>
        /// Gets or sets the delivery point for the School.
        /// </summary>
        /// <value>
        /// The delivery point for the address
        /// </value>
        [DataMember]
        [Column(DbType = "NVARCHAR(100)", UpdateCheck = UpdateCheck.Never)]
        public string DeliveryPoint
        {
            get
            {
                return _deliveryPoint;
            }
            set
            {
                OnPropertyChanged("DeliveryPoint", ref _deliveryPoint, value);
            }
        }

        private string _email;
        /// <summary>
        /// Gets or sets the email address for the School.
        /// </summary>
        /// <value>
        /// The email address.
        /// </value>
        [DataMember]
        [Column(DbType = "NVARCHAR(255)", UpdateCheck = UpdateCheck.Never)]
        public string EmailAddress
        {
            get
            {
                return _email;
            }
            set
            {
                OnPropertyChanged("EmailAddress", ref _email, value);
            }
        }

        private Guid _guid;
        /// <summary>
        /// Gets or sets the primary key GUID.
        /// </summary>
        /// <value>
        /// The primary key GUID.
        /// </value>
        [DataMember]
        [Column(IsPrimaryKey = true)]
        public Guid Guid
        {
            get
            {
                return _guid;
            }
            set
            {
                OnPropertyChanged("Guid", ref _guid, value);

                foreach (var ot in OpeningHours)
                {
                    ot.Guid = value;
                }

                foreach (var ot in ContactNumbers)
                {
                    ot.Guid = value;
                }
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
        [Column(UpdateCheck = UpdateCheck.Never)]
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

        private DateTime _lastInspected;
        /// <summary>
        /// Gets or sets the last modification time.
        /// </summary>
        /// <value>
        /// The last modified.
        /// </value>
        [DataMember]
        [Column(UpdateCheck = UpdateCheck.Never)]
        public DateTime LastInspected
        {
            get
            {
                return _lastInspected;
            }
            set
            {
                OnPropertyChanged("LastInspected", ref _lastInspected, value);
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
        [Column(UpdateCheck = UpdateCheck.Never)]
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
        [Column(UpdateCheck = UpdateCheck.Never)]
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

        private int _lea;
        public int LocalEducationAuthority
        {
            get
            {
                return _lea;
            }
            set
            {
                OnPropertyChanged("LocalEducationAuthority", ref _lea, value);
            }
        }

        private Postcode _location = new Postcode();
        /// <summary>
        /// Gets or sets the location postal code.
        /// </summary>
        /// <value>
        /// The location.
        /// </value>
        public Postcode Location
        {
            get
            {
                return _location;
            }
            set
            {
                OnPropertyChanged("Location", ref _location, value);
            }
        }

        private string _name;
        /// <summary>
        /// Gets or sets the School name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [DataMember(IsRequired = true)]
        [Required]
        [Column(DbType = "NVARCHAR(255)", UpdateCheck = UpdateCheck.Never)]
        [DisplayName("School name")]
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                OnPropertyChanged("Name", ref _name, (value == null ? string.Empty : value.Trim()));
            }
        }

        private ICollection<OpeningHours> _openingHours;
        [DataMember]
        public ICollection<OpeningHours> OpeningHours
        {
            get
            {
                if (_openingHours == null)
                {
                    _openingHours = new ObservableCollection<OpeningHours>();
                }
                return _openingHours;
            }
        }

        /// <summary>
        /// Gets or sets the postcode.
        /// </summary>
        /// <value>
        /// The postcode.
        /// </value>
        /// <remarks>
        /// Intended for use by the JSON serialisers
        /// </remarks>
        [DataMember(Name = "Postcode")]
        [Column(DbType = "NVARCHAR(8)", UpdateCheck = UpdateCheck.Never)]
        [DisplayName("Postal code")]
        public string Postcode
        {
            get
            {
                return Location.ToString();
            }
            set
            {
                if (LocalAngle.Postcode.IsValid(value))
                {
                    Location = new Postcode(value);
                }
                else
                {
                    Location = new Postcode();
                }
            }
        }

        private int _ofstedId;
        public int OfstedId
        {
            get
            {
                return _ofstedId;
            }
            set
            {
                OnPropertyChanged("OfstedId", ref _ofstedId, value);
            }
        }

        private Uri _website;
        /// <summary>
        /// Gets or sets the website.
        /// </summary>
        /// <value>
        /// The website.
        /// </value>
        [DataMember]
        public Uri Website
        {
            get
            {
                return _website;
            }
            set
            {
                OnPropertyChanged("Website", ref _website, value);
            }
        }

        #endregion

        #region Public Static Methods

        /// <summary>
        /// Begins an asynchronous search for Schools near the specified location.
        /// </summary>
        /// <param name="callback">The callback to call when the results are ready to be read</param>
        /// <param name="location">The location to search near.</param>
        /// <param name="range">The range in miles to search for events in.</param>
        /// <param name="since">The date to bring back changes since.</param>
        /// <param name="credentials">The credentials.</param>
        /// <returns></returns>
        public static IAsyncResult BeginSearchNear(AsyncCallback callback, Postcode location, double range, DateTime since, IOAuthCredentials credentials)
        {
            if (location == null)
            {
                throw new ArgumentNullException("location", "You must specify a location to search near");
            }

            OAuthWebRequest req = new OAuthWebRequest(new Uri(ApiHelper.BaseUri, new Uri("info/schools", UriKind.Relative)), credentials);
            req.RequestParameters.Add(new RequestParameter("location", location.ToString()));
            req.RequestParameters.Add(new RequestParameter("range", range.ToString(CultureInfo.InvariantCulture)));
            if (since != default(DateTime))
            {
                req.RequestParameters.Add(new RequestParameter("since", since.ToUnixTime().ToString(CultureInfo.InvariantCulture)));
            }

            return req.BeginGetResponse(callback, req);
        }

        /// <summary>
        /// Begins an asynchronous searches for Schools near the specified location.
        /// </summary>
        /// <param name="callback">The callback to call when the results are ready to be read</param>
        /// <param name="location">The location to search near.</param>
        /// <param name="range">The range in miles to search for events in.</param>
        /// <param name="since">The date to bring back changes since.</param>
        /// <param name="credentials">The credentials.</param>
        /// <returns></returns>
        public static IAsyncResult BeginSearchNear(AsyncCallback callback, IGeoLocation location, double range, DateTime since, IOAuthCredentials credentials)
        {
            if (location == null)
            {
                throw new ArgumentNullException("location", "You must specify a location to search near");
            }

            OAuthWebRequest req = new OAuthWebRequest(new Uri(ApiHelper.BaseUri, new Uri("info/schools", UriKind.Relative)), credentials);
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
        /// Begins an asynchronous search for Schools near the specified location.
        /// </summary>
        /// <param name="callback">The callback to call when the results are ready to be read</param>
        /// <param name="localEducationAuthority">The Local Education Authority code that the school comes under.</param>
        /// <param name="since">The date to bring back changes since.</param>
        /// <param name="credentials">The credentials.</param>
        /// <returns></returns>
        public static IAsyncResult BeginSearchIn(AsyncCallback callback, int localEducationAuthority, IOAuthCredentials credentials)
        {
            return BeginSearchIn(callback, localEducationAuthority, default(DateTime), credentials);
        }

        /// <summary>
        /// Begins an asynchronous searches for Schools near the specified location.
        /// </summary>
        /// <param name="callback">The callback to call when the results are ready to be read</param>
        /// <param name="localEducationAuthority">The Local Education Authority code that the school comes under.</param>
        /// <param name="since">The date to bring back changes since.</param>
        /// <param name="credentials">The credentials.</param>
        /// <returns></returns>
        public static IAsyncResult BeginSearchIn(AsyncCallback callback, int localEducationAuthority, DateTime since, IOAuthCredentials credentials)
        {
            if (localEducationAuthority == 0)
            {
                throw new ArgumentNullException("localEducationAuthority", "You must specify a Local Education Authority to search within");
            }

            OAuthWebRequest req = new OAuthWebRequest(new Uri(ApiHelper.BaseUri, new Uri("info/schools", UriKind.Relative)), credentials);
            req.RequestParameters.Add(new RequestParameter("lea", localEducationAuthority.ToString(CultureInfo.InvariantCulture)));
            if (since != default(DateTime))
            {
                req.RequestParameters.Add(new RequestParameter("since", since.ToUnixTime().ToString(CultureInfo.InvariantCulture)));
            }

            return req.BeginGetResponse(callback, req);
        }

        /// <summary>
        /// Ends an asynchronous search for Schools near a location.
        /// </summary>
        /// <param name="asyncResult">The async result.</param>
        /// <returns></returns>
        public static IEnumerable<School> EndSearch(IAsyncResult asyncResult)
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

            return LoadJson(res.GetResponseStream());
        }

        /// <summary>
        /// Loads a collection of Schools from json in a stream.
        /// </summary>
        /// <param name="stream">The stream containing the JSON.</param>
        /// <remarks>
        /// It is for the caller to close the stream, as required
        /// </remarks>
        /// <returns></returns>
        public static IEnumerable<School> LoadJson(Stream stream)
        {
            try
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(IEnumerable<School>));
                IEnumerable<School> retval = (IEnumerable<School>)ser.ReadObject(stream);
                return retval;
            }
            catch (ArgumentNullException)
            {
                // Sometimes we're seeing an ArgumentNullException, even though ser and the stream returned are not null
            }

            return null;
        }

        /// <summary>
        /// Searches for Schools near the specified location.
        /// </summary>
        /// <param name="location">The location to search near.</param>
        /// <param name="range">The range in miles to search for events in.</param>
        /// <param name="credentials">The credentials.</param>
        /// <returns></returns>
        public static IEnumerable<School> SearchNear(Postcode location, double range, IOAuthCredentials credentials)
        {
            return SearchNear(location, range, default(DateTime), credentials);
        }

        /// <summary>
        /// Searches for Schools near the specified location.
        /// </summary>
        /// <param name="location">The location to search near.</param>
        /// <param name="range">The range in miles to search for events in.</param>
        /// <param name="since">The date to bring back changes since.</param>
        /// <param name="credentials">The credentials.</param>
        /// <returns></returns>
        /// <remarks>If the postcode is not recognised by the server, it will fall back to making a guess based on the client IP address</remarks>
        public static IEnumerable<School> SearchNear(Postcode location, double range, DateTime since, IOAuthCredentials credentials)
        {
            IAsyncResult res = BeginSearchNear(callback => { }, location, range, since, credentials);
            res.AsyncWaitHandle.WaitOne();
            return EndSearch(res);
        }

        /// <summary>
        /// Searches for Schools near the specified location.
        /// </summary>
        /// <param name="location">The location to search near.</param>
        /// <param name="range">The range in miles to search for events in.</param>
        /// <param name="credentials">The credentials.</param>
        /// <returns></returns>
        public static IEnumerable<School> SearchNear(IGeoLocation location, double range, IOAuthCredentials credentials)
        {
            return SearchNear(location, range, default(DateTime), credentials);
        }

        /// <summary>
        /// Searches for Schools near the specified location.
        /// </summary>
        /// <param name="location">The location to search near.</param>
        /// <param name="range">The range in miles to search for events in.</param>
        /// <param name="since">The date to bring back changes since.</param>
        /// <param name="credentials">The credentials.</param>
        /// <returns></returns>
        public static IEnumerable<School> SearchNear(IGeoLocation location, double range, DateTime since, IOAuthCredentials credentials)
        {
            IAsyncResult res = BeginSearchNear(callback => { }, location, range, since, credentials);
#if SILVERLIGHT
            while (!res.IsCompleted)
            {
                System.Threading.Thread.Sleep(100);
            }
#else
            res.AsyncWaitHandle.WaitOne();
#endif
            return EndSearch(res);
        }

        /// <summary>
        /// Searches for Schools near the specified location.
        /// </summary>
        /// <param name="localEducationAuthority">The Local Education Authority code that the school comes under.</param>
        /// <param name="since">The date to bring back changes since.</param>
        /// <param name="credentials">The credentials.</param>
        /// <returns></returns>
        public static IEnumerable<School> SearchIn(int localEducationAuthority, DateTime since, IOAuthCredentials credentials)
        {
            IAsyncResult res = BeginSearchIn(callback => { }, localEducationAuthority, since, credentials);
#if SILVERLIGHT
            while (!res.IsCompleted)
            {
                System.Threading.Thread.Sleep(100);
            }
#else
            res.AsyncWaitHandle.WaitOne();
#endif
            return EndSearch(res);
        }

        #endregion
    }
}
