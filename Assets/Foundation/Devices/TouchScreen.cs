﻿using Game.Foundation.Events;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Foundation.Devices
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

            ScrollManager.Instance.DispatchTouches(Input.touches);
        }
    }
}
