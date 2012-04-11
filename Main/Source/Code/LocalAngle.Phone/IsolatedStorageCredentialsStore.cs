using System;
using System.ComponentModel;
using System.IO.IsolatedStorage;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace LocalAngle
{
    public class IsolatedStorageCredentialsStore : BindableBase, IOAuthCredentials
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="IsolatedStorageCredentialsStore"/> class.
        /// </summary>
        /// <param name="consumerKey">The consumer key.</param>
        /// <param name="consumerSecret">The consumer secret.</param>
        public IsolatedStorageCredentialsStore(string consumerKey, string consumerSecret) : base()
        {
            this.ConsumerKey = consumerKey;
            this.ConsumerSecret = consumerSecret;

            isolatedStore = IsolatedStorageSettings.ApplicationSettings;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the consumer (application) key.
        /// </summary>
        /// <value>
        /// The consumer key.
        /// </value>
        public string ConsumerKey { get; private set; }

        /// <summary>
        /// Gets or sets the consumer secret.
        /// </summary>
        /// <value>
        /// The consumer secret.
        /// </value>
        public string ConsumerSecret { get; private set; }

        /// <summary>
        /// Gets or sets the token (user key).
        /// </summary>
        /// <value>
        /// The token.
        /// </value>
        public string Token
        {
            get
            {
                return GetValueOrDefault(KeyForProperty("Token"), string.Empty);
            }
            set
            {
                OnPropertyChanged("Token", value);
            }
        }

        /// <summary>
        /// Gets or sets the token secret.
        /// </summary>
        /// <value>
        /// The token secret.
        /// </value>
        public string TokenSecret
        {
            get
            {
                return GetValueOrDefault(KeyForProperty("TokenSecret"), string.Empty);
            }
            set
            {
                OnPropertyChanged("TokenSecret", value);
            }
        }

        #endregion

        #region Private Properties

        /// <summary>
        /// The base store for storing the data
        /// </summary>
        private IsolatedStorageSettings isolatedStore;

        #endregion

        #region " Protected Methods "

        /// <summary>
        /// Update a setting value for our application. If the setting does not
        /// exist, then add the setting.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        protected bool AddOrUpdateValue(string key, Object value)
        {
            bool valueChanged = false;

            if (isolatedStore.Contains(key))
            {
                // If the key exists
                if (isolatedStore[key] != value)
                {
                    // If the value has changed, store the new value
                    isolatedStore[key] = value;
                    valueChanged = true;
                }
            }
            else
            {
               // Otherwise create the key.
                isolatedStore.Add(key, value);
                valueChanged = true;
            }

            return valueChanged;
        }

        /// <summary>
        /// Get the current value of the setting, or if it is not found, set the 
        /// setting to the default setting.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        protected T GetValueOrDefault<T>(string key, T defaultValue)
        {
            if (isolatedStore.Contains(key))
            {
                // If the key exists, retrieve the value.
                return (T)isolatedStore[key];
            }
            else
            {
                // Otherwise, use the default value.
                return defaultValue;
            }
        }

        /// <summary>
        /// Checks if a property already matches a desired value.  Sets the property and
        /// notifies listeners only when necessary.
        /// </summary>
        /// <typeparam name="T">Type of the property.</typeparam>
        /// <param name="propertyName">Name of the property used to notify listeners.</param>
        /// <param name="value">Desired value for the property.</param>
        protected void OnPropertyChanged<T>(string propertyName, T value)
        {
            string key = KeyForProperty(propertyName);
            T oldValue = GetValueOrDefault(key, default(T));

            if (!object.Equals(oldValue, value))
            {
                //T oldValue = storage;
                OnPropertyChanging(new PropertyChangingEventArgs(propertyName));
                AddOrUpdateValue(key, value);
                OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// Returns the key to use to store the specified property.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns></returns>
        protected string KeyForProperty(string propertyName)
        {
            return "OAuth_" + propertyName + "_" + Token;
        }

        #endregion
    }
}
