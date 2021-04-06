using Game.Editor.GameDesign.Settings;
using Game.Editor.GameDesign.Terrains;
using UnityEditor;
using UnityEngine;

namespace Game.Editor.GameDesign
{
    public static class GameDesignMenu
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

        [MenuItem("Game Design/Terrains/Create Terrain")]
        public static void OpenObject2TerrainWindow()
        {
            EditorWindow.GetWindow<Object2TerrainWindow>(true);
        }

        [MenuItem("Game Design/Terrains/Export Terrain")]
        public static void OpenExportTerrainWindow()
        {
            EditorWindow.GetWindow<ExportTerrainWindow>(true);
        }

        /*
        [MenuItem("Game Design/Terrains/Terrain2Mesh")]
        public static void OpenTerrain2MeshWindow()
        {
            EditorWindow.GetWindow<Terrain2MeshWindow>(true);
        }
        */
    }
}