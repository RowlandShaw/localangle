using System.ComponentModel;
using System.Runtime.Serialization;

namespace LocalAngle
{
    /// <summary>
    /// Base implementation for INotifyPropertyChanged
    /// </summary>
    [DataContract]
    public abstract class BindableBase : INotifyPropertyChanged
#if !NETFX_CORE
        , INotifyPropertyChanging
#endif
    {
        #region Public Events

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

#if !NETFX_CORE
        /// <summary>
        /// Occurs when a property value is changing.
        /// </summary>
        public event PropertyChangingEventHandler PropertyChanging;
#endif

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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "1#")]
        protected void OnPropertyChanged<T>(string propertyName, ref T storage, T value)
        {
            if (!object.Equals(storage, value))
            {
                //T oldValue = storage;
#if !NETFX_CORE
                OnPropertyChanging(new PropertyChangingEventArgs(propertyName));
#endif
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
            if (PropertyChanged != null)
            {
                PropertyChanged(this, e);
            }
        }

#if !NETFX_CORE
        /// <summary>
        /// Raises the <see cref="E:PropertyChanging"/> event.
        /// </summary>
        /// <param name="e">The <see cref="System.ComponentModel.PropertyChangedEventArgs"/> instance containing the event data.</param>
        protected virtual void OnPropertyChanging(PropertyChangingEventArgs e)
        {
            if (PropertyChanging != null)
            {
                PropertyChanging(this, e);
            }
        }
#endif

        #endregion
    }
}
