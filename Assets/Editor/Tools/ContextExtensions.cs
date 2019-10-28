using Assets.Editor.GameEditor;
using UnityEditor;

namespace Assets.Editor.Tools
{
    class ContextExtensions
    {
        [MenuItem("CONTEXT/Image/SelectTexture")]
        private static void SelectTexture()
        {
            EditorWindow.GetWindow<TextureSelectWindow>();
        }
    }
}
