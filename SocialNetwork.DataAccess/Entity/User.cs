// -----------------------------------------------------------------------
// <copyright file="User.cs" company="RusWizards">
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
    public class User
    {
        /// <summary>
        /// Gets or sets.
        /// </summary>
        public Guid UserID { get; set; }

        /// <summary>
        /// Gets or sets.
        /// </summary>
        public String Password { get; set; }
        
        /// <summary>
        /// Gets or sets.
        /// </summary>
        public String Email { get; set; }
        
        /// <summary>
        /// Gets or sets.
        /// </summary>
        public Int32? SecretQuestionID { get; set; }
        
        /// <summary>
        /// Gets or sets.
        /// </summary>
        public String SecretResponce { get; set; }
        
        /// <summary>
        /// Gets or sets.
        /// </summary>
        public DateTime RegistrationDate { get; set; }
        
        /// <summary>
        /// Gets or sets.
        /// </summary>
        public DateTime LastLoginDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether.
        /// </summary>
        public Boolean IsDeleted { get; set; }
    }
}