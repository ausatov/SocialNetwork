// -----------------------------------------------------------------------
// <copyright file="FriendRepository.cs" company="">
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
    public class FriendRepository
    {
        public static IEnumerable<Entity.Friend> GetUserFriends(Guid userID)
        {
            IEnumerable<Entity.Friend> friendList = null;
            using (SocialNetworkDBEntities record = new SocialNetworkDBEntities())
            {
                friendList = record.Bans
                    .Where(x => x.UserID.Equals(userID) && !x.IsDeleted)
                    .Select(x => new Entity.Friend
                    {
                        ID = x.ID,
                        UserID = x.UserID,
                       
                    }).ToList();
            }
            return friendList;
        }
    }
}
