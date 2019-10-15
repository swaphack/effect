using Assets.Editor.EGUI;
using Assets.Editor.Widgets;
using UnityEngine;


namespace Assets.Editor.Windows
{
    class TestW : UIWindow
    {
        protected override void InitUI(UIDisplay layout)
        {
            BWindow window = new BWindow();
            window.ClientRect = new Rect(100, 100, 640, 960);
            layout.Add(window);
        }
    }
}
