using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LocalAngle
{
    /// <summary>
    /// The type of contact numebr
    /// </summary>
    public enum ContactType
    {
        /// <summary>
        /// Telephone (voice, or shared fax)
        /// </summary>
        Tel,
        /// <summary>
        /// Fax
        /// </summary>
        Fax,
        /// <summary>
        /// Mobile telephone
        /// </summary>
        Mob,
        /// <summary>
        /// Textphone
        /// </summary>
        Text,
        /// <summary>
        /// Unknown
        /// </summary>
        Other,
        /// <summary>
        /// Minicom
        /// </summary>
        Minicom,
        /// <summary>
        /// Telex
        /// </summary>
        Telex,
        /// <summary>
        /// Twitter
        /// </summary>
        Twitter
    }
}
