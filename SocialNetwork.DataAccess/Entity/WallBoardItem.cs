// -----------------------------------------------------------------------
// <copyright file="WallBoardItem.cs" company="RusWizards">
// Author: Mankevich M.V. 
// Date: 29.06.12
// </copyright>
// -----------------------------------------------------------------------

namespace SocialNetwork.DataAccess.Entity
{
    #region Using
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    #endregion

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class WallBoardItem
    {
        /// <summary>
        /// Gets or sets.
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// Gets or sets.
        /// </summary>
        public Int32 ContentTypeID { get; set; }

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
        public String Message { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether.
        /// </summary>
        public Boolean IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets.
        /// </summary>
        public Int32? NullLink { get; set; } 
    }
}
