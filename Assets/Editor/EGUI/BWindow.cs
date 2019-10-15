using System;
using UnityEngine;

namespace Assets.Editor.EGUI
{
    /// <summary>
    /// 窗口
    /// </summary>
    public class BWindow : Widget
    {
        /// <summary>
        /// 编号
        /// </summary>
        private int _id;
        /// <summary>
        /// 位置
        /// </summary>
        private Rect _clientRect;
        /// <summary>
        /// 触发事件
        /// </summary>
        private UnityEngine.GUI.WindowFunction _func;
        /// <summary>
        /// 编号
        /// </summary>
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }
        /// <summary>
        /// 位置
        /// </summary>
        public Rect ClientRect
        {
            get { return _clientRect; }
            set { _clientRect = value; }
        }
        /// <summary>
        /// 触发事件
        /// </summary>
        public UnityEngine.GUI.WindowFunction Func
        {
            get { return _func; }
            set { _func = value; }
        }

        protected override void OnDraw()
        {
            Rect rect = GUILayout.Window(ID, ClientRect, Func, Content, Option.Values);
            if (rect != ClientRect)
            {
                ClientRect = rect;
                this.DipatchEvent();
            }
        }
    }
}
