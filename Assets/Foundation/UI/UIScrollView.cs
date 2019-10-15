using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Assets.Foundation.Extensions;

namespace Assets.Foundation.UI
{
    /// <summary>
    /// 滚动方向
    /// </summary>
    public enum ScrollDirection
    {
        /// <summary>
        /// 水平
        /// </summary>
        Horizontal,
        /// <summary>
        /// 垂直
        /// </summary>
        Vertical,
    }

    public abstract class UIScrollView : ScrollRect
    {
        public UIScrollView()
        {

        }

        protected override void OnEnable()
        {
            base.OnEnable();

            UpdateScrollView();
        }

        /// <summary>
        /// 添加物件
        /// </summary>
        /// <param name="item"></param>
        public void AddItem(UIBehaviour item)
        {
            if (item == null)
            {
                return;
            }

            content.gameObject.AddChild(item.gameObject);
            this.UpdateScrollView();
        }

        /// <summary>
        /// 移除物件
        /// </summary>
        /// <param name="item"></param>
        public void RemoveItem(UIBehaviour item)
        {
            if (item == null)
            {
                return;
            }

            content.gameObject.RemoveChild(item.gameObject);
            this.UpdateScrollView();
        }

        /// <summary>
        /// 移除所有物件
        /// </summary>
        public void RemoveAllItems()
        {
            content.gameObject.RemoveAllChildren();
            this.UpdateScrollView();
        }

        public void FormatScrollView()
        {
            this.UpdateScrollView();
        }

        protected virtual void UpdateScrollView()
        {
 
        }
    }
}
