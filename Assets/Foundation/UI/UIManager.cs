using UnityEngine;
using UnityEngine.UI;
using Assets.Foundation.Managers;
using Assets.Foundation.DataAccess;
using Assets.Foundation.Extensions;
using System.IO;
using Assets.Foundation.Data;
using Assets.SDK.Project;

namespace Assets.Foundation.UI
{
    /// <summary>
    /// ui管理类
    /// </summary>
    public class UIManager : SingletonBehaviour<UIManager>
    {
        public GameObject root;

        private bool _init = false;

        public override void Initialize()
        {
            if (_init)
            {
                return;
            }
            _init = true;
            this.InitGameObject();
        }


        void InitGameObject()
        {
            if (root == null)
            {
                root = GameObject.Find("UI");
                if (root == null)
                {
                    root = new GameObject();
                    root.name = "UI";
                    var canvas = root.AddComponent<Canvas>();
                    canvas.renderMode = RenderMode.ScreenSpaceOverlay;
                    var canvasScaler = root.AddComponent<CanvasScaler>();
                    canvasScaler.scaleFactor = 1;
                    canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ConstantPixelSize;
                    var raycaster = root.AddComponent<GraphicRaycaster>();
                    raycaster.ignoreReversedGraphics = true;
                }
            }
        }
        

        /// <summary>
        /// 添加界面
        /// </summary>
        /// <param name="frame"></param>
        public void AddFrame(UIFrame frame)
        {
            if (root == null)
            {
                return;
            }
            if (frame == null)
            {
                return;
            }

            var rect = frame.GetComponent<RectTransform>();
            if (rect != null)
            {
                rect.anchorMin = Vector2.zero;
                rect.anchorMax = Vector2.one;

                rect.offsetMax = Vector2.zero;
                rect.offsetMin = Vector2.zero;
            }

            root.AddChild(frame.gameObject);
        }
        /// <summary>
        /// 移除界面
        /// </summary>
        /// <param name="frame"></param>
        public void RemoveFrame(UIFrame frame)
        {
            if (root == null)
            {
                return;
            }
            if (frame == null)
            {
                return;
            }
            root.RemoveChild(frame.gameObject);
        }

        /// <summary>
        /// 移除所有界面
        /// </summary>
        public void RemoveAllFrames()
        {
            if (root == null)
            {
                return;
            }
            root.RemoveAllChildren();
        }

        /// <summary>
        /// 显示界面
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        public static void ShowUI<T>(params object[] data) where T : UIFile, new()
        {
            var root = UIManager.Instance.root;
            var temp = root.AddComponent<T>();
            string assetPath = string.Format("UI/{0}", temp.Path);
            Debug.LogFormat("ui asset path {0}", assetPath);
            GameObject go = FilePath.Instance.LoadAssetAtPath<GameObject>(assetPath);
            if (go == null)
            {
                Object.DestroyImmediate(temp);
                return;
            }

            var instance = GameObject.Instantiate(go);
            int begin = temp.Path.LastIndexOf("/") + 1;
            instance.name = temp.Path.Substring(begin, temp.Path.Length - begin);
            var frame = instance.AddComponent<T>();
            frame.InitWithParams(data);
            UIManager.Instance.AddFrame(frame);

            Object.DestroyImmediate(temp);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static void CloseUI<T>() where T : UIFile
        {
            var ui = UIManager.Instance;
            var t = ui.GetComponentInChildren<T>();
            ui.RemoveFrame(t);
        }
    }
}
