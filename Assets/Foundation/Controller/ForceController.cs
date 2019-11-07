using System;
using UnityEngine;

namespace Game.Foundation.Controller
{
    public class ForceController : MonoBehaviour
    {
        /// <summary>
        /// 作用力度
        /// </summary>
        [SerializeField]
        private float _adjustDirectionForce = 2f;

        public float AdjustDirectionForce
        {
            get { return _adjustDirectionForce; }
            set { _adjustDirectionForce = value; }
        }

        public void TurnLeft()
        {

            Rigidbody body = this.GetComponent<Rigidbody>();
            if (body != null)
            {
                var up = this.transform.right;
                var force = -up * AdjustDirectionForce;
                body.AddForce(force);
            }
        }

        public void TurnRight()
        {
            Rigidbody body = this.GetComponent<Rigidbody>();
            if (body != null)
            {
                var up = this.transform.right;
                var force = up * AdjustDirectionForce;
                body.AddForce(force);
            }
        }

        public void MoveForward()
        {

            Rigidbody body = this.GetComponent<Rigidbody>();
            if (body != null)
            {
                var up = this.transform.right;
                var force = -up * AdjustDirectionForce;
                body.AddForce(force);
            }
        }

        public void MoveBack()
        {
            Rigidbody body = this.GetComponent<Rigidbody>();
            if (body != null)
            {
                var up = this.transform.right;
                var force = up * AdjustDirectionForce;
                body.AddForce(force);
            }
        }

        public void Reset()
        {
            this.transform.localEulerAngles = Vector3.zero;
        }
    }
}
