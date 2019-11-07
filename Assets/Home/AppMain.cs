using Game.Foundation.UI;
using Game.Home.UI;
using Game.App;
using Game.Foundation.Devices;
using Game.Foundation.Events;
using UnityEngine;
using Game.SDK.Project;
using Game.Foundation.Common;
using Game.Foundation.DataAccess;
using Game.Foundation.Controller;
using Game.Foundation.Extensions;

namespace Game.Home
{
    public class AppMain : AppInstance
    {
        private void Start()
        {
            var role = GameObject.Find("Role");
            if (role != null)
            {
                UIManager.ShowUI<RoleControlUI>(role);
            }

            //new GameWorkflow().Start();

        }

        protected override void Init()
        {
            EnumeratorManager.Init();
            DownloadManager.Init();
            UIManager.Init();
            InputManager.Init();
            TouchManager.Init();
            AppTime.Init();
        }
    }
}


