namespace SocialNetwork.DataAccess.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using SocialNetwork.DataAccess.Enums;

    /// <summary>
    /// Work with dbo.WallBoardItem.
    /// </summary>
    public static class WallBoardItemRepository
    {
        /// <summary>
        /// Delete item from wallboard by item identificator.
        /// </summary>
        /// <param name="id">Item identificator <see cref="?"/></param>
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

        public static IQueryable<WallBoardItem> SelectAllItems()
        {
            IQueryable<WallBoardItem> list;
            using (SocialNetworkDBEntities record = new SocialNetworkDBEntities())
            {
                list = from w in record.WallBoardItems
                       select w;
                   
            }
            return list;
        }
    }
}
