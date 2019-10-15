using UnityEditor;
using UnityEngine;

namespace Assets.Editor.EGUI
{
    public class EMinMaxSlider : Widget
    {
        /// <summary>
        /// 最小限定值
        /// </summary>
        private float _minLimit;
        /// <summary>
        /// 最大限定值
        /// </summary>
        private float _maxLimit;
        /// <summary>
        /// 最小值
        /// </summary>
        private float _minValue;
        /// <summary>
        /// 最大值
        /// </summary>
        private float _maxValue = 100;

        /// <summary>
        /// 最小限定值
        /// </summary>
        public float MinLimit
        {
            get { return _minLimit; }
            set { _minLimit = value; }
        }
        /// <summary>
        /// 最大限定值
        /// </summary>
        public float MaxLimit
        {
            get { return _maxLimit; }
            set { _maxLimit = value; }
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
            EditorGUILayout.MinMaxSlider(Content, ref _minValue, ref _maxValue, MinLimit, MaxLimit, Option.Values);
        }
    }
}
