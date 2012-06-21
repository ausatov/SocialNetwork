namespace SocialNetwork.DataAccess.Enums
{
    /// <summary>
    /// Статусы пользователя
    /// </summary>
    public enum UserStatus
    {
        Offline = 0,
        Online = 1,
        AtHome = 2,
        AtWork = 3,
        Busy = 4
    }

    /// <summary>
    /// Типы размещаемого на стене контента
    /// </summary>
    public enum WallBoardItemType
    {
        Text = 0,
        Audio = 1,
        Video = 2,
        Image = 3
    }

    /// <summary>
    /// Пол
    /// </summary>
    public enum Sex
    {
        Male = 0,
        Female = 1,
        NotSet = 2
    }

    /// <summary>
    /// Привелегии пользователя
    /// </summary>
    public enum UserPrivelege
    {
        User = 0,
        God = 1
    }

    /// <summary>
    /// Type of select filter.
    /// </summary>
    public enum MessageSelectType
    {
        All = 0,
        OnlyNew = 1,
        OnlyOld = 2
    }

    /// <summary>
    /// Type of.
    /// </summary>
    public enum MessageType
    {
        Posted = 0,
        Received = 1
    }
}
