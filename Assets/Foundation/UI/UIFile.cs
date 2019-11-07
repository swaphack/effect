using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Foundation.UI
{
    /// <summary>
    /// ui文件
    /// </summary>
    public abstract class UIFile : UIFrame
    {
        /// <summary>
        /// ui文件路径
        /// </summary>
        public virtual string Path
        {
            get
            {
                return "";
            }
        }

        protected override void InitControls()
        {
        }

        protected override void InitLogic()
        {
        }

        /// <summary>
        /// 赋值
        /// </summary>
        /// <param name="data"></param>
        public abstract void InitWithParams(params object[] data);
    }
}
