using UnityEngine;
using Assets.Foundation.Extensions;
using Assets.App;

namespace Assets.Foundation.Managers
{
    public interface ISingletonBehaviour
    {
        void Initialize();
    }

    /// <summary>
    /// 单例
    /// </summary>
    public class SingletonBehaviour
    {
        public static GameObject getRoot()
        {
            if (AppInstance.IsQuit == true)
            {
                return null;
            }
            if (AppInstance.App == null)
            {
                return null;
            }
            return AppInstance.App.gameObject;
        }

        public static T GetInstance<T>() where T : MonoBehaviour
        {
            var root = getRoot();
            if (root == null)
            {
                return null;
            }
            var t = root.CreateComponent<T>();
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
    }

    /// <summary>
    /// 单例
    /// </summary>
    public class SingletonBehaviour<T> : MonoBehaviour, ISingletonBehaviour where T : MonoBehaviour, ISingletonBehaviour
    {
        public static T Instance
        {
            get
            {
                var t = SingletonBehaviour.GetInstance<T>();
                if (t != null)
                {
                    t.Initialize();
                }
                return t;
            }
        }

        public static void Init()
        {
            var t = SingletonBehaviour.GetInstance<T>();
            if (t != null)
            {
                t.Initialize();
            }
            return;
        }

        public virtual void Initialize()
        {
        }
    }
}
