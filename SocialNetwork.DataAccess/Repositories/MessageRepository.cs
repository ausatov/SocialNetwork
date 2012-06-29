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
                switch (type)
                {
                    case MessageType.Posted:
                        {
                            switch (selectType)
                            {
                                case MessageSelectType.All:
                                    {
                                        recordList = record.Messages
                                            .Where(w => w.FromID == userID && !w.IsDeleted)
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
                                                IsDeleted = s.IsDeleted
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
                                            .Where(w => w.ToID == userID && !w.IsDeleted)
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
                                                IsDeleted = s.IsDeleted
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
        #endregion
    }
}

/*

[22.06.2012 19:28:36] Alexander Kopachov: а как вам такое решение:
сначала в select создаём анонимный класс где для каждого месседжа вычисляем его тип, потом делаем селект только тех месседжев где тип нужный нам
[22.06.2012 19:28:55] Alexander Kopachov: либо совместить и налету выбирать только то что нужно
[22.06.2012 19:29:29] Усатов Андрей: звучит убедительно)
[22.06.2012 19:30:02] Усатов Андрей: но я не совсем понял
[22.06.2012 19:30:55] Alexander Kopachov: в офисе?
[22.06.2012 19:30:58] Max Mankevich: да
[22.06.2012 19:31:03] Усатов Андрей: нет
[22.06.2012 19:31:17] Усатов Андрей: ну ты ему покажи , он мне расскажет
[22.06.2012 19:31:42] Alexander Kopachov: в понедельник тогда

*/