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

        private void ResetCameraAngle()
        {
            if (Camera.main == null)
            {
                return;
            }

            Camera.main.transform.localEulerAngles = Vector3.zero;
        }

        public void TurnLeft()
        {
            ResetCameraAngle();

            this.transform.Rotate(this.transform.up, -RotateSpeed);
        }

        public void TurnRight()
        {
            ResetCameraAngle();

            this.transform.Rotate(this.transform.up, RotateSpeed);
        }

        public void MoveForward()
        {
            ResetCameraAngle();

            this.transform.localPosition += this.transform.forward * MoveSpeed;
        }

        public void MoveBack()
        {
            ResetCameraAngle();

            this.transform.localPosition -= this.transform.forward * MoveSpeed;
        }
    }
}
