namespace SocialNetwork.DataAccess.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// 
    /// </summary>
    public static class AddressRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static Address GetUserAddress(Guid userID)
        {
            Address address = null;
            using (SocialNetworkDBEntities record = new SocialNetworkDBEntities())
            {
                address = record.Addresses
                    .Where(x => x.PersonalInfo.UserID == userID && !x.IsDeleted)
                    .FirstOrDefault();
            }
            return address;
        }
    }
}