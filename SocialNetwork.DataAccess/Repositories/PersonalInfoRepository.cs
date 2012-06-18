using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using SocialNetwork.DataAccess;

namespace SocialNetwork.DataAccess.Repositories
{
    public class PersonalInfoRepository
    {
        private List<PersonalInfo> personalInfo = new List<PersonalInfo>();
        



        /// <summary>
        /// Получить полное имя пользователя по идентификатору
        /// </summary>
        /// <param name="UserID">Идентификатор пользователя</param>
        /// <returns>Полное имя пользователя</returns>
        public String GetFullUserName(Guid userID)
        {
            String fullUserName = String.Empty;
            using (SocialNetworkDBEntities entity = new SocialNetworkDBEntities())
            {
                fullUserName = entity.PersonalInfoes
                    .Where
                        (x => x.UserID == userID)
                    .Select 
                        (x => x.FirstName + " "
                        + x.MiddleName + " "
                        + x.LastName + " "
                        + x.NickName)
                        .FirstOrDefault();
            }
            return fullUserName;
        }

        public enum NameComponents
        {
            FirstName = 0,
            LastName = 1,
            MiddleName = 2,
            NickName = 3
        }


        public String GetUserName(Guid userID, params NameComponents[] nameComponents)
        {
            String fullUserName = String.Empty;
            using (SocialNetworkDBEntities entity = new SocialNetworkDBEntities())
            {
                
                fullUserName = entity.PersonalInfoes
                    .Where(x => x.UserID == userID)
                    .Select(x => x.FirstName + " "
                        + x.MiddleName + " "
                        + x.LastName + " "
                        + x.NickName)
                        .FirstOrDefault();
            }
            return fullUserName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public PersonalInfo GetUserInfoByID(Guid userID)
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
