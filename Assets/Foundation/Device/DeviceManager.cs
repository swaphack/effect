using Assets.Foundation.Managers;
using UnityEngine;

namespace Assets.Foundation.Device
{
    /// <summary>
    /// 设备管理
    /// </summary>
    public class DeviceManager : Singleton<DeviceManager>
    {
        public void Init()
        {
            if (Input.touchSupported)
            {
                this.gameObject.AddComponent<TouchScreen>();
            }

            if (Input.mousePresent)
            {
                this.gameObject.AddComponent<Mouse>();
            }

            this.gameObject.AddComponent<Keyboard>();
        }

        public Touch Touch 
        {
            get
            {
                return this.GetComponent<Touch>();
            }
        }

        public Mouse Mouse
        {
            get
            {
                return this.GetComponent<Mouse>();
            }
        }

        public Keyboard Keyboard
        {
            get
            {
                return this.GetComponent<Keyboard>();
            }
        }
    }
}
