using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Editor.EGUI
{
    /// <summary>
    /// 布局
    /// </summary>
    public abstract class Layout : Widget
    {
        /// <summary>
        /// 填充
        /// </summary>
        private float _padding;
        public float Padding
        {
            get { return _padding; }
            set { _padding = value; }
        }
        /// <summary>
        /// 内部控件间隔
        /// </summary>
        private float _innerSpace;
        /// <summary>
        /// 内部控件间隔
        /// </summary>
        public float InnerSpace
        {
            get 
            {
                return _innerSpace;
            }
            set 
            {
                _innerSpace = value;
            }
        }
        /// <summary>
        /// 子控件
        /// </summary>
        private List<IWidget> _widgets;

        protected List<IWidget> Children
        {
            get
            {
                return _widgets;
            }
        }

        public Layout()
        {
            _padding = 2;
            _innerSpace = 1;
            _widgets = new List<IWidget>();
        }

        /// <summary>
        /// 添加控件
        /// </summary>
        /// <param name="widget"></param>
        public void Add(IWidget widget)
        {
            if (widget == null)
            {
                return;
            }

            if (_widgets.Contains(widget))
            {
                return;
            }

            _widgets.Add(widget);
        }

        /// <summary>
        /// 插入控件
        /// </summary>
        /// <param name="indexWidget"></param>
        /// <param name="insertWidget"></param>
        public void InsertAfter(IWidget indexWidget, IWidget insertWidget)
        {
            if (indexWidget == null || insertWidget == null)
            {
                return;
            }

            if (this.Contains(insertWidget))
            {
                return;
            }

            int index = this.IndexOf(indexWidget);
            if (index == -1)
            {
                return;
            }
            this.Insert(index + 1, insertWidget);
        }


        /// <summary>
        /// 插入控件
        /// </summary>
        /// <param name="index"></param>
        /// <param name="widget"></param>
        public void Insert(int index, IWidget widget)
        {
            if (widget == null)
            {
                return;
            }

            if (this.Contains(widget))
            {
                return;
            }

            index = Mathf.Clamp(index, 0, _widgets.Count - 1);
            if (_widgets.Count == 0)
            {
                _widgets.Add(widget);
            }
            else if (index >= _widgets.Count - 1)
            {
                _widgets.Add(widget);
            }
            else
            {
                _widgets.Insert(index, widget);
            }
        }

        /// <summary>
        /// 控件所在位置
        /// </summary>
        /// <param name="widget"></param>
        /// <returns></returns>
        public int IndexOf(IWidget widget)
        {
            if (widget == null)
            {
                return - 1;
            }

            return _widgets.IndexOf(widget);
        }

        /// <summary>
        /// 移除控件
        /// </summary>
        /// <param name="widget"></param>
        public void Remove(IWidget widget)
        {
            if (widget == null)
            {
                return;
            }

            if (!_widgets.Contains(widget))
            {
                return;
            }

            _widgets.Remove(widget);
        }

        /// <summary>
        /// 移除控件
        /// </summary>
        /// <param name="index"></param>
        public void RemoveAt(int index)
        {
            _widgets.RemoveAt(index);
        }

        /// <summary>
        /// 清空
        /// </summary>
        public void Clear()
        {
            _widgets.Clear();
        }

        public bool Contains(IWidget widget)
        {
            if (widget == null)
            {
                return false;
            }

            return _widgets.Contains(widget);
        }

        private void AddPadding()
        {
            if (Padding >= 0)
            {
                GUILayout.Space(Padding);
            }
            else
            {
                GUILayout.FlexibleSpace();
            }

        }

        private void AddInnerSpace()
        {
            if (InnerSpace >= 0)
            {
                GUILayout.Space(InnerSpace);
            }
            else
            {
                GUILayout.FlexibleSpace();
            }
        }

        /// <summary>
        /// 开始绘制
        /// </summary>
        protected override void BeginDraw()
        {
        }

        /// <summary>
        /// 结束绘制
        /// </summary>
        protected override void EndDraw()
        {
        }

        protected override void OnDraw()
        {
            var items = new List<IWidget>();
            items.AddRange(_widgets);

            this.AddPadding();
            foreach (var item in items)
            {
                item.Draw();
                if (items[items.Count - 1] != item)
                {
                    this.AddInnerSpace();
                }
            }

            this.AddPadding();
        }
    }

    /// <summary>
    /// 水平布局
    /// </summary>
    public class HorizontalLayout : Layout
    {
        public HorizontalLayout()
        {
        }

        protected override void BeginDraw()
        {
            GUILayout.BeginHorizontal(Option.Values);
        }

        protected override void EndDraw()
        {
            GUILayout.EndHorizontal();
        }
    }

    /// <summary>
    /// 垂直布局
    /// </summary>
    public class VerticalLayout : Layout
    {
        public VerticalLayout()
        {
        }
        protected override void BeginDraw()
        {
            GUILayout.BeginVertical(Option.Values);
        }

        protected override void EndDraw()
        {
            GUILayout.EndVertical();
        }
    }
}
