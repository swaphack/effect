using UnityEngine;

namespace Assets.Foundation.Events
{
    /// <summary>
    /// 点击接口
    /// </summary>
    public interface ITouchProtocol
    {
        void TouchBegan(HitTouchInfo hitInfo);
        void TouchMoved(HitTouchInfo hitInfo);
        void TouchEnded(HitTouchInfo hitInfo);
        void TouchCanceled(HitTouchInfo hitInfo);
    }
}
