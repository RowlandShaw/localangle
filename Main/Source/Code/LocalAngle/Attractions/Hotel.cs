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

namespace LocalAngle.Attractions
{
    public class Hotel : BindableBase, IGeoLocation
    {
        #region Public Properties

        private Guid _guid;
        /// <summary>
        /// Gets or sets the primary key GUID.
        /// </summary>
        /// <value>
        /// The primary key GUID.
        /// </value>
        [DataMember]
        public Guid Guid
        {
            get
            {
                return _guid;
            }
            set
            {
                OnPropertyChanged("Guid", ref _guid, value);
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
        [Column(UpdateCheck = UpdateCheck.Never)]
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
        [Column(UpdateCheck = UpdateCheck.Never)]
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
        [Column(UpdateCheck = UpdateCheck.Never)]
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

        private string _name;
        /// <summary>
        /// Gets or sets the establishment name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [DataMember(IsRequired = true)]
        [Required]
        [Column(DbType = "NVARCHAR(255)", UpdateCheck = UpdateCheck.Never)]
        [DisplayName("Establishment name")]
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                OnPropertyChanged("Name", ref _name, (value == null ? string.Empty : value.Trim()));
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
        [Column(DbType = "NVARCHAR(8)", UpdateCheck = UpdateCheck.Never)]
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

        #endregion
    }
}
