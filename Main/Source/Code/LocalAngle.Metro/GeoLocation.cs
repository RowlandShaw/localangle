using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace LocalAngle
{
    /// <summary>
    /// Basic class to allow framework provided type to implement our interface
    /// </summary>
    public class GeoLocation : IGeoLocation
    {
        #region

        public GeoLocation( double latitude, double longitude )
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// The latitude (in decimal degrees North)
        /// </summary>
        public double Latitude { get; private set; }

        /// <summary>
        /// The longitude (in decimal degrees East)
        /// </summary>
        public double Longitude { get; private set; }

        #endregion

        #region Public Static Properties

        /// <summary>
        /// Convert from framework type
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public static implicit operator GeoLocation(Geoposition position)
        {
            return new GeoLocation(position.Coordinate.Latitude, position.Coordinate.Longitude);
        }

        #endregion
    }
}
