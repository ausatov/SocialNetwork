namespace SocialNetwork.DataAccess.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using SocialNetwork.DataAccess.Enums;

    /// <summary>
    /// 
    /// </summary>
    public static class StatusRepository// : IStatusRepository
    {
        /// <summary>
        /// Получить текущее установленное статус-сообщение
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public static String GetStatusMessage(Guid userID)
        {
            String status = String.Empty;
            String statusMessage = String.Empty;
            using (SocialNetworkDBEntities record = new SocialNetworkDBEntities())
            {
                statusMessage = record.Status
                    .Where(x => x.UserID == userID && x.IsSet && !x.IsDeleted)
                    .Select(x => x.StatusMessage)
                    .FirstOrDefault();
            }
            return statusMessage;
        }

        /// <summary>
        /// Получить текущий статус
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static String GetStatusName(Guid userID)
        {
            Int32 statusID = Int32.MinValue;
            using (SocialNetworkDBEntities record = new SocialNetworkDBEntities())
            {
                statusID = record.Status
                    .Where(x => x.UserID == userID && x.IsSet && !x.IsDeleted)
                    .Select(x => x.StatusID).FirstOrDefault();
            }
            return EnumsHelper.ToString((UserStatus)statusID);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static Int32 GetStatusID(Guid userID)
        {
            Int32 statusID = Int32.MinValue;
            using (SocialNetworkDBEntities record = new SocialNetworkDBEntities())
            {
                statusID = record.Status
                    .Where(x => x.UserID == userID && x.IsSet && !x.IsDeleted)
                    .Select(x => x.StatusID).FirstOrDefault();
            }
            return statusID;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="message"></param>
        /// <param name="status"></param>
        public static void SetStatusMessage(Guid userID, String message, UserStatus status)
        {
            using (SocialNetworkDBEntities record = new SocialNetworkDBEntities())
            {
                record.spInsStatus(userID, message, (Int32)status);
                record.SaveChanges(System.Data.Objects.SaveOptions.AcceptAllChangesAfterSave);
            }
        }
    }
}