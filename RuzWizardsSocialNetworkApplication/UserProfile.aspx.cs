using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using SocialNetwork.DataAccess;
using SocialNetwork.DataAccess.Enums;
using SocialNetwork.DataAccess.Repositories;

/// <summary>
/// class UserProfile
/// </summary>
public partial class UserProfile : System.Web.UI.Page
{
    /// <summary>
    /// Тут должен быть идентификатор текущего пользователя <see cref="VeryLongClassName"/>
    /// </summary>
    protected Guid _userID = Guid.Empty;
    //protected PersonalInfoRepository _personalInfoRepository = new PersonalInfoRepository();
    //protected StatusRepository _statusRepository = new StatusRepository();
    //protected AddressRepository _addressRepository = new AddressRepository();
    //protected CountryRepository _countryRepository = new CountryRepository();
    //protected CityRepository _cityRepository = new CityRepository();

    /// <summary>
    /// 
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        // Тут должен быть идентификатор текущего пользователя
        if (Guid.TryParse("e80cd2ac-8517-4e95-8321-3f4593d2106a", out _userID))
        {
        }

        if (!Page.IsPostBack)
        {
            // Записать имя и статус пользователя в заголовочные поля страницы (шапка таблицы в основной колонке)
            PersonalInfo personalInfo = PersonalInfoRepository.GetUserInfo(_userID);
            lblHeadUserName.Text = String.Format("{0} {1}",
                personalInfo.FirstName, personalInfo.LastName);
            lblHeadUserStatus.Text = StatusRepository.GetStatusName(_userID);
            // Записать значения в дроплист статусов
            ddlStatus.DataBind();
            // Записать данные о пользователе в таблицу детальной информации
            dtlUserInfo.DataBind();
            // Записать полное имя пользователя и текущее статус-сообщение
            lblUserName.Text = String.Format("{0} {1} {2} {3}",
                personalInfo.FirstName, personalInfo.LastName, 
                personalInfo.MiddleName, personalInfo.NickName);
            btnStatusMessage.Text = StatusRepository.GetStatusMessage(_userID);
            if (btnStatusMessage.Text == String.Empty)
            {
                btnStatusMessage.Text = "Not set";
            }
        }

        if (Page.IsPostBack)
        {
            // Скрыть панель изменения статуса на постбеке
            StatusPanelEditMode(false);
        }

        // Вывести на стену сообщения, адресованные текущему пользователю
        edsWall.Where = String.Format("it.ToID = GUID '{0}'", _userID);
        edsPersonalInfo.Where = String.Format("it.UserID = GUID '{0}'", _userID);
    }
    /// <summary>
    /// Событие, происходящее после привязки данных к dtlUserInfo
    /// </summary>
    protected void dtlUserInfo_DataBound(object sender, EventArgs e)
    {
        Address address = AddressRepository.GetUserAddress(_userID);
        PersonalInfo personalInfo = PersonalInfoRepository.GetUserInfo(_userID);
        #region // Получение ссылок на элементы dtlUserInfo
        Label lblBirthday = dtlUserInfo.FindControl("lblBirthday") as Label;
        Label lblPhone = dtlUserInfo.FindControl("lblPhone") as Label;
        Label lblSex = dtlUserInfo.FindControl("lblSex") as Label;
        Label lblCountry = dtlUserInfo.FindControl("lblCountry") as Label;
        Label lblCity = dtlUserInfo.FindControl("lblCity") as Label;
        Label lblArea = dtlUserInfo.FindControl("lblArea") as Label;
        Label lblStreet = dtlUserInfo.FindControl("lblStreet") as Label;
        Label lblHome = dtlUserInfo.FindControl("lblHome") as Label;
        Label lblApartment = dtlUserInfo.FindControl("lblApartment") as Label;
        #endregion
        // Записываем в таблицу адресные данные пользователя, если они существуют
        if (!address.IsDeleted)
        {
            if (SetValueOrInvisible(dtlUserInfo, 0, lblBirthday, personalInfo.Birthday.ToString()))
            {
                lblBirthday.Text = lblBirthday.Text.Split()[0];
            }
            if (SetValueOrInvisible(dtlUserInfo, 1, lblSex, personalInfo.Sex.ToString()))
            {
                lblSex.Text = EnumsHelper.ToString((Sex)Convert.ToInt32(lblSex.Text));
            }
            SetValueOrInvisible(dtlUserInfo, 2, lblPhone, personalInfo.Phone);
            Int32? countryID = address.CountryID;
            if (countryID != null)
            {
                SetValueOrInvisible(dtlUserInfo, 3, 
                    lblCountry, CountryRepository.GetCountryName(Convert.ToInt32(countryID)));
            }
            Int32? cityID = address.CityID;
            if (cityID != null)
            {
                SetValueOrInvisible(dtlUserInfo, 4,
                    lblCity, CityRepository.GetCityName(Convert.ToInt32(cityID)));
            }
            SetValueOrInvisible(dtlUserInfo, 5, lblArea, address.Area);
            SetValueOrInvisible(dtlUserInfo, 6, lblStreet, address.Street);
            SetValueOrInvisible(dtlUserInfo, 7, lblHome, address.Home);
            SetValueOrInvisible(dtlUserInfo, 8, lblApartment, address.Apartment);
            
        }
    }

    /// <summary>
    /// Записываем в lbl принадлежащий dtl значение value, при условии, 
    /// что оно существует, иначе скрываем строку с номером row.
    /// В случае успешного присвоения значения возвращаем true, иначе - false
    /// </summary>
    protected Boolean SetValueOrInvisible(DetailsView dtl, Int32 row, Label lbl, String value)
    {
        if (value != null && lbl != null && value != string.Empty)
        {
            lbl.Text = value;
            return true;
        }
        else
        {
            dtl.Rows[row].Visible = false;
            return false;
        }
    }

    /// <summary>
    /// Действие по нажатию на кнопку изменения статус-сообщения
    /// </summary>
    protected void btnStatusMessage_Click(object sender, EventArgs e)
    {
        tbxStatusMessage.Text = btnStatusMessage.Text;

        ddlStatus.SelectedValue = StatusRepository.GetStatusID(_userID).ToString();
        StatusPanelEditMode(true);
    }

    /// <summary>
    /// Действие по нажатию на кнопку сохранения статус-сообщения
    /// </summary>
    protected void btnSaveStatusMessage_Click(object sender, ImageClickEventArgs e)
    {
        btnStatusMessage.Text = tbxStatusMessage.Text;
        if (btnStatusMessage.Text == String.Empty)
        {
            btnStatusMessage.Text = "Not Set";
        }
        StatusRepository.SetStatusMessage(_userID, 
            btnStatusMessage.Text, (UserStatus)Convert.ToInt32(ddlStatus.SelectedValue));
        StatusPanelEditMode(false);
    }

    /// <summary>
    /// Привязка данных к стене
    /// </summary>
    protected void grdWall_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        GridViewRow grdRowItem = e.Row;
        // По идентификаторам выводить имена пользователей оставивших сообщения
        Label lblFromID = grdRowItem.FindControl("lblFromID") as Label;
        if (lblFromID != null)
        {
            Guid fromID = Guid.Parse(lblFromID.Text);
            PersonalInfo personalInfo = PersonalInfoRepository.GetUserInfo(_userID);
            lblFromID.Text = String.Format("{0:s} {1:s}", personalInfo.FirstName, personalInfo.LastName);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    protected void ddlStatus_DataBinding(object sender, EventArgs e)
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
    /// Установка состояния редактирования для панели изменения статуса 
    /// </summary>
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
}