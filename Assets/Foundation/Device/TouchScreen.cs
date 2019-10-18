using Assets.Foundation.Events;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Foundation.Device
{
    /// <summary>
    /// 触摸屏
    /// </summary>
    public class TouchScreen : MonoBehaviour
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

            

            TouchManager.Instance.DispatchTouches(Input.touches);
        }
    }
}
