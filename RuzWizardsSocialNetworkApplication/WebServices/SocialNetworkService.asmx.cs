namespace RuzWizardsSocialNetworkApplication.WebServices
{
    using RuzWizardsSocialNetworkApplication.Constants;
    using SocialNetwork.DataAccess.Entity;
    using SocialNetwork.DataAccess.Repositories;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Services;
    using System.Web.Script.Services;
    using System.Web.Script.Serialization;

    /// <summary>
    ///  SocialNetworkService.
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]

    /// <summary>
    ///  Class for SocialNetworkService.
    /// </summary>
    public class SocialNetworkService : System.Web.Services.WebService
    {
        /// <summary>
        /// Modify 
        /// </summary>
        /// <param name="banReason"></param>
        /// <param name="toDate"></param>
        /// <param name="banId"></param>
        [WebMethod]
        public void UpdateBan(Guid banID, String banReason, DateTime toDate)
        {
            BanRepository.UpdateBan(banID, banReason, toDate);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="banId"></param>
        [WebMethod]
        public void DeleteBan(Guid banID)
        {
            BanRepository.DeleteBan(banID);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mail"></param>
        /// <returns></returns>
        [WebMethod]
        public List<SocialNetwork.DataAccess.Entity.User> FetchEmailList(String userEmail)
        {
            var fetchEmail = UserRepository.GetAllUsers()
                .Where(w => w.Email.ToLower().StartsWith(userEmail.ToLower()));
            return fetchEmail.ToList();
        }    

        /// <summary>
        /// Get all user's bans.
        /// </summary>
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<KeyValuePair<Guid, String>> GetAllBans(String userEmail)
        {
            Guid userID = UserRepository.GetUserID(userEmail);
            var banList = BanRepository.GetUserBans(userID);
            return banList
                .Select(s => new KeyValuePair<Guid, String>(
                    s.ID,
                    (String.Join("-", s.FromDate.ToString(Constants._dateFormat), s.ToDate.ToString(Constants._dateFormat))))
                    ).ToList();

        }

        /// <summary>
        /// All friends of current user.
        /// </summary>
        [WebMethod]
        public List<KeyValuePair<Guid, String>> GetAllFriends(Guid userID)
        {
            var friendList = FriendRepository.GetUserFriends(userID);
            return friendList
                .Select(s => new KeyValuePair<Guid, String>(
                    s.FriendID,
                    PersonalInfoRepository.GetUserInfo(s.FriendID).LastName)).ToList();
        }

        /// <summary>
        /// Get ban's object
        /// </summary>
        //[WebMethod]
        //public KeyValuePair<Guid, String> GetBan(Guid banId)
        //{

        //    SocialNetwork.DataAccess.Entity.Ban banObj = BanRepository.GetBanInfo(banId);
        //    return (new KeyValuePair<Guid, String>(banObj.ID, (banObj.FromDate + "-" + banObj.ToDate).ToString()));
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="banID"></param>
        /// <returns></returns>
        [WebMethod]
        public Ban GetBan(Guid banID)
        {
            Ban ban = BanRepository.GetBanInfo(banID);
            return ban;
        }

        /// <summary>
        /// Get user's object.
        /// </summary>
        [WebMethod]
        public KeyValuePair<Guid, String> GetUser(Guid userID)
        {
            var userObject = UserRepository.GetUserInfo(userID);

            return (new KeyValuePair<Guid, String>(userObject.UserID, userObject.Email));
        }

        /// <summary>
        /// Get all user's friend invitations.
        /// </summary>
        [WebMethod]
        public List<KeyValuePair<Guid, String>> GetFriendInvitations(Guid userID)
        {
            var friendInvList = FriendInvitationsRepository.GetUserFriends(userID);

            return friendInvList
                .Select(s => new KeyValuePair<Guid, String>(s.InvitationID, s.Message)).ToList();
        }
    }
}
