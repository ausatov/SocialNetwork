// -----------------------------------------------------------------------
// <copyright file="WallBoardItemRepository.cs" company="RusWizards">
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
    /// Work with dbo.WallBoardItem.
    /// </summary>
    public static class WallBoardItemRepository
    {
        #region Public methods
        /// <summary>
        /// Delete item from wallboard by item identifier.
        /// </summary>
        /// <param name="id">Item identificator.</param>
        public static void DeleteItem(Guid id)
        {
            using (SocialNetworkDBEntities record = new SocialNetworkDBEntities())
            {
                record.spDelWallBoardItem(id);
            }
        }

        /// <summary>
        /// Send message to wallboard.
        /// </summary>
        /// <param name="type">Content type (text, music, etc).</param>
        /// <param name="senderID">Who send message.</param>
        /// <param name="receiverID">Who receive message.</param>
        /// <param name="message">Message content.</param>
        public static void InsertMessage(WallBoardItemType type, Guid senderID, Guid receiverID, Object message)
        {
            using (SocialNetworkDBEntities record = new SocialNetworkDBEntities())
            {
                switch (type)
                {
                    case WallBoardItemType.Text:
                        {
                            record.spInsWallBoardTextMessage(
                                (Int32)WallBoardItemType.Text, senderID, receiverID, (String)message);
                        } 
                        break;
                    case WallBoardItemType.Image:
                        {
                        } 
                        break;
                    case WallBoardItemType.Audio:
                        {
                        } 
                        break;
                    case WallBoardItemType.Video:
                        {
                        } 
                        break;
                }
            }
        }

        /// <summary>
        /// Select list of wallboards items.
        /// </summary>
        /// <returns>List of wallboards items.</returns>
        public static IEnumerable<WallBoardItem> SelectAllItems()
        {
            IEnumerable<WallBoardItem> list = null;
            using (SocialNetworkDBEntities record = new SocialNetworkDBEntities())
            {
                list = record.WallBoardItems
                    .OrderByDescending(o => o.SendDate)
                    .Select(s => new WallBoardItem
                        {
                            ID = s.ID,
                            ContentTypeID = s.ContentTypeID,
                            FromID = s.FromID,
                            ToID = s.ToID,
                            SendDate = s.SendDate,
                            Message = s.Message,
                            IsDeleted = s.IsDeleted,
                            NullLink = s.NullLink
                        }).ToList();
            }
            return list;
        }

        /// <summary>
        /// Select list of current user wallboards items.
        /// </summary>
        /// <param name="userID">User identifier.</param>
        /// <returns>List of wallboards items.</returns>
        public static IEnumerable<WallBoardItem> GetUserWallboardItems(Guid userID)
        {
            IEnumerable<WallBoardItem> recordList = null;
            using (SocialNetworkDBEntities record = new SocialNetworkDBEntities())
            {
                recordList = record.WallBoardItems
                    .Where(w => w.ToID == userID && !w.IsDeleted)
                    .OrderByDescending(o => o.SendDate)
                    .Select(s => new WallBoardItem
                    {
                        ID = s.ID,
                        ContentTypeID = s.ContentTypeID,
                        FromID = s.FromID,
                        ToID = s.ToID,
                        SendDate = s.SendDate,
                        Message = s.Message,
                        IsDeleted = s.IsDeleted,
                        NullLink = s.NullLink
                    }).ToList();
            }
            return recordList;
        }
        #endregion
    }
}