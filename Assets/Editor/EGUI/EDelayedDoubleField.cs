using UnityEditor;

namespace Assets.Editor.EGUI
{
    public class EDelayedDoubleField : Widget
    {
        private double _value;

        public double Value
        {
            get { return _value; }

            set { _value = value; }
        }

        protected override void OnDraw()
        {
            double value = EditorGUILayout.DelayedDoubleField(Content, Value,Option.Values);
            if (value != Value)
            {
                Value = value;
                this.DipatchEvent();
            }
        }
    }
}
