using System;
using UnityEngine;

namespace Game.App
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
