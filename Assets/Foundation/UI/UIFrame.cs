using UnityEngine;
using UnityEngine.UI;
using Assets.Foundation.Extensions;
using UnityEngine.Events;
using Assets.Foundation.Effects;

namespace Assets.Foundation.UI
{
    /// <summary>
    /// ui界面
    /// </summary>
    public abstract class UIFrame : MonoBehaviour
    {
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

        /// <summary>
        /// 绑定事件
        /// </summary>
        /// <param name="btn"></param>
        /// <param name="func"></param>
        public void BindEvent(Button btn, UnityAction func)
        {
            if (btn == null || func == null)
            {
                return;
            }

            btn.onClick.AddListener(func);
        }

        /// <summary>
        /// 绑定事件
        /// </summary>
        /// <param name="name"></param>
        /// <param name="func"></param>
        public void BindEvent(string name, UnityAction func)
        {
            if (string.IsNullOrEmpty(name) || func == null)
            {
                return;
            }

            Button btn = this.FindControl<Button>(name);
            if (btn == null)
            {
                return;
            }

            BindEvent(btn, func);
        }

        /// <summary>
        /// 长按操作
        /// </summary>
        /// <param name="name"></param>
        /// <param name="press"></param>
        /// <param name="up"></param>
        public void BindPressEvent(string name, UnityAction pressFunc, UnityAction upFunc = null)
        {
            if (string.IsNullOrEmpty(name))
            {
                return;
            }

            Button btn = this.FindControl<Button>(name);
            if (btn == null)
            {
                return;
            }

            var pressButton = btn.gameObject.AddComponent<PressButton>();
            if (pressFunc != null)
            {
                pressButton.PressCallBack.AddListener(pressFunc);
            }
            if (upFunc != null)
            {
                pressButton.UpCallBack.AddListener(upFunc);
            }
        }

        public T GetModule<T>() where T : Component
        {
            var t = this.CreateComponent<T>();
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
    }
}
