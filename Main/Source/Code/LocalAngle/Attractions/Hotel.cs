using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using LocalAngle.Net;
using System.Collections.ObjectModel;
#if NETFX_CORE
using System.Threading.Tasks;
#endif
#if WINDOWS_UWP
using System.ComponentModel.DataAnnotations.Schema;
#else
using System.Data.Linq;
using System.Data.Linq.Mapping;
#endif

namespace LocalAngle.Attractions
{
#if !WINDOWS_UWP
    [Table]
#endif
    public class Hotel : BindableBase, IEmployer
    {
        #region Public Properties

        private BrownSignCategory _brownSign = BrownSignCategory.T12;
        /// <summary>
        /// Gets or sets the brown sign category.
        /// </summary>
        /// <value>
        /// The brown sign category.
        /// </value>
        public BrownSignCategory BrownSignCategory
        {
            get
            {
                return _brownSign;
            }
            set
            {
                OnPropertyChanged("BrownSignCategory", ref _brownSign, value);
            }
        }

        private ICollection<ContactNumber> _contacts;
        /// <summary>
        /// Gets the contact numbers.
        /// </summary>
        /// <value>
        /// The contact numbers.
        /// </value>
        public ICollection<ContactNumber> ContactNumbers
        {
            get
            {
                if (_contacts == null)
                {
                    _contacts = new ObservableCollection<ContactNumber>();
                }
                return _contacts;
            }
        }

        private string _deliveryPoint;
        /// <summary>
        /// Gets or sets the delivery point for the establishment.
        /// </summary>
        /// <value>
        /// The delivery point for the address
        /// </value>
        [DataMember]
#if WINDOWS_UWP
        [MaxLength(100)]
#else
        [Column(DbType = "NVARCHAR(100)", UpdateCheck = UpdateCheck.Never)]
#endif

        public string DeliveryPoint
        {
            get
            {
                return _deliveryPoint;
            }
            set
            {
                OnPropertyChanged("DeliveryPoint", ref _deliveryPoint, value);
            }
        }

        private string _email;
        /// <summary>
        /// Gets or sets the email address for the School.
        /// </summary>
        /// <value>
        /// The email address.
        /// </value>
        [DataMember]
#if WINDOWS_UWP
        [MaxLength(255)]
#else
        [Column(DbType = "NVARCHAR(255)", UpdateCheck = UpdateCheck.Never)]
#endif
        public string EmailAddress
        {
            get
            {
                return _email;
            }
            set
            {
                OnPropertyChanged("EmailAddress", ref _email, value);
            }
        }

        private Guid _guid;
        /// <summary>
        /// Gets or sets the primary key GUID.
        /// </summary>
        /// <value>
        /// The primary key GUID.
        /// </value>
        [DataMember]
#if !WINDOWS_UWP
        [Column(IsPrimaryKey = true)]
#endif
        public Guid HotelId
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
        Guid IEmployer.Guid { get { return HotelId; } set { HotelId = value; } }

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

        private string _name;
        /// <summary>
        /// Gets or sets the establishment name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [DataMember(IsRequired = true)]
        [Required]
#if WINDOWS_UWP
        [MaxLength(255)]
#else
        [Column(DbType = "NVARCHAR(255)", UpdateCheck = UpdateCheck.Never)]
#endif
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

        private ICollection<OpeningHours> _openingHours;
        [DataMember]
        public ICollection<OpeningHours> OpeningHours
        {
            get
            {
                if (_openingHours == null)
                {
                    _openingHours = new ObservableCollection<OpeningHours>();
                }
                return _openingHours;
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

#if WINDOWS_UWP
        [NotMapped]
#endif
        Uri IEmployer.Website
        {
            get
            {
                return _website;
            }
            set
            {
                Website = (value == null ? "" : value.ToString());
            }
        }

#if !WINDOWS_UWP
#pragma warning disable 0169
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        [Column(IsVersion = true)]
        private Binary _version;
#pragma warning restore 0169
#endif

        #endregion
    }
}
