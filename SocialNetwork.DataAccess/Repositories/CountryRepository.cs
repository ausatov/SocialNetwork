using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SocialNetwork.DataAccess;

namespace SocialNetwork.DataAccess.Repositories
{
    
    public class CountryRepository
    {
        public List<Country> country = new List<Country>();

        //----------------- вариант 1

        public List<Country> GetAllCountriesExample1()
        {
            // каждый раз создавать объект в инструкции using
            using (SocialNetworkDBEntities entity = new SocialNetworkDBEntities())
            {
                country = entity.Countries.ToList<Country>();
            }
            return country;
        }
    }
}
