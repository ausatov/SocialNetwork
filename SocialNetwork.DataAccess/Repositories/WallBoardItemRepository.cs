namespace SocialNetwork.DataAccess.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using SocialNetwork.DataAccess.Entity;
    using SocialNetwork.DataAccess.Enums;
    
    /// <summary>
    /// Work with dbo.WallBoardItem.
    /// </summary>
    public static class WallBoardItemRepository
    {
        /// <summary>
        /// Delete item from wallboard by item identificator.
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
                            record.SaveChanges();
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
        /// Select list of wallboars items.
        /// </summary>
        /// <returns>List of wallboars items.</returns>
        public static IEnumerable<WallBoardItem> SelectAllItems()
        {
            IEnumerable<WallBoardItem> list = null;
            using (SocialNetworkDBEntities record = new SocialNetworkDBEntities())
            {
                list = record.WallBoardItems
                    .OrderByDescending(x => x.SendDate)
                    .Select(x => new WallBoardItem
                        {
                            ID = x.ID,
                            ContentTypeID = x.ContentTypeID,
                            FromID = x.FromID,
                            ToID = x.ToID,
                            SendDate = x.SendDate,
                            Message = x.Message,
                            IsDeleted = x.IsDeleted,
                            NullLink = x.NullLink
                        }).ToList();
            }
            return list;
        }


        /// <summary>
        /// Select list of current user wallboars items.
        /// </summary>
        /// <returns>List of wallboars items.</returns>
        public static IEnumerable<WallBoardItem> GetUserWallboardItems(Guid UserID)
        {
            IEnumerable<WallBoardItem> recordList = null;
            using (SocialNetworkDBEntities record = new SocialNetworkDBEntities())
            {
                recordList = record.WallBoardItems
                    .Where(x => x.ToID == UserID && !x.IsDeleted)
                    .OrderByDescending(x => x.SendDate)
                    .Select(x => new WallBoardItem
                    {
                        ID = x.ID,
                        ContentTypeID = x.ContentTypeID,
                        FromID = x.FromID,
                        ToID = x.ToID,
                        SendDate = x.SendDate,
                        Message = x.Message,
                        IsDeleted = x.IsDeleted,
                        NullLink = x.NullLink
                    }).ToList();
            }
            return recordList;
        }
    }
}
