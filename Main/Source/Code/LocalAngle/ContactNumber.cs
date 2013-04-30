using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace LocalAngle
{
    public class ContactNumber : BindableBase
    {
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
                OnPropertyChanged("Detail", ref _detail, value);
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
    }
}
