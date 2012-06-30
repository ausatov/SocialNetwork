// -----------------------------------------------------------------------
// <copyright file="UserXUserRoles.cs" company="RusWizards">
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
    public class UserXUserRoles
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
        public Guid UserRoleID { get; set; }
    }
}