using Assets.Foundation.Events;
using UnityEngine;

namespace Assets.Foundation.Device
{
    /// <summary>
    /// 鼠标
    /// </summary>
    class Mouse : MonoBehaviour
    {
        void Update()
        {
            if (!Input.mousePresent)
            {
                return;
            }
            if (Input.GetMouseButtonDown(0))
            {
                TouchInfo touchInfo = new TouchInfo(Input.mousePosition, TouchPhase.Began);
                TouchManager.Instance.OnSingleTouch(touchInfo);
            }
            else if (Input.GetMouseButton(0))
            {
                TouchInfo touchInfo = new TouchInfo(Input.mousePosition, TouchPhase.Moved);
                TouchManager.Instance.OnSingleTouch(touchInfo);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                TouchInfo touchInfo = new TouchInfo(Input.mousePosition, TouchPhase.Ended);
                TouchManager.Instance.OnSingleTouch(touchInfo);
            }
        }
    }
}
