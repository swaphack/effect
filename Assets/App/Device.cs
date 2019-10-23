using System;
using Assets.SDK.IOS;
using UnityEngine;

namespace Assets.App
{
    /// <summary>
    /// 设备信息
    /// </summary>
    public abstract class Device
    {
        private static Device _instance;

        public static Device Instance
        {
            get
            {
                if (_instance == null)
                {
#if UNITY_IOS && !UNITY_EDITOR
                    _instance = new DeviceIOS();
#elif UNITY_ANDROID && !UNITY_EDITOR
                    _instance = new DeviceAndroid();
#else
                    _instance = new DeviceVirtual();
#endif
                }
                return _instance;
            }
        }

        public abstract RectOffset SafeAreaInsets { get; }
    }
}
