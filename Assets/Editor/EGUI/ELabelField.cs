using UnityEditor;
using UnityEngine;
namespace Assets.Editor.EGUI
{
    public class ELabelField : BText
    {
        protected override void InitStyle()
        {
            Style = GUI.skin.label;
        }

        protected override void OnDraw()
        {
            EditorGUILayout.LabelField(Content, Style, Option.Values);
        }
    }
}
