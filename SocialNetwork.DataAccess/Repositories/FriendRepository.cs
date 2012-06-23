// -----------------------------------------------------------------------
// <copyright file="FriendRepository.cs" company="z53">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace SocialNetwork.DataAccess.Repositories
{
    using SocialNetwork.DataAccess.Entity;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public static class FriendRepository
    {
        public static IEnumerable<Friend> GetUserFriends(Guid UserID)
        {
            IEnumerable<Friend> recordList = null;
            using (SocialNetworkDBEntities record = new SocialNetworkDBEntities())
            {
                recordList = record.Friends
                    .Where(x => x.UserID == UserID && !x.IsDeleted)
                    .Select(x => new Friend
                        {
                            ID = x.ID,
                            UserID = x.UserID,
                            FriendID = x.FriendID,
                            FriendshipDate = x.FriendshipDate,
                            IsDeleted = x.IsDeleted
                        }).ToList();
            }
            return null;
        }
    }
}
