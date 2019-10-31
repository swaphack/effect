using Assets.Foundation.DataAccess;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor.Widgets
{
    /// <summary>
    /// gui事件
    /// </summary>
    /// <param name="?"></param>
    public delegate void WidgetEvent(Widget widget);

    /// <summary>
    /// 控件
    /// </summary>
    public abstract class Widget : IWidget
    {
        /// <summary>
        /// 布局参数
        /// </summary>
        public LayoutOption Option { get; }

        /// <summary>
        /// 控件内容
        /// </summary>
        public GUIContent Content { get; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// 图片路径
        /// </summary>
        public string ImagePath { get; set; }

        /// <summary>
        /// 图片和文字的位置关系
        /// </summary>
        public ImagePosition ImagePosition { get; set; }

        /// <summary>
        /// 提示
        /// </summary>
        public string Tooltip { get; set; }

        /// <summary>
        /// 触发事件
        /// </summary>
        public WidgetEvent TriggerHandler { get; set; }

        /// <summary>
        /// 派发事件
        /// </summary>
        protected void DipatchEvent()
        {
            TriggerHandler?.Invoke(this);
        }

        private bool _initContent;
        private bool _initStyle;


        public Widget()
        {
            ImagePosition = ImagePosition.ImageAbove;
            Option = new LayoutOption();
            Content = new GUIContent();
        }

        public void Draw()
        {
            if (_initContent == false)
            {
                this.InitContent();
                _initContent = true;
            }

            if (_initStyle == false)
            {
                this.InitStyle();
                _initStyle = true;
            }

            this.UpdateContentAndStyle();

            this.BeginDraw();
            this.OnDraw();
            this.EndDraw();
        }

        protected virtual void UpdateContentAndStyle()
        {
            this.UpdateContent();
            this.UpdateStyle();
        }

        protected virtual void InitContent()
        { 

        }

        protected virtual void UpdateContent()
        {
            var texture = AssetDatabase.LoadAssetAtPath<Texture>(ImagePath);
            Content.image = texture;
            Content.text = Text;
            Content.tooltip = Tooltip;
        }

        protected virtual void InitStyle()
        { 
        }

        protected virtual void UpdateStyle()
        { 
        }

        protected virtual void OnDraw()
        {
        }

        protected virtual void BeginDraw()
        {
        }

        protected virtual void EndDraw()
        {
        }

        private GUIStyle _style;

        protected GUIStyle Style
        {
            get { return _style; }
            set { _style = value; }
        }

        /*
        public GUIStyleState ActiveState
        {
            get { return _style.active; }
            set { _style.active = value; }
        }
        public GUIStyleState NormalState
        {
            get { return _style.normal; }
            set { _style.normal = value; }
        }
        public GUIStyleState HoverState
        {
            get { return _style.hover; }
            set { _style.hover = value; }
        }
        public GUIStyleState FocusedState
        {
            get { return _style.focused; }
            set { _style.focused = value; }
        }
        public GUIStyleState OnActiveState
        {
            get { return _style.onActive; }
            set { _style.onActive = value; }
        }
        public GUIStyleState OnFocusedState
        {
            get { return _style.onFocused; }
            set { _style.onFocused = value; }
        }
        public GUIStyleState OnHoverState
        {
            get { return _style.onHover; }
            set { _style.onHover = value; }
        }
        public GUIStyleState OnNormalState
        {
            get { return _style.onNormal; }
            set { _style.onNormal = value; }
        }


        public RectOffset Border
        {
            get { return _style.border; }
            set { _style.border = value; }
        }

        public RectOffset Overflow
        {
            get { return _style.overflow; }
            set { _style.overflow = value; }
        }

        public RectOffset Margin
        {
            get { return _style.margin; }
            set { _style.margin = value; }
        }

        public RectOffset Padding
        {
            get { return _style.padding; }
            set { _style.padding = value; }
        }

        public bool StretchHeight
        {
            get { return _style.stretchHeight; }
            set { _style.stretchHeight = value; }
        }

        public bool StretchWidth
        {
            get { return _style.stretchWidth; }
            set { _style.stretchWidth = value; }
        }

        public float FixedHeight
        {
            get { return _style.fixedHeight; }
            set { _style.fixedHeight = value; }
        }

        public float FixedWidth
        {
            get { return _style.fixedWidth; }
            set { _style.fixedWidth = value; }
        }

        public Vector2 ContentOffset
        {
            get { return _style.contentOffset; }
            set { _style.contentOffset = value; }
        }
        */
    }
}
