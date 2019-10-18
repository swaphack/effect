
using UnityEngine;
namespace Assets.Foundation.Events
{
    /// <summary>
    /// 触摸缩放
    /// </summary>
    public class TouchScrollBehaviour : MultiTouchBehaviour, IScrollProtocol
    {
        public virtual void DoScale(float delta)
        {

        }

        private Touch _firstTouch;
        private Touch _secondTouch;

        private float _distance = -1;

        private void Reset()
        {
            _firstTouch.fingerId = -1;
            _secondTouch.fingerId = -1;
            _distance = -1;
        }

        private bool CheckFingerChanged(Touch[] touches)
        {
            if (touches == null || touches.Length == 0)
            {
                return false;
            }

            bool changed = false;
            if (_firstTouch.fingerId == -1)
            {
                _firstTouch = touches[0];
                changed = true;
            }
            else if (_secondTouch.fingerId == -1)
            {
                _secondTouch = touches[0];
                changed = true;
            }

            return changed;
        }

        private bool CheckPositionChanged(Touch[] touches)
        {
            if (touches == null || touches.Length == 0)
            {
                return false;
            }

            if (_firstTouch.fingerId < 0 || 
                _secondTouch.fingerId < 0 ||
                _distance < 0)
            {
                return false;
            }

            bool changed = false;
            for (var i = 0; i < touches.Length; i++)
            {
                if (touches[i].fingerId == _firstTouch.fingerId)
                {
                    _firstTouch = touches[i];
                    changed = true;
                }
                else if (touches[i].fingerId == _secondTouch.fingerId)
                {
                    _secondTouch = touches[i];
                    changed = true;
                }
            }

            return changed;
        }

        private void ShowTwoTouchInfos()
        {
            Debug.LogFormat("first fingerId : {0}, phase : {1}, position : {2}", 
                    _firstTouch.fingerId, _firstTouch.phase, _firstTouch.position);
            Debug.LogFormat("second fingerId : {0}, phase : {1}, position : {2}", 
                    _secondTouch.fingerId, _secondTouch.phase, _secondTouch.position);
        }

        public override void TouchBegan(Touch[] touches)
        {
            if (CheckFingerChanged(touches))
            {
                _distance = Vector2.Distance(_firstTouch.position, _secondTouch.position);
            }
        }

        public override void TouchStationary(Touch[] touches)
        {
            if (CheckPositionChanged(touches))
            {
                float distance = Vector2.Distance(_firstTouch.position, _secondTouch.position);
                if (distance != _distance)
                {
                    Debug.LogFormat("TouchStationary, new distance : {0}, old distance : {1}, diff : {2}", 
                        distance, _distance, distance - _distance);

                    this.DoScale(distance - _distance);
                    _distance = distance;
                }
            }
        }

        public override void TouchMoved(Touch[] touches)
        {
            if (CheckPositionChanged(touches))
            {
                float distance = Vector2.Distance(_firstTouch.position, _secondTouch.position);
                if (distance != _distance)
                {
                    Debug.LogFormat("TouchStationary, new distance : {0}, old distance : {1}, diff : {2}", 
                        distance, _distance, distance - _distance);

                    this.DoScale(distance - _distance);
                    _distance = distance;
                }
            }
        }

        public override void TouchEnded(Touch[] touches)
        {
            for (var i = 0; i < touches.Length; i++)
            {
                if (touches[i].fingerId == _firstTouch.fingerId)
                {
                    _firstTouch.fingerId = -1;
                    _distance = -1;
                }
                else if (touches[i].fingerId == _secondTouch.fingerId)
                {
                    _secondTouch.fingerId = -1;
                    _distance = -1;
                }
            }
        }

        public override void TouchCanceled(Touch[] touches)
        {
            for (var i = 0; i < touches.Length; i++)
            {
                if (touches[i].fingerId == _firstTouch.fingerId)
                {
                    _firstTouch.fingerId = -1;
                    _distance = -1;
                }
                else if (touches[i].fingerId == _secondTouch.fingerId)
                {
                    _secondTouch.fingerId = -1;
                    _distance = -1;
                }
            }
        }
    }
}
