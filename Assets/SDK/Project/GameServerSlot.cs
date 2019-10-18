using Assets.Foundation.Managers;
using System.Collections;
using System;

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
            client.SetRemote(GameDetail.GameServerAddress, GameDetail.GameServerPort);
            client.RecvBuffHand = (byte[] buff) =>
            {
                MessageManager.Instance.AddBuff(buff);
            };

            client.ConnectCallback = (Client c) =>
            {// 连接
                client.StartRecv();
            };
            client.DisconnectCallback = (Client c) =>
            {// 断开连接
                client.EndRecv();
            };

            yield return null;
        }

        public override IEnumerator DoEvent()
        {
            State = WorkState.Update;
            Client.Instance.Disconnect();
            Client.Instance.Connect();

            yield return null;
        }

        public override IEnumerator Finish()
        {
            State = WorkState.End;
            yield return null;
        }
    }
}
