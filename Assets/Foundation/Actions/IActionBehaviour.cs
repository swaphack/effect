using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Foundation.Actions
{
    /// <summary>
    /// 动作行为接口
    /// </summary>
    public interface IActionBehaviour
    {
        /// <summary>
        /// 动作是否可用
        /// </summary>
        bool ActionEnabled { get; set; }
        /// <summary>
        /// 更新动作
        /// </summary>
        /// <param name="dt"></param>
        void UpdateAction(float dt);
    }
}
