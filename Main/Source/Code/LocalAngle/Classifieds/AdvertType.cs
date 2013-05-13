using System;

namespace LocalAngle.Classifieds
{
    /// <summary>
    /// The advert type
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1008:EnumsShouldHaveZeroValue")]
    public enum AdvertType
    {
        /// <summary>
        /// Item for sale
        /// </summary>
        ForSale = 1,
        /// <summary>
        /// Item wanted
        /// </summary>
        Wanted = 2,
        /// <summary>
        /// Missing pet
        /// </summary>
        Missing = 3,
        /// <summary>
        /// Free-cycling
        /// </summary>
        FreeCycle = 4
    }
}
