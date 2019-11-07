using Game.Editor.GameDesign.Objects;
using Game.Editor.Layouts;
using UnityEditor;

namespace Game.Editor
{
    public static class GameEditorMenu
    {
        [MenuItem("Game Editor/Object Creator")]
        public static void ShowCreatorWindow()
        {
            EditorWindow.GetWindow<CreatorWindow>();
        }

        [MenuItem("Game Editor/Layout")]
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
