// -----------------------------------------------------------------------
// <copyright file="AddressRepository.cs" company="RusWizards">
// Author: Mankevich M.V. 
// Date: 29.06.12
// </copyright>
// -----------------------------------------------------------------------

#region Using
using AjaxControlToolkit;
using RuzWizardsSocialNetworkApplication.Constants;
using SocialNetwork.DataAccess.Entity;
using SocialNetwork.DataAccess.Enums;
using SocialNetwork.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
#endregion

/// <summary>
/// EditProfile class.
/// </summary>
public partial class EditProfile : System.Web.UI.Page
{
    #region Constants
    /// <summary>
    /// Default value to not set items.
    /// </summary>
    private const String _defaultUnsetName = "-- Select --";

    /// <summary>
    /// Max byte size of uploaded file.
    /// </summary>
    private const Int32 _defaultUploadLimit = 5242880;

    /// <summary>
    /// List of allowed extensions.
    /// </summary>
    private readonly List<String> allowedFileExtensions = new List<String>
    {
        ".jpg",
        ".jpeg",
        ".png"
    };
    #endregion

    #region Private fields
    /// <summary>
    /// Current user identifier.
    /// </summary>
    private Guid _userID = Guid.Empty;
    #endregion

    #region Page handlers
    /// <summary>
    /// PageLoad event handler.
    /// </summary>
    /// <param name="sender">Object sender.</param>
    /// <param name="e">EventArgs e.</param>
    protected void Page_Load(Object sender, EventArgs e)
    {
        Guid.TryParse("e80cd2ac-8517-4e95-8321-3f4593d2106a", out this._userID);
        if (!Page.IsPostBack)
        {
            PersonalInfo personalInfo = PersonalInfoRepository.GetUserInfo(this._userID);
            fvMain.DataSource = new List<PersonalInfo> { personalInfo };
            fvAddress.DataSource = new List<Address> { AddressRepository.GetUserAddress(this._userID) };
            imgAvatar.ImageUrl = personalInfo.ImagePath;
            if (this.imgAvatar.ImageUrl == Constants._defaultAvatarImage)
            {
                imgAvatar.ImageUrl = String.Concat(Constants._defaultPhotoPath, Constants._defaultAvatarImage);
            }
            else
            {
                this.imgAvatar.ImageUrl = String.Concat(Constants._defaultPhotoPath, imgAvatar.ImageUrl);
            }
            Page.DataBind();
        }
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
                tbxBirthday.Text = birthday.ToString(Constants._dateFormat);
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
        // Get droplist element from paren control.
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
    /// Save changes in main settings.
    /// </summary>
    /// <param name="sender">Object sender : fvMain.</param>
    /// <param name="e">FormViewUpdateEventArgs e.</param>
    protected void OnMainItemUpdating(Object sender, FormViewUpdateEventArgs e)
    {
        #region Inner controls
        TextBox tbxNickName = fvMain.FindControl("tbxNickName") as TextBox;
        TextBox tbxFirstName = fvMain.FindControl("tbxFirstName") as TextBox;
        TextBox tbxLastName = fvMain.FindControl("tbxLastName") as TextBox;
        TextBox tbxMiddleName = fvMain.FindControl("tbxMiddleName") as TextBox;
        TextBox tbxPhone = fvMain.FindControl("tbxPhone") as TextBox;
        TextBox tbxBirthday = fvMain.FindControl("tbxBirthday") as TextBox;
        TextBox tbxDescription = fvMain.FindControl("tbxDescription") as TextBox;
        DropDownList ddlSex = fvMain.FindControl("ddlSex") as DropDownList;
        #endregion

        DateTime birthday;
        DateTime? vBirthday = null;

        // Convert date to default date format.
        if (DateTime.TryParseExact(
            tbxBirthday.Text, 
            Constants._dateFormat, 
            CultureInfo.InvariantCulture, 
            DateTimeStyles.None, 
            out birthday))
        {
            vBirthday = birthday;
        }

        Sex sex = new Sex();
        Sex? vSex = null;
        Int32 ddlSexSelectedValue;
        if (Int32.TryParse(ddlSex.SelectedValue, out ddlSexSelectedValue))
        {
            if (Enum.TryParse(
                Enum.GetName(
                typeof(Sex), 
                Convert.ToInt32(ddlSex.SelectedValue)), 
                out sex))
            {
                vSex = sex; 
            }
        }

        PersonalInfoRepository.ModifyPersonalInfo(
            null,
            true,
            false,
            this._userID,
            tbxNickName.Text.Trim(),
            tbxFirstName.Text.Trim(),
            tbxLastName.Text.Trim(),
            tbxMiddleName.Text.Trim(),
            vSex,
            tbxPhone.Text.Trim(),
            vBirthday,
            null,
            tbxDescription.Text.Trim());
    }

    /// <summary>
    /// Save changes in address settings.
    /// </summary>
    /// <param name="sender">Object sender : fvAddress.</param>
    /// <param name="e">FormViewUpdateEventArgs e.</param>
    protected void OnAddressItemUpdating(Object sender, FormViewUpdateEventArgs e)
    {
        DropDownList ddlCountry = fvAddress.FindControl("ddlCountry") as DropDownList;
        DropDownList ddlCity = fvAddress.FindControl("ddlCity") as DropDownList;
        TextBox tbxArea = fvAddress.FindControl("tbxArea") as TextBox;
        TextBox tbxStreet = fvAddress.FindControl("tbxStreet") as TextBox;
        TextBox tbxHome = fvAddress.FindControl("tbxHome") as TextBox;
        TextBox tbxApartment = fvAddress.FindControl("tbxApartment") as TextBox;
        Guid country;
        Guid? vCountry = null;
        Guid city;
        Guid? vCity = null;
        if (Guid.TryParse(ddlCountry.SelectedValue, out country))
        {
            vCountry = country;
        }

        if (Guid.TryParse(ddlCity.SelectedValue, out city))
        {
            vCity = city;
        }

        AddressRepository.ModifyAddress(
            null,
            true,
            this._userID,
            vCountry,
            vCity,
            tbxArea.Text.Trim(),
            tbxStreet.Text.Trim(),
            tbxHome.Text.Trim(),
            tbxApartment.Text.Trim());
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
                // File size limit exceeded (_defaultUploadLimit/1048576 Mb);
            }
            else if (this.allowedFileExtensions.Contains(fileExtension))
            {
                // Make file path string.
                String destDir = Server.MapPath(Constants._defaultPhotoPath);
                String keyName = this._userID.ToString();
                String destPath = Path.Combine(destDir, String.Concat(keyName, fileExtension));

                // Save photo-file on server.
                afu.PostedFile.SaveAs(destPath);

                // Save photo-path in database.
                PersonalInfoRepository.ModifyAvatar(
                    this._userID, String.Concat(keyName, fileExtension));
            }
        }
    }
    #endregion
}