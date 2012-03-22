using System;
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
    public class SpecialEvent
    {
        #region Constructors

        public SpecialEvent()
        {
            Location = new Postcode();
        }

        #endregion

        #region Public Properties

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Description { get; set; }

        public Postcode Location { get; set; }

        [DataMember]
        public DateTime StartTime { get; set; }

        [DataMember]
        public DateTime EndTime { get; set; }

        #endregion

        #region Protected Properties

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

        #region Public Static Methods

        public static IEnumerable<SpecialEvent> SearchNear(Postcode location, double range, IOAuthCredentials credentials)
        {
            return SearchNear(location, range, default(DateTime), credentials);
        }

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
