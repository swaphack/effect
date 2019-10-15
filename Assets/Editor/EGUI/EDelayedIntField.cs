using UnityEditor;

namespace Assets.Editor.EGUI
{
    public class EDelayedIntField : Widget
    {
        private int _value;

        public int Value
        {
            get { return _value; }

            set { _value = value; }
        }

        protected override void OnDraw()
        {
            int value = EditorGUILayout.DelayedIntField(Content, Value,Option.Values);
            if (value != Value)
            {
                Value = value;
                this.DipatchEvent();
            }
        }
    }
}
