using Game.Foundation.Events;
using UnityEngine;

namespace Game.Foundation.UI
{
    public class UITouchPanel : SingleTouchBehaviour
    {

        public override void TouchBegan(Touch hitInfo)
        {
            Debug.LogFormat("{0}-{1}", "TouchBegan", hitInfo);
        }
        public override void TouchMoved(Touch hitInfo)
        {
            Debug.LogFormat("{0}-{1}", "TouchMoved", hitInfo);
        }
        public override void TouchEnded(Touch hitInfo)
        {
            Debug.LogFormat("{0}-{1}", "TouchEnded", hitInfo);
        }
        public override void TouchCanceled(Touch hitInfo)
        {
            Debug.LogFormat("{0}-{1}", "TouchCanceled", hitInfo);
        }
    }
}
