using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using LocalAngle.Net;
using System.Threading.Tasks;
#if NETFX_CORE
using System.Threading.Tasks;
#endif

namespace LocalAngle.Events
{
    /// <summary>
    /// Represents an event
    /// </summary>
    [DataContract]
    public class SpecialEvent : BindableBase, IComparable<SpecialEvent>, IEquatable<SpecialEvent>, IGeoLocation
    {
        #region Public Properties

        private int _eventId;
        /// <summary>
        /// Gets or sets a unique identifier for the event.
        /// </summary>
        /// <value>
        /// The event id.
        /// </value>
        [DataMember]
        public int EventId
        {
            get
            {
                return _eventId;
            }
            set
            {
                OnPropertyChanged("EventId", ref _eventId, value);
            }
        }

        private string _name;
        /// <summary>
        /// Gets or sets the event name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [DataMember(IsRequired=true)]
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

        private string _description;
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        [DataMember(IsRequired = true)]
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                OnPropertyChanged("Description", ref _description, (value == null ? string.Empty : value.Trim()));
            }
        }

        /// <summary>
        /// Gets or sets the distance (in miles) from the search point used to load it
        /// </summary>
        /// <value>
        /// The distance in UK miles.
        /// </value>
        /// <remarks>Only meaningful when populated during a search (so doesn't respect INotfifyProperty...).</remarks>
        [DataMember]
        public double Distance { get; set; }

        /// <summary>
        /// Gets the URI for the event details for attribution.
        /// </summary>
        /// <remarks>
        /// Under the terms of use for the public API, any use of the event data in an electronic form must include a hyperlink to the event.
        /// It is desirable that any printed use of the event data incorporate a link, either textual, as a QR code, or both, if practical. 
        /// Displaying a URL, or using it as a hyperlink is enough to satisfy local.angle's attribution requirement of the licence to use the data.
        /// </remarks>
        public Uri EventUri
        {
            get
            {
                return new Uri("http://r.angle.uk.com/ev/" + EventId.ToString(CultureInfo.InvariantCulture), UriKind.Absolute);
            }
        }

        private string _venueName;
        /// <summary>
        /// Gets or sets the venue name.
        /// </summary>
        /// <value>
        /// The name of the venue.
        /// </value>
        [DataMember]
        public string VenueName
        {
            get
            {
                return _venueName;
            }
            set
            {
                OnPropertyChanged("VenueName", ref _venueName, (value == null ? string.Empty : value.Trim()));
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

        private double _latitude = 90;
        /// <summary>
        /// Gets or sets the latitude in decimal degrees.
        /// </summary>
        /// <value>
        /// The latitude.
        /// </value>
        /// <remarks>Uses the WGS84 datum</remarks>
        [DataMember]
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

        private DateTime _start;
        /// <summary>
        /// Gets or sets the start time.
        /// </summary>
        /// <value>
        /// The start time.
        /// </value>
        [DataMember(IsRequired = true)]
        public DateTime StartTime
        {
            get
            {
                return _start;
            }
            set
            {
                OnPropertyChanged("StartTime", ref _start, value);
            }
        }

        private DateTime _end;
        /// <summary>
        /// Gets or sets the end time.
        /// </summary>
        /// <value>
        /// The end time.
        /// </value>
        [DataMember(IsRequired = true)]
        public DateTime EndTime
        {
            get
            {
                return _end;
            }
            set
            {
                OnPropertyChanged("EndTime", ref _end, value);
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

        private string _tags;
        /// <summary>
        /// Gets or sets the tags.
        /// </summary>
        /// <value>
        /// The tags.
        /// </value>
        [DataMember]
        public string Tags
        {
            get
            {
                return _tags;
            }
            set
            {
                OnPropertyChanged("Tags", ref _tags, value);
            }
        }

        private Uri _ticketUri;
        /// <summary>
        /// Gets or sets the URI for ticketing information.
        /// </summary>
        /// <value>
        /// The ticket URI.
        /// </value>
        [DataMember]
        public Uri TicketUri
        {
            get
            {
                return _ticketUri;
            }
            set
            {
                OnPropertyChanged("TicketUri", ref _ticketUri, value);
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
        public string Postcode
        {
            get
            {
                return Location.ToString();
            }
            set
            {
                Location = new Postcode(value);
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Compares the current object with another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// A 32-bit signed integer that indicates the relative order of the objects being compared. The return value has the following meanings:
        /// Value
        /// Meaning
        /// Less than zero
        /// This object is less than the <paramref name="other"/> parameter.
        /// Zero
        /// This object is equal to <paramref name="other"/>.
        /// Greater than zero
        /// This object is greater than <paramref name="other"/>.
        /// </returns>
        /// <remarks>
        /// This only evaluates the natural key -- if data were duplicated in the DB, they would be compare as the same, 
        /// even though <see cref="Equals"/> would report them as different records
        /// </remarks>
        public int CompareTo(SpecialEvent other)
        {
            if (object.ReferenceEquals(this, other))
            {
                return 0;
            }

            if (object.ReferenceEquals(null, other))
            {
                return -1;
            }

            int retval = this.StartTime.CompareTo(other.StartTime);
            if (retval == 0)
            {
                retval = this.EndTime.CompareTo(other.EndTime);
            }

            if (retval == 0)
            {
                retval = string.Compare(this.VenueName, other.VenueName, StringComparison.CurrentCultureIgnoreCase);
            }

            if (retval == 0)
            {
                retval = string.Compare(this.Postcode, other.Postcode, StringComparison.CurrentCultureIgnoreCase);
            }

            // TODO: Other fallback comparisons.

            return retval;
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object"/> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            return Equals(obj as SpecialEvent);
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the other parameter; otherwise, false.
        /// </returns>
        public bool Equals(SpecialEvent other)
        {
            if (other == null)
            {
                return false;
            }

            return (CompareTo(other) == 0) && (EventId == other.EventId);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return EventId;
        }

        /// <summary>
        /// Saves changes to the event using the specified credentials.
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

            OAuthWebRequest req = new OAuthWebRequest(new Uri(ApiHelper.BaseUri, new Uri("event/save", UriKind.Relative)), credentials);
            req.Method = "POST";
            req.RequestParameters.Add(new RequestParameter("name", Name));
            req.RequestParameters.Add(new RequestParameter("description", Description));
            req.RequestParameters.Add(new RequestParameter("locname", VenueName));
            req.RequestParameters.Add(new RequestParameter("locpostcode", Location.ToString()));
            req.RequestParameters.Add(new RequestParameter("start", StartTime.ToString(DateFormat,CultureInfo.InvariantCulture)));
            req.RequestParameters.Add(new RequestParameter("end", EndTime.ToString(DateFormat, CultureInfo.InvariantCulture)));
            req.RequestParameters.Add(new RequestParameter("tag", Tags));
            req.RequestParameters.Add(new RequestParameter("ticketurl", (TicketUri == null ? "" : TicketUri.ToString())));
            req.RequestParameters.Add(new RequestParameter("id", EventId.ToString(CultureInfo.InvariantCulture)));
            req.RequestParameters.Add(new RequestParameter("status", ((int)PublishStatus).ToString(CultureInfo.InvariantCulture)));
            HttpWebResponse res = req.GetResponse() as HttpWebResponse;

            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(SpecialEvent));
            SpecialEvent retval = (SpecialEvent)ser.ReadObject(res.GetResponseStream());
            if (retval != null)
            {
                this.EventId = retval.EventId;
                this.Name = retval.Name;
                this.Description = retval.Description;
                this.VenueName = retval.VenueName;
                this.Location = retval.Location;
                this.StartTime = retval.StartTime;
                this.EndTime = retval.EndTime;
                this.TicketUri = retval.TicketUri;
            }
        }

        #endregion

        #region Protected Properties

        private const string DateFormat = "yyyy-MM-dd HH:mm";

        #endregion

        #region Public Static Methods

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator ==(SpecialEvent left, SpecialEvent right)
        {
            if( object.ReferenceEquals(left, right) )
            {
                return true;
            }

            if (!object.ReferenceEquals(left, null))
            {
                return left.Equals(right);
            }
            else if (!object.ReferenceEquals(right, null))
            {
                return right.Equals(left);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator !=(SpecialEvent left, SpecialEvent right)
        {
            return !(left == right);
        }

        /// <summary>
        /// Implements the operator &gt;.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator >(SpecialEvent left, SpecialEvent right)
        {
            if (left == null)
            {
                return (right == null ? false : true);
            }

            return left.CompareTo(right) > 0;
        }

        /// <summary>
        /// Implements the operator &lt;=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator <=(SpecialEvent left, SpecialEvent right)
        {
            return !(left > right);
        }

        /// <summary>
        /// Implements the operator &gt;=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator >=(SpecialEvent left, SpecialEvent right)
        {
            return !(left < right);
        }

        /// <summary>
        /// Implements the operator &lt;.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator <(SpecialEvent left, SpecialEvent right)
        {
            if (left == null)
            {
                return false;
            }

            return left.CompareTo(right) < 0;
        }

        /// <summary>
        /// Searches for events near the specified location.
        /// </summary>
        /// <param name="location">The location to search near.</param>
        /// <param name="range">The range in miles to search for events in.</param>
        /// <param name="credentials">The credentials.</param>
        /// <returns></returns>
        public static IEnumerable<SpecialEvent> SearchNear(Postcode location, double range, IOAuthCredentials credentials)
        {
            return SearchNear(location, range, default(DateTime), null, credentials);
        }

        /// <summary>
        /// Searches for events near the specified location.
        /// </summary>
        /// <param name="location">The location to search near.</param>
        /// <param name="range">The range in miles to search for events in.</param>
        /// <param name="since">The date to bring back changes since.</param>
        /// <param name="credentials">The credentials.</param>
        /// <returns></returns>
        public static IEnumerable<SpecialEvent> SearchNear(Postcode location, double range, DateTime since, IOAuthCredentials credentials)
        {
            return SearchNear(location, range, since, null, credentials);
        }

        /// <summary>
        /// Searches for events near the specified location.
        /// </summary>
        /// <param name="location">The location to search near.</param>
        /// <param name="range">The range in miles to search for events in.</param>
        /// <param name="since">The date to bring back changes since.</param>
        /// <param name="topic">The category of events to bring back.</param>
        /// <param name="credentials">The credentials.</param>
        /// <returns></returns>
        /// <remarks>If the postcode is not recognised by the server, it will fall back to making a guess based on the client IP address</remarks>
        public static IEnumerable<SpecialEvent> SearchNear(Postcode location, double range, DateTime since, string topic, IOAuthCredentials credentials)
        {
            IAsyncResult res = BeginSearchNear(callback => { }, location, range, since, topic, credentials);
            res.AsyncWaitHandle.WaitOne();
            return EndSearchNear(res);
        }

        /// <summary>
        /// Searches for events near the specified location.
        /// </summary>
        /// <param name="location">The location to search near.</param>
        /// <param name="range">The range in miles to search for events in.</param>
        /// <param name="since">The date to bring back changes since.</param>
        /// <param name="topic">The category of events to bring back.</param>
        /// <param name="credentials">The credentials.</param>
        /// <returns></returns>
        public async static Task<IEnumerable<SpecialEvent>> SearchNearAsync(Postcode location, double range, DateTime since, string topic, IOAuthCredentials credentials)
        {
            if (location == null)
            {
                throw new ArgumentNullException("location", "You must specify a location to search near");
            }

            OAuthWebRequest req = new OAuthWebRequest(new Uri(ApiHelper.BaseUri, new Uri("events/nearby", UriKind.Relative)), credentials);
            req.RequestParameters.Add(new RequestParameter("location", location.ToString()));
            req.RequestParameters.Add(new RequestParameter("range", range.ToString(CultureInfo.InvariantCulture)));
            if (since != default(DateTime))
            {
                req.RequestParameters.Add(new RequestParameter("since", since.ToUnixTime().ToString(CultureInfo.InvariantCulture)));
            }
            if (!string.IsNullOrEmpty(topic))
            {
                req.RequestParameters.Add(new RequestParameter("topic", topic));
            }

            WebResponse res = await req.GetResponseAsync();

            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(IEnumerable<SpecialEvent>));

            try
            {
                IEnumerable<SpecialEvent> retval = (IEnumerable<SpecialEvent>)ser.ReadObject(res.GetResponseStream());
                return retval;
            }
            catch (ArgumentNullException)
            {
                // Sometimes we're seeing an ArgumentNullException, even though ser and the stream returned are not null
            }

            return null;
        }

        /// <summary>
        /// Begins an asynchronous searches for events near the specified location.
        /// </summary>
        /// <param name="location">The location to search near.</param>
        /// <param name="range">The range in miles to search for events in.</param>
        /// <param name="since">The date to bring back changes since.</param>
        /// <param name="topic">The category of events to bring back.</param>
        /// <param name="credentials">The credentials.</param>
        /// <param name="callback">The callback to call when the results are ready to be read</param>
        /// <returns></returns>
        public static IAsyncResult BeginSearchNear(AsyncCallback callback, Postcode location, double range, DateTime since, string topic, IOAuthCredentials credentials)
        {
            if (location == null)
            {
                throw new ArgumentNullException("location", "You must specify a location to search near");
            }

            OAuthWebRequest req = new OAuthWebRequest(new Uri(ApiHelper.BaseUri, new Uri("events/nearby", UriKind.Relative)), credentials);
            req.RequestParameters.Add(new RequestParameter("location", location.ToString()));
            req.RequestParameters.Add(new RequestParameter("range", range.ToString(CultureInfo.InvariantCulture)));
            if (since != default(DateTime))
            {
                req.RequestParameters.Add(new RequestParameter("since", since.ToUnixTime().ToString(CultureInfo.InvariantCulture)));
            }
            if (!string.IsNullOrEmpty(topic))
            {
                req.RequestParameters.Add(new RequestParameter("topic", topic));
            }

            return req.BeginGetResponse(callback, req);
        }

        /// <summary>
        /// Searches for events near the specified location.
        /// </summary>
        /// <param name="location">The location to search near.</param>
        /// <param name="range">The range in miles to search for events in.</param>
        /// <param name="credentials">The credentials.</param>
        /// <returns></returns>
        public static IEnumerable<SpecialEvent> SearchNear(IGeoLocation location, double range, IOAuthCredentials credentials)
        {
            return SearchNear(location, range, default(DateTime), null, credentials);
        }

        /// <summary>
        /// Searches for events near the specified location.
        /// </summary>
        /// <param name="location">The location to search near.</param>
        /// <param name="range">The range in miles to search for events in.</param>
        /// <param name="since">The date to bring back changes since.</param>
        /// <param name="credentials">The credentials.</param>
        /// <returns></returns>
        public static IEnumerable<SpecialEvent> SearchNear(IGeoLocation location, double range, DateTime since, IOAuthCredentials credentials)
        {
            return SearchNear(location, range, since, null, credentials);
        }

#if NETFX_CORE

        /// <summary>
        /// Searches for events near the specified location.
        /// </summary>
        /// <param name="location">The location to search near.</param>
        /// <param name="range">The range in miles to search for events in.</param>
        /// <param name="since">The date to bring back changes since.</param>
        /// <param name="topic">The category of events to bring back.</param>
        /// <param name="credentials">The credentials.</param>
        /// <param name="callback">The callback to call when the results are ready to be read</param>
        /// <returns></returns>
        public async static Task<IEnumerable<SpecialEvent>> SearchNearAsync(Postcode location, double range, DateTime since, string topic, IOAuthCredentials credentials)
        {
            if (location == null)
            {
                throw new ArgumentNullException("location", "You must specify a location to search near");
            }

            OAuthWebRequest req = new OAuthWebRequest(new Uri(ApiHelper.BaseUri, new Uri("events/nearby", UriKind.Relative)), credentials);
            req.RequestParameters.Add(new RequestParameter("location", location.ToString()));
            req.RequestParameters.Add(new RequestParameter("range", range.ToString(CultureInfo.InvariantCulture)));
            if (since != default(DateTime))
            {
                req.RequestParameters.Add(new RequestParameter("since", since.ToUnixTime().ToString(CultureInfo.InvariantCulture)));
            }
            if (!string.IsNullOrEmpty(topic))
            {
                req.RequestParameters.Add(new RequestParameter("topic", topic));
            }

            HttpWebResponse res;
            try
            {
                res = await req.GetResponseAsync() as HttpWebResponse;
            }
            catch (WebException)
            {
                // Sometimes we're seeing a 404, even though the server is not reporting one.
                return null;
            }

            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(IEnumerable<SpecialEvent>));

            try
            {
                IEnumerable<SpecialEvent> retval = (IEnumerable<SpecialEvent>)ser.ReadObject(res.GetResponseStream());
                return retval;
            }
            catch (ArgumentNullException)
            {
                // Sometimes we're seeing an ArgumentNullException, even though ser and the stream returned are not null
            }

            return null;
        }

        /// <summary>
        /// Searches for events near the specified location.
        /// </summary>
        /// <param name="location">The location to search near.</param>
        /// <param name="range">The range in miles to search for events in.</param>
        /// <param name="since">The date to bring back changes since.</param>
        /// <param name="topic">The category of events to bring back.</param>
        /// <param name="credentials">The credentials.</param>
        /// <returns></returns>
        public async static Task<IEnumerable<SpecialEvent>> SearchNearAsync(IGeoLocation location, double range, DateTime since, string topic, IOAuthCredentials credentials)
        {
            if (location == null)
            {
                throw new ArgumentNullException("location", "You must specify a location to search near");
            }

            OAuthWebRequest req = new OAuthWebRequest(new Uri(ApiHelper.BaseUri, new Uri("events/nearby", UriKind.Relative)), credentials);
            req.RequestParameters.Add(new RequestParameter("latitude", location.Latitude.ToString(CultureInfo.InvariantCulture)));
            req.RequestParameters.Add(new RequestParameter("longitude", location.Longitude.ToString(CultureInfo.InvariantCulture)));
            req.RequestParameters.Add(new RequestParameter("range", range.ToString(CultureInfo.InvariantCulture)));
            if (since != default(DateTime))
            {
                req.RequestParameters.Add(new RequestParameter("since", since.ToUnixTime().ToString(CultureInfo.InvariantCulture)));
            }
            if (!string.IsNullOrEmpty(topic))
            {
                req.RequestParameters.Add(new RequestParameter("topic", topic));
            }

            HttpWebResponse res;
            try
            {
                res = await req.GetResponseAsync() as HttpWebResponse;
            }
            catch (WebException)
            {
                // Sometimes we're seeing a 404, even though the server is not reporting one.
                return null;
            }

            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(IEnumerable<SpecialEvent>));

            try
            {
                IEnumerable<SpecialEvent> retval = (IEnumerable<SpecialEvent>)ser.ReadObject(res.GetResponseStream());
                return retval;
            }
            catch (ArgumentNullException)
            {
                // Sometimes we're seeing an ArgumentNullException, even though ser and the stream returned are not null
            }

            return null;
        }

#endif

        /// <summary>
        /// Searches for events near the specified location.
        /// </summary>
        /// <param name="location">The location to search near.</param>
        /// <param name="range">The range in miles to search for events in.</param>
        /// <param name="since">The date to bring back changes since.</param>
        /// <param name="topic">The category of events to bring back.</param>
        /// <param name="credentials">The credentials.</param>
        /// <returns></returns>
        public static IEnumerable<SpecialEvent> SearchNear(IGeoLocation location, double range, DateTime since, string topic, IOAuthCredentials credentials)
        {
            IAsyncResult res = BeginSearchNear( callback => {}, location, range, since, topic, credentials);
#if SILVERLIGHT
            while (!res.IsCompleted)
            {
                System.Threading.Thread.Sleep(100);
            }
#else
            res.AsyncWaitHandle.WaitOne();
#endif
            return EndSearchNear(res);
        }

        /// <summary>
        /// Searches for events near the specified location.
        /// </summary>
        /// <param name="location">The location to search near.</param>
        /// <param name="range">The range in miles to search for events in.</param>
        /// <param name="since">The date to bring back changes since.</param>
        /// <param name="topic">The category of events to bring back.</param>
        /// <param name="credentials">The credentials.</param>
        /// <returns></returns>
        public async static Task<IEnumerable<SpecialEvent>> SearchNearAsync(IGeoLocation location, double range, DateTime since, string topic, IOAuthCredentials credentials)
        {
            if (location == null)
            {
                throw new ArgumentNullException("location", "You must specify a location to search near");
            }

            OAuthWebRequest req = new OAuthWebRequest(new Uri(ApiHelper.BaseUri, new Uri("events/nearby", UriKind.Relative)), credentials);
            req.RequestParameters.Add(new RequestParameter("latitude", location.Latitude.ToString(CultureInfo.InvariantCulture)));
            req.RequestParameters.Add(new RequestParameter("longitude", location.Longitude.ToString(CultureInfo.InvariantCulture)));
            req.RequestParameters.Add(new RequestParameter("range", range.ToString(CultureInfo.InvariantCulture)));
            if (since != default(DateTime))
            {
                req.RequestParameters.Add(new RequestParameter("since", since.ToUnixTime().ToString(CultureInfo.InvariantCulture)));
            }
            if (!string.IsNullOrEmpty(topic))
            {
                req.RequestParameters.Add(new RequestParameter("topic", topic));
            }

            WebResponse res = await req.GetResponseAsync();

            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(IEnumerable<SpecialEvent>));

            try
            {
                IEnumerable<SpecialEvent> retval = (IEnumerable<SpecialEvent>)ser.ReadObject(res.GetResponseStream());
                return retval;
            }
            catch (ArgumentNullException)
            {
                // Sometimes we're seeing an ArgumentNullException, even though ser and the stream returned are not null
            }

            return null;
        }

        /// <summary>
        /// Begins an asynchronous searches for events near the specified location.
        /// </summary>
        /// <param name="location">The location to search near.</param>
        /// <param name="range">The range in miles to search for events in.</param>
        /// <param name="since">The date to bring back changes since.</param>
        /// <param name="topic">The category of events to bring back.</param>
        /// <param name="credentials">The credentials.</param>
        /// <param name="callback">The callback to call when the results are ready to be read</param>
        /// <returns></returns>
        public static IAsyncResult BeginSearchNear(AsyncCallback callback, IGeoLocation location, double range, DateTime since, string topic, IOAuthCredentials credentials)
        {
            if (location == null)
            {
                throw new ArgumentNullException("location", "You must specify a location to search near");
            }

            OAuthWebRequest req = new OAuthWebRequest(new Uri(ApiHelper.BaseUri, new Uri("events/nearby", UriKind.Relative)), credentials);
            req.RequestParameters.Add(new RequestParameter("latitude", location.Latitude.ToString(CultureInfo.InvariantCulture)));
            req.RequestParameters.Add(new RequestParameter("longitude", location.Longitude.ToString(CultureInfo.InvariantCulture)));
            req.RequestParameters.Add(new RequestParameter("range", range.ToString(CultureInfo.InvariantCulture)));
            if (since != default(DateTime))
            {
                req.RequestParameters.Add(new RequestParameter("since", since.ToUnixTime().ToString(CultureInfo.InvariantCulture)));
            }
            if (!string.IsNullOrEmpty(topic))
            {
                req.RequestParameters.Add(new RequestParameter("topic", topic));
            }

            return req.BeginGetResponse(callback, req);
        }

        /// <summary>
        /// Ends an asynchronous search for events near a location.
        /// </summary>
        /// <param name="asyncResult">The async result.</param>
        /// <returns></returns>
        public static IEnumerable<SpecialEvent> EndSearchNear(IAsyncResult asyncResult)
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

            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(IEnumerable<SpecialEvent>));

            try
            {
                IEnumerable<SpecialEvent> retval = (IEnumerable<SpecialEvent>)ser.ReadObject(res.GetResponseStream());
                return retval;
            }
            catch (ArgumentNullException)
            {
                // Sometimes we're seeing an ArgumentNullException, even though ser and the stream returned are not null
            }

            return null;
        }

        #endregion
    }
}
