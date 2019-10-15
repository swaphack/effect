using UnityEditor;
using UnityEngine;

namespace Assets.Editor.EGUI
{
    public class EVector4Field : Widget
    {
        private Vector4 _value;

        public Vector4 Value
        {
            get { return _value; }

            set { _value = value; }
        }

        protected override void OnDraw()
        {
            Vector4 value = EditorGUILayout.Vector4Field(Content, Value, Option.Values);
            if (value != Value)
            {
                Value = value;
                this.DipatchEvent();
            }
        }
    }
}
