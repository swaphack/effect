using UnityEngine;
using UnityEngine.UI;
using Assets.Foundation.Extensions;

namespace Assets.Foundation.UI
{
    /// <summary>
    /// ui界面
    /// </summary>
    public abstract class UIFrame : MonoBehaviour
    {
        public virtual string Path
        {
            get
            {
                return "";
            }
        }

        /// <summary>
        /// 获取控件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        public T FindControl<T>(string name) where T : Component
        {
            var go = this.gameObject.FindChildWithRecurse(name);
            if (go == null)
            {
                return null;
            }
            return go.GetComponent<T>();
        }

        public T GetModule<T>() where T : Component
        {
            var t = this.GetComponent<T>();
            if (t == null)
            {
                t = this.gameObject.AddComponent<T>();
            }

            return t;
        }

        private void Start()
        {
            this.InitControls();
            this.InitLogic();
        }

        /// <summary>
        /// 初始化控件
        /// </summary>
        protected abstract void InitControls();
        /// <summary>
        /// 初始化逻辑
        /// </summary>
        protected abstract void InitLogic();
        /// <summary>
        /// 赋值
        /// </summary>
        /// <param name="data"></param>
        public abstract void InitWithParams(params object[] data);
    }
}
