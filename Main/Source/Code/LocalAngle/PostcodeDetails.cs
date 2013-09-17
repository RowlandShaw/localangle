using System;
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
#if NETFX_CORE
using System.Threading.Tasks;
#endif

namespace LocalAngle
{
    /// <summary>
    /// Contains meta data deemed from a postal code
    /// </summary>
    /// <remarks>
    /// Data returned is derived from and subject to the Ordnance Survey OpenData licence at:
    /// http://www.ordnancesurvey.co.uk/docs/licences/os-opendata-licence.pdf
    /// </remarks>
    [DataContract]
    public class PostcodeDetails : BindableBase, IGeoLocation
    {

        #region Public Properties

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
        [DisplayName("Postal code")]
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

        private string _county;
        /// <summary>
        /// Gets or sets the county.
        /// </summary>
        /// <value>
        /// The county.
        /// </value>
        [DataMember]
        public string County
        {
            get
            {
                return _county;
            }
            set
            {
                OnPropertyChanged("County", ref _county, value);
            }
        }

        private string _district;
        /// <summary>
        /// Gets or sets the district.
        /// </summary>
        /// <value>
        /// The district.
        /// </value>
        [DataMember]
        public string District
        {
            get
            {
                return _district;
            }
            set
            {
                OnPropertyChanged("District", ref _district, value);
            }
        }

        private string _ward;
        /// <summary>
        /// Gets or sets the electoral ward name.
        /// </summary>
        /// <value>
        /// The ward name.
        /// </value>
        [DataMember]
        public string Ward
        {
            get
            {
                return _ward;
            }
            set
            {
                OnPropertyChanged("Ward", ref _ward, value);
            }
        }

        #endregion

        #region Private Static Methods

        /// <summary>
        /// Searches for details about the specified postal code
        /// </summary>
        /// <param name="location">The location to search for details of.</param>
        /// <param name="credentials">The credentials.</param>
        /// <returns></returns>
        public static PostcodeDetails Load(Postcode location, IOAuthCredentials credentials)
        {
            IAsyncResult res = BeginLoad(callback => { }, location, credentials);
#if SILVERLIGHT
            while (!res.IsCompleted)
            {
                System.Threading.Thread.Sleep(100);
            }
#else
            res.AsyncWaitHandle.WaitOne();
#endif
            return EndLoad(res);
        }

#if NETFX_CORE

        /// <summary>
        /// Begins an asynchronous search for details about the specified postal code
        /// </summary>
        /// <param name="location">The location to search for.</param>
        /// <param name="credentials">The credentials.</param>
        /// <param name="callback">The callback to call when the results are ready to be read</param>
        /// <returns></returns>
        public async static Task<PostcodeDetails> LoadAsync(Postcode location, IOAuthCredentials credentials)
        {
            if (location == null)
            {
                throw new ArgumentNullException("location", "You must specify a postcode to search near");
            }

            OAuthWebRequest req = new OAuthWebRequest(new Uri(ApiHelper.BaseUri, new Uri("info/postcode", UriKind.Relative)), credentials);
            req.RequestParameters.Add(new RequestParameter("location", location.ToString()));

            HttpWebResponse res = await req.GetResponseAsync() as HttpWebResponse;
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(IEnumerable<PostcodeDetails>));
            IEnumerable<PostcodeDetails> retval = (IEnumerable<PostcodeDetails>)ser.ReadObject(res.GetResponseStream());

            return retval.FirstOrDefault();
        }

#endif

        /// <summary>
        /// Begins an asynchronous search for details about the specified postal code
        /// </summary>
        /// <param name="location">The location to search for.</param>
        /// <param name="credentials">The credentials.</param>
        /// <param name="callback">The callback to call when the results are ready to be read</param>
        /// <returns></returns>
        public static IAsyncResult BeginLoad(AsyncCallback callback, Postcode location, IOAuthCredentials credentials)
        {
            if (location == null)
            {
                throw new ArgumentNullException("location", "You must specify a postcode to search near");
            }

            OAuthWebRequest req = new OAuthWebRequest(new Uri(ApiHelper.BaseUri, new Uri("info/postcode", UriKind.Relative)), credentials);
            req.RequestParameters.Add(new RequestParameter("location", location.ToString()));

            return req.BeginGetResponse(callback, req);
        }

        /// <summary>
        /// Ends an asynchronous search for events near a location.
        /// </summary>
        /// <param name="asyncResult">The async result.</param>
        /// <returns></returns>
        public static PostcodeDetails EndLoad(IAsyncResult asyncResult)
        {
            if (asyncResult == null)
            {
                throw new ArgumentNullException("asyncResult");
            }

            OAuthWebRequest req = (OAuthWebRequest)asyncResult.AsyncState;
            HttpWebResponse res = req.EndGetResponse(asyncResult) as HttpWebResponse;
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(IEnumerable<PostcodeDetails>));
            IEnumerable<PostcodeDetails> retval = (IEnumerable<PostcodeDetails>)ser.ReadObject(res.GetResponseStream());

            return retval.FirstOrDefault();
        }

        #endregion
    }
}
