namespace RuzWizardsSocialNetworkApplication.WebServices
{
    using SocialNetwork.DataAccess.Entity;
    using SocialNetwork.DataAccess.Repositories;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Services;

    /// <summary>
    /// Summary description for FillItemsService.
    /// </summary>
    [System.Web.Script.Services.ScriptService]
    public class FillItemsService : System.Web.Services.WebService
    {
        /// <summary>
        /// Name of default 'noimage' photo.
        /// </summary>
        private const String _defaultAvatarImage = "no_photo.jpg";

        /// <summary>
        /// Return List of cities of the current country.
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [WebMethod]
        public List<KeyValuePair<Guid, String>> GetAllCountryCities(Guid countryID)
        {
            return AddressRepository.GetAllCountryCities(countryID)
                .Select(s => new KeyValuePair<Guid, String>(s.CityID, s.Name))
                .ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [WebMethod(EnableSession = true)] 
        public String GetUploadedAvatarImage()
        {
            HttpContext.Current.Session["_userID"] = "e80cd2ac-8517-4e95-8321-3f4593d2106a";

            Guid userID = Guid.Empty;

            if (Guid.TryParse((String)Session["_userID"], out userID))
            {
                if (userID != Guid.Empty)
                {
                    return PersonalInfoRepository.GetUserInfo(userID)
                        .Select(s => s.ImagePath).FirstOrDefault();
                }
            }
            
            return _defaultAvatarImage;
        }
    }
}