using System;
using Game.App;
using UnityEngine;

namespace Game.Foundation.SimpleEffect
{
    /// <summary>
    /// 自动旋转
    /// </summary>
    public class AutoRotation : MonoBehaviour
    {
        /// <summary>
        /// 旋转轴
        /// </summary>
        [SerializeField]
        private Vector3 _axis = Vector3.right;

        public Vector3 Axis
        {
            get
            {
                return _axis;
            }
            set
            {
                _axis = value;
            }
        }

        /// <summary>
        /// 每秒旋转角度
        /// </summary>
        [SerializeField]
        private float _anglePerSecond = 0.00417f;

        public float AnglePerSecond
        {
            get
            {
                return _anglePerSecond;
            }
            set
            {
                _anglePerSecond = value;
            }
        }

        void Update()
        {
            this.transform.Rotate(Axis, Time.deltaTime * AnglePerSecond);
        }
    }
}
