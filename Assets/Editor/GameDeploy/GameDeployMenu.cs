using System;
using Assets.Editor.GameDeploy.Configs;
using Assets.Editor.GameDeploy.Packages;
using UnityEditor;

namespace Assets.Editor.Tools.GameDeploy
{
    public class GameDeployMenu
    {
        [MenuItem("Game Deploy/Configs/Version")]
        public static void ShowVersionConfigWindow()
        {
            EditorWindow.GetWindow<VersionConfigWindow>();
        }

        [MenuItem("Game Deploy/Configs/Update")]
        public static void ShowUpdateConfigWindow()
        {
            EditorWindow.GetWindow<UpdateConfigWindow>();
        }

        [MenuItem("Game Deploy/Packages/Image Setting")]
        private static void ShowImageSettingWindow()
        {
            EditorWindow.GetWindow<ImageSettingWindow>();
        }

        [MenuItem("Game Deploy/Packages/Asset Pack")]
        private static void ShowAssetPackWindow()
        {
            EditorWindow.GetWindow<AssetPackWindow>();
        }        
    }
}
