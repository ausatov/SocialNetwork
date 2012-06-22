namespace SocialNetwork.DataAccess.Repositories
{
    using SocialNetwork.DataAccess.Entity;
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
            PersonalInfo userInfo;// = new PersonalInfo();
            using (SocialNetworkDBEntities record = new SocialNetworkDBEntities())
            {
                //userInfo = record.PersonalInfoes
                //    .FirstOrDefault(x => x.UserID.Equals(userID));
                userInfo = record.PersonalInfoes
                    .Where(x=> x.UserID.Equals(userID))
                    .Select(x => new PersonalInfo
                        {
                            ID = x.ID,
                            UserID = x.UserID,
                            NickName = x.NickName,
                            FirstName = x.FirstName,
                            LastName = x.LastName,
                            MiddleName = x.MiddleName,
                            Sex = x.Sex,
                            Phone = x.Phone,
                            Birthday = x.Birthday,
                            ImagePath = x.ImagePath,
                            Description = x.Description
                        }).FirstOrDefault();
            }
            return userInfo;
        }
    }
}