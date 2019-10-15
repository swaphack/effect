using UnityEngine;

namespace Assets.Foundation.Managers
{
    /// <summary>
    /// 单例
    /// </summary>
    public class Singleton : MonoBehaviour
    {
        /// <summary>
        /// 退出应用，防止重新生成
        /// </summary>
        private static bool _quitApplication = false;
        protected static GameObject getRoot()
        {
            if (_quitApplication == true)
            {
                return null;
            }
            var root = GameObject.Find("Singleton");
            if (root == null)
            {
                root = new GameObject();
                root.name = "Singleton";
                DontDestroyOnLoad(root);
            }

            return root;
        }

        public static T GetInstance<T>() where T : MonoBehaviour
        {
            var root = getRoot();
            if (root == null)
            {
                return null;
            }
            var t = root.GetComponent<T>();
            if (t == null)
            {
                t = root.AddComponent<T>();
            }
            return t;
        }

        public static bool isValid<T>() where T : MonoBehaviour
        {
            var root = getRoot();
            if (root == null)
            {
                return false;
            }
            var t = root.GetComponent<T>();
            return t != null;
        }

        private void OnDestroy()
        {
            _quitApplication = true;
        }
    }
    /// <summary>
    /// 单例
    /// </summary>
    public class Singleton<T> : Singleton where T : MonoBehaviour
    {
        public static T Instance
        {
            get
            {
                var root = getRoot();
                if (root == null)
                {
                    return null;
                }
                var t = root.GetComponent<T>();
                if (t == null)
                {
                    t = root.AddComponent<T>();
                }
                return t;
            }
        }
    }
}
