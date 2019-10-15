using UnityEditor;
using UnityEngine;

namespace Assets.Editor.EGUI
{
    public class ESelectableLabel : Widget
    {
        protected override void OnDraw()
        {
            EditorGUILayout.SelectableLabel(Text, Option.Values);
        }
    }
}
