using UnityEditor;
using UnityEngine;

namespace Assets.Editor.EGUI
{
    public class ESpace : Widget
    {
        protected override void OnDraw()
        {
            EditorGUILayout.Space();
        }
    }
}
