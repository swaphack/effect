using System;
using UnityEngine;

namespace Assets.Editor.EGUI
{
    /// <summary>
    /// 按钮
    /// </summary>
    public class BButton : Widget
    {
        public BButton()
        {
            
        }

        protected override void OnDraw()
        {
            if (GUILayout.Button(Content,Option.Values))
            {
                this.DipatchEvent();
            }
        }
    }
}
