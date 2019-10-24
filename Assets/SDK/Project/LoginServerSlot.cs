using Assets.Foundation.Managers;
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

            MessageManager.Instance.AddHand((int)GameServerMessage.MessageID.GAME_SEditorRVEditorR_DEditorTAIL, this.UppackMessage);

            yield return null;
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

        public override IEnumerator DoEvent()
        {
            State = WorkState.Update;

            Client.Instance.Disconnect();
            Client.Instance.Connect();

            yield return null;
        }

        public override IEnumerator Finish()
        {
            yield return null;
        }
    }
}
