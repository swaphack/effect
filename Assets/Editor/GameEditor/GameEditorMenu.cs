using Assets.Editor.GameDesign.Objects;
using Assets.Editor.GameEditor.Layouts;
using UnityEditor;

namespace Assets.Editor.GameEditor
{
    public class GameEditorMenu
    {
        [MenuItem("Game Design/Objects/Object Creator")]
        public static void ShowCreatorWindow()
        {
            EditorWindow.GetWindow<CreatorWindow>();
        }

        /// <summary>
        /// 创建、显示窗体
        /// </summary>
        [MenuItem("Game Editor/Objects/Texture Select")]
        private static void ShowTextureSelectWindow()
        {
            EditorWindow.GetWindow<TextureSelectWindow>();
        }

        [MenuItem("Game Editor/Layouts/Layout")]
        public static void ShowLayoutWindow()
        {
            EditorWindow.GetWindow<LayoutWindow>();
        }

        [MenuItem("Game Editor/Style Viewer")]
        public static void ShowStyleViewer()
        {
            EditorWindow.GetWindow<EditorStyleViewer>();
        }
    }
}
