using UnityEditor;
using UnityEngine;

namespace Assets.Editor.EGUI
{
    public class ESeparator : Widget
    {
        protected override void OnDraw()
        {
            EditorGUILayout.Separator();
        }
    }
}
