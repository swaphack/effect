using UnityEditor;
using UnityEngine;

namespace Assets.Editor.EGUI
{
    public class ECurveField : Widget
    {
        private AnimationCurve _animationCurve;

        public AnimationCurve AnimationCurve
        {
            get
            {
                return _animationCurve;
            }
        }

        public ECurveField()
        {
            _animationCurve = new AnimationCurve();
        }

        protected override void OnDraw()
        {
            AnimationCurve animationCurve = EditorGUILayout.CurveField(Content, AnimationCurve, Option.Values);
            if (animationCurve != AnimationCurve)
            {
                _animationCurve = animationCurve;
                this.DipatchEvent();
            }
        }
    }
}
