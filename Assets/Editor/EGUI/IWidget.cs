using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Editor.EGUI
{
    /// <summary>
    /// 控件接口
    /// </summary>
    public interface IWidget
    {
        /// <summary>
        /// 绘制
        /// </summary>
        void Draw();        
    }
}
