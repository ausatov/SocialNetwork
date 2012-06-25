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
    //[WebService(Namespace = "http://tempuri.org/")]
    //[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class FillItemsService : System.Web.Services.WebService
    {
        /// <summary>
        /// Return List of cities of the current country.
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [WebMethod]
        public List<City> GetAllCountryCities(Guid countryID)
        {
            return AddressRepository.GetAllCountryCities(countryID).ToList();
        }

        /// <summary>
        /// Return List of countries.
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public List<Country> GetAllCountries()
        {
            return AddressRepository.GetAllCountries().ToList();
        }
    }
}