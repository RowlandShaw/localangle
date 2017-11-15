using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
#if WINDOWS_UWP
using System.ComponentModel.DataAnnotations.Schema;
#else
using System.Data.Linq;
using System.Data.Linq.Mapping;
#endif

namespace LocalAngle.Events
{
    /// <summary>
    /// Represents a club
    /// </summary>
    [DataContract]
#if !WINDOWS_UWP
    [Table]
#endif
    public class Club : BindableBase, IGeoLocation
    {
        #region Public Properties

        private string _alternateContactEmail;
#if WINDOWS_UWP
        [MaxLength(255)]
#else
        [Column(DbType = "NVARCHAR(255)", UpdateCheck = UpdateCheck.Never)]
#endif
        public string AlternateContactEmail
        {
            get
            {
                return _alternateContactEmail;
            }
            set
            {
                OnPropertyChanged("AlternateContactEmail", ref _alternateContactEmail, value);
            }
        }

        private string _alternateContactName;
#if WINDOWS_UWP
        [MaxLength(128)]
#else
        [Column(DbType = "NVARCHAR(128)", UpdateCheck = UpdateCheck.Never)]
#endif
        public string AlternateContactName
        {
            get
            {
                return _alternateContactName;
            }
            set
            {
                OnPropertyChanged("AlternateContactName", ref _alternateContactName, value);
            }
        }

        private string _alternateContactNumber;
#if WINDOWS_UWP
        [MaxLength(64)]
#else
        [Column(DbType = "NVARCHAR(64)", UpdateCheck = UpdateCheck.Never)]
#endif
        public string AlternateContactNumber
        {
            get
            {
                return _alternateContactNumber;
            }
            set
            {
                OnPropertyChanged("AlternateContactNumber", ref _alternateContactNumber, value);
            }
        }

        private string _contactEmail;
#if WINDOWS_UWP
        [MaxLength(255)]
#else
        [Column(DbType = "NVARCHAR(255)", UpdateCheck = UpdateCheck.Never)]
#endif
        public string ContactEmail
        {
            get
            {
                return _contactEmail;
            }
            set
            {
                OnPropertyChanged("ContactEmail", ref _contactEmail, value);
            }
        }

        private string _contactName;
#if WINDOWS_UWP
        [MaxLength(128)]
#else
        [Column(DbType = "NVARCHAR(128)", UpdateCheck = UpdateCheck.Never)]
#endif
        public string ContactName
        {
            get
            {
                return _contactName;
            }
            set
            {
                OnPropertyChanged("ContactName", ref _contactName, value);
            }
        }

        private string _contactNumber;
#if WINDOWS_UWP
        [MaxLength(64)]
#else
        [Column(DbType = "NVARCHAR(64)", UpdateCheck = UpdateCheck.Never)]
#endif
        public string ContactNumber
        {
            get
            {
                return _contactNumber;
            }
            set
            {
                OnPropertyChanged("ContactNumber", ref _contactNumber, value);
            }
        }

        private string _description;
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        [DataMember(IsRequired = true)]
        [Required]
#if !WINDOWS_UWP
        [Column(DbType = "NTEXT", UpdateCheck = UpdateCheck.Never)]
#endif
        [DisplayName("Description")]
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                OnPropertyChanged("Description", ref _description, (value == null ? string.Empty : value.Trim()));
            }
        }

        private string _name;
        /// <summary>
        /// Gets or sets the event name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [DataMember(IsRequired = true)]
        [Required]
#if WINDOWS_UWP
        [MaxLength(128)]
#else
        [Column(DbType = "NVARCHAR(128)", UpdateCheck = UpdateCheck.Never)]
#endif
        [DisplayName("Club name")]
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

        private string _venueName;
        /// <summary>
        /// Gets or sets the venue name.
        /// </summary>
        /// <value>
        /// The name of the venue.
        /// </value>
        [DataMember]
        [Required]
#if WINDOWS_UWP
        [MaxLength(255)]
#else
        [Column(DbType = "NVARCHAR(255)", UpdateCheck = UpdateCheck.Never)]
#endif
        [DisplayName("Venue")]
        public string VenueName
        {
            get
            {
                return _venueName;
            }
            set
            {
                OnPropertyChanged("VenueName", ref _venueName, (value == null ? string.Empty : value.Trim()));
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
#if !WINDOWS_UWP
        [Column(UpdateCheck = UpdateCheck.Never)]
#endif
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
#if !WINDOWS_UWP
        [Column(UpdateCheck = UpdateCheck.Never)]
#endif
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
#if !WINDOWS_UWP
        [Column(UpdateCheck = UpdateCheck.Never)]
#endif
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
#if WINDOWS_UWP
        [NotMapped]
#endif
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
#if WINDOWS_UWP
        [MaxLength(8)]
#else
        [Column(DbType = "NVARCHAR(8)", UpdateCheck = UpdateCheck.Never)]
#endif
        [DisplayName("Venue postal code")]
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

        private string _tags;
        /// <summary>
        /// Gets or sets the tags.
        /// </summary>
        /// <value>
        /// The tags.
        /// </value>
#if WINDOWS_UWP
        [MaxLength(100)]
#else
        [Column(DbType = "NVARCHAR(100)", UpdateCheck = UpdateCheck.Never)]
#endif
        [DataMember]
        public string Tags
        {
            get
            {
                return _tags;
            }
            set
            {
                OnPropertyChanged("Tags", ref _tags, value);
            }
        }

        private Uri _website;
        /// <summary>
        /// Gets or sets the website.
        /// </summary>
        /// <value>
        /// The website.
        /// </value>
        [DataMember]
#if WINDOWS_UWP
        [MaxLength(250)]
#else
        [Column(DbType = "NVARCHAR(250)", UpdateCheck = UpdateCheck.Never)]
#endif
        public string Website
        {
            get
            {
                return (_website == null ? "" : _website.ToString());
            }
            set
            {
                try
                {
                    Uri working = (string.IsNullOrEmpty(value) || string.Equals(value, "NULL", StringComparison.OrdinalIgnoreCase) ? null : new Uri(value.Replace(",", ".")));
                    OnPropertyChanged("Website", ref _website, working);
                }
                catch (FormatException ex)
                {
                    if (System.Diagnostics.Debugger.IsAttached)
                    {
                        System.Diagnostics.Debug.WriteLine("'{1}' is not a valid URI: {0}", ex.Message, value);
                    }
                }
            }
        }

        #endregion
    }
}
