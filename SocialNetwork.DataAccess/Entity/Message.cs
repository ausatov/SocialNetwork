// -----------------------------------------------------------------------
// <copyright file="Message.cs" company="RusWizards">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace SocialNetwork.DataAccess.Entity
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

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
        public Boolean IsDeleted { get; set; }
    }
}