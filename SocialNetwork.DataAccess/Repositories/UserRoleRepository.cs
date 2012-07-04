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
    /// TODO: Update summary.
    /// </summary>
    public class UserRoleRepository
    {
        #region Public methods

        public static IEnumerable<SocialNetwork.DataAccess.UserRole> GetUserRoles(Guid userID)
        {
            IEnumerable<SocialNetwork.DataAccess.UserRole> userRoleList = null;
            using (SocialNetworkDBEntities record = new SocialNetworkDBEntities())
            {
                userRoleList = record.spRoleGet(userID).ToList();
            }
            return userRoleList;
        }

        /// <summary>
        /// Ger needled role info by role ID.
        /// </summary>
        /// <param name="roleID"></param>
        /// <returns></returns>
        public static UserRole GetRoleInfo(Guid roleID)
        {
            Entity.UserRole rawRoleInfo;
            using (SocialNetworkDBEntities record = new SocialNetworkDBEntities())
            {
                rawRoleInfo = record.UserRoles
                  .Select(s => new UserRole
                  {
                      UserRoleID = s.UserRoleID,
                      Name = s.Name,
                      PrivelegeMask = s.PrivilegeMask
                  })
                  .FirstOrDefault(f => f.UserRoleID.Equals(roleID));
                
            }
            return rawRoleInfo;
        }

        /// <summary>
        /// Get all user roles on server.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Entity.UserRole> GetAllRoles()
        {
            IEnumerable<Entity.UserRole> userRoleList = null;
            using (SocialNetworkDBEntities record = new SocialNetworkDBEntities())
            {
                userRoleList = record.UserRoles
                    .Select(s => new Entity.UserRole
                    {
                        UserRoleID = s.UserRoleID,
                        Name = s.Name,
                        PrivelegeMask = s.PrivilegeMask
                    })
                    .ToList();
            }
            return userRoleList;
        }

        /// <summary>
        /// Get min privelege mask of role.
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static Nullable<Int16> GetMinPrivelegeMask(Guid userID)
        {
            System.Nullable<Int16> privelegeMask = 0;
            using (SocialNetworkDBEntities record = new SocialNetworkDBEntities())
            {
                privelegeMask = record.spRoleGet(userID).Min(m => m.PrivilegeMask);
            }
            return privelegeMask;
        }

        /// <summary>
        /// Get max privelege mask of role.
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static Nullable<Int16> GetMaxPrivelegeMask(Guid userID)
        {
            System.Nullable<Int16> privelegeMask = 0;
            using (SocialNetworkDBEntities record = new SocialNetworkDBEntities())
            {
                privelegeMask = record.spRoleGet(userID).Max(m => m.PrivilegeMask);
            }
            return privelegeMask;
        }
        #endregion
    }
}