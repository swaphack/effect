using UnityEngine;

namespace Assets.Foundation.Events
{
    /// <summary>
    /// 触摸信息
    /// </summary>
    public struct TouchInfo
    {
        /// <summary>
        /// 点击点
        /// </summary>
        public Vector3 hitPosition;
        /// <summary>
        /// 触摸状态
        /// </summary>
        public TouchPhase touchPhase;

        public TouchInfo(Vector3 pos, TouchPhase phase)
        {
            this.hitPosition = pos;
            this.touchPhase = phase;
        }
    }
}
