using System;

namespace LocalAngle.Classifieds
{
    /// <summary>
    /// The different publishing statuses
    /// </summary>
    public enum PublishStatus
    {
        /// <summary>
        /// The advert is still shown, as unsold
        /// </summary>
        Active,
        /// <summary>
        /// The advert is still shown, as sold
        /// </summary>
        Sold,
        /// <summary>
        /// The advert is not to be shown
        /// </summary>
        Deleted
    }
}
