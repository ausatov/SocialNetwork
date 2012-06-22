namespace SocialNetwork.DataAccess.Repositories
{
    using SocialNetwork.DataAccess.Entity;
    using SocialNetwork.DataAccess.Enums;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// 
    /// </summary>
    public static class MessageRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="senderID"></param>
        /// <param name="type"></param>
        /// <param name="selectType"></param>
        /// <returns></returns>
        public static IEnumerable<Message> GetUserMessages(
            Guid UserID, MessageType type, MessageSelectType selectType)
        {
            IEnumerable<Message> recordList = null;

            using (SocialNetworkDBEntities record = new SocialNetworkDBEntities())
            {
                switch (type)
                {
                    case MessageType.Posted:
                        {
                            switch (selectType)
                            {
                                case MessageSelectType.All:
                                    {
                                        recordList = record.Messages
                                            .Where(x => x.FromID.Equals(UserID) && !x.IsDeleted)
                                            .OrderByDescending(x => x.SendDate)
                                            .Select(x => new Message
                                            {
                                                MessageID = x.MessageID,
                                                FromID = x.FromID,
                                                ToID = x.ToID,
                                                SendDate = x.SendDate,
                                                Header = x.Header,
                                                Text = x.Text,
                                                Status = x.Status,
                                                IsDeleted = x.IsDeleted
                                            }).ToList();
                                    }
                                    break;
                                case MessageSelectType.OnlyNew:
                                    {
                                    }
                                    break;
                                case MessageSelectType.OnlyOld:
                                    {
                                    }
                                    break;
                            }
                        } 
                        break;
                    case MessageType.Received:
                        {
                            switch (selectType)
                            {
                                case MessageSelectType.All:
                                    {
                                        recordList = record.Messages
                                            .Where(x => x.ToID == UserID && !x.IsDeleted)
                                            .OrderByDescending(x => x.SendDate)
                                            .Select(x => new Message
                                            {
                                                MessageID = x.MessageID,
                                                FromID = x.FromID,
                                                ToID = x.ToID,
                                                SendDate = x.SendDate,
                                                Header = x.Header,
                                                Text = x.Text,
                                                Status = x.Status,
                                                IsDeleted = x.IsDeleted
                                            }).ToList();
                                    }
                                    break;
                                case MessageSelectType.OnlyNew:
                                    {
                                    }
                                    break;
                                case MessageSelectType.OnlyOld:
                                    {
                                    }
                                    break;
                            }
                        } 
                        break;
            
                }
            }
            return recordList;
        }
    }
}