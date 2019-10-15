using UnityEditor;
using UnityEngine;

namespace Assets.Editor.EGUI
{
    public class ERectField : Widget
    {
        private Rect _value;

        public Rect Value
        {
            get { return _value; }

            set { _value = value; }
        }

        protected override void OnDraw()
        {
            Rect value = EditorGUILayout.RectField(Content, Value, Option.Values);
            if (value != Value)
            {
                Value = value;
                this.DipatchEvent();
            }
        }
    }
}
