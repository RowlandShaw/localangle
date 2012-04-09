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
        Active = 1,
        /// <summary>
        /// The advert is still shown, as sold out
        /// </summary>
        SoldOut = 2,
        /// <summary>
        /// The advert is not to be shown
        /// </summary>
        Deleted = 0
    }
}
