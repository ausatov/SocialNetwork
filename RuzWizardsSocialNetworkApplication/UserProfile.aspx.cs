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


public partial class UserProfile : System.Web.UI.Page
{
    //protected RusWizardsSocialNetworkDBEntities _DB = new 
    // Тут должен быть идентификатор текущего пользователя
    protected Guid _userID = Guid.Empty;
    protected PersonalInfoRepository _personalInfoRepository = new PersonalInfoRepository();
    protected StatusRepository _statusRepository = new StatusRepository();

    protected void Page_Load(object sender, EventArgs e)
    {
        //if ()

        // обновляем данные в справочнике 
        //(смысла нет, т.к. названия стран не должны изменяться впринципе)
        //db = new RusWizardsSocialNetworkDBEntities();



        // Тут должен быть идентификатор текущего пользователя
        //if (Guid.TryParse("e80cd2ac-8517-4e95-8321-3f4593d2106a", out _userID))
        //{
        //}

        ////edsWall.Where = "it.ToID = " + _userID.ToString();

        //if (!Page.IsPostBack)
        //{
        //    ddlStatus.DataBind();
        //}

        //if (Page.IsPostBack)
        //{
        //    StatusPanelEditMode(false);
        //}

        //lblUserName.Text = _personalInfoRepository.GetFullUserName(_userID);

        //btnStatusMessage.Text = _statusRepository.GetStatusMessage(_userID);
        //if (btnStatusMessage.Text.Length == 0)
        //{
        //    btnStatusMessage.Text = "Not set";
        //} 
        
        //#region // Узнаем статус и имя текущего пользователя

        
        //lblHeadUserStatus.Text = _statusRepository.GetStatusName(_userID);

        //lblHeadUserName.Text = _personalInfoRepository.GetFullUserName(_userID);

        //#endregion
    }

    /// <summary>
    /// Событие, происходящее после привязки данных к dtlUserInfo
    /// </summary>
    protected void dtlUserInfo_DataBound(object sender, EventArgs e)
    {
        // Выводим словесное значение названия страны, указанной пользователем 
        /*Label lblCountry = dtlUserInfo.FindControl("lblCountryID") as Label;
        if (lblCountry != null)
        {
            // Если страна не указана (ID = 0)
            if (lblCountry.Text == "0")
            {
                lblCountry.Text = "Not defined";
            }
            // иначе записываем в lblCountry название страны, соответствующее ее ID
            
            /*else
            {
                Int32 countryID = Convert.ToInt32(lblCountry.Text);
                lblCountry.Text =
                    _DB.Countries
                    .Where(x => x.CountryID == countryID)
                    .Select(x => x.Name).FirstOrDefault();
            }
        }*/

        // Выводим словесное значение названия города, указанного пользователем 
        /*Label lblCity = dtlUserInfo.FindControl("lblCityID") as Label;
        if (lblCity != null)
        {
            if (lblCity.Text == "0")
            {
                lblCity.Text = "Not defined";
            }
            
            /*else
            {
                Int32 cityID = Convert.ToInt32(lblCity.Text);
                lblCity.Text =
                    _DB.Cities
                    .Where(x => x.CityID == cityID)
                    .Select(x => x.Name).FirstOrDefault();
            }
        }*/

        // Выводим словесное значение пола пользователя в зависимости 
        // от содержания текстовой записи lblSex
        Label lblSex = dtlUserInfo.FindControl("lblSex") as Label;
        if (lblSex != null)
        {
            lblSex.Text = "fdsfsd";
        }
        /*Label lblSex = dtlUserInfo.FindControl("lblSex") as Label;
        if (lblSex != null)
        {
            switch (lblSex.Text)
            {
                case "True":
                    {
                        lblSex.Text = "Man";
                    } break;
                case "False":
                    {
                        lblSex.Text = "Woman";
                    } break;
                default:
                    {
                        lblSex.Text = "Not selected";
                    } break;
            }
        }*/
    }

    /// <summary>
    /// Действие по нажатию на кнопку сохранения статус-сообщения
    /// </summary>
    protected void btnSaveStatusMessage_Click(object sender, ImageClickEventArgs e)
    {

        btnStatusMessage.Text = tbxStatusMessage.Text;
        if (btnStatusMessage.Text.Length == 0)
        {
            btnStatusMessage.Text = "Not Set";
        }
        _statusRepository.SetStatusMessage(_userID, btnStatusMessage.Text, (UserStatus)Convert.ToInt32(ddlStatus.SelectedValue));

        StatusPanelEditMode(false);
    }

    /// <summary>
    /// Действие по нажатию на кнопку изменения статус-сообщения
    /// </summary>
    protected void btnStatusMessage_Click(object sender, EventArgs e)
    {
        tbxStatusMessage.Text = btnStatusMessage.Text;
        
        ddlStatus.SelectedValue = _statusRepository.GetStatusID(_userID).ToString();
        StatusPanelEditMode(true);
    }

    /// <summary>
    /// Привязка данных к стене
    /// </summary>
    protected void grdWall_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        GridViewRow grdRowItem = e.Row;

        // По идентификаторам выводить имена пользователей ,оставивших сообщения
        Label lblFromID = grdRowItem.FindControl("lblFromID") as Label;
        
        if (lblFromID != null)
        {
            Guid fromID = Guid.Parse(lblFromID.Text);

            PersonalInfo personalInfo = _personalInfoRepository.GetUserInfoByID(_userID);

            lblFromID.Text = String.Format("{0:s} {1:s}", personalInfo.FirstName, personalInfo.LastName);
                
                
        }
    }
    protected void dtlUserInfo_PageIndexChanging(object sender, DetailsViewPageEventArgs e)
    {

    }

    protected void ddlStatus_DataBinding(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
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
    }

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

