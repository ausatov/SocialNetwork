﻿// -----------------------------------------------------------------------
// <copyright file="PersonalInfo.cs" company="RusWizards">
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
    public class PersonalInfo
    {
        /// <summary>
        /// Gets or sets.
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// Gets or sets.
        /// </summary>
        public Guid UserID { get; set; }

        /// <summary>
        /// Gets or sets.
        /// </summary>
        public String NickName { get; set; }

        /// <summary>
        /// Gets or sets.
        /// </summary>
        public String FirstName { get; set; }

        /// <summary>
        /// Gets or sets.
        /// </summary>
        public String LastName { get; set; }

        /// <summary>
        /// Gets or sets.
        /// </summary>
        public String MiddleName { get; set; }

        /// <summary>
        /// Gets or sets.
        /// </summary>
        public Int16? Sex { get; set; }

        /// <summary>
        /// Gets or sets.
        /// </summary>
        public String Phone { get; set; }

        /// <summary>
        /// Gets or sets.
        /// </summary>
        public DateTime? Birthday { get; set; }

        /// <summary>
        /// Gets or sets.
        /// </summary>
        public String ImagePath { get; set; }

        /// <summary>
        /// Gets or sets.
        /// </summary>
        public String Description { get; set; }
    }
}
