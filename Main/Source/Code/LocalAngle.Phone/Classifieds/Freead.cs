using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using LocalAngle.Net;

namespace LocalAngle.Classifieds
{
    public class Freead : BindableBase
    {
        #region Public Properties

        private string _advertId;
        /// <summary>
        /// Gets or sets a unique identifier for the event.
        /// </summary>
        /// <value>
        /// The event id.
        /// </value>
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

        private string _name;
        /// <summary>
        /// Gets or sets the event name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
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

        private string _contact;
        /// <summary>
        /// Gets or sets the contact details.
        /// </summary>
        /// <value>
        /// The contact details.
        /// </value>
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

            OAuthWebRequest req = new OAuthWebRequest(new Uri("http://api.angle.uk.com/oauth/1.0/freead/save"), credentials);
            req.Method = "POST";
            // TODO:
            req.RequestParameters.Add(new RequestParameter("name", Name));
            req.RequestParameters.Add(new RequestParameter("description", Description));
            req.RequestParameters.Add(new RequestParameter("contact", ContactDetails));
            HttpWebResponse res = req.GetResponse() as HttpWebResponse;

            /*
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Freead));
            Freead retval = (Freead)ser.ReadObject(res.GetResponseStream());
            this.AdvertId = retval.AdvertId;
            this.Name = retval.Name;
            this.Description = retval.Description;
            this.ContactDetails = retval.ContactDetails;
            */
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
        public static IEnumerable<Freead> SearchNear(Postcode location, double range, IOAuthCredentials credentials)
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
        public static IEnumerable<Freead> SearchNear(Postcode location, double range, DateTime since, IOAuthCredentials credentials)
        {
            OAuthWebRequest req = new OAuthWebRequest(new Uri("http://api.angle.uk.com/oauth/1.0/adverts/nearby"), credentials);
            req.RequestParameters.Add(new RequestParameter("location", Uri.EscapeDataString(location.ToString())));
            req.RequestParameters.Add(new RequestParameter("range", range.ToString()));
            if (since != default(DateTime))
            {
                req.RequestParameters.Add(new RequestParameter("since", since.ToUnixTime().ToString()));
            }
            HttpWebResponse res = req.GetResponse() as HttpWebResponse;

            /*
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(IEnumerable<Freead>));
            IEnumerable<Freead> retval = (IEnumerable<Freead>)ser.ReadObject(res.GetResponseStream());

            return retval;
            */
            throw new NotImplementedException();
        }

        #endregion
    }
}
