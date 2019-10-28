using Assets.Foundation.Common;
using Assets.Foundation.Net;
using Assets.Foundation.Protocol;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Assets.SDK.Project
{
    /// <summary>
    /// 登陆服务器
    /// </summary>
    class LoginServerSlot : WorkSlot
    {
        public override void Init()
        {
            Client client = Client.Instance;
            client.SetEndPoint(GameDetail.LoginServerAddress, GameDetail.LoginServerPort);
            client.AddMessageParse((int)GameServerMessage.MessageID.GAME_SERVER_MESSAGE_INFO, this.UppackMessage);
            client.StartConnect();
        }

        private void UppackMessage(byte[] data)
        {
            GameServerDetail msg = new GameServerDetail();
            if (!msg.Unpack(data))
            {
                State = WorkState.End;
                return;
            }

            GameDetail.GameServerAddress = msg.IP;
            GameDetail.GameServerPort = msg.Port;

            State = WorkState.End;
        }

        public override void DoEvent()
        {
            State = WorkState.Update;

            Client client = Client.Instance;
            client.Disconnect();
            client.StartConnect();
        }

        public override void Finish()
        {
        }
    }
}
