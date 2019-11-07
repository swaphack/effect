using Game.Foundation.Events;
using UnityEngine;

namespace Game.Foundation.Devices
{
    /// <summary>
    /// 鼠标
    /// </summary>
    public class Mouse : MonoBehaviour
    {
        private float _lastUpdateTime;
        private Vector2 _lastTouchPosition = Vector2.zero;

        private Vector2 GetVector2(Vector3 pos)
        {
            return new Vector2(pos.x, pos.y);
        }

        private Touch CreateTouch(TouchPhase phase, float deltaTime)
        {
            Vector2 mousePos = GetVector2(Input.mousePosition);
            Vector2 deltaPosition = mousePos - _lastTouchPosition;
            _lastTouchPosition = mousePos;

            Touch touch = new Touch();
            touch.phase = phase;
            touch.position = mousePos;
            touch.fingerId = 1;
            touch.deltaTime = deltaTime;
            touch.deltaPosition = deltaPosition;
            return touch;
        }

        private void Start()
        {
            _lastUpdateTime = Time.realtimeSinceStartup;
        }

        private void Update()
        {
            float time = Time.realtimeSinceStartup;
            if (!Input.mousePresent)
            {
                _lastUpdateTime = time;
                return;
            }

            float diffTime = time - _lastUpdateTime;

            if (Input.GetMouseButtonDown(0))
            {
                _lastTouchPosition = GetVector2(Input.mousePosition);
                Touch touch = CreateTouch(TouchPhase.Began, diffTime);
                TouchManager.Instance.DispatchTouch(touch);
            }
            else if (Input.GetMouseButton(0))
            {
                Touch touchInfo = CreateTouch(TouchPhase.Moved, diffTime);
                TouchManager.Instance.DispatchTouch(touchInfo);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                Touch touchInfo = CreateTouch(TouchPhase.Ended, diffTime);
                TouchManager.Instance.DispatchTouch(touchInfo);
            }

            if (Input.mouseScrollDelta != Vector2.zero)
            {
                ScrollManager.Instance.DispatchScale(Input.mouseScrollDelta.y);
            }

            _lastUpdateTime = time;
        }
    }
}
