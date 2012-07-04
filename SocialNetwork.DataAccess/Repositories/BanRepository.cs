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
    using System.Data.Objects;
    using System.Linq;
    using System.Text;
    #endregion

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public static class BanRepository
    {
        private const String _defaultBanName = "-- not select ban --";

        #region Public methods
        /// <summary>
        ///  Get count of all bans.
        /// </summary>
        /// <returns></returns>
        public static Int32 GetAllBansCount()
        {
            IEnumerable<Entity.Ban> ban = null;
            using (SocialNetworkDBEntities record = new SocialNetworkDBEntities())
            {
                ban = record.Bans
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
            return ban.Count();
        }

        /// <summary>
        /// Get all bans.
        /// </summary>
        /// <param name="startRowIndex">Select started row.</param>
        /// <param name="maximumRows">Select row count.</param>
        /// <returns></returns>
        public static IEnumerable<Entity.Ban> GetAllBans(Int32 startRowIndex, Int32 maximumRows)
        {
            IEnumerable<Entity.Ban> ban = null;
            using (SocialNetworkDBEntities record = new SocialNetworkDBEntities())
            {
                ban = record.Bans
                    .Where(w => !w.IsDeleted)
                    .Select(s => new Entity.Ban
                    {
                        ID = s.ID,
                        UserID = s.UserID,
                        AdminID = s.AdminID,
                        FromDate = s.FromDate,
                        ToDate = s.ToDate,
                        Reason = s.Reason
                    })
                    .OrderBy(o => o.FromDate)
                    .Skip(startRowIndex)
                    .Take(maximumRows)
                    .ToList();
            }
            return ban;
        }

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
                    })
                    .ToList();
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
            Entity.Ban rawBanInfo;
            using (SocialNetworkDBEntities record = new SocialNetworkDBEntities())
            {
                rawBanInfo = record.Bans
                  .Select(s => new Ban
                  {
                      ID = s.ID,
                      UserID = s.UserID,
                      AdminID = s.AdminID,
                      FromDate = s.FromDate,
                      ToDate = s.ToDate,
                      Reason = s.Reason
                  })
                  .FirstOrDefault(f => f.ID.Equals(banID));

            }
            return rawBanInfo;
        }

        /// <summary>
        /// Modify ban's object.
        /// </summary>
        /// <param name="id">Ban's identeficator.</param>
        /// <param name="updateByUser">Updated by user flag.</param>
        /// <param name="userID">User's identeficator.</param>
        /// <param name="adminID">Admin's identeficator.</param>
        /// <param name="reason">Ban's reason.</param>
        /// <param name="fromDate">Ban's from date.</param>
        /// <param name="toDate">Ban's to date.</param>
        /// <param name="isDeleted">Logical deleting.</param>
        public static void ModifyBan(
           Guid? id,
           Boolean? updateByUser,
           Guid? userID,
           Guid? adminID,
           String reason,
           DateTime? fromDate,
           DateTime? toDate,
           Boolean? isDeleted)
        {
            ObjectParameter pkID = (id != null) ? new ObjectParameter("pkID", id)
                : new ObjectParameter("pkID", typeof(Guid));
            using (SocialNetworkDBEntities record = new SocialNetworkDBEntities())
            {
                record.spBan(
                    pkID,
                    updateByUser,
                    userID,
                    adminID,
                    reason,
                    fromDate,
                    toDate,
                    isDeleted
                    );
            }
        }
        #endregion
    }
}
