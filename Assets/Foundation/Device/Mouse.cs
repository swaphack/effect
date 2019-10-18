using Assets.Foundation.Events;
using UnityEngine;

namespace Assets.Foundation.Device
{
    /// <summary>
    /// 鼠标
    /// </summary>
    public class Mouse : MonoBehaviour
    {
        private Touch CreateTouch(TouchPhase phase)
        {
            Touch touch = new Touch();
            touch.phase = phase;
            touch.position = Input.mousePosition;
            touch.fingerId = 1;
            return touch;
        }
        void Update()
        {
            if (!Input.mousePresent)
            {
                return;
            }
            if (Input.GetMouseButtonDown(0))
            {
                Touch touch = CreateTouch(TouchPhase.Began);
                TouchManager.Instance.DispatchTouch(touch);
            }
            else if (Input.GetMouseButton(0))
            {
                Touch touchInfo = CreateTouch(TouchPhase.Moved);
                TouchManager.Instance.DispatchTouch(touchInfo);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                Touch touchInfo = CreateTouch(TouchPhase.Ended);
                TouchManager.Instance.DispatchTouch(touchInfo);
            }

            if (Input.mouseScrollDelta != Vector2.zero)
            {
                ScrollManager.Instance.DispatchScale(Input.mouseScrollDelta.y);
            }
        }
    }
}
