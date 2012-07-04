namespace RuzWizardsSocialNetworkApplication.WebServices
{
    #region Using
    using RuzWizardsSocialNetworkApplication.Constants;
    using RuzWizardsSocialNetworkApplication.UserControls;
    using SocialNetwork.DataAccess.Entity;
    using SocialNetwork.DataAccess.Repositories;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Web;
    using System.Web.Services;
    using System.Web.Script.Services;
    using System.Web.Script.Serialization;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    #endregion

    /// <summary>
    ///  SocialNetworkService.
    /// </summary>
    //[WebService(Namespace = "http://tempuri.org/")]
    //[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]

    /// <summary>
    ///  Class for SocialNetworkService.
    /// </summary>
    public class SocialNetworkService : System.Web.Services.WebService
    {
        /// <summary>
        ///  Get current user roles by user ID.
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<KeyValuePair<Guid, String>> GetUserRoles(String userEmail)
        {
            Guid userID = UserRepository.GetUserID(userEmail);
            var roleList = UserRoleRepository.GetUserRoles(userID);
            return roleList
                .Select(s => new KeyValuePair<Guid, String>(
                    s.UserRoleID,
                    s.Name
                    ))
                .ToList();
        }

        /// <summary>
        /// Gets role object.
        /// </summary>
        /// <param name="banID"></param>
        /// <returns></returns>
        [WebMethod]
        public UserRole GetRole(Guid roleID)
        {
            UserRole role = UserRoleRepository.GetRoleInfo(roleID);
            return role;
        }

        /// <summary>
        /// Return the list of all roles.
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<KeyValuePair<Guid, String>> GetAllRoles()
        {
            var roleList = UserRoleRepository.GetAllRoles();
            return roleList
                .Select(s => new KeyValuePair<Guid, String>(
                    s.UserRoleID,
                    s.Name
                    ))
                .ToList();
        }

        /// <summary>
        /// Get data for scrolling on friends page.
        /// </summary>
        /// <param name="userID">User's identeficator.</param>
        /// <param name="startIndex">Index for start select rows.</param>
        /// <returns></returns>
        [WebMethod]
        public String GetScrollData(Guid userID, Int32 startIndex)
        {
            StringBuilder ucHtmlText = new StringBuilder();
            IEnumerable<SocialNetwork.DataAccess.Entity.Friend>
                friendList = FriendRepository.GetUserFriendsWithParams(userID, startIndex, 1, false);
            foreach (SocialNetwork.DataAccess.Entity.Friend frItem in friendList)
            {
                Page page = new Page();
                FriendItem userControl = (FriendItem)page.LoadControl("~/UserControls/FriendItem.ascx");
                userControl.EnableViewState = false;
                userControl.FriendID = frItem.FriendID;
                HtmlForm form = new HtmlForm();
                form.Controls.Add(userControl);
                page.Controls.Add(form);

                StringWriter textWriter = new StringWriter();
                HttpContext.Current.Server.Execute(page, textWriter, false);
                ucHtmlText.Append(textWriter.ToString());
            }
            return ucHtmlText.ToString();
        }
        /// <summary>
        /// Modify  ban's object.
        /// </summary>
        /// <param name="banReason">The reason of ban.</param>
        /// <param name="toDate">Date untill ban will be active.</param>
        /// <param name="banId">Ban's identeficator.</param>
        [WebMethod]
        public void UpdateBan(String banReason, DateTime toDate, Guid banID)
        {
            DateTime fefd = toDate;
            BanRepository.ModifyBan(banID, false, null, null, banReason, null, toDate, null);
        }

        /// <summary>
        /// Remove  ban's object.
        /// </summary>
        /// <param name="banId">Ban's identeficator</param>
        [WebMethod]
        public void DeleteBan(Guid banID)
        {
            BanRepository.ModifyBan(banID, false, null, null, null, null, null, true);
        }

        /// <summary>
        /// Fetches Email list.
        /// </summary>
        /// <param name="UserEmail">User's Email</param>
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
                    )
                .ToList();

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
