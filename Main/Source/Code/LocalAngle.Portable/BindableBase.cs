using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LocalAngle
{
    /// <summary>
    /// Base implementation for INotifyPropertyChanged
    /// </summary>
    [DataContract]
    public abstract class BindableBase : INotifyPropertyChanged
    {
        #region Public Events

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Protected Methods

        /// <summary>
        /// Checks if a property already matches a desired value.  Sets the property and
        /// notifies listeners only when necessary.
        /// </summary>
        /// <typeparam name="T">Type of the property.</typeparam>
        /// <param name="propertyName">Name of the property used to notify listeners.</param>
        /// <param name="storage">Reference to the backing field for a property with both getter and setter.</param>
        /// <param name="value">Desired value for the property.</param>
        protected void OnPropertyChanged<T>(string propertyName, ref T storage, T value)
        {
            if (!object.Equals(storage, value))
            {
                storage = value;
                OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// Raises the <see cref="E:PropertyChanged"/> event.
        /// </summary>
        /// <param name="e">The <see cref="System.ComponentModel.PropertyChangedEventArgs"/> instance containing the event data.</param>
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        #endregion
    }
}
