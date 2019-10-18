using UnityEngine;

namespace Assets.Foundation.SimpleEffect
{
    /// <summary>
    /// 自动旋转
    /// </summary>
    public class AutoRotation : MonoBehaviour
    {
        /// <summary>
        /// 旋转轴
        /// </summary>
        public Vector3 Axis = Vector3.up;
        /// <summary>
        /// 旋转角度
        /// </summary>
        public float Angle = 30.0f;

        void Update()
        {
            this.transform.Rotate(Axis, Time.deltaTime * Angle);
        }
    }
}
