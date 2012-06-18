namespace SocialNetwork.DataAccess.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using SocialNetwork.DataAccess;

    /// <summary>
    /// 
    /// </summary>
    public static class CountryRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="countryID"></param>
        /// <returns></returns>
        public static String GetCountryName(Int32 countryID)
        {
            String countryName = String.Empty;
            using (SocialNetworkDBEntities record = new SocialNetworkDBEntities())
            {
                countryName = record.Countries
                    .Where(x => x.CountryID == countryID)
                    .Select(x => x.Name)
                    .First();
            }
            return countryName;
        }
    }
}
