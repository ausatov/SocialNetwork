// -----------------------------------------------------------------------
// <copyright file="StatusRepository.cs" company="RusWizards">
// Author: Mankevich M.V. 
// Date: 29.06.12
// </copyright>
// -----------------------------------------------------------------------

namespace SocialNetwork.DataAccess.Repositories
{
    #region Using
    using SocialNetwork.DataAccess.Entity;
    using SocialNetwork.DataAccess.Enums;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    #endregion

    /// <summary>
    /// Work with dbo.Status.
    /// </summary>
    public static class StatusRepository
    {
        #region Constants
        /// <summary>
        /// Default user status.
        /// </summary>
        private const UserStatus _defaultStatus = UserStatus.Online;

        /// <summary>
        /// Default empty status message.
        /// </summary>
        private const String _defaultStatusMessage = "";
        #endregion

        #region Public methods
        /// <summary>
        /// Get current status message.
        /// </summary>
        /// <param name="userID">User identifier.</param>
        /// <returns>String with status message.</returns>
        public static String GetStatusMessage(Guid userID)
        {
            String status = String.Empty;
            String statusMessage = String.Empty;
            using (SocialNetworkDBEntities record = new SocialNetworkDBEntities())
            {
                var rawStatusMessage = record.Status
                    .FirstOrDefault(f => f.UserID.Equals(userID) && f.IsSet && !f.IsDeleted);
                statusMessage = (rawStatusMessage == null) ? _defaultStatusMessage : rawStatusMessage.StatusMessage;
            }
            return statusMessage;
        }

        /// <summary>
        /// Get current status identifier.
        /// </summary>
        /// <param name="userID">User identificator.</param>
        /// <returns>Status code.</returns>
        public static Int32 GetStatusID(Guid userID)
        {
            Int32 statusID = Int32.MinValue;
            using (SocialNetworkDBEntities record = new SocialNetworkDBEntities())
            {
                var rawStatus = record.Status.FirstOrDefault(f => f.UserID.Equals(userID) && f.IsSet && !f.IsDeleted);
                statusID = (rawStatus == null) ? (Int32)_defaultStatus : rawStatus.StatusID;
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
            }
        }
        #endregion
    }
}