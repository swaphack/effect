using System;
using UnityEngine;

namespace Assets.App
{
    public class DeviceVirtual : Device
    {
        public override RectOffset SafeAreaInsets
        {
            get
            {
                return new RectOffset();
            }
        }
    }
}
