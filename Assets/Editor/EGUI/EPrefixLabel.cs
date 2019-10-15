using System;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor.EGUI
{
    public class EPrefixLabel : Widget
    {
        public EPrefixLabel()
        {
        }

        protected override void OnDraw()
        {
            EditorGUILayout.PrefixLabel(Content);
        }
    }
}
