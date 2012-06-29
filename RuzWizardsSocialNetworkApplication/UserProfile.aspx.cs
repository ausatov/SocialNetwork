// -----------------------------------------------------------------------
// <copyright file="UserProfile.cs" company="RusWizards">
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
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using RuzWizardsSocialNetworkApplication.Constants;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
#endregion

/// <summary>
/// Class UserProfile.
/// </summary>
public partial class UserProfile : System.Web.UI.Page
{
    #region Constants
    /// <summary>
    /// Default empty status message.
    /// </summary>
    private const String _defaultEmptyStatusMessage = "-- not set --";
    #endregion

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
        // Тут должен быть идентификатор текущего пользователя
        this._userID = Guid.Parse("e80cd2ac-8517-4e95-8321-3f4593d2106a");

        if (!Page.IsPostBack)
        {
            // Set user name and status on the top of table.
            PersonalInfo personalInfo = PersonalInfoRepository.GetUserInfo(this._userID);
            if (personalInfo != null)
            {
                dvUserInfo.DataSource = new List<PersonalInfo> { personalInfo };
                lblHeadUserName.Text = String.Join(" ", personalInfo.FirstName, personalInfo.LastName);
                lblHeadUserStatus.Text = EnumsHelper.ToString(((UserStatus)StatusRepository.GetStatusID(this._userID)));

                // Show user full name.
                lblUserName.Text = String.Join(
                    " ",
                    personalInfo.FirstName, 
                    personalInfo.LastName,
                    personalInfo.MiddleName, 
                    personalInfo.NickName);

                // Show status message.
                btnStatusMessage.Text = StatusRepository.GetStatusMessage(this._userID);
                if (btnStatusMessage.Text == String.Empty)
                {
                    btnStatusMessage.Text = _defaultEmptyStatusMessage;
                }
            }

            // Bind data to wallboard.
            IEnumerable<WallBoardItem> dsWallBoardItem = WallBoardItemRepository.GetUserWallboardItems(this._userID);
            fvWall.DataSource = dsWallBoardItem;
            grdWall.DataSource = dsWallBoardItem;

            // Bind all page data elements.
            Page.DataBind();
        }

