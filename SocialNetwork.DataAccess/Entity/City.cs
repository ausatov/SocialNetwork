// -----------------------------------------------------------------------
// <copyright file="City.cs" company="RusWizards">
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
    public class City
    {
        /// <summary>
        /// Gets or sets.
        /// </summary>
        public Guid CityID { get; set; }

        /// <summary>
        /// Gets or sets.
        /// </summary>
        public Guid CountryID { get; set; }

        /// <summary>
        /// Gets or sets.
        /// </summary>
        public String Name { get; set; }
    }
}
