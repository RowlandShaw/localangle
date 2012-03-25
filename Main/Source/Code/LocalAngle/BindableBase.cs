using System;
using System.ComponentModel;

namespace LocalAngle
{
    public abstract class BindableBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Checks if a property already matches a desired value.  Sets the property and
        /// notifies listeners only when necessary.
        /// </summary>
        /// <typeparam name="T">Type of the property.</typeparam>
        /// <param name="propertyName">Name of the property used to notify listeners.</param>
        /// <param name="storage">Reference to a property with both getter and setter.</param>
        /// <param name="value">Desired value for the property.</param>
        protected void OnPropertyChanged<T>(string propertyName, ref T storage, T value)
        {
            if (!object.Equals(storage, value))
            {
                T oldValue = storage;
                storage = value;
                OnPropertyChanged(propertyName, oldValue, value);
            }
        }

        /// <summary>
        /// Raises the PropertyChanged event
        /// </summary>
        /// <typeparam name="T">Type of the property.</typeparam>
        /// <param name="propertyName">Name of the property used to notify listeners.</param>
        /// <param name="oldValue">The old value for the property.</param>
        /// <param name="newValue">Desired value for the property.</param>
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
