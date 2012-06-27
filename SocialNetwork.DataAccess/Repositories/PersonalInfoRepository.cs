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
        public static IEnumerable<PersonalInfo> GetUserInfo(Guid userID)
        {
            IEnumerable<PersonalInfo> recordList = null;
            using (SocialNetworkDBEntities record = new SocialNetworkDBEntities())
            {
                //userInfo = record.PersonalInfoes
                //    .FirstOrDefault(x => x.UserID.Equals(userID));
                recordList = record.PersonalInfoes
                    .Where(x => x.UserID.Equals(userID))
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
                        }).ToList();
            }
            return recordList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="imagePath"></param>
        public static void UpdAvatar(Guid userID, String imagePath)
        {
            using (SocialNetworkDBEntities record = new SocialNetworkDBEntities())
            {
                record.spUpdAvatar(userID, imagePath);
                record.SaveChanges();
            }
        }
    }
}