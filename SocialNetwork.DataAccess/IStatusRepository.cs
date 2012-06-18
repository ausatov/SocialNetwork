using System;
using System.Collections.Generic;
using SocialNetwork.DataAccess;
using SocialNetwork.DataAccess.Enums;

namespace SocialNetwork.DataAccess
{
    public interface IStatusRepository
    {
        String GetStatusMessage(Guid userID);
        String GetStatusName(Guid userID);
        void SetStatusMessage(Guid userID, String message, UserStatus status);
    }
}
