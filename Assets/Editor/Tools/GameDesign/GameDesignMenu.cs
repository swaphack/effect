using UnityEditor;
using UnityEngine;

namespace Assets.Editor.Tools.GameDesign
{
    public class GameDesignMenu
    {
        [MenuItem("GameDesign/Object Creator")]
        public static void ShowGameObjectCreator()
        {
            EditorWindow.GetWindow<GameObjectCreator>();
        }

        [MenuItem("GameDesign/Object Layout")]
        public static void ShowGameObjectLayout()
        {
            EditorWindow.GetWindow<GameObjectLayout>();
        }

        [MenuItem("GameDesign/Material Setting")]
        public static void ShowMaterialSetting()
        {
            EditorWindow.GetWindow<MaterialSetting>();
        }

        [MenuItem("GameDesign/Shader Setting")]
        public static void ShowShaderSetting()
        {
            EditorWindow.GetWindow<ShaderSetting>();
        }
    }
}