
using Assets.Foundation.Protocol;

namespace Assets.SDK.Project
{
    public class GameServerMessage : MessageHeader
    {
        public enum MessageID
        {
            /// <summary>
            /// 游戏服务器信息
            /// </summary>
            GAME_SERVER_DETAIL = 1,
            /// <summary>
            /// 游戏服务器登陆
            /// </summary>
            GAME_SERVER_LOGIN = 1,
        }

        public GameServerMessage(MessageID id)
            :base((int)id)
        { 
        }

    }
    

    /// <summary>
    /// 游戏服务器清单
    /// </summary>
    public class GameServerDetail : GameServerMessage
    {
        /// <summary>
        /// 服务器ip
        /// </summary>
        public string IP;
        /// <summary>
        /// 服务器端口
        /// </summary>
        public int Port;

        public GameServerDetail()
            : base(MessageID.GAME_SERVER_DETAIL)
        {
        }
    }

    /// <summary>
    /// 登陆游戏服务器
    /// </summary>
    public class GameServerLogin : GameServerMessage
    {
        /// <summary>
        /// 唯一id
        /// </summary>
        public long UID;
        /// <summary>
        /// 游戏id
        /// </summary>
        public long GameID;
        /// <summary>
        /// 版本
        /// </summary>
        public int Version;
        /// <summary>
        /// 平台
        /// </summary>
        public int PlatformType;

        public GameServerLogin()
            : base(MessageID.GAME_SERVER_LOGIN)
        {
        }
    }
}
