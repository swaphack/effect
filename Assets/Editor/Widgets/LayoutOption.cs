using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Editor.Widgets
{
    /// <summary>
    /// 布局
    /// </summary>
    public class LayoutOption
    {
        /// <summary>
        /// 是否可缩放高度
        /// </summary>
        private bool _expandHeight;
        /// <summary>
        /// 是否可缩放宽度
        /// </summary>
        private bool _expandWidth;
        /// <summary>
        /// 高度
        /// </summary>
        private float _height;
        /// <summary>
        /// 宽度
        /// </summary>
        private float _width;
        /// <summary>
        /// 最小高度
        /// </summary>
        private float _minHeight;
        /// <summary>
        /// 最小开宽度
        /// </summary>
        private float _minWidth;
        /// <summary>
        /// 最大高度
        /// </summary>
        private float _maxHeight;
        /// <summary>
        /// 最大宽度
        /// </summary>
        private float _maxWidth;
        /// <summary>
        /// 是否可缩放高度
        /// </summary>
        public bool ExpandHeight
        {
            get
            {
                return _expandHeight;
            }
            set
            {
                _expandHeight = value;
            }
        }
        /// <summary>
        /// 是否可缩放宽度
        /// </summary>
        public bool ExpandWidth
        {
            get
            {
                return _expandWidth;
            }
            set
            {
                _expandWidth = value;
            }
        }
        /// <summary>
        /// 高度
        /// </summary>
        public float Height
        {
            get
            {
                return _height;
            }
            set
            {
                _height = value;
            }
        }
        /// <summary>
        /// 宽度
        /// </summary>
        public float Width
        {
            get
            {
                return _width;
            }
            set
            {
                _width = value;
            }
        }
        /// <summary>
        /// 最小高度
        /// </summary>
        public float MinHeight
        {
            get
            {
                return _minHeight;
            }
            set
            {
                _minHeight = value;
            }
        }
        /// <summary>
        /// 最小宽度
        /// </summary>
        public float MinWidth
        {
            get
            {
                return _minWidth;
            }
            set
            {
                _minWidth = value;
            }
        }
        /// <summary>
        /// 最大高度
        /// </summary>
        public float MaxHeight
        {
            get
            {
                return _maxHeight;
            }
            set
            {
                _maxHeight = value;
            }
        }
        /// <summary>
        /// 最大宽度
        /// </summary>
        public float MaxWidth
        {
            get
            {
                return _maxWidth;
            }
            set
            {
                _maxWidth = value;
            }
        }

        private GUILayoutOption[] GetLayoutOptions()
        {
            List<GUILayoutOption> lstOption = new List<GUILayoutOption>();
            if (_expandHeight)
            {
                lstOption.Add(GUILayout.ExpandHeight(_expandHeight));
            }

            if (_expandWidth)
            {
                lstOption.Add(GUILayout.ExpandWidth(_expandWidth));
            }

            if (_height != 0)
            {
                lstOption.Add(GUILayout.Height(_height));
            }

            if (_width != 0)
            {
                lstOption.Add(GUILayout.Width(_width));
            }

            if (_minHeight != 0)
            {
                lstOption.Add(GUILayout.MinHeight(_minHeight));
            }

            if (_minWidth != 0)
            {
                lstOption.Add(GUILayout.MinWidth(_minWidth));
            }

            if (_maxHeight != 0)
            {
                lstOption.Add(GUILayout.MaxHeight(_maxHeight));
            }

            if (_maxWidth != 0)
            {
                lstOption.Add(GUILayout.MaxWidth(_maxWidth));
            }

            return lstOption.ToArray();
        }

        public GUILayoutOption[] Values
        {
            get
            {
                var result = GetLayoutOptions();
                if (result == null || result.Length == 0)
                {
                    return null;
                }

                return result;
            }
            
        }
    }
}
