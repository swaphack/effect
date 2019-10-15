using UnityEditor;
using UnityEngine;

namespace Assets.Editor.EGUI
{
    public class EPropertyField : Widget
    {
        private SerializedProperty _value;
        private bool _includeChildren;

        public SerializedProperty Value
        {
            get { return _value; }

            set { _value = value; }
        }

        public bool IncludeChildren
        {
            get { return _includeChildren; }

            set { _includeChildren = value; }
        }

        protected override void OnDraw()
        {
            bool value = EditorGUILayout.PropertyField(Value, Content, IncludeChildren, Option.Values);
            if (value != IncludeChildren)
            {
                IncludeChildren = value;
                this.DipatchEvent();
            }
        }
    }
}
