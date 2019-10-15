using UnityEditor;
using UnityEngine;

namespace Assets.Editor.EGUI
{
    public class ETextArea : BText
    {
        protected override void InitStyle()
        {
            Style = GUI.skin.textArea;
        }

        protected override void OnDraw()
        {
            string value = EditorGUILayout.TextArea(Text, Style, Option.Values);
            if (value != Text)
            {
                Text = value;
                this.DipatchEvent();
            }
        }
    }
}
