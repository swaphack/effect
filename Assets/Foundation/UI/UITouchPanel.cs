using Assets.Foundation.Events;
using UnityEngine;

namespace Assets.Foundation.UI
{
    public class UITouchPanel : TouchBehaviour
    {
        void Start()
        {
            IsTouchEnabled = true;
        }
        public override void TouchBegan(HitTouchInfo hitInfo)
        {
            Debug.LogFormat("{0}-{1}", "TouchBegan", hitInfo);
        }
        public override void TouchMoved(HitTouchInfo hitInfo)
        {
            Debug.LogFormat("{0}-{1}", "TouchMoved", hitInfo);
        }
        public override void TouchEnded(HitTouchInfo hitInfo)
        {
            Debug.LogFormat("{0}-{1}", "TouchEnded", hitInfo);
        }
        public override void TouchCanceled(HitTouchInfo hitInfo)
        {
            Debug.LogFormat("{0}-{1}", "TouchCanceled", hitInfo);
        }
    }
}
