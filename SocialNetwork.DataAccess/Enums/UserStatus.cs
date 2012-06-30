// -----------------------------------------------------------------------
// <copyright file="UserStatus.cs" company="RusWizards">
// Author: Mankevich M.V. 
// Date: 29.06.12
// </copyright>
// -----------------------------------------------------------------------

namespace SocialNetwork.DataAccess.Enums
{
    /// <summary>
    /// User status.
    /// </summary>
    public enum UserStatus
    {
        /// <summary>
        /// User offline.
        /// </summary>
        Offline = 0,

        /// <summary>
        /// User online.
        /// </summary>
        Online = 1,

        /// <summary>
        /// User at home.
        /// </summary>
        AtHome = 2,

        /// <summary>
        /// User at work.
        /// </summary>
        AtWork = 3,

        /// <summary>
        /// User busy.
        /// </summary>
        Busy = 4
    }
}
