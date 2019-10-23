using Assets.Foundation.UI;
using Assets.Home.UI;
using Assets.App;
using Assets.Foundation.Devices;
using Assets.Foundation.Events;
using UnityEngine;
using Assets.Foundation.SimpleEffect;

namespace Assets.Home
{
    public class AppMain : AppInstance
    {
        private void Start()
        {
            var role = GameObject.Find("Role");
            if (role != null)
            {
                UIManager.ShowUI<MainUI>(role);

                UIManager.ShowUI<RoleControlUI>(role);
            }
        }

        protected override void Init()
        {
            UIManager.Init();
            InputManager.Init();
            TouchManager.Init();
            AppTime.Init();
        }
    }
}


