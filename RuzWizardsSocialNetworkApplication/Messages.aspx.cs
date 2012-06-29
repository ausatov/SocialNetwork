#region Using
using SocialNetwork.DataAccess.Entity;
using SocialNetwork.DataAccess.Enums;
using SocialNetwork.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
#endregion

/// <summary>
/// Class Messages.
/// </summary>
public partial class Messages : System.Web.UI.Page
{
    #region Private fields
    /// <summary>
    /// Current user identifier.
    /// </summary>
    private Guid _userID = Guid.Empty;
    #endregion

    #region Page handlers
    /// <summary>
    /// Page_Load event.
    /// </summary>
    /// <param name="sender">Object sender.</param>
    /// <param name="e">EventArgs e.</param>
    protected void Page_Load(Object sender, EventArgs e)
    {
        if (Guid.TryParse("e80cd2ac-8517-4e95-8321-3f4593d2106a", out this._userID))
        {
        }

        if (!Page.IsPostBack)
        {
            if (Request.QueryString["Posted"] != null && Request.QueryString["Posted"] == "1")
            {
                rptMessages.DataSource = MessageRepository.GetUserMessages(
                    this._userID, MessageType.Posted, MessageSelectType.All);
            }
            else
            {
                rptMessages.DataSource = MessageRepository.GetUserMessages(
                    this._userID, MessageType.Received, MessageSelectType.All);
            }
            rptMessages.DataBind();
        }
    }

    /// <summary>
    /// After binding data to repeater.
    /// </summary>
    /// <param name="sender">Object sender : Repeater.</param>
    /// <param name="e">RepeaterItemEventArgs e.</param>
    protected void OnMessagesItemDataBound(Object sender, RepeaterItemEventArgs e)
    {
        RepeaterItem item = e.Item;
        LinkButton btnSender = item.FindControl("btnSender") as LinkButton;
        if (btnSender != null)
        {
            PersonalInfo person = PersonalInfoRepository.GetUserInfo(Guid.Parse(btnSender.Text));
            btnSender.Text = String.Join(" ", person.FirstName, person.LastName);
        }
    }

    /// <summary>
    /// Show received messages.
    /// </summary>
    /// <param name="sender">Object sender : ImageButton.</param>
    /// <param name="e">ImageClickEventArgs e.</param>
    protected void OnShowReceivedClick(Object sender, ImageClickEventArgs e)
    {
        Response.Redirect("Messages.aspx");
    }

    /// <summary>
    /// Show posted messages.
    /// </summary>
    /// <param name="sender">Object sender : ImageButton.</param>
    /// <param name="e">ImageClickEventArgs e.</param>
    protected void OnShowPostedClick(Object sender, ImageClickEventArgs e)
    {
        Response.Redirect("Messages.aspx?Posted=1");
    }
    #endregion
}
