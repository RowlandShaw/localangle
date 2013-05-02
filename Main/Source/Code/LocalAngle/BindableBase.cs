using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace LocalAngle
{
    /// <summary>
    /// Base implementation for INotifyPropertyChanged and INotifyPropertyChanging
    /// </summary>
    [DataContract]
    public abstract class BindableBase : INotifyPropertyChanged, INotifyPropertyChanging
    {
        #region Public Events

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Occurs when a property value is changing.
        /// </summary>
        public event PropertyChangingEventHandler PropertyChanging;

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
                OnPropertyChanging(new PropertyChangingEventArgs(propertyName));
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

        /// <summary>
        /// Raises the <see cref="E:PropertyChanging"/> event.
        /// </summary>
        /// <param name="e">The <see cref="System.ComponentModel.PropertyChangedEventArgs"/> instance containing the event data.</param>
        protected virtual void OnPropertyChanging(PropertyChangingEventArgs e)
        {
            PropertyChangingEventHandler handler = PropertyChanging;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        #endregion
    }

    public static class BindableBaseExtensions
    {
        /// <summary>
        /// Saves in JSON notation to the specified stream.
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <param name="stream">The stream.</param>
        public static void SaveJson<T>(this IEnumerable<T> collection, Stream stream) where T : BindableBase
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(IEnumerable<T>));
            ser.WriteObject(stream, collection);
        }
    }
}
