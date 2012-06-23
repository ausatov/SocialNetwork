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

/// <summary>
/// Усатов
/// </summary>
public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["reg"] != null)
        {
            pnlLogin.Visible = false;
            pnlRegistrtion.Visible = true;
            btnRegistration.ImageUrl = "~/App_Themes/MainSkin/img/buttons/snw_button_registration_disabled.png";
            btnRegistration.Enabled = false;
        }
        else
        {
            pnlLogin.Visible = true;
            pnlRegistrtion.Visible = false;
            btnRegistration.ImageUrl = "~/App_Themes/MainSkin/img/buttons/snw_button_registration.png";
            btnRegistration.Enabled = true; 
        }
    }

    /// <summary>
    /// Переход на станицу регистрации
    /// </summary>
    protected void btnRegistration_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("Login.aspx?Reg=1");
    }

    /// <summary>
    /// Установить выбор городов в соотвествии с выбранной страной
    /// </summary>
    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
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
    /// Загрузка фотографии
    /// </summary>
    protected void fuPhoto_UploadedComplete(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
    {

    }

    /// <summary>
    /// Подтверждение регистрации
    /// </summary>
    protected void btnRegCompleteRegistration_Click(object sender, ImageClickEventArgs e)
    {
        // это все будет сделано с помощью хранимой процедуры
        SqlConnection sqlConnection = new SqlConnection();
        sqlConnection.ConnectionString = 
            ConfigurationManager.ConnectionStrings["RusWizardsSocialNetworkDBConnectionString"]
            .ConnectionString;
        SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.CommandType = CommandType.StoredProcedure;
        sqlCommand.Connection = sqlConnection;
        sqlCommand.CommandText = "spInsUser";
        sqlCommand.Parameters.Add("@Email", SqlDbType.VarChar, 40);
        sqlCommand.Parameters["@Email"].Value = tbxRegEmail.Text;
        sqlCommand.Parameters.Add("@Password", SqlDbType.VarChar, 32);
        sqlCommand.Parameters["@Password"].Value = tbxRegPassword1.Text;
        sqlCommand.Parameters.Add("@FirstName", SqlDbType.VarChar, 24);
        sqlCommand.Parameters["@FirstName"].Value = tbxFirstName.Text;
        sqlCommand.Parameters.Add("@SecondName", SqlDbType.VarChar, 24);
        sqlCommand.Parameters["@SecondName"].Value = tbxSecondName.Text;
        sqlCommand.Parameters.Add("@FatherName", SqlDbType.VarChar, 24);
        sqlCommand.Parameters["@FatherName"].Value = tbxFatherName.Text;
        sqlCommand.Parameters.Add("@NickName", SqlDbType.VarChar, 36);
        sqlCommand.Parameters["@NickName"].Value = tbxNickName.Text;
        sqlCommand.Parameters.Add("@Sex", SqlDbType.Bit);
        switch (ddlSex.SelectedValue)
        {
            // Вариант не доступен для выбора, но возможен
            case "-1":
                {
                    sqlCommand.Parameters["@Sex"].Value = DBNull.Value;
                } break;
            // Мужской пол
            case "0":
                {
                    sqlCommand.Parameters["@Sex"].Value = 1;
                } break;
            // Женский пол
            case "1":
                {
                    sqlCommand.Parameters["@Sex"].Value = 0;
                } break;
        }
        sqlCommand.Parameters.Add("@Phone", SqlDbType.VarChar, 24);
        sqlCommand.Parameters["@Phone"].Value =  tbxPhone.Text;
        sqlCommand.Parameters.Add("@Country", SqlDbType.Int);
        if (ddlCountry.SelectedIndex > 0)
        {
            sqlCommand.Parameters["@Country"].Value = ddlCountry.SelectedIndex;
        }
        else
        {
            sqlCommand.Parameters["@Country"].Value = DBNull.Value;
        }
        sqlCommand.Parameters.Add("@City", SqlDbType.Int);
        if (ddlCity.SelectedIndex > 0 && ddlCountry.SelectedIndex > 0)
        {
            sqlCommand.Parameters["@City"].Value = ddlCountry.SelectedIndex;
        }
        else
        {
            sqlCommand.Parameters["@City"].Value = DBNull.Value;
        }
        sqlCommand.Parameters.Add("@Photo", SqlDbType.VarChar, 80);
        sqlCommand.Parameters["@Photo"].Value = "nophoto";
        try
        {
            sqlConnection.Open();
            sqlCommand.ExecuteNonQuery();
        }
        catch (SqlException ex)
        {
            //Label1.Text = ex.InnerException.Message;
        }
        catch (Exception ex)
        {
            //Label1.Text = ex.Message;
        }
        

        ////чтобы не загромождать обработчик события нажатия кнопки
        ////этот код будет вынесен в отдельный cs файл
        //{
        //    //Label1.Text = ex.Message;
        //}
        finally

        {
            sqlConnection.Close();
        }
    }
}