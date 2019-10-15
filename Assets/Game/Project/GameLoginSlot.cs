using Assets.Foundation.Managers;
using Assets.Foundation.Protocol;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Game.Project
{
    /// <summary>
    /// 游戏登陆
    /// </summary>
    class GameLoginSlot : WorkSlot
    {
        public override IEnumerator Init(Object data)
        {
            State = WorkState.Start;

            Client client = Client.Instance;
            client.SetRemote(GameDetail.LoginServerAddress, GameDetail.LoginServerPort);
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
