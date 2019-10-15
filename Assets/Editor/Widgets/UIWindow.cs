using UnityEditor;
using UnityEngine;
using System.IO;
using Assets.Editor.EGUI;

namespace Assets.Editor.Widgets
{
    /// <summary>
    /// 编辑器界面
    /// </summary>
    public class UIWindow : EditorWindow
    {
        /// <summary>
        /// 布局
        /// </summary>
        private UIDisplay _layout;

        private Rect _lastRect;

        /// <summary>
        /// 布局是否有修改
        /// </summary>
        private bool _dirty;

        /// <summary>
        /// 布局是否有修改
        /// </summary>
        public bool Dirty
        {
            get
            {
                return _dirty;
            }
            set
            {
                _dirty = value;
            }
        }

        public UIWindow()
        {
            this.titleContent = new GUIContent(this.GetType().Name);

            _layout = new UIDisplay();
            
        }

        protected virtual void InitUI(UIDisplay layout)
        {
 
        }

        protected void ShowDisplay()
        {
            if (_layout != null)
            {
                _layout.Draw();
            }
        }

        void OnGUI()
        {
            if (_lastRect.size != this.position.size)
            {
                _lastRect = this.position;
                Dirty = true;
            }

            if (Dirty)
            {
                _layout.Clear();
                this.InitUI(_layout);

                Dirty = false;
            }

            ShowDisplay();
        }
    }
}
