using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;

namespace LocalAngle
{
    /// <summary>
    /// Represents a point of contact for an organisation or individual
    /// </summary>
    /// <seealso cref="LocalAngle.BindableBase" />
    public class ContactNumber : BindableBase
    {
        #region Public Properties

        private string _detail;
        /// <summary>
        /// Gets or sets the actual contact detail.
        /// </summary>
        /// <value>
        /// The number.
        /// </value>
        [DataMember]
        public string Detail
        {
            get
            {
                return _detail;
            }
            set
            {
                OnPropertyChanged("Detail", ref _detail, (this.ContactType == ContactType.Twitter ? value: TidyUKNumber(value)));
            }
        }

        private string _description;
        /// <summary>
        /// Gets or sets meta data to differentiate multiple numbers
        /// </summary>
        /// <value>
        /// The number.
        /// </value>
        [DataMember]
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

        private ContactType _contactType;
        /// <summary>
        /// Gets or sets the type of the contact.
        /// </summary>
        /// <value>
        /// The type of the contact.
        /// </value>
        [DataMember]
        public ContactType ContactType
        {
            get
            {
                return _contactType;
            }
            set
            {
                OnPropertyChanged("ContactType", ref _contactType, value);
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
#if WINDOWS_UWP
        [Key]
#endif
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

#endregion

#region Public Methods

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return Detail;
        }

#endregion

#region Protected Static Methods

        /// <summary>
        /// Tidies a UK phone number.
        /// </summary>
        /// <param name="number">The number.</param>
        /// <returns></returns>
        protected static string TidyUKNumber( string number )
        {
            if (string.IsNullOrEmpty(number))
            {
                return string.Empty;
            }

            number = Regex.Replace(number,"[^+0-9]","");

            if (string.IsNullOrEmpty(number))
            {
                return string.Empty;
            }

            if (number.StartsWith("+440", StringComparison.OrdinalIgnoreCase))
            {
                number = number.Substring(3);
            }
            else if (number.StartsWith("+00440", StringComparison.OrdinalIgnoreCase))
            {
                number = '0' + number.Substring(5);
            }
            else if (number.StartsWith("+0044", StringComparison.OrdinalIgnoreCase))
            {
                number = '0' + number.Substring(5);
            }
            else if (number.StartsWith("440", StringComparison.OrdinalIgnoreCase))
            {
                number = '0' + number.Substring(3);
            }
            else if (number.StartsWith("441", StringComparison.OrdinalIgnoreCase))
            {
                number = '0' + number.Substring(2);
            }
            else if (number.StartsWith("442", StringComparison.OrdinalIgnoreCase))
            {
                number = '0' + number.Substring(2);
            }
            else if (number.StartsWith("443", StringComparison.OrdinalIgnoreCase))
            {
                number = '0' + number.Substring(2);
            }
            else if (number.StartsWith("448", StringComparison.OrdinalIgnoreCase))
            {
                number = '0' + number.Substring(2);
            }
            else if (number.StartsWith("+44", StringComparison.OrdinalIgnoreCase))
            {
                number = '0' + number.Substring(3);
            }

            switch (number[1])
            {
                case '1':
                    if (number[2] == '1' || number[3] == '1')
                    {
                        // 011x yyy zzzz, 01x1 yyy zzzz
                        number = string.Format(CultureInfo.CurrentCulture, "{0} {1} {2}", number.Substring(0, 4), number.Substring(4, 3), number.Substring(7));
                    }
                    else
                    {
                        number = string.Format(CultureInfo.CurrentCulture, "{0} {1}", number.Substring(0, 5), number.Substring(5));
                    }
                    break;

                case '2':
                    // 02x yyyy zzzz
                    number = string.Format(CultureInfo.CurrentCulture, "{0} {1} {2}", number.Substring(0, 3), number.Substring(3, 4), number.Substring(7));
                    break;

                case '3':
                case '5':
                case '8':
                case '9':
                    // 03xx yyy zzz(z)
                    number = string.Format(CultureInfo.CurrentCulture, "{0} {1} {2}", number.Substring(0, 4), number.Substring(4, 3), number.Substring(7));
                    break;

                case '7':
                    // 07xxx yyyyyy
                    number = string.Format(CultureInfo.CurrentCulture, "{0} {1}", number.Substring(0, 5), number.Substring(5));
                    break;
            }

            return number;
        }

#endregion
    }
}
