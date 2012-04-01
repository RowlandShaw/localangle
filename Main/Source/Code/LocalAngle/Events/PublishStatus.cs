using System;

namespace LocalAngle.Events
{
    /// <summary>
    /// The different publishing statuses
    /// </summary>
    public enum PublishStatus
    {
        /// <summary>
        /// The advert is still shown
        /// </summary>
        Active,
        /// <summary>
        /// The advert is still shown, as sold out
        /// </summary>
        SoldOut,
        /// <summary>
        /// The advert is not to be shown
        /// </summary>
        Deleted
    }
}
