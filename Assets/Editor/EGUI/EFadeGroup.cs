using System;
using UnityEditor;

namespace Assets.Editor.EGUI
{
    public class EFadeGroup : Layout
    {
        private float _value;

        public float Value
        {
            get { return _value; }

            set { _value = value; }
        }

        protected override void BeginDraw()
        {
            bool value = EditorGUILayout.BeginFadeGroup(Value);
            if (value)
            {
                this.DipatchEvent();
            }
        }

        protected override void EndDraw()
        {
            EditorGUILayout.EndFadeGroup();
        }
    }
}
