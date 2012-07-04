// -----------------------------------------------------------------------
// <copyright file="AddressRepository.cs" company="RusWizards">
// Author: Mankevich M.V. 
// Date: 29.06.12
// </copyright>
// -----------------------------------------------------------------------

namespace SocialNetwork.DataAccess.Repositories
{
    #region Using
    using SocialNetwork.DataAccess.Entity;
    using System;
    using System.Collections.Generic;
    using System.Data.Objects;
    using System.Linq;
    using System.Text;
    #endregion

    /// <summary>
    /// Work with dbo.Address, dbo.Country, dbo.City. 
    /// </summary>
    public static class AddressRepository
    {
        #region Constants
        /// <summary>
        /// Default name of country.
        /// </summary>
        private const String _defaultCountryName = "-- not select country --";

        /// <summary>
        /// Default name of city.
        /// </summary>
        private const String _defaultCityName = "-- not select city --";
        #endregion

        #region Public methods
        /// <summary>
        /// Get Address of current user.
        /// </summary>
        /// <param name="userID">User identificator.</param>
        /// <returns>Object Address.</returns>
        public static Address GetUserAddress(Guid userID)
        {
            Address address = null;
            using (SocialNetworkDBEntities record = new SocialNetworkDBEntities())
            {
                address = record.Addresses
                    .Where(w => w.PersonalInfo.UserID == userID)
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
                    })
                    .FirstOrDefault(f => !f.IsDeleted);
            }

            return address;
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
                    .OrderBy(o => o.Name)
                    .Select(s => new Country
                    {
                        CountryID = s.CountryID,
                        Name = s.Name
                    })
                    .ToList();
            }

            return recordList;
        }

        /// <summary>
        /// Get list, consists all cities.
        /// </summary>
        /// <param name="countryID">Country identifier.</param>
        /// <returns>All cities.</returns>
        public static IEnumerable<City> GetAllCountryCities(Guid countryID)
        {
            IEnumerable<City> recordList = null;
            using (SocialNetworkDBEntities record = new SocialNetworkDBEntities())
            {
                recordList = record.Cities
                    .Where(w => w.CountryID == countryID)
                    .OrderBy(o => o.Name)
                    .Select(s => new City
                    {
                        CityID = s.CityID,
                        CountryID = s.CountryID,
                        Name = s.Name
                    })
                    .ToList();
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
                var rawCountry = record.Countries.FirstOrDefault(f => f.CountryID == countryID);
                countryName = rawCountry == null ? _defaultCountryName : rawCountry.Name;
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
                var rawCity = record.Cities.FirstOrDefault(f => f.CityID == cityID);
                cityName = rawCity == null ? _defaultCityName : rawCity.Name;
            }

            return cityName;
        }

        /// <summary>
        /// Modify user address information.
        /// </summary>
        /// <param name="id">Row itentify.</param>
        /// <param name="updateByUserInfo">Update by user id flag.</param>
        /// <param name="userID">User identify.</param>
        /// <param name="countryID">Country identify.</param>
        /// <param name="cityID">City identify.</param>
        /// <param name="area">Area name.</param>
        /// <param name="street">Street name.</param>
        /// <param name="home">Home address.</param>
        /// <param name="apartment">Apartment info.</param>
        public static void ModifyAddress(
            Guid? id,
            Boolean updateByUserInfo,
            Guid? userID,
            Guid? countryID,
            Guid? cityID,
            String area,
            String street,
            String home,
            String apartment)
        {
            ObjectParameter pkID = (id != null) ? new ObjectParameter("pkID", id)
                : new ObjectParameter("pkID", typeof(Guid));
            using (SocialNetworkDBEntities record = new SocialNetworkDBEntities())
            {
                var rawUserInfoID = record.PersonalInfoes
                    .FirstOrDefault(f => f.UserID == userID);
                Guid? userInfoID = (rawUserInfoID == null) ? Guid.Empty : rawUserInfoID.ID;    

                record.spAddress(
                    pkID,
                    updateByUserInfo,
                    userInfoID,
                    countryID,
                    cityID,
                    area,
                    street,
                    home,
                    apartment,
                    false);
            }
        }
        #endregion
    }
}