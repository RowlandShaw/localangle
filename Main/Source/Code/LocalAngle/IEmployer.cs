using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LocalAngle
{
    /// <summary>
    /// Common interface for employers
    /// </summary>
    public interface IEmployer : IGeoLocation
    {
        /// <summary>
        /// Gets the contact numbers.
        /// </summary>
        /// <value>
        /// The contact numbers.
        /// </value>
        ICollection<ContactNumber> ContactNumbers { get; }

        /// <summary>
        /// Gets or sets the delivery point.
        /// </summary>
        /// <value>
        /// The delivery point.
        /// </value>
        string DeliveryPoint { get; set; }

        /// <summary>
        /// Gets or sets the email address.
        /// </summary>
        /// <value>
        /// The email address.
        /// </value>
        string EmailAddress { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier.
        /// </summary>
        /// <value>
        /// The unique identifier.
        /// </value>
        Guid Guid { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        string Name { get; set; }

        /// <summary>
        /// Gets the opening hours.
        /// </summary>
        /// <value>
        /// The opening hours.
        /// </value>
        ICollection<OpeningHours> OpeningHours { get; }

        /// <summary>
        /// Gets or sets the postcode.
        /// </summary>
        /// <value>
        /// The postcode.
        /// </value>
        string Postcode { get; set; }

        /// <summary>
        /// Gets or sets the website.
        /// </summary>
        /// <value>
        /// The website.
        /// </value>
        Uri Website { get; set; }
    }
}
