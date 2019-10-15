using UnityEditor;
using UnityEngine;

namespace Assets.Editor.EGUI
{
    public class EIntField : Widget
    {
        private int _value;
        public int Value
        {
            get { return _value; }

            set { _value = value; }
        }

        protected override void OnDraw()
        {
            int value = EditorGUILayout.IntField(Content, Value, Option.Values);
            if (value != Value)
            {
                Value = value;
                this.DipatchEvent();
            }
        }
    }
}
