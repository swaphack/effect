using Assets.Editor.Tools.EditorControl;
using UnityEditor;

namespace Assets.Editor.Tools
{
    class ContextExtensions
    {
        [MenuItem("CONTEXT/Image/SelectTexture")]
        private static void SelectTexture()
        {
            EditorWindow.GetWindow<TextureDisplay>();
        }
    }
}
