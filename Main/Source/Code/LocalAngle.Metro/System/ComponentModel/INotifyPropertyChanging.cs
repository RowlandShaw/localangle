namespace System.ComponentModel
{
    /// <summary>
    /// Replicates just enough of LINQ to SQL to allow source compatibility for Metro for notifying clients that a property value is changing.
    /// </summary>
    public interface INotifyPropertyChanging
    {
        /// <summary>
        /// Occurs when a property value is changing.
        /// </summary>
        event EventHandler<PropertyChangingEventArgs> PropertyChanging;
    }

    /// <summary>
    /// Replicates part of the framework not present for Metro to provide the data for the INotifyPropertyChanging.PropertyChanging event
    /// </summary>
    public class PropertyChangingEventArgs : PropertyChangedEventArgs
    {
        /// <summary>
        /// The name of the property that is changing.
        /// </summary>
        /// <param name="propertyName">The name of the property that is changing.</param>
        public PropertyChangingEventArgs(string propertyName) : base(propertyName) { }
    }
}