using System;
using System.ComponentModel.DataAnnotations;
#if !WINDOWS_UWP
using System.Data.Linq;
using System.Data.Linq.Mapping;
#endif
using System.Runtime.Serialization;

namespace LocalAngle.Recruitment
{
    /// <summary>
    /// Represents a position that is being recruited for
    /// </summary>
    [DataContract]
#if !WINDOWS_UWP
    [Table]
#endif
    public class Position : BindableBase, IGeoLocation
    {
        #region Public Properties

        private Guid _jobId;
        /// <summary>
        /// Gets or sets a unique identifier for the job.
        /// </summary>
        /// <value>
        /// The event id.
        /// </value>
        [DataMember]
#if !WINDOWS_UWP
        [Column(IsPrimaryKey = true)]
#endif
        public Guid JobId
        {
            get
            {
                return _jobId;
            }
            set
            {
                OnPropertyChanged("JobId", ref _jobId, value);
            }
        }

        private string _title;
        /// <summary>
        /// Gets or sets the job title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        [DataMember]
#if !WINDOWS_UWP
        [Column(CanBeNull=false, DbType="NVARCHAR(255)")]
#else
        [MaxLength(255)]
#endif
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                OnPropertyChanged("Title", ref _title, value);
            }
        }

        private string _contact;
        /// <summary>
        /// Gets or sets the contact details.
        /// </summary>
        /// <value>
        /// The contact details.
        /// </value>
        [DataMember]
#if !WINDOWS_UWP
        [Column(DbType="NVARCHAR(255)")]
#else
        [MaxLength(255)]
#endif
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

        private DateTime? _closingDate;
        /// <summary>
        /// Gets or sets the closing date for applications for the position.
        /// </summary>
        /// <value>
        /// The start date.
        /// </value>
        /// <remarks>A null value can be used to denote where no closing date has been set</remarks>
        [DataMember]
#if !WINDOWS_UWP
        [Column]
#endif
        public DateTime? ClosingDate
        {
            get
            {
                return _closingDate;
            }
            set
            {
                OnPropertyChanged("ClosingDate", ref _closingDate, value);
            }
        }

        private string _description;
        /// <summary>
        /// Gets or sets the description of the role
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        [DataMember(IsRequired = true)]
#if !WINDOWS_UWP
        [Column(DbType = "NTEXT", UpdateCheck = UpdateCheck.Never)]
#endif
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                OnPropertyChanged("Description", ref _description, value);
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
        [Column]
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
        [Column]
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

        private DateTime? _startDate;
        /// <summary>
        /// Gets or sets the start date for the role.
        /// </summary>
        /// <value>
        /// The start date.
        /// </value>
        /// <remarks>A null value can be used to denote "As soon as possible"</remarks>
        [DataMember]
#if !WINDOWS_UWP
        [Column]
#endif
        public DateTime? StartDate
        {
            get
            {
                return _startDate;
            }
            set
            {
                OnPropertyChanged("StartDate", ref _startDate, value);
            }
        }

        private RemunerationRange _rate;
        /// <summary>
        /// Gets or sets the pay range.
        /// </summary>
        /// <value>
        /// The pay range.
        /// </value>
        public RemunerationRange PayRange
        {
            get
            {
                return _rate;
            }
            set
            {
                OnPropertyChanged("PayRange", ref _rate, value);
            }
        }

        private string _reference;
        /// <summary>
        /// Gets or sets the reference number for this advert.
        /// </summary>
        /// <value>
        /// The reference.
        /// </value>
        [DataMember]
#if !WINDOWS_UWP
        [Column]
#endif
        public string Reference
        {
            get
            {
                return _reference;
            }
            set
            {
                OnPropertyChanged("Reference", ref _reference, value);
            }
        }

        #endregion
    }
}
