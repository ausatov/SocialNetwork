namespace RuzWizardsSocialNetworkApplication.WebServices
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Services;
    using SocialNetwork.DataAccess;
    using SocialNetwork.DataAccess.Repositories;
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
        /// Get all user's bans.
        /// </summary>
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<KeyValuePair<Guid, DateTime>> GetAllBans(Guid userID)
        {
            var banList = BanRepository.GetUserBans(userID);
            return banList.Select(s => new KeyValuePair<Guid, DateTime>(s.ID, s.FromDate)).ToList();

        }

        /// <summary>
        /// All friends of current user.
        /// </summary>
        [WebMethod]
        public List<KeyValuePair<Guid, String>> GetAllFriends(Guid userID)
        {
            var friendList = FriendRepository.GetUserFriends(userID);
            return friendList.Select(s => new KeyValuePair<Guid, String>(s.FriendID,
            PersonalInfoRepository.GetUserInfo(s.FriendID).LastName)).ToList();
        }

        /// <summary>
        /// Get ban's object
        /// </summary>
        [WebMethod]
        public KeyValuePair<Guid, DateTime> GetBan(Guid banID)
        {
            var ban = BanRepository.GetBanInfo(banID);
            return (new KeyValuePair<Guid, DateTime>(ban.ID, ban.ToDate));
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

            return friendInvList.Select(s => new KeyValuePair<Guid, String>(s.InvitationID, s.Message)).ToList();
        }

        ///// <summary>
        ///// Get all user's messages.
        ///// </summary>
        //[WebMethod]
        //public List<Message> GetMessages(Guid userID)
        //{
        //    List<Message> _messageList = new List<Message>();
        //    using (SocialNetworkDBEntities record = new SocialNetworkDBEntities())
        //    {
        //        _messageList = record.Messages
        //            .Where(x => x.ToID.Equals(userID)).ToList<Message>();
        //    }
        //    return _messageList;
        //}

        ///// <summary>
        ///// Get message's object.
        ///// </summary>
        //[WebMethod]
        //public Message GetMessage(Guid messageID)
        //{
        //    Message _userObject = new Message();
        //    using (SocialNetworkDBEntities record = new SocialNetworkDBEntities())
        //    {
        //        _userObject = record.Messages
        //            .Where(x => x.MessageID.Equals(messageID)).FirstOrDefault<Message>();
        //    }
        //    return _userObject;
        //}
    }
}
