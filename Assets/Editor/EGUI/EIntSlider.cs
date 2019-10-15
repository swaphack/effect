using UnityEditor;
using UnityEngine;

namespace Assets.Editor.EGUI
{
    public class EIntSlider : Widget
    {
        /// <summary>
        /// 当前值
        /// </summary>
        private int _value;
        /// <summary>
        /// 最小值
        /// </summary>
        private int _minValue;
        /// <summary>
        /// 最大值
        /// </summary>
        private int _maxValue = 100;

        /// <summary>
        /// 当前值
        /// </summary>
        public int Value
        {
            get { return _value; }
            set { _value = value; }
        }
        /// <summary>
        /// 最小值
        /// </summary>
        public int MinValue
        {
            get { return _minValue; }
            set { _minValue = value; }
        }
        /// <summary>
        /// 最大值
        /// </summary>
        public int MaxValue
        {
            get { return _maxValue; }
            set { _maxValue = value; }
        }

        protected override void OnDraw()
        {
            int value = EditorGUILayout.IntSlider(Content, Value, MinValue, MaxValue, Option.Values);
            if (value != Value)
            {
                Value = value;
                this.DipatchEvent();
            }
        }
    }
}
