using UnityEditor;
using UnityEngine;

namespace Assets.Editor.EGUI
{
    public class EInspectorTitlebar : Widget
    {

        private bool _foldout;

        private bool _expandable;

        private UnityEngine.Object[] _targetObjs;

        public bool Foldout
        {
            get { return _foldout; }

            set { _foldout = value; }
        }

        public bool Expandable
        {
            get { return _expandable; }

            set { _expandable = value; }
        }

        public UnityEngine.Object[] TargetObjs
        {
            get { return _targetObjs; }

            set { _targetObjs = value; }
        }

        protected override void OnDraw()
        {
            bool value = EditorGUILayout.InspectorTitlebar(Foldout, TargetObjs, Expandable);
            if (value != Foldout)
            {
                Foldout = value;
                this.DipatchEvent();
            }
        }
    }
}
