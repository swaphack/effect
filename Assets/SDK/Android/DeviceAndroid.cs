using System;
using Assets.App;
using UnityEngine;

namespace Assets.SDK.Android
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
