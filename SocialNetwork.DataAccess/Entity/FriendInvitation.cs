// -----------------------------------------------------------------------
// <copyright file="FriendInvitation.cs" company="RusWizards">
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
    public class FriendInvitation
    {
        /// <summary>
        /// Gets or sets.
        /// </summary>
        public Guid InvitationID { get; set; }

        /// <summary>
        /// Gets or sets.
        /// </summary>
        public Guid FromID { get; set; }

        /// <summary>
        /// Gets or sets.
        /// </summary>
        public Guid ToID { get; set; }

        /// <summary>
        /// Gets or sets.
        /// </summary>
        public String Message { get; set; }

        /// <summary>
        /// Gets or sets.
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether.
        /// </summary>
        public Boolean IsDeleted { get; set; }
    }
}