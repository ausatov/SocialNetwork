namespace RuzWizardsSocialNetworkApplication.Administration
{
    using SocialNetwork.DataAccess.Repositories;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    /// <summary>
    /// Bans page class.
    /// </summary>
    public partial class Bans : System.Web.UI.Page
    {
        /// <summary>
        /// PageLoad event handler.
        /// </summary>
        /// <param name="sender">Object sender.</param>
        /// <param name="e">EventArgs e.</param>
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// Gridview row data bound event.
        /// </summary>
        /// <param name="sender">Object sender.</param>
        /// <param name="e">EventArgs e.</param>
        protected void gvBans_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hl = e.Row.FindControl("hlUserPage") as HyperLink;
                hl.Text = PersonalInfoRepository.GetFullName(new Guid(e.Row.Cells[0].Text));
                hl.NavigateUrl = "~/UserProfile.aspx?id=" + e.Row.Cells[0].Text;
            }

        }

        /// <summary>
        /// Gridview row created event.
        /// </summary>
        /// <param name="sender">Object sender.</param>
        /// <param name="e">EventArgs e.</param>
        protected void gvBans_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Visible = false; 
            }
        }

        /// <summary>
        /// Select gridview pagesize.
        /// </summary>
        /// <param name="sender">Object sender.</param>
        /// <param name="e">EventArgs e.</param>
        protected void rowsToDisplay_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvBans.PageSize = int.Parse(rowsToDisplay.SelectedValue);
        }
    }
}