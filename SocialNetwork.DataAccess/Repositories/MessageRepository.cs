// -----------------------------------------------------------------------
// <copyright file="MessageRepository.cs" company="RusWizards">
// Author: Mankevich M.V. 
// Date: 29.06.12
// </copyright>
// -----------------------------------------------------------------------

namespace SocialNetwork.DataAccess.Repositories
{
    #region Using
    using SocialNetwork.DataAccess.Entity;
    using SocialNetwork.DataAccess.Enums;
    using System;
    using System.Collections.Generic;
    using System.Data.Objects;
    using System.Linq;
    using System.Text;
    #endregion

    /// <summary>
    /// Message repository.
    /// </summary>
    public static class MessageRepository
    {
        #region Public methods
        /// <summary>
        /// Get all messages of current user.
        /// </summary>
        /// <param name="userID">Sender user idetifier.</param>
        /// <param name="type">Type of message.</param>
        /// <param name="selectType">Type of message selecting.</param>
        /// <returns>List of user messages.</returns>
        public static IEnumerable<Message> GetUserMessages(
            Guid userID, MessageType type, MessageSelectType selectType)
        {
            IEnumerable<Message> recordList = null;
            using (SocialNetworkDBEntities record = new SocialNetworkDBEntities())
            {
                if (type.Equals(MessageType.Posted))
                {
                    switch (selectType)
                    {
                        case MessageSelectType.All:
                            {
                                recordList = record.Messages
                                    .Where(w => w.FromID == userID && !w.IsDeletedBySender)
                                    .OrderByDescending(o => o.SendDate)
                                    .Select(s => new Message
                                    {
                                        MessageID = s.MessageID,
                                        FromID = s.FromID,
                                        ToID = s.ToID,
                                        SendDate = s.SendDate,
                                        Header = s.Header,
                                        Text = s.Text,
                                        Status = s.Status,
                                        IsDeletedBySender = s.IsDeletedBySender,
                                        IsDeletedByReceiver = s.IsDeletedByReceiver
                                    })
                                    .ToList();
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
                else
                {
                    switch (selectType)
                    {
                        case MessageSelectType.All:
                            {
                                recordList = record.Messages
                                    .Where(w => w.ToID == userID && !w.IsDeletedByReceiver)
                                    .OrderByDescending(o => o.SendDate)
                                    .Select(s => new Message
                                    {
                                        MessageID = s.MessageID,
                                        FromID = s.FromID,
                                        ToID = s.ToID,
                                        SendDate = s.SendDate,
                                        Header = s.Header,
                                        Text = s.Text,
                                        Status = s.Status,
                                        IsDeletedBySender = s.IsDeletedBySender,
                                        IsDeletedByReceiver = s.IsDeletedByReceiver
                                    })
                                    .ToList();
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
            }

            return recordList;
        }

        /// <summary>
        /// Send message to another user.
        /// </summary>
        /// <param name="senderID">Sender identifier.</param>
        /// <param name="receiverID">Receiver identifier.</param>
        /// <param name="header">Message title.</param>
        /// <param name="text">Message text.</param>
        public static void SendPrivateMessage(Guid senderID, Guid receiverID, String header, String text)
        {
            ObjectParameter pkMessageID = new ObjectParameter("pkMessageID", typeof(Guid));

            using (SocialNetworkDBEntities record = new SocialNetworkDBEntities())
            {
                record.spMessage(pkMessageID, senderID, receiverID, header, text, true, false, false);
            }
        }

        /// <summary>
        /// Delete private message.
        /// </summary>
        /// <param name="messageID">Message identifier.</param>
        /// <param name="type">Type of message (sended or received).</param>
        public static void DeletePrivateMessage(Guid messageID, MessageType type)
        {
            ObjectParameter pkMessageID = new ObjectParameter("pkMessageID", messageID);

            using (SocialNetworkDBEntities record = new SocialNetworkDBEntities())
            {
                if (type == MessageType.Posted)
                {
                    record.spMessage(pkMessageID, null, null, null, null, null, true, null);
                }
                else
                {
                    record.spMessage(pkMessageID, null, null, null, null, null, null, true);
                }
            }
        }

        /// <summary>
        /// Set status 'read' to current message.
        /// </summary>
        /// <param name="messageID">Message identifier.</param>
        public static void SetReadStatus(Guid messageID)
        {
            ObjectParameter pkMessageID = new ObjectParameter("pkMessageID", messageID);

            using (SocialNetworkDBEntities record = new SocialNetworkDBEntities())
            {
                record.spMessage(pkMessageID, null, null, null, null, true, null, null);
            }
        }
        #endregion
    }
}