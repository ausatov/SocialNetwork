// -----------------------------------------------------------------------
// <copyright file="Messages.cs" company="RusWizards">
// Author: Mankevich M.V. 
// Date: 29.06.12
// </copyright>
// -----------------------------------------------------------------------

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

    private MessageType _messageType = MessageType.Received;
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
            rptMessages.DataSource = MessageRepository.GetUserMessages(
                this._userID, MessageType.Received, MessageSelectType.All);
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
        LinkButton btnReceiver = item.FindControl("btnReceiver") as LinkButton;
        if (_messageType == MessageType.Received && btnSender != null && btnReceiver != null)
        {
            FillMessageInfo(item, btnSender);
            btnSender.Visible = true;
            btnReceiver.Visible = false;
        }
        else if (btnSender != null && btnReceiver != null)
        {
            FillMessageInfo(item, btnReceiver);
            btnSender.Visible = false;
            btnReceiver.Visible = true;
        }
    }

    private void FillMessageInfo(RepeaterItem item, LinkButton btnUserName)
    {
        if (btnUserName != null)
        {
            PersonalInfo person = PersonalInfoRepository.GetUserInfo(Guid.Parse(btnUserName.Text));
            btnUserName.Text = String.Join(" ", person.FirstName, person.LastName);
        }
    }

    /// <summary>
    /// Show received messages.
    /// </summary>
    /// <param name="sender">Object sender : ImageButton.</param>
    /// <param name="e">ImageClickEventArgs e.</param>
    protected void OnShowReceivedClick(Object sender, ImageClickEventArgs e)
    {
        _messageType = MessageType.Received;
        rptMessages.DataSource = MessageRepository.GetUserMessages(
            this._userID, MessageType.Received, MessageSelectType.All);
        rptMessages.DataBind();
    }

    /// <summary>
    /// Show posted messages.
    /// </summary>
    /// <param name="sender">Object sender : ImageButton.</param>
    /// <param name="e">ImageClickEventArgs e.</param>
    protected void OnShowPostedClick(Object sender, ImageClickEventArgs e)
    {
        _messageType = MessageType.Posted;
        rptMessages.DataSource = MessageRepository.GetUserMessages(
            this._userID, MessageType.Posted, MessageSelectType.All);
        rptMessages.DataBind(); 
    }
    #endregion
}
