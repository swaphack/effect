using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Foundation.Actions
{
    /// <summary>
    /// 回调处理动作
    /// </summary>
    public class CallFunc : ActionInterval
    {
        public delegate void CallFuncDelegate();

        public CallFuncDelegate Callback { get; set; }

        protected virtual void Step(float percent)
        {
            Callback?.Invoke();
            CurrentTime = TotalTime;
        }
    }

    /// <summary>
    /// 带参数的回调处理动作
    /// </summary>
    public class CallFuncN : ActionInterval
    {
        public delegate void CallFuncNDelegate(Object obj);

        public CallFuncNDelegate Callback { get; set; }

        protected virtual void Step(float percent)
        {
            Callback?.Invoke(Target);
            CurrentTime = TotalTime;
        }
    }
}
