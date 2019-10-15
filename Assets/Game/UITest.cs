using Assets.Foundation.DataAccess;
using Assets.Foundation.Extensions;
using Assets.Foundation.Protocol;
using Assets.Foundation.UI;
using Assets.Game.Protocol;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Game
{
    public class UITest : UIFrame
    {
        protected override void InitControls()
        {
        }

        protected override void InitLogic()
        {
            this.GetModule<UIMessage>().AddMessageHand<MessageLogin>(this.Login);
        }

        private void Login(MessageHeader msg)
        {
            //var loginMsg = msg.To<MessageLogin>();
        }

        public override void InitWithParams(params object[] data)
        {

        }
    }
}