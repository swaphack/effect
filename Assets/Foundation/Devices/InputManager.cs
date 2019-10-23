using Assets.Foundation.Managers;
using UnityEngine;

namespace Assets.Foundation.Devices
{
    /// <summary>
    /// 设备管理
    /// </summary>
    public class InputManager : SingletonBehaviour<InputManager>
    {
        public override void Initialize()
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
