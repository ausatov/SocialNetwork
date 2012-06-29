// -----------------------------------------------------------------------
// <copyright file="Country.cs" company="RusWizards">
// Author: Mankevich M.V. 
// Date: 29.06.12
// </copyright>
// -----------------------------------------------------------------------

namespace SocialNetwork.DataAccess.Entity
{
    #region Using
    using System;
    #endregion

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Country
    {
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
