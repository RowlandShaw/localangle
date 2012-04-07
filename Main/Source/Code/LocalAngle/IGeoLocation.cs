using System;
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
}
