// -----------------------------------------------------------------------
// <copyright file="BanRepository.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace SocialNetwork.DataAccess.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public static class BanRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userID">User id.</param>
        /// <returns></returns>
        public static IEnumerable<Entity.Ban> GetUserBans(Guid userID)
        {
            IEnumerable<Entity.Ban> ban = null;
            using (SocialNetworkDBEntities record = new SocialNetworkDBEntities())
            {
                ban = record.Bans
                    .Where(x => x.UserID.Equals(userID) && !x.IsDeleted)
                    .Select(x => new Entity.Ban
                    {
                        ID = x.ID,
                        UserID = x.UserID,
                        AdminID = x.AdminID,
                        FromDate = x.FromDate,
                        ToDate = x.ToDate,
                        Reason = x.Reason
                    }).ToList();
            }
            return ban;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="banID">Ban id.</param>
        /// <returns></returns>
        public static Entity.Ban GetBanInfo(Guid banID)
        {
            Entity.Ban userInfo;
            using (SocialNetworkDBEntities record = new SocialNetworkDBEntities())
            {
                userInfo = record.Bans
                   .Where(x => x.ID.Equals(banID))
                   .Select(x => new Entity.Ban
                   {
                       UserID = x.UserID

                   }).FirstOrDefault();
            }
            return userInfo;
        }
    }
}
