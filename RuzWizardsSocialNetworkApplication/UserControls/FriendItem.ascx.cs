namespace RuzWizardsSocialNetworkApplication.UserControls
{
    #region Using
    using SocialNetwork.DataAccess.Repositories;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    #endregion

    /// <summary>
    /// FriendItem user control class.
    /// </summary>
    public partial class FriendItem : System.Web.UI.UserControl
    {
        #region Private fields
        private Guid _friendID;
        #endregion

        /// <summary>
        /// Gets or sets ID of current friend.
        /// </summary>
        public Guid FriendID
        {
            get { return this._friendID; }
            set { this._friendID = value; }
        }

        /// <summary>
        /// Gets or sets username of current friend.
        /// </summary>
        public String UserName
        {
            get { return hlFio.Text; }
            set
            {
                hlFio.Text = value;
                hlFio.NavigateUrl = "~/UserProfile.aspx?id=" + this.FriendID;
            }
        }

        /// <summary>
        /// Gets or sets status of current friend.
        /// </summary>
        public String UserStatus
        {
            get { return lblStatus.Text; }
            set { lblStatus.Text = value + "<br/"; }
        }

        //public String UserPhoto
        //{
        //    set { lblFio.Text = value; }
        //}

        /// <summary>
        /// Page load event.
        /// </summary>
        /// <param name="sender">Object sender.</param>
        /// <param name="e">Eventargs e.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            UserName = PersonalInfoRepository.GetFullName(this.FriendID);
            UserStatus = StatusRepository.GetStatusMessage(this.FriendID);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">Object sender.</param>
        /// <param name="e">Eventargs e.</param>
        protected void btnSendMsg_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">Object sender.</param>
        /// <param name="e">Eventargs e.</param>
        protected void btnDelete_Click(object sender, EventArgs e)
        {

        }
    }
}