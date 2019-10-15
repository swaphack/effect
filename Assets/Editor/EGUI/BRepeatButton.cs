using System;
using UnityEngine;

namespace Assets.Editor.EGUI
{
    /// <summary>
    /// 按住后会重复执行单击操作的按钮
    /// </summary>
    public class BRepeatButton : BButton
    {
        protected override void OnDraw()
        {
            if (GUILayout.RepeatButton(Content,Option.Values))
            {
                this.DipatchEvent();
            }
        }
    }
}
