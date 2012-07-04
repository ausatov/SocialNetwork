namespace RuzWizardsSocialNetworkApplication
{
    using RuzWizardsSocialNetworkApplication.App_Code;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    
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
                    Response.Redirect("~/Administration/Bans.aspx?id=" + SessionHelper.UserID);
                    break;
            }
        }
    }
}