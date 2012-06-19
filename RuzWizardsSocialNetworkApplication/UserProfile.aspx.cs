using SocialNetwork.DataAccess;
using SocialNetwork.DataAccess.Enums;
using SocialNetwork.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

/// <summary>
/// Class UserProfile.
/// </summary>
public partial class UserProfile : System.Web.UI.Page
{
    /// <summary>
    /// Curent user identifacator.
    /// </summary>
    private Guid _userID = Guid.Empty;

    /// <summary>
    /// Page_Load event.
    /// </summary>
    /// <param name="sender">Object sender.</param>
    /// <param name="e">EventArgs e.</param>
    protected void Page_Load(Object sender, EventArgs e)
    {
        // Тут должен быть идентификатор текущего пользователя
        if (Guid.TryParse("e80cd2ac-8517-4e95-8321-3f4593d2106a", out this._userID))
        {
        }

        if (!Page.IsPostBack)
        {
            // Записать имя и статус пользователя в заголовочные поля страницы (шапка таблицы в основной колонке)
            PersonalInfo personalInfo = PersonalInfoRepository.GetUserInfo(this._userID);
            if (personalInfo != null)
            {
                lblHeadUserName.Text = String.Format(
                    "{0} {1}", personalInfo.FirstName, personalInfo.LastName);
                lblHeadUserStatus.Text = StatusRepository.GetStatusName(this._userID);

                // Записать значения в дроплист статусов.
                ddlStatus.DataBind();

                // Записать данные о пользователе в таблицу детальной информации.
                dvUserInfo.DataBind();

                // Записать полное имя пользователя и текущее статус-сообщение.
                lblUserName.Text = String.Format(
                    "{0} {1} {2} {3}",
                    personalInfo.FirstName, 
                    personalInfo.LastName,
                    personalInfo.MiddleName, 
                    personalInfo.NickName);
                btnStatusMessage.Text = StatusRepository.GetStatusMessage(this._userID);
                if (btnStatusMessage.Text == String.Empty)
                {
                    btnStatusMessage.Text = "Not set";
                }
            }
        }

        if (Page.IsPostBack)
        {
            // Скрыть панель изменения статуса на постбеке.
            this.StatusPanelEditMode(false);
        }

        // Вывести на стену сообщения, адресованные текущему пользователю
        dsWall.Where = String.Format("it.ToID = GUID '{0}' AND it.IsDeleted = {1}", this._userID, false);
        dsPersonalInfo.Where = String.Format("it.UserID = GUID '{0}'", this._userID);
    }

    /// <summary>
    /// DetailsView event.
    /// Событие, происходящее после привязки данных к dvUserInfo.
    /// </summary>
    /// <param name="sender">Object sender.</param>
    /// <param name="e">EventArgs e.</param>
    protected void OnUserInfoDataBound(Object sender, EventArgs e)
    {
        Address address = AddressRepository.GetUserAddress(this._userID);
        PersonalInfo personalInfo = PersonalInfoRepository.GetUserInfo(this._userID);
        #region // Получение ссылок на элементы dvUserInfo
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
        // Записываем в таблицу адресные данные пользователя, если они существуют
        if (address != null && !address.IsDeleted)
        {
            if (this.SetValueOrInvisible(
                this.dvUserInfo, 
                0, 
                lblBirthday, 
                personalInfo.Birthday.ToString()))
            {
                lblBirthday.Text = lblBirthday.Text.Split()[0];
            }
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
    /// Записываем в lbl принадлежащий dv значение value, при условии, 
    /// что оно существует, иначе скрываем строку с номером row.
    /// В случае успешного присвоения значения возвращаем true, иначе - false.
    /// </summary>
    /// <param name="dv">Control DetailsViev.</param>
    /// <param name="row">Control row number.</param>
    /// <param name="lbl">Label, consisted in this row.</param>
    /// <param name="value">New content for this label.</param>
    /// <returns>Status of assigment value.</returns>
    protected Boolean SetValueOrInvisible(DetailsView dv, Int32 row, Label lbl, String value)
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
    /// Button event.
    /// Действие по нажатию на кнопку изменения статус-сообщения.
    /// Button StatusMessage_Click: 
    /// 1. get current status message;
    /// 2. enable 'edit mode' in pnlStatusMessage.
    /// </summary>
    /// <param name="sender">Object sender.</param>
    /// <param name="e">EventArgs e.</param>
    protected void OnStatusMessageClick(Object sender, EventArgs e)
    {
        tbxStatusMessage.Text = btnStatusMessage.Text;
        ddlStatus.SelectedValue = StatusRepository.GetStatusID(this._userID).ToString();
        this.StatusPanelEditMode(true);
    }

    /// <summary>
    /// Button event.
    /// Действие по нажатию на кнопку сохранения статус-сообщения.
    /// OnSaveStatusMessageClick:
    /// 1. set text value in btnStatusMessage;
    /// 2. insert new status message to bd;
    /// 3. disable 'edit mode' in pnlStatusMessage.
    /// </summary>
    /// <param name="sender">Object sender.</param>
    /// <param name="e">EventArgs e.</param>
    protected void OnSaveStatusMessageClick(Object sender, ImageClickEventArgs e)
    {
        btnStatusMessage.Text = tbxStatusMessage.Text;
        if (btnStatusMessage.Text == String.Empty)
        {
            btnStatusMessage.Text = "Not Set";
        }
        StatusRepository.SetStatusMessage(
            this._userID, btnStatusMessage.Text, (UserStatus)Convert.ToInt32(ddlStatus.SelectedValue));
        this.StatusPanelEditMode(false);
    }

    /// <summary>
    /// GridView event.
    /// Привязка данных к стене.
    /// Bind data to wallboard.
    /// </summary>
    /// <param name="sender">Object sender.</param>
    /// <param name="e">EventArgs e.</param>
    protected void OnWallRowDataBound(Object sender, GridViewRowEventArgs e)
    {
        GridViewRow grdRowItem = e.Row;

        // По идентификаторам выводить имена пользователей оставивших сообщения.
        Label lblFromID = grdRowItem.FindControl("lblFromID") as Label;
        PersonalInfo personalInfo = PersonalInfoRepository.GetUserInfo(this._userID);
        if (lblFromID != null && personalInfo != null)
        {
            Guid fromID = Guid.Parse(lblFromID.Text);
            lblFromID.Text = String.Format("{0:s} {1:s}", personalInfo.FirstName, personalInfo.LastName);
        }
    }

    /// <summary>
    /// DropDownList event.
    /// Bind data to ddlStatus.
    /// </summary>
    /// <param name="sender">Object sender.</param>
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
    /// Установка состояния редактирования для панели изменения статуса. 
    /// </summary>
    /// <param name="enabled">Type of status panel 'edit mode'.</param>
    protected void StatusPanelEditMode(Boolean enabled)
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

    /// <summary>
    /// GridView event.
    /// Delete row from user wallboard.
    /// </summary>
    /// <param name="sender">Object sender.</param>
    /// <param name="e">EventArgs e.</param>
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
    /// <param name="sender">Object sender.</param>
    /// <param name="e">EventArgs e.</param>
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
}