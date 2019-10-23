using Assets.Foundation.Events;
using UnityEngine;

namespace Assets.Foundation.Controller
{
    public class ViewController : SingleTouchBehaviour
    {
        [SerializeField]
        private float _minAngle = -15;
        [SerializeField]
        private float _maxAngle = 15;
        [SerializeField]
        private float _angleAccelerate = 0.1f;

        public float MinAngle
        {
            get
            {
                return _minAngle;
            }
            set
            {
                _minAngle = value;
            }
        }

        public float MaxAngle
        {
            get
            {
                return _maxAngle;
            }
            set
            {
                _maxAngle = value;
            }
        }

        public float AngleAccelerate
        {
            get
            {
                return _angleAccelerate;
            }
            set
            {
                _angleAccelerate = value;
            }
        }

        private float GetNewAngle(float angle, Vector3 axis, float angleDelta)
        {
            if (axis.x < 0)
            {
                angle *= -1;
            }

            float newAngle = angle + angleDelta;
            if (newAngle < _minAngle || newAngle > _maxAngle)
            {
                return angle;
            }

            return newAngle;
        }

        private void UpdateAngle(Touch touch)
        {
            float y = touch.deltaPosition.y;
            if (Mathf.Approximately(y, 0))
            {
                return;
            }

            float angle;
            Vector3 axis;

            float angleDelta = -y * _angleAccelerate;

            this.transform.localRotation.ToAngleAxis(out angle, out axis);
            Debug.LogFormat("View Control GameObject angle : {0}, axis {1}, angleDelta {2}", angle, axis, angleDelta);

            float newAngle = GetNewAngle(angle, axis, angleDelta);

            Debug.LogFormat("New Angle {0}", newAngle);

            this.transform.localEulerAngles = new Vector3(newAngle, 0, 0);
        }

        public override void TouchBegan(Touch touch)
        {
            UpdateAngle(touch);
        }

        public override void TouchStationary(Touch touch)
        {
            UpdateAngle(touch);
        }

        public override void TouchMoved(Touch touch)
        {
            UpdateAngle(touch);
        }

        public override void TouchEnded(Touch touch)
        {
            UpdateAngle(touch);
        }

        public override void TouchCanceled(Touch touch)
        {
            UpdateAngle(touch);
        }
    }
}
