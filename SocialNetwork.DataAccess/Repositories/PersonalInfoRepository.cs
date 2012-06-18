namespace SocialNetwork.DataAccess.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// 
    /// </summary>
    public static class PersonalInfoRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static PersonalInfo GetUserInfo(Guid userID)
        {
            PersonalInfo userInfo = new PersonalInfo();
            using (SocialNetworkDBEntities record = new SocialNetworkDBEntities())
            {
                userInfo = record.PersonalInfoes
                    .Where(x => x.UserID == userID)
                    .FirstOrDefault();
            }
            return userInfo;
        }
    }
}