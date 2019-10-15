using UnityEditor;
using UnityEngine;

namespace Assets.Editor.EGUI
{
    public class EKnob : Widget
    {
        public Vector2 _knobSize;
        
        public float _value;
        
        public float _minValue;
        
        public float _maxValue;
        
        public string _unit;
        
        public Color _backgroundColor;
        
        public Color _activeColor;
        
        public bool _showValue;
        
        public Vector2 KnobSize
        {
            get { return _knobSize; }
            set { _knobSize = value; }
        }

        public float Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public float MinValue
        {
            get { return _minValue; }
            set { _minValue = value; }
        }

        public string Unit
        {
            get { return _unit; }
            set { _unit = value; }
        }

        public float MaxValue
        {
            get { return _maxValue; }
            set { _maxValue = value; }
        }

        public Color BackgroundColor
        {
            get { return _backgroundColor; }
            set { _backgroundColor = value; }
        }

        public Color ActiveColor
        {
            get { return _activeColor; }
            set { _activeColor = value; }
        }

        public bool ShowValue
        {
            get { return _showValue; }
            set { _showValue = value; }
        }

        protected override void OnDraw()
        {
            float value = EditorGUILayout.Knob(KnobSize, Value, MinValue, MaxValue, Unit, BackgroundColor, ActiveColor, ShowValue, Option.Values);
            if (value != Value)
            {
                Value = value;
                this.DipatchEvent();
            }
        }
    }
}
