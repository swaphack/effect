using UnityEditor;
using UnityEngine;
namespace Assets.Editor.EGUI
{
    public class ELongField : Widget
    {
        /// <summary>
        /// 当前选中项
        /// </summary>
        private long _value;
        /// <summary>
        /// 当前选中项
        /// </summary>
        public long Value
        {
            get { return _value; }

            set { _value = value; }
        }

        protected override void OnDraw()
        {
            long value = EditorGUILayout.LongField(Content, Value,Option.Values);
            if (value != Value)
            {
                Value = value;
                this.DipatchEvent();
            }
        }
    }
}
