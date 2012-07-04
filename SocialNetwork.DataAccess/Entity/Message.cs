// -----------------------------------------------------------------------
// <copyright file="Message.cs" company="RusWizards">
// Author: Mankevich M.V. 
// Date: 29.06.12
// </copyright>
// -----------------------------------------------------------------------

namespace SocialNetwork.DataAccess.Entity
{
    #region Using
    using System;
    #endregion

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Message
    {
        /// <summary>
        /// Gets or sets.
        /// </summary>
        public Guid MessageID { get; set; }

        /// <summary>
        /// Gets or sets.
        /// </summary>
        public Guid FromID { get; set; }

        /// <summary>
        /// Gets or sets.
        /// </summary>
        public Guid ToID { get; set; }

        /// <summary>
        /// Gets or sets.
        /// </summary>
        public DateTime SendDate { get; set; }

        /// <summary>
        /// Gets or sets.
        /// </summary>
        public String Header { get; set; }

        /// <summary>
        /// Gets or sets.
        /// </summary>
        public String Text { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether.
        /// </summary>
        public Boolean Status { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether.
        /// </summary>
        public Boolean IsDeletedBySender { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether.
        /// </summary>
        public Boolean IsDeletedByReceiver { get; set; }
    }
}