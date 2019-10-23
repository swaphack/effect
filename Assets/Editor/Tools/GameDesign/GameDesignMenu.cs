using UnityEditor;
using UnityEngine;

namespace Assets.Editor.Tools.GameDesign
{
    public class GameDesignMenu
    {
        [MenuItem("GameDesign/Object Creator")]
        public static void ShowObjectCreator()
        {
            EditorWindow.GetWindow<ObjectCreator>();
        }
    }
}