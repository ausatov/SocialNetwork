using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RuzWizardsSocialNetworkApplication.App_Code
{
    public class SessionHelper
    {
        /// <summary>
        /// Проверяем прошел ли пользователь аутентификацию
        /// </summary>
        public static Boolean IsAuthenticated
        {
            get { return (bool)HttpContext.Current.Session["Authenticated"]; }
            set { HttpContext.Current.Session["Authenticated"] = value; }
        }

        /// <summary>
        /// Проверяем является ли пользователь администратором
        /// </summary>
        public static Boolean IsAdmin
        {
            get { return (bool)HttpContext.Current.Session["Admin"]; }
            set { HttpContext.Current.Session["Admin"] = value; }
        }
    }
}