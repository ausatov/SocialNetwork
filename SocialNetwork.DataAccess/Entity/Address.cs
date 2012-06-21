// -----------------------------------------------------------------------
// <copyright file="Address.cs" company="RusWizards">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace SocialNetwork.DataAccess.Entity
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Address
    {
        /// <summary>
        /// Gets or sets.
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// Gets or sets.
        /// </summary>
        public Guid? UserInfoID { get; set; }

        /// <summary>
        /// Gets or sets.
        /// </summary>
        public Guid? CountryID { get; set; }

        /// <summary>
        /// Gets or sets.
        /// </summary>
        public Guid? CityID { get; set; }

        /// <summary>
        /// Gets or sets.
        /// </summary>
        public String Area { get; set; }

        /// <summary>
        /// Gets or sets.
        /// </summary>
        public String Street { get; set; }

        /// <summary>
        /// Gets or sets.
        /// </summary>
        public String Home { get; set; }

        /// <summary>
        /// Gets or sets.
        /// </summary>
        public String Apartment { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether.
        /// </summary>
        public Boolean IsDeleted { get; set; }
    }
}
