namespace RuzWizardsSocialNetworkApplication
{
    using SocialNetwork.DataAccess.Entity;
    using SocialNetwork.DataAccess.Repositories;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Services;
    using System.Web.Script.Services;

    /// <summary>
    /// Summary description for AutocompleteService.
    /// </summary>
    [System.Web.Script.Services.ScriptService]
    public class AutocompleteService : System.Web.Services.WebService
    {

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<String> GetCustomers(Guid userID, String prefix)
        {
            IEnumerable<Friend> friendsList = FriendRepository.GetUserFriends(userID);
            List<String> friends = friendsList
                .Select(x => x.ID.ToString()).ToList();


            return friends;
            
        }
    }
}
