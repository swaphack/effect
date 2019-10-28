﻿using Assets.Editor.Widgets;
using Assets.Foundation.Data;
using System;
using System.Collections.Generic;
using UnityEditor;

namespace Assets.Editor.Inspectors
{
    /// <summary>
    /// editor 视图
    /// </summary>
    public class UIInspector : UnityEditor.Editor
    {
        /// <summary>
        /// 布局
        /// </summary>
        private UIWidget _layout;

        /// <summary>
        /// 布局是否有修改
        /// </summary>
        private bool _dirty;
        /// <summary>
        /// 是否使用默认布局
        /// </summary>
        private bool _useDefaultInspector = false;
        public bool UseDefaultInspector
        {
            get { return _useDefaultInspector; }
            set { _useDefaultInspector = value; }
        }
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

        public UIInspector()
        {
            _layout = new UIWidget();
            _layout.Direction = LayoutDirection.Vertical;
            Dirty = true;
            UseDefaultInspector = false;
        }

        protected virtual void InitUI(UIWidget layout)
        {
            object t = target;
            var widget = UIWidgetHelper.CreateWidget(target.GetType().Name, t);
            if (widget != null)
            {
                layout.Add(widget);
            }
        }

        public override void OnInspectorGUI()
        {
            if (UseDefaultInspector)
            {
                base.OnInspectorGUI();
            }
            if (Dirty)
            {
                _layout.Clear();
                this.InitUI(_layout);
                Dirty = false;
            }

            this.ShowDisplay();
        }

        protected T GetTarget<T>() where T : UnityEngine.Object
        {
            return target as T;
        }

        protected void ShowDisplay()
        {
            if (_layout != null)
            {
                _layout.Draw();
            }
        }
    }
}
