using UnityEditor;
using UnityEngine;

namespace Assets.Editor.EGUI
{
    public class ETagField : Widget
    {
        protected override void OnDraw()
        {
            string value = EditorGUILayout.TagField(Content, Text,Option.Values);
            if (value != Text)
            {
                Text = value;
                this.DipatchEvent();
            }
        }
    }
}
