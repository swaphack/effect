using Assets.Foundation.Protocol;
using System.Collections;
using System;
using Assets.Foundation.Net;

namespace Assets.SDK.Project
{
    /// <summary>
    /// 游戏服务器
    /// </summary>
    class GameServerSlot : WorkSlot
    {
        public override void Init()
        {
            Client client = Client.Instance;
            client.SetEndPoint(GameDetail.GameServerAddress, GameDetail.GameServerPort);
        }

        public override void DoEvent()
        {
            Client client = Client.Instance;
            
            client.Disconnect();
            client.StartConnect();
        }

        public override void Finish()
        {
        }
    }
}
