using UnityEditor;

namespace Assets.Editor.Tools.Packages
{
    public class PackageMenu
    {
        [MenuItem("Packages/Asset Pack")]
        private static void ShowAssetPack()
        {
            EditorWindow.GetWindow<AssetPack>();
        }

        [MenuItem("Packages/Image Setting")]
        private static void ShowImageSetting()
        {
            EditorWindow.GetWindow<ImageSetting>();
        }
    }
}
