using UnityEditor;
using UnityEngine;

namespace Assets.Editor.EGUI
{
    public class EBoundsField : Widget
    {
        private Bounds _bounds;

        public Bounds Bounds
        {
            get
            {
                return _bounds;
            }
            set
            {
                _bounds = value;
            }
        }

        public EBoundsField()
        {
            _bounds = new Bounds();
        }

        protected override void OnDraw()
        {
            Bounds bounds = EditorGUILayout.BoundsField(Content, Bounds, Option.Values);
            if (bounds != Bounds)
            {
                _bounds = bounds;
                this.DipatchEvent();
            }
        }
    }
}
