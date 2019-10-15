using UnityEditor;
using UnityEngine;

namespace Assets.Editor.EGUI
{
    public class EColorField : Widget
    {
        private Color _color;

        public Color Color
        {
            get
            {
                return _color;
            }
            set
            {
                _color = value;
            }
        }

        public EColorField()
        {
            _color = new Color();
        }

        protected override void OnDraw()
        {
            Color color = EditorGUILayout.ColorField(Content, Color, Option.Values);
            if (color != Color)
            {
                _color = color;
                this.DipatchEvent();
            }
        }
    }
}
