using UnityEditor;
using UnityEngine;

namespace Assets.Editor.EGUI
{
    public class ESlider : Widget
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

        protected override void OnDraw()
        {
            float value = EditorGUILayout.Slider(Content, Value, MinValue, MaxValue, Option.Values);
            if (value != Value)
            {
                Value = value;
                this.DipatchEvent();
            }
        }
    }
}
