using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LocalAngle
{
    public class ContactNumber : BindableBase
    {
        private string _number;
        public string Number
        {
            get
            {
                return _number;
            }
            set
            {
                OnPropertyChanged("Number", ref _number, value);
            }
        }

        private ContactType _contactType;
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
