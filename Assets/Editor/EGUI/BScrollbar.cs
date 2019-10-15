using System;
using UnityEngine;

namespace Assets.Editor.EGUI
{
    /// <summary>
    /// 滑动条
    /// </summary>
    public abstract class BScrollbar : Widget
    {
        /// <summary>
        /// 当前值
        /// </summary>
        private float _value;
        /// <summary>
        /// 最小值
        /// </summary>
        private float _minValue;
        /// <summary>
        /// 最大值
        /// </summary>
        private float _maxValue = 100;
        /// <summary>
        /// 大小
        /// </summary>
        private float _size;
        /// <summary>
        /// 当前值
        /// </summary>
        public float Value
        {
            get { return _value; }
            set { _value = value; }
        }
        /// <summary>
        /// 最小值
        /// </summary>
        public float MinValue
        {
            get { return _minValue; }
            set { _minValue = value; }
        }
        /// <summary>
        /// 最大值
        /// </summary>
        public float MaxValue
        {
            get { return _maxValue; }
            set { _maxValue = value; }
        }
        /// <summary>
        /// 大小
        /// </summary>
        public float Size
        {
            get { return _size; }
            set { _size = value; }
        }
    }
    /// <summary>
    /// 水平滑动条
    /// </summary>
    public class HorizontalScrollbar : BScrollbar
    {
        protected override void OnDraw()
        {
            float value = GUILayout.HorizontalScrollbar(Value, Size, MinValue, MaxValue,Option.Values);
            if (value != Value)
            {
                Value = value;
                this.DipatchEvent();
            }
        }
    }

    /// <summary>
    /// 垂直滑动条
    /// </summary>
    public class VerticalScrollbar : BScrollbar
    {
        protected override void OnDraw()
        {
            float value = GUILayout.VerticalScrollbar(Value, Size, MinValue, MaxValue,Option.Values);
            if (value != Value)
            {
                Value = value;
                this.DipatchEvent();
            }
        }
    }
}
