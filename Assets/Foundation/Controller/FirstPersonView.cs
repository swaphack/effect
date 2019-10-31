using System;
using UnityEngine;

namespace Assets.Foundation.Controller
{
    /// <summary>
    /// 第一人称视角
    /// </summary>
    public class FirstPersonView : MonoBehaviour
    {
        /// <summary>
        /// 目标
        /// </summary>
        [SerializeField]
        private Camera _target;

        /// <summary>
        /// 便宜位置
        /// </summary>
        [SerializeField]
        private Vector3 _offsetPosition;
        
        public Camera Target
        {
            get { return _target; }
            set { _target = value; }
        }

        public Vector3 OffsetPosition
        {
            get { return _offsetPosition; }
            set
            {
                _offsetPosition = value;
            }
        }

        private void OnEnable()
        {
            if (Target == null)
            {
                Target = Camera.main;
            }

            Target.transform.parent = this.transform;
            Target.transform.localPosition = OffsetPosition;
            Target.transform.localEulerAngles = Vector3.zero;
        }
    }
}
