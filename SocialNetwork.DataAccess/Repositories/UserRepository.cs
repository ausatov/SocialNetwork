namespace SocialNetwork.DataAccess.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// 
    /// </summary>
    public static class UserRepository
    {
        /// <summary>
        /// Get user info.
        /// </summary>
        /// <param name="userID">User id.</param>
        /// <returns></returns>
        public static Entity.User GetUserInfo(Guid userID)
        {
            Entity.User userInfo;
            using (SocialNetworkDBEntities record = new SocialNetworkDBEntities())
            {
                 userInfo = record.Users
                    .Where(x => x.UserID.Equals(userID))
                    .Select(x => new Entity.User
                    {
                        UserID = x.UserID,

                    }).FirstOrDefault();
            }
            return userInfo;
        }
        
        /// <summary>
        /// Get user id.
        /// </summary>
        /// <param name="userID">User id.</param>
        /// <returns></returns>
        public static Guid GetUserID(String userEmail, String userPassword)
        {
            Guid userId = Guid.Empty;
            using (SocialNetworkDBEntities record = new SocialNetworkDBEntities())
            {
                 userId = record.Users
                .Where(x => x.Email.Equals(userEmail) && x.Password.Equals(userPassword))
                .Select(x => x.UserID)
                .FirstOrDefault();
            }
            return userId;
        }

    }
}