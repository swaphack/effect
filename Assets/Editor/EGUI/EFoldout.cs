using UnityEditor;

namespace Assets.Editor.EGUI
{
    public class EFoldout : Widget
    {
        private bool _foldout;

        private bool _toggleOnLabelClick;

        public bool Foldout
        {
            get { return _foldout; }

            set { _foldout = value; }
        }

        public bool ToggleOnLabelClick
        {
            get { return _toggleOnLabelClick; }

            set { _toggleOnLabelClick = value; }
        }

        protected override void OnDraw()
        {
            bool value = EditorGUILayout.Foldout(Foldout, Content, ToggleOnLabelClick);
            if (value != Foldout)
            {
                Foldout = value;
                this.DipatchEvent();
            }
        }
    }
}
