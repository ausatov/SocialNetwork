// -----------------------------------------------------------------------
// <copyright file="EnumsHelper.cs" company="RusWizards">
// Author: Mankevich M.V. 
// Date: 29.06.12
// </copyright>
// -----------------------------------------------------------------------

namespace SocialNetwork.DataAccess.Enums
{
    #region Using
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    #endregion

    /// <summary>
    /// Help to work with Enums.
    /// </summary>
    public static class EnumsHelper
    {
        #region ToString(Enum)
        /// <summary>
        /// Convert UserStatus to string status name.
        /// </summary>
        /// <param name="status">Enum UserStatus.</param>
        /// <returns>Status name.</returns>
        public static String ToString(UserStatus status)
        {
            String statusName = String.Empty;
            switch (status)
            {
                case UserStatus.Offline:
                    {
                        statusName = "offline";
                    } 
                    break;
                case UserStatus.Online:
                    {
                        statusName = "online";
                    } 
                    break;
                case UserStatus.AtHome:
                    {
                        statusName = "at home";
                    } 
                    break;
                case UserStatus.AtWork:
                    {
                        statusName = "at work";
                    } 
                    break;
                case UserStatus.Busy:
                    {
                        statusName = "busy";
                    } 
                    break;
            }
            return statusName;
        }

        /// <summary>
        /// Convert Enum Sex to string sex name.
        /// </summary>
        /// <param name="sex">Enum Sex.</param>
        /// <returns>Sex name.</returns>
        public static String ToString(Sex sex)
        {
            String sexName = String.Empty;
            switch (sex)
            {
                case Sex.Male:
                    {
                        sexName = "male";
                    } 
                    break;
                case Sex.Female:
                    {
                        sexName = "female";
                    } 
                    break;
            }
            return sexName;
        }
        #endregion
    }
}
