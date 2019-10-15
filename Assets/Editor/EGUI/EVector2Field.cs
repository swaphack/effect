using UnityEditor;
using UnityEngine;

namespace Assets.Editor.EGUI
{
    public class EVector2Field : Widget
    {
        private Vector2 _value;

        public Vector2 Value
        {
            get { return _value; }

            set { _value = value; }
        }

        protected override void OnDraw()
        {
            Vector2 value = EditorGUILayout.Vector2Field(Content, Value, Option.Values);
            if (value != Value)
            {
                Value = value;
                this.DipatchEvent();
            }
        }
    }
}
