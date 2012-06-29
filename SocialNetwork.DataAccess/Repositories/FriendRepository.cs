// -----------------------------------------------------------------------
// <copyright file="FriendRepository.cs" company="RusWizards">
// Author: Usatov A.B. 
// Date: 29.06.12
// </copyright>
// -----------------------------------------------------------------------

namespace SocialNetwork.DataAccess.Repositories
{
    #region Using
    using SocialNetwork.DataAccess.Entity;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    #endregion

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public static class FriendRepository
    {
        #region Public methods
        /// <summary>
        /// Update summary.
        /// </summary>
        /// <param name="userID">User ID.</param>
        /// <returns>Return result.</returns>
        public static IEnumerable<Friend> GetUserFriends(Guid userID)
        {
            IEnumerable<Friend> recordList = null;
            using (SocialNetworkDBEntities record = new SocialNetworkDBEntities())
            {
                recordList = record.Friends
                    .Where(w => w.UserID.Equals(userID) && !w.IsDeleted)
                    .Select(s => new Friend
                        {
                            ID = s.ID,
                            UserID = s.UserID,
                            FriendID = s.FriendID,
                            FriendshipDate = s.FriendshipDate,
                            IsDeleted = s.IsDeleted
                        }).ToList();
            }
            return null;
        }
        #endregion
    }
}
