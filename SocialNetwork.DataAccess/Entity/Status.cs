// -----------------------------------------------------------------------
// <copyright file="Status.cs" company="RusWizards">
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
    public class Status
    {
        /// <summary>
        /// Gets or sets.
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// Gets or sets.
        /// </summary>
        public Int32 StatusID { get; set; }

        /// <summary>
        /// Gets or sets.
        /// </summary>
        public Guid UserID { get; set; }

        /// <summary>
        /// Gets or sets.
        /// </summary>
        public String StatusMessage { get; set; }

        /// <summary>
        /// Gets or sets.
        /// </summary>
        public DateTime SetTime { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether.
        /// </summary>
        public Boolean IsSet { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether.
        /// </summary>
        public Boolean IsDeleted { get; set; }
    }
}
