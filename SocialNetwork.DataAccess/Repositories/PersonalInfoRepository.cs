namespace SocialNetwork.DataAccess.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Work with dbo.PersonalInfo.
    /// </summary>
    public static class PersonalInfoRepository
    {
        /// <summary>
        /// Get object of PersonalInfo.
        /// </summary>
        /// <param name="userID">Current user identificator.</param>
        /// <returns>PersonalInfo item.</returns>
        public static PersonalInfo GetUserInfo(Guid userID)
        {
            PersonalInfo userInfo = new PersonalInfo();
            using (SocialNetworkDBEntities record = new SocialNetworkDBEntities())
            {
                userInfo = record.PersonalInfoes
                    .FirstOrDefault(x => x.UserID.Equals(userID));
            }
            return userInfo;
        }
    }
}