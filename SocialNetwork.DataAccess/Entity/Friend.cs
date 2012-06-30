// -----------------------------------------------------------------------
// <copyright file="Friend.cs" company="RusWizards">
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
    public class Friend
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
        public Guid FriendID { get; set; }

        /// <summary>
        /// Gets or sets.
        /// </summary>
        public DateTime FriendshipDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether.
        /// </summary>
        public Boolean IsDeleted { get; set; }
    }
}
