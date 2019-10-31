using Assets.Editor.GameDesign.Objects;
using Assets.Editor.Layouts;
using UnityEditor;

namespace Assets.Editor
{
    public class GameEditorMenu
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
