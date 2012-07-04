// -----------------------------------------------------------------------
// <copyright file="NewMessage.cs" company="RusWizards">
// Author: Mankevich M.V. 
// Date: 29.06.12
// </copyright>
// -----------------------------------------------------------------------

#region Using
using SocialNetwork.DataAccess.Entity;
using SocialNetwork.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
#endregion

/// <summary>
/// Class NewMessage.
/// </summary>
public partial class NewMessage : System.Web.UI.Page
{
    #region Private fields
    /// <summary>
    /// Current user identifier.
    /// </summary>
    private Guid _userID = Guid.Empty;
    #endregion

    #region Page handlers
    /// <summary>
    /// Page_Load event 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        Guid.TryParse("e80cd2ac-8517-4e95-8321-3f4593d2106a", out this._userID);

        if (!Page.IsPostBack)
        {
            ddlReceiver.DataSource = FriendRepository.GetFriendlist(_userID);
            ddlReceiver.DataValueField = "Key";
            ddlReceiver.DataTextField = "Value";
            Page.DataBind();
        }
    }

    /// <summary>
    /// Send message.
    /// </summary>
    /// <param name="sender">Object sender : ImageButton.</param>
    /// <param name="e">ImageClickEventArgs e.</param>
    protected void OnSendMessageClick(Object sender, ImageClickEventArgs e)
    {
        Guid senderID = _userID;
        Guid receiverID;
        String messageHeader = tbxMessageHeader.Text;
        String messageText = tbxMessageText.Text;
        if (Guid.TryParse(ddlReceiver.SelectedValue, out receiverID)
            && messageHeader.Length > 0
            && messageText.Length > 0)
        {
            MessageRepository.SendPrivateMessage(senderID, receiverID, messageHeader, messageText);
        }
    }
    #endregion
}
