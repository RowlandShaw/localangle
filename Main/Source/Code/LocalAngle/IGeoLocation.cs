﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LocalAngle
{
    /// <summary>
    /// Allow us to abstract out the different sources of geographical position
    /// </summary>
    public interface IGeoLocation
    {
        /// <summary>
        /// The latitude (in decimal degrees North)
        /// </summary>
        double Latitude { get; }

        /// <summary>
        /// The longitude (in decimal degrees East)
        /// </summary>
        double Longitude { get; }
    }

    public static class GeoLocationExtensions
    {
        /// <summary>
        /// Return the great circle distance in kilometres between two IGeoLocation instances
        /// </summary>
        /// <param name="start">The start point</param>
        /// <param name="end">The end point</param>
        /// <returns>The great circle distance in kilometres</returns>
        public static double KilometresTo(this IGeoLocation start, IGeoLocation end)
        {
            if (start == null || end == null)
            {
                return -1;
            }

            return start.KilometresTo(end.Latitude, end.Longitude);
        }

        /// <summary>
        /// Return the great circle distance in kilometres between two IGeoLocation instances
        /// </summary>
        /// <param name="start">The start point</param>
        /// <param name="endLatitude">The end latitude</param>
        /// <param name="endLongitude">The end longitude</param>
        /// <returns>The great circle distance in kilometres</returns>
        public static double KilometresTo(this IGeoLocation start, double endLatitude, double endLongitude)
        {
            if (start == null)
            {
                return -1;
            }

            double xa = (Math.Cos(Math.PI * start.Latitude / 180.0)) * (Math.Cos(Math.PI * start.Longitude / 180.0));
            double ya = (Math.Cos(Math.PI * start.Latitude / 180.0)) * (Math.Sin(Math.PI * start.Longitude / 180.0));
            double za = (Math.Sin(Math.PI * start.Latitude / 180.0));

            double xb = (Math.Cos(Math.PI * endLatitude / 180.0)) * (Math.Cos(Math.PI * endLongitude / 180.0));
            double yb = (Math.Cos(Math.PI * endLatitude / 180.0)) * (Math.Sin(Math.PI * endLongitude / 180.0));
            double zb = (Math.Sin(Math.PI * endLatitude / 180.0));
            
            double val = xa * xb + ya * yb + za * zb;
            if (val > 1)
            {
                // sometimes rounding can cause this to get squiffy when there is no distance between the points
                val = 1;
            }

            return (MeanRadiusKilometres * Math.Acos(val));
        }

        /// <summary>
        /// Return the great circle distance in miles between two IGeoLocation instances
        /// </summary>
        /// <param name="start">The start point</param>
        /// <param name="end">The end point</param>
        /// <returns>The great circle distance in miles</returns>
        public static double MilesTo(this IGeoLocation start, IGeoLocation end)
        {
            if (start == null || end == null)
            {
                return -1;
            }

            return start.KilometresTo(end) / KilometresInAMile;
        }

        /// <summary>
        /// Return the great circle distance in kilometres between two IGeoLocation instances
        /// </summary>
        /// <param name="start">The start point</param>
        /// <param name="endLatitude">The end latitude</param>
        /// <param name="endLongitude">The end longitude</param>
        /// <returns>The great circle distance in kilometres</returns>
        public static double MilesTo(this IGeoLocation start, double endLatitude, double endLongitude)
        {
            if (start == null)
            {
                return -1;
            }

            return start.KilometresTo(endLatitude, endLongitude);
        }

        private const double MeanRadiusKilometres = 19113.0263142 / 3.0; // WGS84 definition
        private const double KilometresInAMile = 1.609344; // 1760 yards in a mile
    }
}
