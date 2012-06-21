﻿namespace SocialNetwork.DataAccess.Repositories
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
        /// Get Address of current user.
        /// </summary>
        /// <param name="userID">User identificator.</param>
        /// <returns>Object Address.</returns>
        //public static Address GetUserAddress(Guid userID)
        //{
        //    Address address = null;
        //    using (SocialNetworkDBEntities record = new SocialNetworkDBEntities())
        //    {
        //        address = record.Addresses
        //            .FirstOrDefault(x => x.PersonalInfo.UserID.Equals(userID) && !x.IsDeleted);
        //    }
        //    return address;
        //}
        public static IEnumerable<Address> GetUserAddress(Guid userID)
        {
            IEnumerable<Address> recordList = null;
            using (SocialNetworkDBEntities record = new SocialNetworkDBEntities())
            {
                recordList = record.Addresses
                    .Where(x => x.PersonalInfo.UserID.Equals(userID) && !x.IsDeleted)
                    .Select(x => new Address
                        {
                            ID = x.ID,
                            UserInfoID = x.UserInfoID,
                            CountryID = x.CountryID,
                            CityID = x.CityID,
                            Area = x.Area,
                            Street = x.Street,
                            Home = x.Home,
                            Apartment = x.Apartment,
                            IsDeleted = x.IsDeleted
                        }).ToList();
            }
            return recordList;
        }

        /// <summary>
        /// Get name of user country.
        /// </summary>
        /// <param name="countryID">Country identificator.</param>
        /// <returns>Country name.</returns>
        //public static String GetCountryName(Guid countryID)
        //{
        //    String countryName = String.Empty;
        //    using (SocialNetworkDBEntities record = new SocialNetworkDBEntities())
        //    {
        //        countryName = record.Countries
        //            .Where(x => x.CountryID == countryID)
        //            .Select(x => x.Name)
        //            .First();
        //    }
        //    return countryName;
        //}
        public static String GetCountryName(Guid countryID)
        {
            String countryName = String.Empty;
            using (SocialNetworkDBEntities record = new SocialNetworkDBEntities())
            {
                countryName = record.Countries
                    .Where(x => x.CountryID == countryID)
                    .Select(x => new Country
                        {
                            Name = x.Name
                        }).First().Name;
            }
            return countryName;
        }

        /// <summary>
        /// Get name of user city.
        /// </summary>
        /// <param name="cityID">City identificator.</param>
        /// <returns>City name.</returns>
        //public static String GetCityName(Guid cityID)
        //{
        //    String cityName = String.Empty;
        //    using (SocialNetworkDBEntities record = new SocialNetworkDBEntities())
        //    {
        //        cityName = record.Cities
        //            .Where(x => x.CityID == cityID)
        //            .Select(x => x.Name)
        //            .First();
        //    }
        //    return cityName;
        //}
        public static String GetCityName(Guid cityID)
        {
            String cityName = String.Empty;
            using (SocialNetworkDBEntities record = new SocialNetworkDBEntities())
            {
                cityName = record.Cities
                    .Where(x => x.CityID == cityID)
                    .Select(x => new City
                        {
                            Name = x.Name
                        }).First().Name;
            }
            return cityName;
        }
    }
}