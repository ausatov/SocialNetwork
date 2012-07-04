// -----------------------------------------------------------------------
// <copyright file="PersonalInfoRepository.cs" company="RusWizards">
// Author: Mankevich M.V. 
// Date: 29.06.12
// </copyright>
// -----------------------------------------------------------------------

namespace SocialNetwork.DataAccess.Repositories
{
    #region Using.
    using SocialNetwork.DataAccess.Entity;
    using SocialNetwork.DataAccess.Enums;
    using System;
    using System.Collections.Generic;
    using System.Data.Objects;
    using System.Linq;
    using System.Text;
    #endregion

    /// <summary>
    /// Work with dbo.PersonalInfo.
    /// </summary>
    public static class PersonalInfoRepository
    {
        #region Public methods
        /// <summary>
        /// Get object of PersonalInfo.
        /// </summary>
        /// <param name="userID">Current user identificator.</param>
        /// <returns>PersonalInfo item.</returns>
        public static PersonalInfo GetUserInfo(Guid userID)
        {
            PersonalInfo personalInfo = null;
            using (SocialNetworkDBEntities record = new SocialNetworkDBEntities())
            {
                personalInfo = record.PersonalInfoes
                    .Select(s => new PersonalInfo
                        {
                            ID = s.ID,
                            UserID = s.UserID,
                            NickName = s.NickName,
                            FirstName = s.FirstName,
                            LastName = s.LastName,
                            MiddleName = s.MiddleName,
                            Sex = s.Sex,
                            Phone = s.Phone,
                            Birthday = s.Birthday,
                            ImagePath = s.ImagePath,
                            Description = s.Description
                        })
                    .FirstOrDefault(f => f.UserID.Equals(userID));
            }

            return personalInfo;
        }

        /// <summary>
        /// Modify path to user avatar image.
        /// </summary>
        /// <param name="userID">User identifier.</param>
        /// <param name="imagePath">Image path.</param>
        public static void ModifyAvatar(Guid userID, String imagePath)
        {
            ModifyPersonalInfo(
                null,
                true,
                true,
                userID,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                imagePath,
                null);
        }

        /// <summary>
        /// Modify PersonalInfo.
        /// </summary>
        /// <param name="id">Primary key identity.</param>
        /// <param name="updateByUser">Update type flag.</param>
        /// <param name="updateImagePath">Update image flag.</param>
        /// <param name="userID">Foreign key identity.</param>
        /// <param name="nickName">Nick name.</param>
        /// <param name="firstName">First name.</param>
        /// <param name="lastName">Last name.</param>
        /// <param name="middleName">Middle name.</param>
        /// <param name="sex">User sex.</param>
        /// <param name="phone">User phone.</param>
        /// <param name="birthday">Birthday date.</param>
        /// <param name="imagePath">Avatar image path.</param>
        /// <param name="description">Personal description.</param>
        public static void ModifyPersonalInfo(
            Guid? id,
            Boolean updateByUser,
            Boolean updateImagePath,
            Guid userID,
            String nickName,
            String firstName,
            String lastName,
            String middleName,
            Sex? sex,
            String phone,
            DateTime? birthday,
            String imagePath,
            String description)
        {
            ObjectParameter pkID = (id != null) ? new ObjectParameter("pkID", id) 
                : new ObjectParameter("pkID", typeof(Guid));

            using (SocialNetworkDBEntities record = new SocialNetworkDBEntities())
            {
                record.spPersonalInfo(
                    pkID,
                    updateByUser,
                    updateImagePath,
                    userID,
                    nickName,
                    firstName,
                    lastName,
                    middleName,
                    (Byte?)sex,
                    phone,
                    birthday,
                    imagePath,
                    description);
            }
        }
        #endregion
    }
}