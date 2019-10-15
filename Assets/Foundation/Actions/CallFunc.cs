using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Foundation.Actions
{
    /// <summary>
    /// 回调处理动作
    /// </summary>
    public class CallFunc : ActionInterval
    {
        public delegate void Func();

        private Func _callback;

        public Func Callback 
        {
            get 
            {
                return _callback;
            }
            set
            {
                _callback = value;
            }
        }

        protected virtual void Step(float percent)
        {
            if (_callback != null) 
            {
                _callback();
            }
            CurrentTime = TotalTime;
        }
    }

    /// <summary>
    /// 带参数的回调处理动作
    /// </summary>
    public class CallFuncN : ActionInterval
    {
        public delegate void Func(Object obj);

        private Func _callback;

        public Func Callback
        {
            get
            {
                return _callback;
            }
            set
            {
                _callback = value;
            }
        }

        protected virtual void Step(float percent)
        {
            if (_callback != null)
            {
                _callback(Target);
            }
            CurrentTime = TotalTime;
        }
    }
}
