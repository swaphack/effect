using UnityEngine;

namespace Assets.Foundation.SimpleEffect
{
    /// <summary>
    /// 自动行走
    /// </summary>
    public class AutoMove : MonoBehaviour
    {
        /// <summary>
        /// 旋转角度
        /// </summary>
        public float Speed = 1f;

        void Update()
        {
            this.transform.localPosition += this.transform.forward * Speed * Time.deltaTime;
        }
    }
}
