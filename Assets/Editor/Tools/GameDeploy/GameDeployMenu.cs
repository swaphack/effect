using UnityEditor;

namespace Assets.Editor.Tools.GameDeploy
{
    public class GameDeployMenu
    {
        [MenuItem("GameDeploy/VersionSetting")]
        public static void ShowVersionSetting()
        {
            EditorWindow.GetWindow<VersionSetting>();
        }

        [MenuItem("GameDeploy/TestServer")]
        public static void ShowTestServer()
        {
            EditorWindow.GetWindow<TestServer>();
        }
    }
}
