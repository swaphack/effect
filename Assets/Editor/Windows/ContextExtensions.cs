using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using Assets.Editor.Windows.Tools;

namespace Assets.Editor.Windows
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
