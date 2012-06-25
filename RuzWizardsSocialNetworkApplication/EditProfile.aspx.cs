using AjaxControlToolkit;
using SocialNetwork.DataAccess.Entity;
using SocialNetwork.DataAccess.Enums;
using SocialNetwork.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


/// <summary>
/// EditProfile class.
/// </summary>
public partial class EditProfile : System.Web.UI.Page
{
    /// <summary>
    /// Default value to not set items.
    /// </summary>
    private const String _defaultUnsetName = "-- Select --";

    /// <summary>
    /// Path to photo upload.
    /// </summary>
    private const String _defaultUploadPhotoPath = "~/Uploads/Photo/";

    /// <summary>
    /// Name of default 'noimage' photo.
    /// </summary>
    private const String _defaultAvatarImage = "no_photo.jpg";

    /// <summary>
    /// Max byte sise of uploaded file.
    /// </summary>
    private const Int32 _defaultUploadLimit = 5242880;

    /// <summary>
    /// Curent user identifacator.
    /// </summary>
    private Guid _userID = Guid.Empty;

    /// <summary>
    /// PageLoad event.
    /// </summary>
    /// <param name="sender">Object sender.</param>
    /// <param name="e">EventArgs e.</param>
    protected void Page_Load(Object sender, EventArgs e)
    {
        Guid.TryParse("e80cd2ac-8517-4e95-8321-3f4593d2106a", out this._userID);
        IEnumerable<PersonalInfo> personalInfo = PersonalInfoRepository.GetUserInfo(this._userID);
        fvMain.DataSource = personalInfo;
        fvAddress.DataSource = AddressRepository.GetUserAddress(this._userID);
        imgAvatar.ImageUrl = personalInfo.Select(s => s.ImagePath).FirstOrDefault();
        if (imgAvatar.ImageUrl == _defaultAvatarImage)
        {
            imgAvatar.ImageUrl = String.Join("", _defaultUploadPhotoPath, _defaultAvatarImage);
        }
        else
        {
            imgAvatar.ImageUrl = String.Join("", _defaultUploadPhotoPath, imgAvatar.ImageUrl);
        }
        Page.DataBind();
    }

    /// <summary>
    /// After bind data to fvMain.
    /// </summary>
    /// <param name="sender">Object sender : fvMain.</param>
    /// <param name="e">EventArgs e.</param>
    protected void OnMainDataBound(Object sender, EventArgs e)
    {
        TextBox tbxBirthday = fvMain.FindControl("tbxBirthday") as TextBox;
        if (tbxBirthday != null)
        {
            DateTime birthday = new DateTime();
            if (DateTime.TryParse(tbxBirthday.Text, out birthday))
            {
                tbxBirthday.Text = birthday.ToString("MM/dd/yyyy");
            }
            else
            {
                tbxBirthday.Text = String.Empty;
            }
        }
    }

    /// <summary>
    /// Bind data elements to droplist sex from Enum Sex.
    /// </summary>
    /// <param name="sender">Object sender : ddlSex.</param>
    /// <param name="e">EventArgs e.</param>
    protected void OnSexDataBinding(Object sender, EventArgs e)
    {
        DropDownList ddlSex = sender as DropDownList;
        String[] names = Enum.GetNames(typeof(Sex));
        Array values = Enum.GetValues(typeof(Sex));
        Dictionary<Int32, String> dictionary = new Dictionary<Int32, String>();
        dictionary.Add(0, _defaultUnsetName);
        for (Int32 i = 0; i < names.Length; i++)
        {
            dictionary.Add((Int32)values.GetValue(i), EnumsHelper.ToString((Sex)values.GetValue(i)));
        }
        ddlSex.DataSource = dictionary;
        ddlSex.DataTextField = "Value";
        ddlSex.DataValueField = "Key";
    }

    /// <summary>
    /// Before bind data elements to droplist country.
    /// </summary>
    /// <param name="sender">Object sender : ddlCountry.</param>
    /// <param name="e">EventArgs e.</param>
    protected void OnCountryDataBinding(Object sender, EventArgs e)
    {
        DropDownList ddlCountry = sender as DropDownList;
        ddlCountry.DataSource = AddressRepository.GetAllCountries();
    }

    /// <summary>
    /// Before bind data elements to droplist city.
    /// </summary>
    /// <param name="sender">Object sender : ddlCity.</param>
    /// <param name="e">EventArgs e.</param>
    protected void OnCityDataBinding(Object sender, EventArgs e)
    {
        // Get droplist elemet from paren control.
        DropDownList ddlCountry = fvAddress.FindControl("ddlCountry") as DropDownList;

        // If user have set country, bind cities from that country to second droplist.
        if (ddlCountry.SelectedIndex > 0)
        {
            DropDownList ddlCity = sender as DropDownList;
            Guid selectedCountry = Guid.Empty;
            if (Guid.TryParse(ddlCountry.SelectedValue, out selectedCountry))
            {
                ddlCity.DataSource = AddressRepository.GetAllCountryCities(selectedCountry);
            }
        }
    }

    /// <summary>
    /// Event after file uploaded.
    /// </summary>
    /// <param name="sender">Object ccAsyncFileUpload.</param>
    /// <param name="e">AsyncFileUploadEventArgs e.</param>
    protected void OnAsyncFileUploadedComplete(Object sender, AsyncFileUploadEventArgs e)
    {
        AsyncFileUpload afu = (AsyncFileUpload)sender;
        if (afu.HasFile)
        {
            String fileExtension = Path.GetExtension(afu.PostedFile.FileName).ToLower();

            // Check max uploaded file size.
            if (afu.PostedFile.ContentLength > _defaultUploadLimit)
            {
                // "Размер файла превышает установленное ограничение в _defaultUploadLimit/1048576 Мб";
            }
            else if (fileExtension.Equals(".jpg") || fileExtension.Equals(".jpeg") || fileExtension.Equals(".png"))
            {
                //Make file path string.
                String destDir = Server.MapPath(_defaultUploadPhotoPath);
                String keyName = _userID.ToString();
                String destPath = Path.Combine(destDir, String.Join("", keyName, fileExtension));

                //Save photo-file on server.
                afu.PostedFile.SaveAs(destPath);

                //Save photo-path in database.
                PersonalInfoRepository.UpdAvatar(
                    _userID, String.Join("", keyName, fileExtension));
            }
        }
    }
}