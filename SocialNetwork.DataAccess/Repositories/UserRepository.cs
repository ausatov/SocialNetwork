// -----------------------------------------------------------------------
// <copyright file="UserRepository.cs" company="RusWizards">
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
    /// 
    /// </summary>
    public static class UserRepository
    {
        #region Public methods
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<User> GetAllUsers()
        {
            IEnumerable<User> userList = null;
            using (
            SocialNetworkDBEntities record = new SocialNetworkDBEntities()
                )
            {
                userList = record.Users
                    .Select(s => new User
                    {
                        UserID = s.UserID,
                        Email = s.Email,
                        Password = s.Password
                    }).ToList();
            }

            return userList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userEmail"></param>
        /// <param name="userPassword"></param>
        /// <returns></returns>
        public static Guid GetUserID(String userEmail)
        {
            Guid userID = Guid.Empty;
            using (SocialNetworkDBEntities record = new SocialNetworkDBEntities())
            {
                var rawUserID = record.Users
                    .FirstOrDefault(f => f.Email.Equals(userEmail));

                userID = (rawUserID == null) ? Guid.Empty : rawUserID.UserID;
            }
            return userID;
        }

        /// <summary>
        /// Get user id from email & password.
        /// </summary>
        /// <param name="userEmail">User's email.</param>
        /// <param name="userPassword">User's password.</param>
        /// <returns>User identifier.</returns>
        public static Guid GetUserID(String userEmail, String userPassword)
        {
            Guid userID = Guid.Empty;
            using (SocialNetworkDBEntities record = new SocialNetworkDBEntities())
            {
                var rawUserID = record.Users
                    .FirstOrDefault(f => f.Email.Equals(userEmail) && f.Password.Equals(userPassword));
                userID = (rawUserID == null) ? Guid.Empty : rawUserID.UserID;
            }
            return userID;
        }

        /// <summary>
        /// Get user info.
        /// </summary>
        /// <param name="userID">User id.</param>
        /// <returns>User object.</returns>
        public static User GetUserInfo(Guid userID)
        {
            Entity.User rawUserInfo;
            using (SocialNetworkDBEntities record = new SocialNetworkDBEntities())
            {
                rawUserInfo = record.Users

                  .Select(s => new User
                  {
                      UserID = s.UserID,
                      Password = s.Password,
                      Email = s.Email
                  })
                  .FirstOrDefault(f => f.UserID.Equals(userID));
                //UserInfo = (rawUserInfo == null) ? null : rawUserInfo;
            }
            return rawUserInfo;
        }
        #endregion
    }
}