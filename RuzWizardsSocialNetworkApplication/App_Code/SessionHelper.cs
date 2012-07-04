namespace RuzWizardsSocialNetworkApplication.App_Code
{
    #region Using
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    #endregion

    /// <summary>
    /// Class for comfortable work with session object.
    /// </summary>
    public class SessionHelper
    {
        #region Properties
        /// <summary>
        /// Gets or sets Store user's Email in sesssion object.
        /// </summary>
        public static String UserEmail
        {
            get
            {
                if (HttpContext.Current != null
                    && HttpContext.Current.Session["UserEmail"] != null
                    && HttpContext.Current.Session["UserEmail"] is String)
                {
                    return HttpContext.Current.Session["UserEmail"].ToString();
                }
                else
                {
                    return String.Empty;
                }
            }
            set
            {
                // if (HttpContext.Current != null &&
                // HttpContext.Current.Session["UserID"] != null &&
                // HttpContext.Current.Session["UserID"] is Guid)
                HttpContext.Current.Session["UserEmail"] = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether Check moderator role.
        /// </summary>
        public static Boolean IsModerator
        {
            get
            {
                if (HttpContext.Current != null
                    && HttpContext.Current.Session["Moderator"] != null
                    && HttpContext.Current.Session["Moderator"] is Boolean)
                {
                    return (Boolean)HttpContext.Current.Session["Moderator"];
                }
                else
                {
                    return false;
                }
            }
            set
            {
                // if (HttpContext.Current != null &&
                // HttpContext.Current.Session["Admin"] != null &&
                // HttpContext.Current.Session["Admin"] is Boolean)
                HttpContext.Current.Session["Moderator"] = value;
            }
        }
        /// <summary>
        /// Gets or sets a value indicating whether check user's authenticated status.
        /// </summary>
        public static Boolean IsAuthenticated
        {
            get
            {
                if (HttpContext.Current != null 
                    && HttpContext.Current.Session["Authenticated"] != null 
                    && HttpContext.Current.Session["Authenticated"] is Boolean)
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
                // if (HttpContext.Current != null &&
                // HttpContext.Current.Session["Authenticated"] != null &&
                // HttpContext.Current.Session["Authenticated"] is Boolean)
                HttpContext.Current.Session["Authenticated"] = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether Check admin role.
        /// </summary>
        public static Boolean IsAdmin
        {
            get
            {
                if (HttpContext.Current != null 
                    && HttpContext.Current.Session["Admin"] != null 
                    && HttpContext.Current.Session["Admin"] is Boolean)
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
                // if (HttpContext.Current != null &&
                // HttpContext.Current.Session["Admin"] != null &&
                // HttpContext.Current.Session["Admin"] is Boolean)
                HttpContext.Current.Session["Admin"] = value;
            }
        }

        /// <summary>
        /// Gets or sets Store UserID in sesssion object.
        /// </summary>
        public static Guid UserID
        {
            get
            {
                if (HttpContext.Current != null 
                    && HttpContext.Current.Session["UserID"] != null 
                    && HttpContext.Current.Session["UserID"] is Guid)
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
                // if (HttpContext.Current != null &&
                // HttpContext.Current.Session["UserID"] != null &&
                // HttpContext.Current.Session["UserID"] is Guid)
                HttpContext.Current.Session["UserID"] = value;
            }
        }
        #endregion
    }
}