using Assets.Foundation.Events;
using UnityEngine;

namespace Assets.Foundation.Device
{
    public class Touch : MonoBehaviour
    {
        void Update()
        {
            if (!Input.touchSupported)
            {
                return;
            }

            if (Input.touchCount == 0 )
            {
                return;
            }

            if (Input.touchCount == 1)
            {// 单点
                var touch = Input.GetTouch(0);
                var touchInfo = new TouchInfo(touch.position, touch.phase);
                TouchManager.Instance.OnSingleTouch(touchInfo);
            }
            else if (Input.touchCount > 1)
            { //多点

            }
        }
    }
}
