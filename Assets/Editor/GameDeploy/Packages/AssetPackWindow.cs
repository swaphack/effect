using Assets.Editor.DataAccess;
using Assets.Editor.Widgets;
using Assets.Foundation.DataAccess;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;

namespace Assets.Editor.GameDeploy.Packages
{
    class AssetPackWindow : UIWindow
    {
        public enum Platform
        {
            Android = BuildTarget.Android,
            iOS = BuildTarget.iOS,
            WindowsEditor = BuildTarget.StandaloneWindows64,
            WindowsPlayer = BuildTarget.StandaloneWindows64,
            StandaloneWindows64 = BuildTarget.StandaloneWindows64,
            StandaloneOSX = BuildTarget.StandaloneOSX,
        };

        private struct PlatformItem
        {
            public string name { get; set; }
            public int index { get; set; }
            public int value { get; set; }

            public PlatformItem(int idx, string n, int v)
            {
                index = idx;
                name = n;
                value = v;
            }
        }

        /// <summary>
        /// 打包平台
        /// </summary>
        private List<PlatformItem> PlatformInfo = new List<PlatformItem>();
        private int platformIndex = 2;

        private UIFolder bundleFolder = new UIFolder();
        private UIFolder outputFolder = new UIFolder();

        void OnEnable()
        {
            PlatformInfo.Clear();

            var eType = typeof(Platform);
            var names = Enum.GetNames(eType);
            var values = Enum.GetValues(eType);
            for (int i = 0; i < names.Length; i++)
            {
                PlatformInfo.Add(new PlatformItem(i, names[i], (int)values.GetValue(i)));
                if (names[i] == "WindowsEditor")
                {
                    platformIndex = i;
                }
            }
        }

        protected override void InitUI(UIWidget layout)
        {
            EditorVerticalLayout innerLayout = new EditorVerticalLayout();
            innerLayout.InnerSpace = 10;
            layout.Add(innerLayout);

            {
                EditorHorizontalLayout hLayout = new EditorHorizontalLayout();
                hLayout.InnerSpace = 10;

                EditorPrefixLabel label = new EditorPrefixLabel();
                label.Text = "Asset Path :";
                hLayout.Add(label);

                bundleFolder.Text = "Open";
                string bundles = FilePath.BundlesPath;
                bundleFolder.Filepath = Path.Combine(EditorAssets.Root, bundles).Replace("\\", "/");
                hLayout.Add(bundleFolder);

                innerLayout.Add(hLayout);
            }
            {
                EditorHorizontalLayout hLayout = new EditorHorizontalLayout();
                hLayout.InnerSpace = 10;

                EditorPrefixLabel label = new EditorPrefixLabel();
                label.Text = "Out Path :";
                hLayout.Add(label);

                outputFolder.Filepath = Path.Combine(EditorAssets.Root, "Output").Replace("\\", "/");
                outputFolder.Text = "Open";
                hLayout.Add(outputFolder);

                innerLayout.Add(hLayout);
            }

            {
                VerticalLayout vLayout = new VerticalLayout();
                vLayout.InnerSpace = 10;

                EditorIntPopup popup = new EditorIntPopup();
                popup.Text = "Choose Platform :";
                popup.Value = platformIndex;
                popup.TriggerHandler = (Widget w) => 
                {
                    platformIndex = popup.Value;
                };

                for (int i = 0; i < PlatformInfo.Count; i++)
                {
                    popup.Add(new EditorIntPopup.DisplayItem(PlatformInfo[i].name, PlatformInfo[i].index));
                }
                
                vLayout.Add(popup);

                GUIButton button = new GUIButton();
                button.Text = "Pack";
                button.TriggerHandler = (Widget w) =>
                {
                    Pack((Platform)PlatformInfo[platformIndex].value, PlatformInfo[platformIndex].name);
                };
                vLayout.Add(button);

                innerLayout.Add(vLayout);
            }
        }

        private void Pack(Platform platform, string name)
        {
            string destPath = Path.Combine(outputFolder.Filepath, name);
            string srcPath = bundleFolder.Filepath;
            Pack(srcPath, destPath, (BuildTarget)platform);
        }

        private void SetBuildPath(ref AssetBundleBuild build, string output, string path, string name, BuildTarget target)
        {
            string[] files = EditorAssets.GetFilePaths(path, "*");
            build.assetBundleName = name + ".unity3d";
            build.assetNames = files;
        }

        public void Pack(string srcPath, string destPath, BuildTarget target)
        {
            if (!Directory.Exists(srcPath))
            {
                return;
            }

            if (!Directory.Exists(destPath))
            {
                Directory.CreateDirectory(destPath);
            }
            string[] dirs = Directory.GetDirectories(srcPath);
            if (dirs == null || dirs.Length == 0)
            {
                return;
            }

            AssetBundleBuild[] buildMap = new AssetBundleBuild[dirs.Length];
            for (int i = 0; i < dirs.Length; i++)
            {
                string name = dirs[i].Substring(srcPath.Length);
                name = name.Replace("/", "");
                name = name.Replace("\\", "");
                SetBuildPath(ref buildMap[i], destPath, Path.Combine(srcPath, dirs[i]), name, target);
            }
            BuildPipeline.BuildAssetBundles(destPath, buildMap, BuildAssetBundleOptions.None, target);
            //刷新
            AssetDatabase.Refresh();
        }
    }
}
