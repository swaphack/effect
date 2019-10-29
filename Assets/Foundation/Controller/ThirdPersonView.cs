using System;
using UnityEngine;

namespace Assets.Foundation.Controller
{
    /// <summary>
    /// 第三人称视角
    /// </summary>
    public class ThirdPersonView : MonoBehaviour
    {
        [SerializeField]
        private Camera _target;
        [SerializeField]
        private Vector3 _offsetPosition;

        private Vector3 _distance;
        
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

        private void Update()
        {
            if (Target == null)
            {
                Target = Camera.main;
            }

            var pos = OffsetPosition + this.transform.position;
            Target.transform.position = pos;
            Target.transform.LookAt(this.transform);
        }
    }
}


