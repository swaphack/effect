using Assets.Editor.Widgets;
using UnityEditor;

namespace Assets.Editor.UIDesign
{
    /// <summary>
    /// 设计界面
    /// </summary>
    public class DesignWindow : UIWindow
    {
        [MenuItem("UIDesign/DesignWindow")]
        private static void ShowUIDesign()
        {
            EditorWindow.GetWindow<DesignWindow>();
        }

        protected override void InitUI(UIWidget layout)
        {

        }
    }
}
