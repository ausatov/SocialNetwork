namespace SocialNetwork.DataAccess.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    
    /// <summary>
    /// 
    /// </summary>
    public static class CityRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cityID"></param>
        /// <returns></returns>
        public static String GetCityName(Int32 cityID)
        {
            String cityName = String.Empty;
            using (SocialNetworkDBEntities record = new SocialNetworkDBEntities())
            {
                cityName = record.Cities
                    .Where(x => x.CityID == cityID)
                    .Select(x => x.Name)
                    .First();
            }
            return cityName;
        }
    }
}
