using System;
using System.Data.Linq.Mapping;
using System.Runtime.Serialization;

namespace LocalAngle.Recruitment
{
    /// <summary>
    /// Represents a Employer
    /// </summary>
    [DataContract]
    [Table]
    public class Employer : BindableBase
    {
        #region Public Properties 

        private Guid _employerId;
        /// <summary>
        /// Gets or sets a unique identifier for the job.
        /// </summary>
        /// <value>
        /// The event id.
        /// </value>
        [DataMember]
        [Column(IsPrimaryKey = true)]
        public Guid EmployerId
        {
            get
            {
                return _employerId;
            }
            set
            {
                OnPropertyChanged("EmployerId", ref _employerId, value);
            }
        }

        private string _deliveryPoint;
        /// <summary>
        /// Gets or sets the delivery point, be it the premises number on a street, or a building name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [DataMember]
        [Column]
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

        private string _name;
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [DataMember]
        [Column(CanBeNull=false)]
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                OnPropertyChanged("Name", ref _name, value);
            }
        }

        private Postcode _location = new Postcode();
        /// <summary>
        /// Gets or sets the postal code for this employment location.
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
        [Column]
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
