using UnityEditor;
using UnityEngine;

namespace Assets.Editor.EGUI
{
    public class ETextField : BText
    {
        protected override void InitStyle()
        {
            Style = GUI.skin.textField;
        }
        protected override void OnDraw()
        {
            string value = EditorGUILayout.TextField(Text, Style, Option.Values);
            if (value != Text)
            {
                Text = value;
                this.DipatchEvent();
            }
        }
    }
}
