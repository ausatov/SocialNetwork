namespace RuzWizardsSocialNetworkApplication.WebServices
{
    #region Using
    using SocialNetwork.DataAccess.Enums;
    using SocialNetwork.DataAccess.Repositories;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Services;
    #endregion

    #region Web methods
    /// <summary>
    /// Summary description for ModifyService
    /// </summary>
    //[WebService(Namespace = "http://tempuri.org/")]
    //[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    //[System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class ModifyService : System.Web.Services.WebService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nickName"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="middleName"></param>
        /// <param name="sex"></param>
        /// <param name="phone"></param>
        /// <param name="birthday"></param>
        /// <param name="description"></param>
        [WebMethod(EnableSession = true)] 
        public void ModifyMainUserInfo(
            String nickName, 
            String firstName, 
            String lastName, 
            String middleName, 
            String sex, 
            String phone,
            String birthday,
            String description)
        {
            HttpContext.Current.Session["_userID"] = "e80cd2ac-8517-4e95-8321-3f4593d2106a";

            Guid userID = Guid.Empty;

            if (Guid.TryParse((String)Session["_userID"], out userID))
            {
                if (userID != Guid.Empty)
                {
                    DateTime? vBirthday = new DateTime?();
                    try
                    {
                        vBirthday = Convert.ToDateTime(birthday);
                    }
                    catch (Exception ex)
                    {
                        vBirthday = null;
                    }

                    Sex vGender = new Sex();
                    Sex? vSex = new Sex?();
                    if (Enum.TryParse(Enum.GetName(typeof(Sex), Convert.ToInt32(sex)), out vGender))
                    {
                        vSex = vGender;
                    }
                    else
                    {
                        vSex = null;
                    }

                    PersonalInfoRepository.ModifyPersonalInfo(
                        null,
                        true,
                        false,
                        userID,
                        nickName,
                        firstName,
                        lastName,
                        middleName,
                        vSex,
                        phone,
                        vBirthday,
                        null,
                        description);
                }
            }
        }
    }
    #endregion
}
