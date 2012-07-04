#region Using
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.Expressions;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using SocialNetwork.DataAccess;
using SocialNetwork.DataAccess.Repositories;
using RuzWizardsSocialNetworkApplication.App_Code;
#endregion

/// <summary>
/// Login page class.
/// </summary>
public partial class Login : System.Web.UI.Page
{
    #region Page handlers
    /// <summary>
    /// Page load event.
    /// </summary>
    /// <param name="sender">Object sender.</param>
    /// <param name="e">Eventargs e.</param>
    protected void Page_Load(Object sender, EventArgs e)
    {
        HttpCookie exCookie = Request.Cookies["EmailCookie"];
        if (exCookie != null)
        {
            tbxEmail.Text = exCookie["Email"].ToString();
        }
        if (Request.QueryString["reg"] != null)
        {
            pnlLogin.Visible = false;
            pnlRegistrtion.Visible = true;
            btnRegistration.Enabled = false;
        }
        else
        {
            pnlLogin.Visible = true;
            pnlRegistrtion.Visible = false;
            btnRegistration.Enabled = true;
        }
    }

    /// <summary>
    /// Go to the registration page.
    /// </summary>
    protected void OnbtnRegistration_Click(Object sender, ImageClickEventArgs e)
    {
        Response.Redirect("Login.aspx?Reg=1");
    }

    /// <summary>
    /// Set cities changes.
    /// </summary>
    protected void OnddlCountry_SelectedIndexChanged(Object sender, EventArgs e)
    {
        // если выбрано значение отличное от "not defined" (по умолчанию : index = 0)
        // устанавливаем условие фильтрации в edsCity
        // для получения значений в ddlCity в соответствии с выбранным значением в ddlCountry
        if (ddlCountry.SelectedIndex > 0)
        {
            trCity.Visible = true;
            edsCity.Where = string.Format("it.[CountryID] = {0}", ddlCountry.SelectedValue);
        }
        // иначе скрываем поле с выбором города
        else
        {
            trCity.Visible = false;
            edsCity.Where = "it.[CountryID] = 0";
        }
        // очищаем список и добавляем элемент "по умолчанию"
        ddlCity.Items.Clear();
        ddlCity.Items.Add(new ListItem("Not defined", "0"));
    }

    /// <summary>
    /// Load photo.
    /// </summary>
    /// <param name="sender">Object sender.</param>
    /// <param name="e">Eventargs e.</param>
    protected void OnfuPhoto_UploadedComplete(Object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
    {

    }

    /// <summary>
    /// Regisration.
    /// </summary>
    /// <param name="sender">Object sender.</param>
    /// <param name="e">Eventargs e.</param>
    protected void OnbtnRegCompleteRegistration_Click(Object sender, ImageClickEventArgs e)
    {
        //Int16? _sex;
        //Guid? _country;
        //Guid? _city;
        //switch (ddlSex.SelectedValue)
        //{
        //    // Вариант не доступен для выбора, но возможен
        //    case "-1":
        //        {
        //            _sex = null;
        //        } break;
        //    // Мужской пол
        //    case "0":
        //        {
        //            _sex = 1;
        //        } break;
        //    // Женский пол
        //    case "1":
        //        {
        //            _sex = 0;
        //        } break;
        //}

        //if (ddlCountry.SelectedIndex > 0)
        //{
        //    _country = ddlCountry.SelectedIndex;
        //}
        //else
        //{
        //    _country = DBNull.Value;
        //}
        //if (ddlCity.SelectedIndex > 0 && ddlCountry.SelectedIndex > 0)
        //{
        //    _city = ddlCountry.SelectedIndex;
        //}
        //else
        //{
        //    _city = DBNull.Value;
        //}

        //using (SocialNetworkDBEntities record = new SocialNetworkDBEntities())
        //{
        //    record.spInsUser(tbxRegEmail.Text, tbxRegPassword1.Text, tbxFirstName.Text, tbxSecondName.Text, tbxFatherName.Text
        //      , tbxNickName.Text, tbxPhone.Text, _sex, _country, _city, "nophoto");
        //}
    }

    /// <summary>
    /// User's log in.
    /// </summary>
    /// <param name="sender">Object sender.</param>
    /// <param name="e">Eventargs e.</param>
    protected void OnbtnLogin_Click(Object sender, ImageClickEventArgs e)
    {
        CheckLoginInfo();
    }
    #endregion

    #region Private methods
    private void CheckLoginInfo()
    {
        Guid userID;
        userID = UserRepository.GetUserID(tbxEmail.Text, tbxPassword.Text);
        if (userID != Guid.Empty)
        {
            SessionHelper.UserID = userID;
            SessionHelper.UserEmail = tbxEmail.Text;
            System.Nullable<Int16> privelegeMask = 0;
            privelegeMask = UserRoleRepository.GetMinPrivelegeMask(userID);
            if (privelegeMask.HasValue)
            {
                if (privelegeMask == 1)
                    return;
                SessionHelper.IsAuthenticated = true;
                privelegeMask = UserRoleRepository.GetMaxPrivelegeMask(userID);
                if (privelegeMask == 127)
                {
                    SessionHelper.IsModerator = true;
                }
                if (privelegeMask >= 254)
                {
                    SessionHelper.IsAdmin = true;
                }
            }
            //Cookies for login.
            HttpCookie emailCookie = new HttpCookie("EmailCookie");
            emailCookie.Expires = DateTime.Now.AddMonths(1);
            //Push email to cookie.
            emailCookie["Email"] = tbxEmail.Text;
            //Add cookie to user's brouser.
            Response.Cookies.Add(emailCookie);
            Response.Redirect("~/UserProfile.aspx?id=" + SessionHelper.UserID);

        }
    }
    #endregion
}