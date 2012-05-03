﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Linq.Mapping;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using LocalAngle.Net;

namespace LocalAngle.Events
{
    /// <summary>
    /// Represents an event
    /// </summary>
    [DataContract]
    [Table]
    public class SpecialEvent : BindableBase, IComparable<SpecialEvent>, IEquatable<SpecialEvent>
    {
        #region Public Properties

        private string _eventId;
        /// <summary>
        /// Gets or sets a unique identifier for the event.
        /// </summary>
        /// <value>
        /// The event id.
        /// </value>
        [DataMember]
        [Column(IsPrimaryKey = true)]
        public string EventId
        {
            get
            {
                return _eventId;
            }
            set
            {
                OnPropertyChanged("AdvertId", ref _eventId, value);
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
        [Required]
        [Column]
        [DisplayName("Event name")]
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

        private string _description;
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        [DataMember(IsRequired = true)]
        [Required]
        [Column]
        [DisplayName("Description")]
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
        /// Gets or sets the distance (in miles) from the search point used to load it
        /// </summary>
        /// <value>
        /// The distance in UK miles.
        /// </value>
        /// <remarks>Only meaningful when populated during a search (so doesn't respect INotfifyProperty...).</remarks>
        [DataMember]
        public double Distance { get; set; }

        private string _venueName;
        /// <summary>
        /// Gets or sets the venue name.
        /// </summary>
        /// <value>
        /// The name of the venue.
        /// </value>
        [DataMember(IsRequired = true)]
        [Required]
        [Column]
        [DisplayName("Venue")]
        public string VenueName
        {
            get
            {
                return _venueName;
            }
            set
            {
                OnPropertyChanged("VenueName", ref _venueName, value);
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
        [Column(IsVersion = true)]
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
        [Required]
        [Column]
        [DisplayName("Start")]
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
        [Required]
        [Column]
        [DisplayName("End")]
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

        private ICollection<string> _tags;
        /// <summary>
        /// Gets or sets the tags.
        /// </summary>
        /// <value>
        /// The tags.
        /// </value>
        [DataMember]
        public ICollection<string> Tags
        {
            get
            {
                if( _tags == null )
                {
                    _tags = new List<string>();
                }

                return _tags;
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
        [Column]
        [DisplayName("Online ticketing")]
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
        [Column]
        [DisplayName("Venue postal code")]
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
            return CompareTo(other) == 0;
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return StartTime.GetHashCode() ^ EndTime.GetHashCode();
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

            OAuthWebRequest req = new OAuthWebRequest(new Uri("http://api.angle.uk.com/oauth/1.0/event/save"), credentials);
            req.Method = "POST";
            req.RequestParameters.Add(new RequestParameter("name", Name));
            req.RequestParameters.Add(new RequestParameter("description", Description));
            req.RequestParameters.Add(new RequestParameter("locname", VenueName));
            req.RequestParameters.Add(new RequestParameter("locpostcode", Location.ToString()));
            req.RequestParameters.Add(new RequestParameter("start", StartTime.ToString(DateFormat,CultureInfo.InvariantCulture)));
            req.RequestParameters.Add(new RequestParameter("end", EndTime.ToString(DateFormat, CultureInfo.InvariantCulture)));
            req.RequestParameters.Add(new RequestParameter("tag", string.Join("-", Tags.ToArray())));
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

            OAuthWebRequest req = new OAuthWebRequest(new Uri("http://api.angle.uk.com/oauth/1.0/events/nearby"), credentials);
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

            OAuthWebRequest req = new OAuthWebRequest(new Uri("http://api.angle.uk.com/oauth/1.0/events/nearby"), credentials);
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
            HttpWebResponse res = req.EndGetResponse(asyncResult) as HttpWebResponse;
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(IEnumerable<SpecialEvent>));
            IEnumerable<SpecialEvent> retval = (IEnumerable<SpecialEvent>)ser.ReadObject(res.GetResponseStream());

            return retval;
        }

        #endregion
    }
}
