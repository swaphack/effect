﻿using Assets.Foundation.UI;
using Assets.Home.UI;
using Assets.SDK.App;
using Assets.Foundation.DataAccess;
using Assets.Foundation.Device;
using Assets.Foundation.Events;
using Assets.Foundation.Managers;
using UnityEngine;

namespace Assets.Home
{
    public class AppMain : AppInstance
    {
        protected override void Init()
        {
        	UIManager.Init();
            DeviceManager.Init();
            TouchManager.Init();

            //UIManager.ShowUI<MainCity>();
            UIManager.ShowUI<MainUI>();

            var role = GameObject.Find("Role");
            if (role != null)
            {
                UIManager.ShowUI<RoleControlUI>(role);
            }
        }
    }
}