        // Set invisible mode to status change panel on postback.
        if (Page.IsPostBack)
        {
            this.StatusPanelEditMode(false);
        }
    }

    /// <summary>
    /// After dvUserInfo data binding.
    /// </summary>
    /// <param name="sender">Object sender : dvUserInfo.</param>
    /// <param name="e">EventArgs e.</param>
    protected void OnUserInfoDataBound(Object sender, EventArgs e)
    {
        Address address = AddressRepository.GetUserAddress(this._userID);
        PersonalInfo personalInfo = PersonalInfoRepository.GetUserInfo(this._userID);
        #region // Inner controls
        Label lblBirthday = dvUserInfo.FindControl("lblBirthday") as Label;
        Label lblPhone = dvUserInfo.FindControl("lblPhone") as Label;
        Label lblSex = dvUserInfo.FindControl("lblSex") as Label;
        Label lblCountry = dvUserInfo.FindControl("lblCountry") as Label;
        Label lblCity = dvUserInfo.FindControl("lblCity") as Label;
        Label lblArea = dvUserInfo.FindControl("lblArea") as Label;
        Label lblStreet = dvUserInfo.FindControl("lblStreet") as Label;
        Label lblHome = dvUserInfo.FindControl("lblHome") as Label;
        Label lblApartment = dvUserInfo.FindControl("lblApartment") as Label;
        #endregion

        imgUserAvatar.ImageUrl = personalInfo.ImagePath;
        if (this.imgUserAvatar.ImageUrl == Constants._defaultAvatarImage)
        {
            this.imgUserAvatar.ImageUrl = String.Concat(Constants._defaultPhotoPath, Constants._defaultAvatarImage);
        }
        else
        {
            this.imgUserAvatar.ImageUrl = String.Concat(Constants._defaultPhotoPath, imgUserAvatar.ImageUrl);
        }

        // If user have address -> set values to controls.
        if (address != null)
        {
            this.SetValueOrInvisible(
                this.dvUserInfo,
                0,
                lblBirthday,
                String.Format("{0:" + Constants._dateFormat + "}", personalInfo.Birthday));
            
            if (this.SetValueOrInvisible(this.dvUserInfo, 1, lblSex, personalInfo.Sex.ToString()))
            {
                lblSex.Text = EnumsHelper.ToString((Sex)Convert.ToInt32(lblSex.Text));
            }
            this.SetValueOrInvisible(this.dvUserInfo, 2, lblPhone, personalInfo.Phone);
            Guid? countryID = address.CountryID;
            if (countryID != null)
            {
                this.SetValueOrInvisible(
                     this.dvUserInfo, 3, lblCountry, AddressRepository.GetCountryName((Guid)countryID));
            }
            Guid? cityID = address.CityID;
            if (cityID != null)
            {
                this.SetValueOrInvisible(
                     this.dvUserInfo, 4, lblCity, AddressRepository.GetCityName((Guid)cityID));
            }
            this.SetValueOrInvisible(this.dvUserInfo, 5, lblArea, address.Area);
            this.SetValueOrInvisible(this.dvUserInfo, 6, lblStreet, address.Street);
            this.SetValueOrInvisible(this.dvUserInfo, 7, lblHome, address.Home);
            this.SetValueOrInvisible(this.dvUserInfo, 8, lblApartment, address.Apartment);
        }
    }

    /// <summary>
    /// Button StatusMessage_Click: 
    /// 1. get current status message;
    /// 2. enable 'edit mode' in pnlStatusMessage.
    /// </summary>
    /// <param name="sender">Object sender : LinkButton.</param>
    /// <param name="e">EventArgs e.</param>
    protected void OnStatusMessageClick(Object sender, EventArgs e)
    {
        tbxStatusMessage.Text = btnStatusMessage.Text;
        ddlStatus.SelectedValue = StatusRepository.GetStatusID(this._userID).ToString();
        this.StatusPanelEditMode(true);
    }

    /// <summary>
    /// OnSaveStatusMessageClick:
    /// 1. set text value in btnStatusMessage;
    /// 2. insert new status message to bd;
    /// 3. disable 'edit mode' in pnlStatusMessage.
    /// </summary>
    /// <param name="sender">Object sender : ImageButton.</param>
    /// <param name="e">ImageClickEventArgs e.</param>
    protected void OnSaveStatusMessageClick(Object sender, ImageClickEventArgs e)
    {
        btnStatusMessage.Text = tbxStatusMessage.Text;
        if (btnStatusMessage.Text == String.Empty)
        {
            btnStatusMessage.Text = _defaultEmptyStatusMessage;
        }
        StatusRepository.SetStatusMessage(
            this._userID, btnStatusMessage.Text, (UserStatus)Convert.ToInt32(ddlStatus.SelectedValue));
        this.StatusPanelEditMode(false);
    }

    /// <summary>
    /// Bind data to wallboard.
    /// </summary>
    /// <param name="sender">Object sender : GridView.</param>
    /// <param name="e">GridViewRowEventArgs e.</param>
    protected void OnWallRowDataBound(Object sender, GridViewRowEventArgs e)
    {
        GridViewRow grdRowItem = e.Row;

        // Get user names on wallboard by id.
        Label lblFromID = grdRowItem.FindControl("lblFromID") as Label;
        PersonalInfo personalInfo = PersonalInfoRepository.GetUserInfo(this._userID);
        if (lblFromID != null && personalInfo != null)
        {
            Guid fromID = Guid.Parse(lblFromID.Text);
            lblFromID.Text = String.Join(" ", personalInfo.FirstName, personalInfo.LastName);
        }
    }

    /// <summary>
    /// Bind data to ddlStatus.
    /// </summary>
    /// <param name="sender">Object sender : DropDownList.</param>
    /// <param name="e">EventArgs e.</param>
    protected void OnStatusDataBinding(Object sender, EventArgs e)
    {
        String[] names = Enum.GetNames(typeof(UserStatus));
        Array values = Enum.GetValues(typeof(UserStatus));
        Dictionary<Int32, String> dictionary = new Dictionary<Int32, String>();
        for (Int32 i = 0; i < names.Length; i++)
        {
            dictionary.Add((Int32)values.GetValue(i), EnumsHelper.ToString((UserStatus)values.GetValue(i)));
        }
        ddlStatus.DataSource = dictionary;
        ddlStatus.DataTextField = "Value";
        ddlStatus.DataValueField = "Key";
    }

    /// <summary>
    /// Delete row from user wallboard.
    /// </summary>
    /// <param name="sender">Object sender : GridView.</param>
    /// <param name="e">GridViewDeleteEventArgs e.</param>
    protected void OnWallRowDeleting(Object sender, GridViewDeleteEventArgs e)
    {
        Guid id = Guid.Empty;
        if (Guid.TryParse(e.Keys["ID"].ToString(), out id))
        {
            WallBoardItemRepository.DeleteItem(id);
        }
    }

    /// <summary>
    /// FormView command-event reactions.
    /// </summary>
    /// <param name="sender">Object sender : FormView.</param>
    /// <param name="e">FormViewCommandEventArgs e.</param>
    protected void OnWallItemCommand(Object sender, FormViewCommandEventArgs e)
    {
        String commandName = e.CommandName;

        TextBox tbxMessage = fvWall.FindControl("tbxMessage") as TextBox;
        if (commandName == "SendWallMessage")
        {
            WallBoardItemRepository.InsertMessage(
                WallBoardItemType.Text, this._userID, this._userID, tbxMessage.Text);
        }
    }
    #endregion

    #region Private methods
    /// <summary>
    /// Write to 'lbl' (consisted in 'dv') param value if it exists, 
    /// else set invisible to row with number 'row'.
    /// </summary>
    /// <param name="dv">Control DetailsViev.</param>
    /// <param name="row">Control row number.</param>
    /// <param name="lbl">Label, consisted in this row.</param>
    /// <param name="value">New content for this label.</param>
    /// <returns>Status of assigment value.</returns>
    private Boolean SetValueOrInvisible(DetailsView dv, Int32 row, Label lbl, String value)
    {
        if (value != null && lbl != null && value != string.Empty)
        {
            lbl.Text = value;
            return true;
        }
        else
        {
            dv.Rows[row].Visible = false;
            return false;
        }
    }

    /// <summary>
    /// Set or unset 'edit mode' to status-edit panel.
    /// </summary>
    /// <param name="enabled">Type of status panel 'edit mode'.</param>
    private void StatusPanelEditMode(Boolean enabled)
    {
        if (enabled)
        {
            pnlStatusMessage.Visible = true;
            btnStatusMessage.Visible = false;
        }
        else
        {
            pnlStatusMessage.Visible = false;
            btnStatusMessage.Visible = true;
        }
    }
    #endregion
}