using RuzWizardsSocialNetworkApplication.App_Code;
using RuzWizardsSocialNetworkApplication.Constants;
using SocialNetwork.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RuzWizardsSocialNetworkApplication.Administration
{
    public partial class Banned : System.Web.UI.Page
    {
        private Guid _userID;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnBan_Click(object sender, EventArgs e)
        {
            this._userID = new Guid(Request.QueryString["id"].ToString());
            BanRepository.ModifyBan(null, true, this._userID, SessionHelper.UserID, tbxReason.Text
                 , DateTime.Parse(tbxFromDate.Text), DateTime.Parse(tbxToDate.Text), false);
        }
    }
}