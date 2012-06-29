// -----------------------------------------------------------------------
// <copyright file="Ban.cs" company="RusWizards">
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
    public class Ban
    {
        /// <summary>
        /// Gets or sets.
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// Gets or sets.
        /// </summary>
        public Guid UserID { get; set; }

        /// <summary>
        /// Gets or sets.
        /// </summary>
        public Guid AdminID { get; set; }

        /// <summary>
        /// Gets or sets.
        /// </summary>
        public String Reason { get; set; }

        /// <summary>
        /// Gets or sets.
        /// </summary>
        public DateTime FromDate { get; set; }

        /// <summary>
        /// Gets or sets.
        /// </summary>
        public DateTime ToDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether.
        /// </summary>
        public Boolean IsDeleted { get; set; }
    }
}