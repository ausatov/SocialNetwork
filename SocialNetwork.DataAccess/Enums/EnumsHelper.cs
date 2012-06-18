using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocialNetwork.DataAccess.Enums
{
    public static class EnumsHelper
    {
        public static String ToString(UserStatus status)
        {
            String statusName = String.Empty;
            switch (status)
            {
                case UserStatus.Offline:
                    {
                        statusName = "offline";
                    } break;
                case UserStatus.Online:
                    {
                        statusName = "online";
                    } break;
                case UserStatus.AtHome:
                    {
                        statusName = "at home";
                    } break;
                case UserStatus.AtWork:
                    {
                        statusName = "at work";
                    } break;
                case UserStatus.Busy:
                    {
                        statusName = "busy";
                    } break;
            }
            return statusName;
        }

        public static String ToString(Sex sex)
        {
            String sexName = String.Empty;
            switch (sex)
            {
                case Sex.Male:
                    {
                        sexName = "male";
                    } break;
                case Sex.Female:
                    {
                        sexName = "female";
                    } break;
                default:
                    {
                        sexName = "not set";
                    } break;
            }
            return sexName;
        }

        
    }
}
