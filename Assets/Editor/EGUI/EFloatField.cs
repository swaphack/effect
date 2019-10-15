using UnityEditor;

namespace Assets.Editor.EGUI
{
    public class EFloatField : Widget
    {
        private float _value;

        public float Value
        {
            get { return _value; }

            set { _value = value; }
        }

        protected override void OnDraw()
        {
            float value = EditorGUILayout.FloatField(Content, Value, Option.Values);
            if (value != Value)
            {
                Value = value;
                this.DipatchEvent();
            }
        }
    }
}
