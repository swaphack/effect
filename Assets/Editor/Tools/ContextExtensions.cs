using Game.Editor;
using UnityEditor;

namespace Game.Editor.Tools
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
