using UnityEditor;

namespace Assets.Editor.Tools.EditorControl
{
    public class EditorControlMenu
    {
        [MenuItem("EditorControl/Style Viewer")]
        public static void ShowStyleViewer()
        {
            EditorWindow.GetWindow<EditorStyleViewer>();
        }

        /// <summary>
        /// 创建、显示窗体
        /// </summary>
        [MenuItem("EditorControl/Texture Settings")]
        private static void ShowTextureSettings()
        {
            EditorWindow.GetWindow<ImageSetting>();
        }
    }
}
