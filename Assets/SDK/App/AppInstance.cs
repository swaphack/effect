using Assets.Foundation.DataAccess;
using Assets.Foundation.Device;
using Assets.Foundation.UI;
using UnityEngine;

namespace Assets.SDK.App
{
    public class AppInstance : MonoBehaviour
    {
        private void Start()
        {
            BundleManager.Instance.Init();
            UIManager.Instance.Init();
            DeviceManager.Instance.Init();
            this.Init();
        }

        protected virtual void Init()
        { 
        }
    }
}
