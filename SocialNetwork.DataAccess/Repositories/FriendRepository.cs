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
    using SocialNetwork.DataAccess.Repositories;
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
        /// Get user's friends with needled params.
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="startRow"></param>
        /// <param name="rowCount"></param>
        /// <returns></returns>
        public static IEnumerable<Friend> GetUserFriendsWithParams(Guid userID, Int32 startRow, Int32 rowCount, bool online)
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
                    }).OrderBy(o => o.FriendshipDate).Skip(startRow).Take(rowCount).ToList();
            }
            if (!online)
                return recordList;
            else return recordList.Where(w => StatusRepository.GetStatusID(w.FriendID) != 0);
        }
        /// <summary>
        /// Ger all friends of current user.
        /// </summary>
        /// <param name="userID">User's identeficator.</param>
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
            return recordList;
        }
        #endregion
    }
}
