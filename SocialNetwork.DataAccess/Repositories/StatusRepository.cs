namespace SocialNetwork.DataAccess.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using SocialNetwork.DataAccess.Enums;

    /// <summary>
    /// Work with dbo.Status.
    /// </summary>
    public static class StatusRepository
    {
        /// <summary>
        /// Get current status message.
        /// </summary>
        /// <param name="userID">User identificator.</param>
        /// <returns>String with status message.</returns>
        public static String GetStatusMessage(Guid userID)
        {
            String status = String.Empty;
            String statusMessage = String.Empty;
            using (SocialNetworkDBEntities record = new SocialNetworkDBEntities())
            {
                statusMessage = record.Status
                    .Where(x => x.UserID.Equals(userID) && x.IsSet && !x.IsDeleted)
                    .Select(x => x.StatusMessage)
                    .FirstOrDefault();
            }
            return statusMessage;
        }

        /// <summary>
        /// Get current status name.
        /// </summary>
        /// <param name="userID">User identificator.</param>
        /// <returns>String with status name.</returns>
        public static String GetStatusName(Guid userID)
        {
            Int32 statusID = Int32.MinValue;
            using (SocialNetworkDBEntities record = new SocialNetworkDBEntities())
            {
                statusID = record.Status
                    .Where(x => x.UserID.Equals(userID) && x.IsSet && !x.IsDeleted)
                    .Select(x => x.StatusID)
                    .FirstOrDefault();
            }
            return EnumsHelper.ToString((UserStatus)statusID);
        }

        /// <summary>
        /// Get current status identificator.
        /// </summary>
        /// <param name="userID">User identificator.</param>
        /// <returns>Status code.</returns>
        public static Int32 GetStatusID(Guid userID)
        {
            Int32 statusID = Int32.MinValue;
            using (SocialNetworkDBEntities record = new SocialNetworkDBEntities())
            {
                statusID = record.Status
                    .Where(x => x.UserID.Equals(userID) && x.IsSet && !x.IsDeleted)
                    .Select(x => x.StatusID)
                    .FirstOrDefault();
            }
            return statusID;
        }

        /// <summary>
        /// Set new status message.
        /// </summary>
        /// <param name="userID">User identificator.</param>
        /// <param name="message">Message content.</param>
        /// <param name="status">Type of status.</param>
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