﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LocalAngle
{
    /// <summary>
    /// Basic class to allow framework provided type to implement our interface
    /// </summary>
    public class GeoLocation : IGeoLocation
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GeoLocation"/> class.
        /// </summary>
        /// <param name="latitude">The latitude.</param>
        /// <param name="longitude">The longitude.</param>
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

#if TODO
        /// <summary>
        /// Convert from framework type
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public static implicit operator GeoLocation(GeoCoordinate position)
        {
            if (position == null)
            {
                return null;
            }

            return new GeoLocation(position.Latitude, position.Longitude);
        }

        /// <summary>
        /// convert from framework type
        /// </summary>
        /// <param name="position">The position.</param>
        /// <returns></returns>
        public static GeoLocation FromGeoCoordinate(GeoCoordinate position)
        {
            return position;
        }

        /// <summary>
        /// Convert to framework type
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public static implicit operator GeoCoordinate(GeoLocation position)
        {
            if (position == null)
            { 
                return null;
            }

            return new GeoCoordinate(position.Latitude, position.Longitude);
        }

        /// <summary>
        /// convert from framework type
        /// </summary>
        /// <param name="position">The position.</param>
        /// <returns></returns>
        public static GeoCoordinate ToGeoCoordinate(GeoLocation position)
        {
            return position;
        }
#endif

        #endregion
    }
}
