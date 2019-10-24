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
        public override IEnumerator Init(Object data)
        {
            State = WorkState.Start;

            Client client = Client.Instance;
            client.SetEndPoint(GameDetail.GameServerAddress, GameDetail.GameServerPort);

            yield return null;
        }

        public override IEnumerator DoEvent()
        {
            State = WorkState.Update;

            Client client = Client.Instance;
            
            client.Disconnect();
            client.StartConnect();

            yield return null;
        }

        public override IEnumerator Finish()
        {
            State = WorkState.End;
            yield return null;
        }
    }
}
