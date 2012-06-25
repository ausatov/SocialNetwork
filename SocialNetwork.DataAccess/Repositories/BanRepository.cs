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
                    .Where(w => w.UserID.Equals(userID) && !w.IsDeleted)
                    .Select(s => new Entity.Ban
                    {
                        ID = s.ID,
                        UserID = s.UserID,
                        AdminID = s.AdminID,
                        FromDate = s.FromDate,
                        ToDate = s.ToDate,
                        Reason = s.Reason
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
            Entity.Ban banInfo;
            using (SocialNetworkDBEntities record = new SocialNetworkDBEntities())
            {

                banInfo = record.Bans
                   .Where(w => w.ID.Equals(banID))
                   .Select(s => new Entity.Ban
                   {
                       ID = s.ID,
                       UserID = s.UserID,
                       AdminID = s.AdminID,
                       FromDate = s.FromDate,
                       ToDate = s.ToDate,
                       Reason = s.Reason

                   }).FirstOrDefault();
            }
            return banInfo;
        }
        public static void UpdateBan(String banReason, DateTime toDate, Guid banId)
        {
            using (SocialNetworkDBEntities record = new SocialNetworkDBEntities())
            {
                record.spUpdBan(banId, banReason, toDate);
                record.SaveChanges();
            }
        }

        public static void DeleteBan(Guid banId)
        {
            using (SocialNetworkDBEntities record = new SocialNetworkDBEntities())
            {
                record.spDelBan(banId);
                record.SaveChanges();

            }
        }
    }
}
