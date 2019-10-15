using UnityEditor;
using UnityEngine;

namespace Assets.Editor.EGUI
{
    public class EToggleLeft : EToggle
    {
        protected override void OnDraw()
        {
            bool value = EditorGUILayout.ToggleLeft(Content, Value,Option.Values);
            if (value != Value)
            {
                Value = value;
                this.DipatchEvent();
            }
        }
    }
}
