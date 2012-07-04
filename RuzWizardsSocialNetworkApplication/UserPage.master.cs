using RuzWizardsSocialNetworkApplication.App_Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class UserPage : System.Web.UI.MasterPage
{
    private Guid _userID;
    protected void Page_Load(object sender, EventArgs e)
    {
        this._userID = new Guid(Request.QueryString["id"].ToString());
    }

    protected void OnbtnAdmin_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Administration/Welcome.aspx?id=" + SessionHelper.UserID);
    }

    protected void OnbtnBan_Click(object sender, ImageClickEventArgs e)
    {

    }

    protected void OnbtnAdmin_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Administration/Welcome.aspx?id="
            + SessionHelper.UserID);
    }

    protected void mnuLeftMenuBar_MenuItemClick(object sender, MenuEventArgs e)
    {
        switch (e.Item.Value)
        {
            case "Friends":
                Response.Redirect("~/Friends.aspx?id=" + SessionHelper.UserID);
                break;
            case "Log out":
                Session.Abandon();
                Response.Redirect("~/Login.aspx");
                break;
        }
    }

    protected void btnBan_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Administration/Banned.aspx?id=" 
            + this._userID);
    }
}
