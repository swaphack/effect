using UnityEngine;
using UnityEngine.UI;
using Assets.Foundation.Managers;
using Assets.Foundation.DataAccess;
using Assets.Foundation.Extensions;
using System.IO;
using Assets.Foundation.Data;
using Assets.Game.Project;

namespace Assets.Foundation.UI
{
    /// <summary>
    /// ui管理类
    /// </summary>
    public class UIManager : Singleton<UIManager>
    {
        public GameObject root;

        private bool _init = false;

        void Start()
        {
            Init();
        }

        public void Init()
        {
            if (_init)
            {
                return;
            }
            _init = true;
            this.InitGameObject();
            this.LoadAssetBundle();
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

        /*
        void LoadAssetBundle()
        {
            string configPath = FilePath.GetBundlePath();
            
            string[] files = Directory.GetFiles(configPath, "*.unity3d");

            var bundles = BundleManager.Instance;
            for (int i = 0; i < files.Length; i++)
            {
                bundles.LoadFromFile(files[i]);
            }
        }
        */

        void LoadAssetBundle()
        {
            string configPath = FilePath.GetBundleManifestPath();

            WWW www = new WWW(configPath);
            while (!www.isDone) { };
            if (!string.IsNullOrEmpty(www.error))
            {
                return;
            }
            ABManifest manifest = new ABManifest();
            manifest.Read(www.text);
            var names = manifest.GetAllAssetBundles();
            if (names == null || names.Count == 0)
            {
                return;
            }

            var bundles = BundleManager.Instance;
            for (int i = 0; i < names.Count; i++)
            {
                string fullpath = Path.Combine(FilePath.GetBundlePath(), names[i]);
                bundles.LoadFromFile(fullpath);
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

            var rootRect = root.GetComponent<RectTransform>();

            var rect = frame.GetComponent<RectTransform>();
            if (rect != null)
            {
                rect.sizeDelta = rootRect.sizeDelta;
                rect.anchoredPosition = 0.5f * rootRect.sizeDelta;
                rect.anchorMin = Vector2.zero;
                rect.anchorMax = Vector2.one;
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
        public static void ShowUI<T>(params object[] data) where T : UIFrame, new()
        {
            var ui = UIManager.Instance;
            ui.Init();


            var bundles = BundleManager.Instance;
            var tempGo = new GameObject();
            var temp = tempGo.AddComponent<T>();
            var go = bundles.LoadAsset<GameObject>(string.Format("Assets/Bundles/Frames/{0}.prefab", temp.Path));
            if (go == null)
            {
                return;
            }

            var instance = GameObject.Instantiate(go);
            int begin = temp.Path.LastIndexOf("/") + 1;
            instance.name = temp.Path.Substring(begin, temp.Path.Length - begin);
            var frame = instance.AddComponent<T>();
            frame.InitWithParams(data);
            ui.AddFrame(frame);

            tempGo.RemoveFromParentAndCleanUp();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static void CloseUI<T>() where T : UIFrame
        {
            var ui = UIManager.Instance;
            var t = ui.GetComponentInChildren<T>();
            ui.RemoveFrame(t);
        }
    }
}
