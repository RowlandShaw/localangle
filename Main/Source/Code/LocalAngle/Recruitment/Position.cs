using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using LocalAngle.Net;

namespace LocalAngle.Recruitment
{
    /// <summary>
    /// Represents a position that is being recruited for
    /// </summary>
    [DataContract]
    [Table]
    public class Position : BindableBase
    {
        private Guid _jobId;
        /// <summary>
        /// Gets or sets a unique identifier for the job.
        /// </summary>
        /// <value>
        /// The event id.
        /// </value>
        [DataMember]
        [Column(IsPrimaryKey = true)]
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
        [Column(CanBeNull=false)]
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                OnPropertyChanged( "Title", ref _title, value)
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
        [Column]
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
        [Column]
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

        private DateTime? _startDate;
        /// <summary>
        /// Gets or sets the start date for the role.
        /// </summary>
        /// <value>
        /// The start date.
        /// </value>
        /// <remarks>A null value can be used to denote "As soon as possible"</remarks>
        [DataMember]
        [Column]
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

        private string _rate;
        private string _reference;

    }
}
