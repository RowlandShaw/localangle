using System;
using System.Collections.Generic;
#if !WINDOWS_UWP
using System.Data.Linq.Mapping;
#endif
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace LocalAngle
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
#if !WINDOWS_UWP
    [Table]
#endif
    public class OpeningHours : BindableBase, IComparable<OpeningHours>, IEquatable<OpeningHours>
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

        private int _id;
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
#if !WINDOWS_UWP
        [Column(IsPrimaryKey = true)]
#endif
        [DataMember]
        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                OnPropertyChanged("Id", ref _id, value);
            }
        }

        private int _dayNumber;
        /// <summary>
        /// Gets or sets the day number.
        /// </summary>
        /// <value>
        /// The day number.
        /// </value>
        [DataMember]
        public int DayNumber
        {
            get
            {
                return _dayNumber;
            }
            set
            {
                OnPropertyChanged("DayNumber", ref _dayNumber, value);
            }
        }

        private string _openTime;
        /// <summary>
        /// Gets or sets the opening time.
        /// </summary>
        /// <value>
        /// The opening time.
        /// </value>
        [DataMember]
        public string OpeningTime
        {
            get
            {
                return _openTime;
            }
            set
            {
                OnPropertyChanged("OpeningTime", ref _openTime, value);
            }
        }

        private string _closeTime;
        /// <summary>
        /// Gets or sets the closing time.
        /// </summary>
        /// <value>
        /// The closing time.
        /// </value>
        [DataMember]
        public string ClosingTime
        {
            get
            {
                return _closeTime;
            }
            set
            {
                OnPropertyChanged("ClosingTime", ref _closeTime, value);
            }
        }

        private string _comments;
        /// <summary>
        /// Gets or sets the comments.
        /// </summary>
        /// <value>
        /// The comments.
        /// </value>
        [DataMember]
        public string Comments
        {
            get
            {
                return _comments;
            }
            set
            {
                OnPropertyChanged("Comments", ref _comments, value);
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Compares to.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns></returns>
        public int CompareTo(OpeningHours other)
        {
            if (object.ReferenceEquals(this, other))
            {
                return 0;
            }

            if (object.ReferenceEquals(null, other))
            {
                return -1;
            }

            int retval = this.DayNumber.CompareTo(other.DayNumber);
            if (retval == 0)
            {
                retval = string.Compare(this.OpeningTime, other.OpeningTime, StringComparison.OrdinalIgnoreCase);
            }

            if (retval == 0)
            {
                retval = string.Compare(this.ClosingTime, other.ClosingTime, StringComparison.OrdinalIgnoreCase);
            }

            if (retval == 0)
            {
                retval = string.Compare(this.Comments, other.Comments, StringComparison.CurrentCultureIgnoreCase);
            }

            if (retval == 0)
            {
                retval = this.Guid.CompareTo(other.Guid);
            }

            return retval;
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            OpeningHours other = obj as OpeningHours;
            return Equals(other);
        }

        /// <summary>
        /// Equalses the specified other.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns></returns>
        public bool Equals(OpeningHours other)
        {
            if (ReferenceEquals(other, null))
            {
                return false;
            }
            else
            {
                return Id == other.Id 
                    && Guid == other.Guid
                    && DayNumber == other.DayNumber
                    && string.Equals(OpeningTime, other.OpeningTime, StringComparison.OrdinalIgnoreCase)
                    && string.Equals(ClosingTime, other.ClosingTime, StringComparison.OrdinalIgnoreCase)
                    && string.Equals(Comments, other.Comments, StringComparison.CurrentCulture);
            }
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return Id;
        }

        #endregion

        #region Public Static Methods

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator ==(OpeningHours left, OpeningHours right)
        {
            if( object.ReferenceEquals(left, right) )
            {
                return true;
            }

            if (!object.ReferenceEquals(left, null))
            {
                return left.Equals(right);
            }
            else if (!object.ReferenceEquals(right, null))
            {
                return right.Equals(left);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator !=(OpeningHours left, OpeningHours right)
        {
            return !(left == right);
        }

        /// <summary>
        /// Implements the operator &gt;.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator >(OpeningHours left, OpeningHours right)
        {
            if (left == null)
            {
                return (right == null ? false : true);
            }

            return left.CompareTo(right) > 0;
        }

        /// <summary>
        /// Implements the operator &lt;=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator <=(OpeningHours left, OpeningHours right)
        {
            return !(left > right);
        }

        /// <summary>
        /// Implements the operator &gt;=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator >=(OpeningHours left, OpeningHours right)
        {
            return !(left < right);
        }

        /// <summary>
        /// Implements the operator &lt;.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator <(OpeningHours left, OpeningHours right)
        {
            if (left == null)
            {
                return false;
            }

            return left.CompareTo(right) < 0;
        }

        #endregion
    }
}
