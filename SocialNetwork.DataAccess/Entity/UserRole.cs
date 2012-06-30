// -----------------------------------------------------------------------
// <copyright file="UserRole.cs" company="RusWizards">
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
    public class UserRole
    {
        /// <summary>
        /// Gets or sets.
        /// </summary>
        public Guid UserRoleID { get; set; }

        /// <summary>
        /// Gets or sets.
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// Gets or sets.
        /// </summary>
        public Int16 PrivelegeMask { get; set; }
    }
}
