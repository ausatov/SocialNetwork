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
                    .Where(w => w.UserID == userID && !w.IsDeleted)
                    .Select(s => new Friend
                        {
                            ID = s.ID,
                            UserID = s.UserID,
                            FriendID = s.FriendID,
                            FriendshipDate = s.FriendshipDate,
                            IsDeleted = s.IsDeleted
                        })
                    .ToList();
            }

            return recordList;
        }

        public static List<KeyValuePair<Guid, String>> GetFriendlist(Guid userID)
        {
            List<KeyValuePair<Guid, String>> recordList = null;
            using (SocialNetworkDBEntities record = new SocialNetworkDBEntities())
            {
                var rawRecord = GetUserFriends(userID);

                recordList = rawRecord
                    .Where(w => w.UserID == userID && !w.IsDeleted)
                    .Select(s => new KeyValuePair<Guid, String>(
                            s.FriendID,
                            record.PersonalInfoes
                                .Where(w => w.UserID == s.FriendID && !w.User.IsDeleted)
                                .Select(ss => ss.FirstName + " " + ss.LastName)
                                .FirstOrDefault()))
                    .ToList();

            }

            return recordList;
        }
        #endregion
    }
}

/*
.Where(w => w.UserID == userID && !w.IsDeleted)
                    .Select(s => new KeyValuePair<Guid, String>(
                            s.FriendID,
                            record.PersonalInfoes
                                .Where(w => w.UserID == userID && !w.User.IsDeleted)
                                .Select(ss => String.Join(" ", ss.FirstName, ss.LastName))
                                .FirstOrDefault().ToString()))
                    .ToList();
*/