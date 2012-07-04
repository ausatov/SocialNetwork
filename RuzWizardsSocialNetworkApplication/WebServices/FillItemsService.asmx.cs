namespace RuzWizardsSocialNetworkApplication.WebServices
{
    #region Using
    using SocialNetwork.DataAccess.Entity;
    using SocialNetwork.DataAccess.Repositories;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Services;
    #endregion

    /// <summary>
    /// Summary description for FillItemsService.
    /// </summary>
    [System.Web.Script.Services.ScriptService]
    public class FillItemsService : System.Web.Services.WebService
    {
        #region Constants
        /// <summary>
        /// Name of default 'noimage' photo.
        /// </summary>
        private const String _defaultAvatarImage = "no_photo.jpg";
        #endregion

        #region Web methods
        /// <summary>
        /// Return List of cities of the current country.
        /// </summary>
        /// <param name="countryID">Country identificator.</param>
        /// <returns>List of country cities.</returns>
        [WebMethod]
        public List<KeyValuePair<Guid, String>> GetAllCountryCities(Guid countryID)
        {
            return AddressRepository.GetAllCountryCities(countryID)
                .Select(s => new KeyValuePair<Guid, String>(s.CityID, s.Name))
                .ToList();
        }

        /// <summary>
        /// Get path to user avatar image.
        /// </summary>
        /// <returns>Path to user avatar image.</returns>
        [WebMethod(EnableSession = true)] 
        public String GetUploadedAvatarImage(Guid? id = null)
        {
            HttpContext.Current.Session["_userID"] = "e80cd2ac-8517-4e95-8321-3f4593d2106a";

            //Guid userID = (id == null) ? Guid.Empty : (Guid)id;

            Guid userID = (id == Guid.Empty) ? Guid.Empty : (Guid)id;

            if (userID == Guid.Empty)
            {
                if (Guid.TryParse((String)Session["_userID"], out userID))
                {
                    return PersonalInfoRepository.GetUserInfo(userID).ImagePath;
                }
            }
            else
            {
                return PersonalInfoRepository.GetUserInfo(userID).ImagePath;
            }

            return _defaultAvatarImage;
        }
        #endregion
    }
}