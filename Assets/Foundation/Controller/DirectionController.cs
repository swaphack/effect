using UnityEngine;

namespace Assets.Foundation.Controller
{
    /// <summary>
    /// 方向控制
    /// </summary>
    [RequireComponent(typeof(Transform))]
    public class DirectionController : MonoBehaviour
    { 
        /// <summary>
        /// 移动速度
        /// </summary>
        [SerializeField]
        private float _moveSpeed = 0.1f;
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
        
        public float RotateSpeed
        {
            get { return _rotateSpeed; }
            set { _rotateSpeed = value; }
        }


        public DirectionController()
        {
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
            this.transform.localPosition += this.transform.forward * MoveSpeed;
        }

        public void MoveBack()
        {
            this.transform.localPosition -= this.transform.forward * MoveSpeed;
        }
    }
}
