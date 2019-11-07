using UnityEngine;

namespace Game.Editor.Widgets
{
    /// <summary>
    /// ui控件
    /// </summary>
    public class UIWidget : Layout
    {
        private LayoutDirection _direction;
        public LayoutDirection Direction
        {
            get { return _direction; }
            set { _direction = value; }
        }
        public UIWidget()
        {
            _direction = LayoutDirection.Horizontal;
        }

        protected override void BeginDraw()
        {
            if (_direction == LayoutDirection.Horizontal)
            {
                GUILayout.BeginHorizontal(Option.Values);
            }
            else
            {
                GUILayout.BeginVertical(Option.Values);
            }
        }

        protected override void EndDraw()
        {
            if (_direction == LayoutDirection.Horizontal)
            {
                GUILayout.EndHorizontal();
            }
            else
            {
                GUILayout.EndVertical();
            }
        }
    }
}
