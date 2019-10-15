using UnityEditor;
using UnityEngine;

namespace Assets.Editor.EGUI
{
    public class EToggle : Widget
    {
        private bool _value;

        public bool Value
        {
            get { return _value; }

            set { _value = value; }
        }

        protected override void OnDraw()
        {
            bool value = EditorGUILayout.Toggle(Content, Value,Option.Values);
            if (value != Value)
            {
                Value = value;
                this.DipatchEvent();
            }
        }
    }
}
