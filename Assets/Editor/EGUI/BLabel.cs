using System;
using UnityEngine;

namespace Assets.Editor.EGUI
{
    /// <summary>
    /// 文本
    /// </summary>
    public class BLabel : BText
    {
        protected override void InitStyle()
        {
            Style = GUI.skin.label;
        }

        protected override void OnDraw()
        {
            GUILayout.Label(Content, Style, Option.Values);
        }
    }
}
