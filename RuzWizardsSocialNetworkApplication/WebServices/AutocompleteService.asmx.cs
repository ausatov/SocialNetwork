namespace RuzWizardsSocialNetworkApplication
{
    #region Using
    using SocialNetwork.DataAccess.Entity;
    using SocialNetwork.DataAccess.Repositories;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Script.Services;
    using System.Web.Services;
    #endregion

    /// <summary>
    /// Summary description for AutocompleteService.
    /// </summary>
    [System.Web.Script.Services.ScriptService]
    public class AutocompleteService : System.Web.Services.WebService
    {
        #region Web methods
        /// <summary>
        /// Get list with friends.
        /// </summary>
        /// <param name="userID">User identifier.</param>
        /// <param name="prefix">Prefix string.</param>
        /// <returns>List with users identifiers.</returns>
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<String> GetFriends(Guid userID, String prefix)
        {
            IEnumerable<Friend> friendsList = FriendRepository.GetUserFriends(userID);
            List<String> friends = friendsList
                .Select(x => x.ID.ToString()).ToList();
            return friends;
        }
        #endregion
    }
}
