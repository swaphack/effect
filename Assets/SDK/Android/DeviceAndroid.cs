using System;
using Game.App;
using UnityEngine;

namespace Game.SDK.Android
{
    public class DeviceAndroid : Device
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
