using UnityEngine;

namespace Assets.Foundation.Controller
{
    /// <summary>
    /// 方向控制
    /// </summary>
    public class DirectionController : MonoBehaviour
    {
        /// <summary>
        /// 移动速度
        /// </summary>
        [SerializeField]
        private float _moveSpeed = 0.1f;
        /// <summary>
        /// 移动加速度
        /// </summary>
        [SerializeField]
        private float _acceleration = 0.1f;
        /// <summary>
        /// 旋转速度
        /// </summary>
        [SerializeField]
        private float _rotateSpeed = 2f;
        

        public float MoveSpeed
        {
            get { return _moveSpeed; }
            set { _moveSpeed = value; }
        }

        public float Acceleration
        {
            get { return _acceleration; }
            set { _acceleration = value; }
        }

        public float RotateSpeed
        {
            get { return _rotateSpeed; }
            set { _rotateSpeed = value; }
        }

        public void TurnLeft()
        {
            this.transform.Rotate(this.transform.up, -RotateSpeed);
        }

        public void TurnRight()
        {
            this.transform.Rotate(this.transform.up, RotateSpeed);
        }

        public void MoveForward()
        {
            var speed = MoveSpeed + Acceleration;
            this.transform.localPosition += this.transform.forward * speed;
        }

        public void MoveBack()
        {
            var speed = MoveSpeed + Acceleration;

            this.transform.localPosition -= this.transform.forward * speed;
        }

        public void Reset()
        {
            this.transform.localEulerAngles = Vector3.zero;
        }
    }
}
