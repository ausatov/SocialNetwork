// -----------------------------------------------------------------------
// <copyright file="FriendInvitationsRepository.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace SocialNetwork.DataAccess.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class FriendInvitationsRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userID">User id.</param>
        /// <returns></returns>
        public static IEnumerable<Entity.FriendInvitation> GetUserFriends(Guid userID)
        {
            IEnumerable<Entity.FriendInvitation> friendList = null;
            using (SocialNetworkDBEntities record = new SocialNetworkDBEntities())
            {
                friendList = record.FriendInvitations
                    .Where(x => x.ToID.Equals(userID) && !x.IsDeleted)
                    .Select(x => new Entity.FriendInvitation
                    {
                        InvitationID = x.InvitationID,
                        FromID = x.FromID,
                        ToID= x.ToID,
                        CreateDate=x.CreateDate,
                        Message=x.Message


                    }).ToList();
            }
            return friendList;
        }
    }
}
