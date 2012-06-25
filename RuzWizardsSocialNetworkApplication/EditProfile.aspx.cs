using SocialNetwork.DataAccess.Entity;
using SocialNetwork.DataAccess.Enums;
using SocialNetwork.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EditProfile : System.Web.UI.Page
{
    /// <summary>
    /// Curent user identifacator.
    /// </summary>
    private Guid _userID = Guid.Empty;


    protected void Page_Load(Object sender, EventArgs e)
    {
        Guid.TryParse("e80cd2ac-8517-4e95-8321-3f4593d2106a", out this._userID);

        fvMain.DataSource = PersonalInfoRepository.GetUserInfo(_userID);

        fvAddress.DataSource = AddressRepository.GetUserAddress(_userID);
        Page.DataBind();
    }

    protected void fvMain_DataBound(Object sender, EventArgs e)
    {
        TextBox tbxBirthday = fvMain.FindControl("tbxBirthday") as TextBox;
        if (tbxBirthday != null)
        {
            tbxBirthday.Text = String.Format("{0:MM/dd/yyyy}", DateTime.Parse(tbxBirthday.Text)); 
        }
    }

    protected void fvAddress_DataBinding(Object sender, EventArgs e)
    {
        
    }

    

    /// <summary>
    /// Bind data elements to droplist sex.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlSex_OnDataBinding(Object sender, EventArgs e)
    {
        DropDownList ddlSex = sender as DropDownList;
        String[] names = Enum.GetNames(typeof(Sex));
        Array values = Enum.GetValues(typeof(Sex));
        Dictionary<Int32, String> dictionary = new Dictionary<Int32, String>();
        dictionary.Add(0, "-- not select --");
        for (Int32 i = 0; i < names.Length; i++)
        {
            dictionary.Add((Int32)values.GetValue(i), EnumsHelper.ToString((Sex)values.GetValue(i)));
        }
        ddlSex.DataSource = dictionary;
        ddlSex.DataTextField = "Value";
        ddlSex.DataValueField = "Key";
    }

    protected void ddlCountry_DataBinding(Object sender, EventArgs e)
    {
        DropDownList ddlCountry = sender as DropDownList;
        ddlCountry.DataSource = AddressRepository.GetAllCountries();
    }

    protected void ddlCountry_OnSelectedIndexChanged(Object sender, EventArgs e)
    {
    }

    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        
    }

    protected void ddlCountry_TextChanged(object sender, EventArgs e)
    {
        //DropDownList ddlCity = fvAddress.FindControl("ddlCity") as DropDownList;

        //ddlCity.Items.Clear();
        //ddlCity.Items.Add(new ListItem("-- Select City --", "0"));


        //DropDownList ddlCountry = sender as DropDownList;
        //if (ddlCity != null && ddlCountry.SelectedValue != "0")
        //{
        //    Guid country = Guid.Empty;
        //    if (Guid.TryParse(ddlCountry.SelectedValue, out country))
        //    {
        //        ddlCity.DataSource = AddressRepository.GetAllCountryCities(country);
        //        ddlCity.DataBind();
        //    }
        //}
    }
}
