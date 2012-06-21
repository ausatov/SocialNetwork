namespace RuzWizardsSocialNetworkApplication.WebServices
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Services;
    using SocialNetwork.DataAccess;
    using System.Web.Script.Services;
    using System.Web.Script.Serialization;
    /// <summary>
    ///  SocialNetworkService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class SocialNetworkService : System.Web.Services.WebService
    {
        [ScriptIgnore]
        List<Ban> _banList;
        /// <summary>
        /// Get all user's bans
        /// </summary>
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        
        public List<KeyValuePair<Guid, DateTime>> GetAllBans(Guid userID)
        {
            _banList = new List<Ban>();
            //using (
            SocialNetworkDBEntities record = new SocialNetworkDB();
            //)
            {
                _banList = record.Bans
                    .Where(x => x.UserID.Equals(userID)).ToList<Ban>();
                return _banList.Select(s=>new KeyValuePair<Guid, DateTime>(s.ID, s.FromDate)).ToList();
            }
        }

        /// <summary>
        /// All friends of current user
        /// </summary>
        [WebMethod]
        public List<Friend> GetAllFriends(Guid userID)
        {
            List<Friend> _friendList = new List<Friend>();
            using (SocialNetworkDBEntities record = new SocialNetworkDBEntities())
            {
                _friendList = record.Friends
                    .Where(x => x.UserID.Equals(userID)).ToList<Friend>();
            }
            return _friendList;
        }

        /// <summary>
        /// Get ban's object
        /// </summary>
        [WebMethod]
        public Ban GetBan(Guid banID)
        {
            Ban _banObject = new Ban();
            using (SocialNetworkDBEntities record = new SocialNetworkDBEntities())
            {
                _banObject = record.Bans
                    .Where(x => x.ID.Equals(banID)).FirstOrDefault<Ban>();
            }
            return _banObject;
        }

        /// <summary>
        /// Get user's object
        /// </summary>
        [WebMethod]
        public User GetUser(Guid userID)
        {
            User _userObject = new User();
            using (SocialNetworkDBEntities record = new SocialNetworkDBEntities())
            {
                _userObject = record.Users
                    .Where(x => x.UserID.Equals(userID)).FirstOrDefault<User>();
            }
            return _userObject;
        }

        /// <summary>
        /// Get all user's friends
        /// </summary>
        [WebMethod]
        public List<Friend> GetFriends(Guid userID)
        {
            List<Friend> _friendList = new List<Friend>();
            using (SocialNetworkDBEntities record = new SocialNetworkDBEntities())
            {
                _friendList = record.Friends
                    .Where(x => x.UserID.Equals(userID)).ToList<Friend>();
            }
            return _friendList;
        }

        /// <summary>
        /// Get all user's friend invitations
        /// </summary>
        [WebMethod]
        public List<FriendInvitation> GetFriendInvitations(Guid userID)
        {
            List<FriendInvitation> _friendInvList = new List<FriendInvitation>();
            using (SocialNetworkDBEntities record = new SocialNetworkDBEntities())
            {
                _friendInvList = record.FriendInvitations
                    .Where(x => x.ToID.Equals(userID)).ToList<FriendInvitation>();
            }
            return _friendInvList;
        }

        /// <summary>
        /// Get all user's messages
        /// </summary>
        [WebMethod]
        public List<Message> GetMessages(Guid userID)
        {
            List<Message> _messageList = new List<Message>();
            using (SocialNetworkDBEntities record = new SocialNetworkDBEntities())
            {
                _messageList = record.Messages
                    .Where(x => x.ToID.Equals(userID)).ToList<Message>();
            }
            return _messageList;
        }

        /// <summary>
        /// Get message's object.
        /// </summary>
        [WebMethod]
        public Message GetMessage(Guid messageID)
        {
            Message _userObject = new Message();
            using (SocialNetworkDBEntities record = new SocialNetworkDBEntities())
            {
                _userObject = record.Messages
                    .Where(x => x.MessageID.Equals(messageID)).FirstOrDefault<Message>();
            }
            return _userObject;
        }
    }
}
