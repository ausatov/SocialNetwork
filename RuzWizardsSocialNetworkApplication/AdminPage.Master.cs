// -----------------------------------------------------------------------
// <copyright file="AddressRepository.cs" company="RusWizards">
// Author: Usatov A.B. 
// Date: 29.06.12
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RuzWizardsSocialNetworkApplication.App_Code;

namespace RuzWizardsSocialNetworkApplication
{
    public partial class AdminPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void menuAdmin_MenuItemClick(object sender, MenuEventArgs e)
        {

            switch (e.Item.Value)
            {
                case "My page":
                    Response.Redirect("~/UserProfile.aspx?id=" + SessionHelper.UserID);
                    break;
                case "Bans":
                    Response.Redirect("~/Administration/Bans.aspx");
                    break;
            }
        }
    }
}