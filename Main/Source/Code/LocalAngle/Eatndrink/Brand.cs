using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;

namespace LocalAngle.Eatndrink
{
    public class Brand : BindableBase
    {
        #region Public Properties 

        private int _id;
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [DataMember]
        [Column(DbType = "INT", IsPrimaryKey = true)]
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

        private string _name;
        /// <summary>
        /// Gets or sets the name of the brand.
        /// </summary>
        /// <value>
        /// The brand name, as a customer may know it, e.g. "Hungry Horse", "Beefeater", "Brewers Fayre", "Pizza Express"
        /// It might not be the name above the door though
        /// </value>
        [DataMember]
        [Column(DbType = "NVARCHAR(100)", UpdateCheck = UpdateCheck.Never)]
        public string Name  
        {
            get
            {
                return _name;
            }
            set
            {
                OnPropertyChanged("Name", ref _name, value);
            }
        }

        #endregion

        #region Public Static Methods

        /// <summary>
        /// Loads a collection of brands from json in a stream.
        /// </summary>
        /// <param name="stream">The stream containing the JSON.</param>
        /// <remarks>
        /// It is for the caller to close the stream, as required
        /// </remarks>
        /// <returns></returns>
        public static IEnumerable<Brand> LoadJson(Stream stream)
        {
            try
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(IEnumerable<Brand>));
                IEnumerable<Brand> retval = (IEnumerable<Brand>)ser.ReadObject(stream);
                return retval;
            }
            catch (ArgumentNullException)
            {
                // Sometimes we're seeing an ArgumentNullException, even though ser and the stream returned are not null
            }

            return null;
        }

        #endregion

    }
}
