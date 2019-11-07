using Game.Foundation.Common;
using Game.Foundation.Net;
using Game.Foundation.Protocol;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Game.SDK.Project
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
                this.MoveTo(WorkState.End);
                return;
            }

            GameDetail.GameServerAddress = msg.IP;
            GameDetail.GameServerPort = msg.Port;

            this.MoveNext();
        }

        public override void DoEvent()
        {
            Client client = Client.Instance;
            client.Disconnect();
            client.StartConnect();
            this.MoveNext();
        }

        public override void Finish()
        {
            this.MoveNext();
        }
    }
}
