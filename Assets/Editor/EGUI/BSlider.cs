
using UnityEngine;

namespace Assets.Editor.EGUI
{
    /// <summary>
    /// 滑杆条
    /// </summary>
    public class BSlider : Widget
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
        /// 滑杆
        /// </summary>
        private GUIStyle _thumb;

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

        public GUIStyle Thumb
        {
            get { return _thumb; }
        }

        public BSlider()
        {
            _thumb = new GUIStyle();
        }
    }

    /// <summary>
    /// 水平滑杆
    /// </summary>
    public class HorizontalSlider : BSlider
    {
        protected override void OnDraw()
        {
            float value = GUILayout.HorizontalSlider(Value, MinValue, MaxValue, Option.Values);
            if (value != Value)
            {
                Value = value;
                this.DipatchEvent();
            }
        }
    }

    /// <summary>
    /// 垂直滑杆
    /// </summary>
    public class VerticalSlider : BSlider
    {
        protected override void OnDraw()
        {
            float value = GUILayout.VerticalSlider(Value, MinValue, MaxValue, Option.Values);
            if (value != Value)
            {
                Value = value;
                this.DipatchEvent();
            }
        }
    }
}
