using System;
using UnityEngine;

namespace Assets.Editor.EGUI
{
    /// <summary>
    /// 盒子
    /// </summary>
    public class BBox : Widget
    {
        protected override void OnDraw()
        {
            GUILayout.Box(Content,Option.Values);
        }
    }
}
