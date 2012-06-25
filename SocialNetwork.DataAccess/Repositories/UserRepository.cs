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
        public static IEnumerable<Entity.User> GetAllUsers()
        {
            IEnumerable<Entity.User> userList = null;
            using (SocialNetworkDBEntities record = new SocialNetworkDBEntities())
            {
                userList = record.Users
                   
                    .Select(x => new Entity.User
                    {
                        UserID = x.UserID,
                        Email=x.Email,
                        Password=x.Password
                 
                       
                    }).ToList();
            }
            return userList;
        }

        }

               /// <summary>
        /// 
        /// </summary>
        /// <param name="userEmail"></param>
        /// <param name="userPassword"></param>
        /// <returns></returns>
        public static Guid GetUserID(String userEmail)
        {
            Guid userId = Guid.Empty;
            using (SocialNetworkDBEntities record = new SocialNetworkDBEntities())
            {
                userId = record.Users
               .Where(x => x.Email.Equals(userEmail))
               .Select(x => x.UserID)
               .FirstOrDefault();
            }
            return userId;

    }
}