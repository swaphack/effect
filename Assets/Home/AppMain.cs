using Assets.Foundation.UI;
using Assets.Home.UI;
using Assets.SDK.App;

namespace Assets.Home
{
    public class AppMain : AppInstance
    {
        protected override void Init()
        {
            UIManager.ShowUI<MainUI>();
        }
    }
}


