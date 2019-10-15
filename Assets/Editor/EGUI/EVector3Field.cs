using UnityEditor;
using UnityEngine;

namespace Assets.Editor.EGUI
{
    public class EVector3Field : Widget
    {
        private Vector3 _value;

        public Vector3 Value
        {
            get { return _value; }

            set { _value = value; }
        }

        protected override void OnDraw()
        {
            Vector3 value = EditorGUILayout.Vector3Field(Content, Value, Option.Values);
            if (value != Value)
            {
                Value = value;
                this.DipatchEvent();
            }
        }
    }
}
