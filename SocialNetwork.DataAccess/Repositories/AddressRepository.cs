namespace SocialNetwork.DataAccess.Repositories
{
    using SocialNetwork.DataAccess.Entity;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Work with dbo.Address, dbo.Country, dbo.City. 
    /// </summary>
    public static class AddressRepository
    {
        /// <summary>
        /// Default name of country.
        /// </summary>
        private const String _defaultCountryName = "-- not select country --";

        /// <summary>
        /// Default name of city.
        /// </summary>
        private const String _defaultCityName = "-- not select city --";

        /// <summary>
        /// Get Address of current user.
        /// </summary>
        /// <param name="userID">User identificator.</param>
        /// <returns>Object Address.</returns>
        public static IEnumerable<Address> GetUserAddress(Guid userID)
        {
            IEnumerable<Address> recordList = null;
            using (SocialNetworkDBEntities record = new SocialNetworkDBEntities())
            {
                recordList = record.Addresses
                    .Where(w => w.PersonalInfo.UserID.Equals(userID) && !w.IsDeleted)
                    .Select(s => new Address
                        {
                            ID = s.ID,
                            UserInfoID = s.UserInfoID,
                            CountryID = s.CountryID,
                            CityID = s.CityID,
                            Area = s.Area,
                            Street = s.Street,
                            Home = s.Home,
                            Apartment = s.Apartment,
                            IsDeleted = s.IsDeleted
                        }).ToList();
            }
            return recordList;
        }

        /// <summary>
        /// Get list, consists all countries.
        /// </summary>
        /// <returns>All countries.</returns>
        public static IEnumerable<Country> GetAllCountries()
        {
            IEnumerable<Country> recordList = null;
            using (SocialNetworkDBEntities record = new SocialNetworkDBEntities())
            {
                recordList = record.Countries
                    .Select(s => new Country
                    {
                        CountryID = s.CountryID,
                        Name = s.Name
                    }).ToList();
            }
            return recordList;
        }

        /// <summary>
        /// Get list, consists all cities.
        /// </summary>
        /// <returns>All cities.</returns>
        public static IEnumerable<City> GetAllCountryCities(Guid countryID)
        {
            IEnumerable<City> recordList = null;
            using (SocialNetworkDBEntities record = new SocialNetworkDBEntities())
            {
                recordList = record.Cities
                    .Where(w => w.CountryID.Equals(countryID))
                    .Select(s => new City
                    {
                        CityID = s.CityID,
                        CountryID = s.CountryID,
                        Name = s.Name
                    }).ToList();
            }
            return recordList;
        }

        /// <summary>
        /// Get name of user country.
        /// </summary>
        /// <param name="countryID">Country identificator.</param>
        /// <returns>Country name.</returns>
        public static String GetCountryName(Guid countryID)
        {
            String countryName = String.Empty;
            using (SocialNetworkDBEntities record = new SocialNetworkDBEntities())
            {
                var rawCountry = record.Countries.FirstOrDefault(f => f.CountryID.Equals(countryID));
                countryName = rawCountry == null ?  _defaultCountryName : rawCountry.Name;
            }
            return countryName;
        }

        /// <summary>
        /// Get name of user city.
        /// </summary>
        /// <param name="cityID">City identificator.</param>
        /// <returns>City name.</returns>
        public static String GetCityName(Guid cityID)
        {
            String cityName = String.Empty;
            using (SocialNetworkDBEntities record = new SocialNetworkDBEntities())
            {
                var rawCity = record.Cities.FirstOrDefault(f => f.CityID.Equals(cityID));
                cityName = rawCity == null ? _defaultCityName : rawCity.Name;
            }
            return cityName;
        }
    }
}