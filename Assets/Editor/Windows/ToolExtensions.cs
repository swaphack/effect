using Assets.Editor.UIDesign;
using Assets.Editor.Windows.Editors;
using Assets.Editor.Windows.Tools;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor.Windows
{
    /// <summary>
    /// 窗体管理
    /// </summary>
    class ToolExtensions
    {
        [MenuItem("Tools/UI Design")]
        private static void ShowUIDesign()
        {
            EditorWindow.GetWindow<DesignWindow>();
        }

        /// <summary>
        /// 创建、显示窗体
        /// </summary>
        [MenuItem("Tools/Texture Settings")]
        private static void ShowTextureSettings()
        {
            EditorWindow.GetWindow<ImageSetting>();
        }

        [MenuItem("Tools/Bundle Pack")]
        private static void ShowBundlePack()
        {
            EditorWindow.GetWindow<BundlePack>();
        }

        [MenuItem("Tools/Test Window")]
        private static void ShowTestWindow()
        {
            EditorWindow.GetWindow<TestW>();
        }

        [MenuItem("Editors/Style Viewer")]
        public static void Init()
        {
            EditorWindow.GetWindow<EditorStyleViewer>();
        }
    }
}
