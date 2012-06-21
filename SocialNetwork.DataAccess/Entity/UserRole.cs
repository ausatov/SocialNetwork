// -----------------------------------------------------------------------
// <copyright file="UserRole.cs" company="RusWizards">
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
    public class UserRole
    {
        /// <summary>
        /// Gets or sets.
        /// </summary>
        public Guid UserRoleID { get; set; }

        /// <summary>
        /// Gets or sets.
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// Gets or sets.
        /// </summary>
        public Int16 PrivelegeMask { get; set; }
    }
}
