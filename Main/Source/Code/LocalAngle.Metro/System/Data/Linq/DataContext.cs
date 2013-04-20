using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace System.Data.Linq
{
    /// <summary>
    /// Reimplement enough of Linq to SQL as a big nasty facade until a better option can be found for Windows Store apps
    /// </summary>
    public class DataContext : IDisposable
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the System.Data.Linq.DataContext class by referencing a file source.
        /// </summary>
        /// <param name="fileOrConnection">
        /// This argument can be any one of the following:
        /// Using the ms-appx:/ prefix, the name of the file where a local database resides in isolated storage. 
        /// Support for complete connection strings may be added later.
        /// </param>
        public DataContext(string fileOrConnection)
        {
            if (fileOrConnection.StartsWith("ms-appx:", StringComparison.OrdinalIgnoreCase))
            {
                BaseUri = new Uri(fileOrConnection);
            }
            else
            {
                throw new FormatException("Expecting connection string to begin with `ms-appx:` prefix and be a URI.");
            }
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="DataContext" /> class.
        /// </summary>
        ~DataContext()
        {
            Dispose(false);
        }

        /// <summary>
        /// Releases unmanaged and managed resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
            }

            // always want to free up unmanaged resources
        }

        #endregion

        #region Public Properties

        public bool ObjectTrackingEnabled { get; set; }

        #endregion

        #region Private Properties

        private Uri BaseUri { get; set; }
        private List<ITable> Tables { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets the table containing a collection of objects of a particular type, where the type is defined by the TEntity parameter.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns>A collection of objects.</returns>
        public Table<TEntity> GetTable<TEntity>() where TEntity : class
        {
            Uri tableUri = GetUriForTable<TEntity>();
            IStorageFile table = (StorageFile.GetFileFromApplicationUriAsync(tableUri)).AsTask().Result;

            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Table<TEntity>));
            Table<TEntity> retval = (Table<TEntity>)ser.ReadObject((table.OpenReadAsync()).AsTask().Result.AsStreamForRead());
            retval.Context = this;
            Tables.Add(retval);
            return retval;
        }

        /// <summary>
        /// Submits the changes bake to our naive database.
        /// </summary>
        public void SubmitChanges()
        {
            SubmitChanges(ConflictMode.FailOnFirstConflict);
        }

        /// <summary>
        /// Submits the changes bake to our naive database, and specifies the action to be taken if the submission fails
        /// </summary>
        /// <param name="failureMode">The action to be taken if the submission fails.</param>
        /// <remarks>For the moment, the failiure mode is ignored, and conflicts are not checked for - last save wins.</remarks>
        public virtual void SubmitChanges(ConflictMode failureMode)
        {
            // TODO: Need to go through the tables we've loaded, and save them out, if they've changed.
        }

        #endregion

        #region Private Methods

        private Uri GetUriForTable<TEntity>()
        {
            return new Uri(BaseUri, "table.json");
        }

        #endregion
    }
}
