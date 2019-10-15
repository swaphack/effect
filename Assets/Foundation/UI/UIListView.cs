using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Assets.Foundation.Extensions;

namespace Assets.Foundation.UI
{
    /// <summary>
    /// 滑动列表
    /// </summary>
    public class UIListView : UIScrollView
    {
        /// <summary>
        /// 滚动方向
        /// </summary>
        private ScrollDirection _direction;

        /// <summary>
        /// 滚动方向
        /// </summary>
        public ScrollDirection Direction
        {
            get 
            { 
                return _direction; 
            }
            set 
            { 
                _direction = value;
            }
        }

        public UIListView()
        {
            Direction = ScrollDirection.Horizontal;
            movementType = MovementType.Clamped;
        }

        private List<UIBehaviour> GetItems()
        {
            List<UIBehaviour> items = new List<UIBehaviour>();
            var children = content.gameObject.GetChildren();
            foreach (var child in children)
            {
                items.Add(child.GetComponent<UIBehaviour>());
            }
            return items;
        }

        private void UpdateHorizontalItems()
        {
            var temp = content.GetComponent<VerticalLayoutGroup>();
            if (temp != null)
            {
                Component.DestroyImmediate(temp);
            }

            var layout = content.GetComponent<HorizontalLayoutGroup>();
            if (layout == null)
            {
                layout = content.gameObject.AddComponent<HorizontalLayoutGroup>();
                SetContentPivot(layout.childAlignment, content);
                SetLayoutParams(layout);
            }
            var items = GetItems();
            float w = layout.padding.left + layout.padding.right;

            foreach (var item in items)
            {
                var r = item.GetComponent<RectTransform>();
                w += r.GetWidth();
            }

            if (items.Count > 1)
            {
                w += (items.Count - 1) * layout.spacing;
            }

            //content.SetWidth(w);
            //content.SetHeight(viewport.rect.height);
            content.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,w);
            content.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, viewport.rect.height);
        }

        private void UpdateVerticalItems()
        {
            var temp = content.GetComponent<HorizontalLayoutGroup>();
            if (temp != null)
            {
                Component.DestroyImmediate(temp);
            }
            var layout = content.GetComponent<VerticalLayoutGroup>();
            if (layout == null)
            {
                layout = content.gameObject.AddComponent<VerticalLayoutGroup>();
                SetContentPivot(layout.childAlignment, content);
                SetLayoutParams(layout);
            }
            var items = GetItems();
            float h = layout.padding.top + layout.padding.bottom;

            foreach (var item in items)
            {
                var r = item.GetComponent<RectTransform>();
                h += r.GetHeight();
            }

            if (items.Count > 1)
            {
                h += (items.Count - 1) * layout.spacing;
            }

            //content.SetWidth(viewport.rect.width);
            //content.SetHeight(h);

            content.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, viewport.rect.width);
            content.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, h);
        }

        private void SetContentPivot(TextAnchor childAlignment, RectTransform rect)
        {
            switch (childAlignment)
            {
                case TextAnchor.UpperLeft: rect.pivot = new Vector2(0, 1); break;
                case TextAnchor.UpperCenter: rect.pivot = new Vector2(0.5f, 1); break;
                case TextAnchor.UpperRight: rect.pivot = new Vector2(1, 1); break;
                case TextAnchor.MiddleLeft: rect.pivot = new Vector2(0, 0.5f); break;
                case TextAnchor.MiddleCenter: rect.pivot = new Vector2(0.5f, 0.5f); break;
                case TextAnchor.MiddleRight: rect.pivot = new Vector2(1, 0.5f); break;
                case TextAnchor.LowerLeft: rect.pivot = new Vector2(0, 0); break;
                case TextAnchor.LowerCenter: rect.pivot = new Vector2(0.5f, 0); break;
                case TextAnchor.LowerRight: rect.pivot = new Vector2(1, 0); break;
            }
        }

        protected virtual void SetLayoutParams(HorizontalOrVerticalLayoutGroup layout)
        {
            if (layout == null)
            {
                return;
            }

            layout.childControlWidth = false;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = true;
            layout.childForceExpandHeight = true;
        }

        protected override void UpdateScrollView()
        {
            if (content == null || viewport == null)
            {
                return;
            }            

            switch (Direction)
            {
                case ScrollDirection.Horizontal:
                    {
                        UpdateHorizontalItems();
                        break;
                    }
                case ScrollDirection.Vertical:
                    {
                        UpdateVerticalItems();
                        break;
                    }
            }
        }
    }
}
