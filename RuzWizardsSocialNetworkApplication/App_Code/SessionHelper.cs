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
            get
            {
                if (HttpContext.Current != null &&
                     HttpContext.Current.Session["Authenticated"] != null &&
                        HttpContext.Current.Session["Authenticated"] is Boolean)
                {
                    return (Boolean)HttpContext.Current.Session["Authenticated"];
                }
                else
                {
                    return false;
                }
            }
            set
            {
                if (HttpContext.Current != null &&
                    HttpContext.Current.Session["Authenticated"] != null &&
                       HttpContext.Current.Session["Authenticated"] is Boolean)
                    HttpContext.Current.Session["Authenticated"] = value;
            }
        }

        /// <summary>
        /// Check admin role.
        /// </summary>
        public static Boolean IsAdmin
        {
            get
            {
                if (HttpContext.Current != null &&
                     HttpContext.Current.Session["Admin"] != null &&
                        HttpContext.Current.Session["Admin"] is Boolean)
                {
                    return (Boolean)HttpContext.Current.Session["Admin"];
                }
                else
                {
                    return false;
                }
            }
            set
            {
                if (HttpContext.Current != null &&
                    HttpContext.Current.Session["Admin"] != null &&
                       HttpContext.Current.Session["Admin"] is Boolean)
                    HttpContext.Current.Session["Admin"] = value;
            }
        }

        /// <summary>
        /// Store UserID in sesssion object.
        /// </summary>
        public static Guid UserID
        {
            get
            {
                if (HttpContext.Current != null &&
                     HttpContext.Current.Session["UserID"] != null &&
                        HttpContext.Current.Session["UserID"] is Guid)
                {
                    return (Guid)HttpContext.Current.Session["UserID"];
                }
                else
                {
                    return Guid.Empty;
                }
            }
            set
            {
                if (HttpContext.Current != null &&
                    HttpContext.Current.Session["UserID"] != null &&
                       HttpContext.Current.Session["UserID"] is Guid)
                    HttpContext.Current.Session["UserID"] = value;
            }
        }

    }
}