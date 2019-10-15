using System;
using UnityEditor;

namespace Assets.Editor.EGUI
{
    public class EEnumMaskField : Widget
    {
        private Enum _value;

        public Enum Value
        {
            get { return _value; }

            set { _value = value; }
        }

        protected override void OnDraw()
        {
            Enum value = EditorGUILayout.EnumMaskField(Content, Value,Option.Values);
            if (value != Value)
            {
                Value = value;
                this.DipatchEvent();
            }
        }
    }
}
