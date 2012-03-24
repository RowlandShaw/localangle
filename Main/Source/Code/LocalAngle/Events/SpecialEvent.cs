﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using LocalAngle.Net;

namespace LocalAngle.Events
{
    [DataContract]
    public class SpecialEvent : BindableBase
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
            protected set
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
            protected set
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
            protected set
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
            protected set
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
            protected set
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
            protected set
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
            protected set
            {
                OnPropertyChanged("EndTime", ref _end, value);
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
            protected set
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

            throw new NotImplementedException();
        }

        #endregion

        #region Public Static Methods

        /// <summary>
        /// Searches for events near the specified location.
        /// </summary>
        /// <param name="location">The location to search near.</param>
        /// <param name="range">The range in miles to search for events in.</param>
        /// <param name="credentials">The credentials.</param>
        /// <returns></returns>
        public static IEnumerable<SpecialEvent> SearchNear(Postcode location, double range, IOAuthCredentials credentials)
        {
            return SearchNear(location, range, default(DateTime), credentials);
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
            OAuthWebRequest req = new OAuthWebRequest(new Uri("http://api.angle.uk.com/oauth/1.0/events/nearby"), credentials);
            req.RequestParameters.Add(new RequestParameter("location", Uri.EscapeDataString(location.ToString())));
            req.RequestParameters.Add(new RequestParameter("range",range.ToString()));
            if (since != default(DateTime))
            {
                req.RequestParameters.Add(new RequestParameter("since", since.ToUnixTime().ToString()));
            }
            HttpWebResponse res = req.GetResponse() as HttpWebResponse;

            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(IEnumerable<SpecialEvent>));
            IEnumerable<SpecialEvent> retval = (IEnumerable<SpecialEvent>)ser.ReadObject(res.GetResponseStream());

            return retval;
        }

        #endregion
    }
}
