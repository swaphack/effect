using System;
using Assets.App;
using UnityEngine;
using System.Runtime.InteropServices;

namespace Assets.SDK.IOS
{
    /// <summary>
    /// ios 设备信息
    /// </summary>
    public class DeviceIOS : Device
    {
        [DllImport("__Internal")]
        public static extern void getSafeAreaInsets(out int left, out int right, out int top, out int bottom);

        private RectOffset _safeAreaInsets;

        public override RectOffset SafeAreaInsets
        {
            get
            {
                if (_safeAreaInsets == null)
                {
                    int left, right, top, bottom;
                    getSafeAreaInsets(out left, out right, out top, out bottom);
                    _safeAreaInsets = new RectOffset(left, right, top, bottom);
                    Debug.LogFormat("DeviceIOS Get SafeAreaInsets {0}", _safeAreaInsets);
                }                
                return _safeAreaInsets;
            }
        }
    }
}

