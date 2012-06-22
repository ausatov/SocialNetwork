using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RuzWizardsSocialNetworkApplication.App_Code;

public partial class UserPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (SessionHelper.IsAdmin)
        {

            btnAdmin.Visible = true;
        }

    }
   

    protected void mnuUser_MenuItemClick(object sender, MenuEventArgs e)
    {
        switch (e.Item.Value)
        {
            case "Log out":
                Session.Abandon();
                Response.Redirect("~/Login.aspx");
                break;

        }

    }
}
