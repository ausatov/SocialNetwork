
namespace RuzWizardsSocialNetworkApplication.App_Code
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    /// <summary>
    /// Class for comfortable work with session object.
    /// </summary>
    public class SessionHelper
    {
        /// <summary>
        /// Check user's authenticated status .
        /// </summary>
        public static Boolean IsAuthenticated
        {
            get { return (bool)HttpContext.Current.Session["Authenticated"]; }
            set { HttpContext.Current.Session["Authenticated"] = value; }
        }
        
        /// <summary>
        /// Check admin role.
        /// </summary>
        public static Boolean IsAdmin
        {
            get { return (Boolean)HttpContext.Current.Session["Admin"]; }
            set { HttpContext.Current.Session["Admin"] = value; }
        }

        /// <summary>
        /// Store UserID in sesssion object.
        /// </summary>
        public static Guid UserID
        {
            get { return (Guid)HttpContext.Current.Session["UserID"]; }
            set { HttpContext.Current.Session["UserID"] = value; }
        }

    }
}