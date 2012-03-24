using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LocalAngle
{
    public abstract class BindableBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged<T>(string propertyName, ref T storage, T value)
        {
            if( !object.Equals( storage, value ) )
            {
                T oldValue = storage;
                storage = value;
                OnPropertyChanged(propertyName, oldValue, value);
            }
        }

        protected void OnPropertyChanged<T>(string propertyName, T oldValue, T newValue)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, e);
            }
        }
    }
}
