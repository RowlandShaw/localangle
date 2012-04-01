using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using LocalAngle.Net;

namespace LocalAngle.Events
{
    [DataContract]
    public class SpecialEvent : BindableBase, IComparable<SpecialEvent>, IEquatable<SpecialEvent>
    {
        #region Constructors

        public SpecialEvent()
        {
            Location = new Postcode();
        }

        #endregion

        #region Public Properties

        private string _eventId;
        /// <summary>
        /// Gets or sets a unique identifier for the event.
        /// </summary>
        /// <value>
        /// The event id.
        /// </value>
        [DataMember]
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
        [DataMember]
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
        [DataMember]
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
                OnPropertyChanged("VenueName", ref _venueName, value);
            }
        }

        private Postcode _location;
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
        [DataMember]
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
        [DataMember]
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

        #endregion

        #region Protected Properties

        /// <summary>
        /// Gets or sets the postcode.
        /// </summary>
        /// <value>
        /// The postcode.
        /// </value>
        /// <remarks>
        /// Intended for use by the JSON serialisers
        /// </remarks>
        [DataMember]
        protected string Postcode
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

        public override bool Equals(object obj)
        {
            return Equals(obj as SpecialEvent);
        }

        public bool Equals(SpecialEvent other)
        {
            return CompareTo(other) == 0;
        }

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
            if (string.IsNullOrEmpty(credentials.Token))
            {
                throw new UnauthorizedAccessException("Insufficient details provided to be able to save changes.");
            }

            OAuthWebRequest req = new OAuthWebRequest(new Uri("http://api.angle.uk.com/oauth/1.0/event/save"), credentials);
            req.Method = "POST";
            // TODO:
            req.RequestParameters.Add(new RequestParameter("name", Name));
            req.RequestParameters.Add(new RequestParameter("description", Description));
            req.RequestParameters.Add(new RequestParameter("locname", VenueName));
            req.RequestParameters.Add(new RequestParameter("locpostcode", Location.ToString()));
            req.RequestParameters.Add(new RequestParameter("start", StartTime.ToString(DateFormat)));
            req.RequestParameters.Add(new RequestParameter("end", EndTime.ToString(DateFormat)));
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

        public static bool operator ==(SpecialEvent left, SpecialEvent right)
        {
            return object.ReferenceEquals(left,right) || left.Equals(right);
        }

        public static bool operator !=(SpecialEvent left, SpecialEvent right)
        {
            return !(left == right);
        }

        public static bool operator >(SpecialEvent left, SpecialEvent right)
        {
            return left.CompareTo(right) > 0;
        }

        public static bool operator <=(SpecialEvent left, SpecialEvent right)
        {
            return left.CompareTo(right) <= 0;
        }

        public static bool operator >=(SpecialEvent left, SpecialEvent right)
        {
            return left.CompareTo(right) >= 0;
        }

        public static bool operator <(SpecialEvent left, SpecialEvent right)
        {
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
            return SearchNear(location, range, default(DateTime), null, credentials);
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
        public static IEnumerable<SpecialEvent> SearchNear(Postcode location, double range, DateTime since, string topic, IOAuthCredentials credentials)
        {
            OAuthWebRequest req = new OAuthWebRequest(new Uri("http://api.angle.uk.com/oauth/1.0/events/nearby"), credentials);
            req.RequestParameters.Add(new RequestParameter("location", location.ToString()));
            req.RequestParameters.Add(new RequestParameter("range",range.ToString()));
            if (since != default(DateTime))
            {
                req.RequestParameters.Add(new RequestParameter("since", since.ToUnixTime().ToString()));
            }
            if (! string.IsNullOrEmpty(topic))
            {
                req.RequestParameters.Add(new RequestParameter("topic", topic));
            }
            HttpWebResponse res = req.GetResponse() as HttpWebResponse;

            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(IEnumerable<SpecialEvent>));
            IEnumerable<SpecialEvent> retval = (IEnumerable<SpecialEvent>)ser.ReadObject(res.GetResponseStream());

            return retval;
        }

        #endregion
    }
}
