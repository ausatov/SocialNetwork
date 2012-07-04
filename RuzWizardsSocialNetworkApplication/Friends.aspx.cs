namespace RuzWizardsSocialNetworkApplication
{
    using RuzWizardsSocialNetworkApplication.UserControls;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using SocialNetwork.DataAccess.Repositories;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    
    /// <summary>
    /// Friend page class.
    /// </summary>
    public partial class Friends : System.Web.UI.Page
    {
        #region Private fields
        private Guid _userID;
        #endregion

        /// <summary>
        /// Page load event.
        /// </summary>
        /// <param name="sender">Object sender.</param>
        /// <param name="e">Eventargs e.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
           this._userID = new Guid(Request.QueryString["id"].ToString());
            hfUserID.Value = this._userID.ToString();
            rptFriends.DataSource = FriendRepository
               .GetUserFriendsWithParams(this._userID, 0, 2, false);
            rptOnlineFriends.DataSource = FriendRepository
               .GetUserFriendsWithParams(this._userID, 0, 2, false);
            if (!IsPostBack)
            {
                rptFriends.DataBind();
                rptOnlineFriends.DataBind();
            }
        }


    }
}