// -----------------------------------------------------------------------
// <copyright file="FriendInvitationsRepository.cs" company="RusWizards">
// Author: Usatov A.B. 
// Date: 29.06.12
// </copyright>
// -----------------------------------------------------------------------

namespace SocialNetwork.DataAccess.Repositories
{
    #region Using
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    #endregion

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class FriendInvitationsRepository
    {
        #region Public methods
        /// <summary>
        /// Get friendlist of current user.
        /// </summary>
        /// <param name="userID">User identifier.</param>
        /// <returns>Friendlist of current user.</returns>
        public static IEnumerable<Entity.FriendInvitation> GetUserFriends(Guid userID)
        {
            IEnumerable<Entity.FriendInvitation> friendList = null;
            using (SocialNetworkDBEntities record = new SocialNetworkDBEntities())
            {
                friendList = record.FriendInvitations
                    .Where(w => w.ToID.Equals(userID) && !w.IsDeleted)
                    .Select(s => new Entity.FriendInvitation
                    {
                        InvitationID = s.InvitationID,
                        FromID = s.FromID,
                        ToID = s.ToID,
                        CreateDate = s.CreateDate,
                        Message = s.Message
                    }).ToList();
            }
            return friendList;
        }
        #endregion
    }
}
