using System;
using Game.Editor.GameDeploy.Configs;
using Game.Editor.GameDeploy.Packages;
using UnityEditor;

namespace Game.Editor.Tools.GameDeploy
{
    public static class GameDeployMenu
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
