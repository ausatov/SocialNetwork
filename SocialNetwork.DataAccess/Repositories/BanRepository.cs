// -----------------------------------------------------------------------
// <copyright file="BanRepository.cs" company="RusWizards">
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
    public static class BanRepository
    {
        #region Public methods
        /// <summary>
        /// Get active bans from current user.
        /// </summary>
        /// <param name="userID">User identifier.</param>
        /// <returns>List of user active bans.</returns>
        public static IEnumerable<Ban> GetUserBans(Guid userID)
        {
            IEnumerable<Ban> ban = null;
            using (SocialNetworkDBEntities record = new SocialNetworkDBEntities())
            {
                ban = record.Bans
                    .Where(w => w.UserID.Equals(userID) && !w.IsDeleted)
                    .Select(s => new Ban
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
        /// Get information about current ban.
        /// </summary>
        /// <param name="banID">Ban identifier.</param>
        /// <returns>Ban object.</returns>
        public static Ban GetBanInfo(Guid banID)
        {
            Entity.Ban banInfo;
            using (SocialNetworkDBEntities record = new SocialNetworkDBEntities())
            {
                var rawBanInfo = record.Bans
                   .Select(s => new Ban
                   {
                       ID = s.ID,
                       UserID = s.UserID,
                       AdminID = s.AdminID,
                       FromDate = s.FromDate,
                       ToDate = s.ToDate,
                       Reason = s.Reason
                   }).FirstOrDefault(f => f.ID.Equals(banID));
                banInfo = (rawBanInfo == null) ? null : rawBanInfo;
            }
            return banInfo;
        }

        /// <summary>
        /// Modify current ban.
        /// </summary>
        /// <param name="banID">Ban identifier.</param>
        /// <param name="banReason">Ban reason.</param>
        /// <param name="toDate">Ban to date.</param>
        public static void UpdateBan(Guid banID, String banReason, DateTime toDate)
        {
            using (SocialNetworkDBEntities record = new SocialNetworkDBEntities())
            {
                record.spUpdBan(banID, banReason, toDate);
            }
        }

        /// <summary>
        /// Remove current ban.
        /// </summary>
        /// <param name="banID">Ban identifier.</param>
        public static void DeleteBan(Guid banID)
        {
            using (SocialNetworkDBEntities record = new SocialNetworkDBEntities())
            {
                record.spDelBan(banID);
            }
        }
        #endregion
    }
}
