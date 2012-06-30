// -----------------------------------------------------------------------
// <copyright file="MessageSelectType.cs" company="RusWizards">
// Author: Mankevich M.V. 
// Date: 29.06.12
// </copyright>
// -----------------------------------------------------------------------

namespace SocialNetwork.DataAccess.Enums
{
    /// <summary>
    /// Type of select filter.
    /// </summary>
    public enum MessageSelectType
    {
        /// <summary>
        /// All items.
        /// </summary>
        All = 0,

        /// <summary>
        /// Only new messages.
        /// </summary>
        OnlyNew = 1,

        /// <summary>
        /// Only old messages.
        /// </summary>
        OnlyOld = 2
    }
}
