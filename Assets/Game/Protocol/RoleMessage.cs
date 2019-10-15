using Assets.Foundation.Protocol;
using System;

namespace Assets.Game.Protocol
{
    /// <summary>
    /// 角色消息
    /// </summary>
    public class RoleMessage
    {
        
    }

    /// <summary>
    /// 登陆
    /// </summary>
    public class MessageLogin : MessageHeader
    {
        public long UID;
        public long GameID;
        public int Version;
        public int PlatformType;

        public MessageLogin()
            : base(ProtocolID.ROlE_LOGIN)
        {
        }
    }
}
