using Assets.Editor.GameDesign.Settings;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor.GameDesign
{
    public class GameDesignMenu
    {
        [MenuItem("Game Design/Settings/Material Setting")]
        public static void ShowMaterialSettingWindow()
        {
            EditorWindow.GetWindow<MaterialSettingWindow>();
        }

        [MenuItem("Game Design/Settings/Shader Setting")]
        public static void ShowShaderSettingWindow()
        {
            EditorWindow.GetWindow<ShaderSettingWindow>();
        }
    }
}