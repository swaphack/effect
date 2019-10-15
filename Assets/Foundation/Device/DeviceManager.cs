using Assets.Foundation.Managers;
using UnityEngine;

namespace Assets.Foundation.Device
{
    /// <summary>
    /// 设备管理
    /// </summary>
    public class DeviceManager : Singleton<DeviceManager>
    {
        void Start()
        {
            if (Input.touchSupported)
            {
                this.gameObject.AddComponent<Touch>();
            }

            if (Input.mousePresent)
            {
                this.gameObject.AddComponent<Mouse>();
            }

            this.gameObject.AddComponent<Keyboard>();
        }
    }
}
