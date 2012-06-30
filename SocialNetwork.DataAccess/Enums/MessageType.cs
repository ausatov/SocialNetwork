// -----------------------------------------------------------------------
// <copyright file="MessageType.cs" company="RusWizards">
// Author: Mankevich M.V. 
// Date: 29.06.12
// </copyright>
// -----------------------------------------------------------------------

namespace SocialNetwork.DataAccess.Enums
{
    /// <summary>
    /// Type of message.
    /// </summary>
    public enum MessageType
    {
        /// <summary>
        /// Posted to another user.
        /// </summary>
        Posted = 0,

        /// <summary>
        /// Received from another user.
        /// </summary>
        Received = 1
    }
}
