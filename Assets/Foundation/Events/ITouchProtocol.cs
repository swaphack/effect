using UnityEngine;

namespace Assets.Foundation.Events
{
    /// <summary>
    /// 点击处理基本接口
    /// </summary>
    public interface ITouchProtocol
    {
        /// <summary>
        /// 目标
        /// </summary>
        GameObject Target { get; }

        void DispatchTouches(Touch[] touches);
    }
    /// <summary>
    /// 单点处理接口
    /// </summary>
    public interface ISingleTouchProtocol : ITouchProtocol
    {
        void TouchBegan(Touch touch);
        void TouchStationary(Touch touch);
        void TouchMoved(Touch touch);
        void TouchEnded(Touch touch);
        void TouchCanceled(Touch touch);
    }

    /// <summary>
    /// 多点处理接口
    /// </summary>
    public interface IMutliTouchProtocol : ITouchProtocol
    {
        void TouchBegan(Touch[] touches);
        void TouchStationary(Touch[] touches);
        void TouchMoved(Touch[] touches);
        void TouchEnded(Touch[] touches);
        void TouchCanceled(Touch[] touches);
    }
}
