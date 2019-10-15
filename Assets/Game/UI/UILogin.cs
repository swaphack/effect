using Assets.Foundation.Managers;
using Assets.Foundation.Protocol;
using Assets.Foundation.UI;
using Assets.Game.Protocol;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Game.UI
{
    /// <summary>
    /// 登陆界面
    /// </summary>
    public class UILogin : UIFrame
    {
        public override string Path
        {
            get
            {
                return "loading/UILogin";
            }
        }

        protected override void InitControls()
        {
            
        }

        protected override void InitLogic()
        {
            this.GetModule<UIMessage>().AddMessageHand<MessageLogin>(this.Login);

            var msg = new MessageLogin();
            msg.UID = 1212;
            msg.GameID = 545445;
            msg.Version = 101;
            msg.PlatformType = 1;

            MessageManager.Instance.DispatchMessage(msg);
        }

        private void Login(MessageHeader msg)
        {
            //var loginMsg = msg.To<MessageLogin>();

            this.GetModule<UIMessage>().RemoveAllMessageHands();
        }

        public override void InitWithParams(params object[] data)
        {
        }
    }
}
