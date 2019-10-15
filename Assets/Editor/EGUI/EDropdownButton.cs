using UnityEditor;
using UnityEngine;

namespace Assets.Editor.EGUI
{
    public class EDropdownButton : Widget
    {
        private FocusType _value;

        public FocusType Value
        {
            get { return _value; }

            set { _value = value; }
        }

        protected override void OnDraw()
        {
            bool result = EditorGUILayout.DropdownButton(Content, Value,Option.Values);
            FocusType value = result ? FocusType.Keyboard : FocusType.Passive;
            if (value != Value)
            {
                Value = value;
                this.DipatchEvent();
            }
        }
    }
}
