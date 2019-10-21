using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Foundation.Schedulers
{
    /// <summary>
    /// 调度器回调处理
    /// </summary>
    public delegate void SchedulerFunc();

    public class Scheduler : IScheduler, IDisposable
    {
        /// <summary>
        /// 当前时间
        /// </summary>
        private float _currentTime;
        /// <summary>
        /// 间隔
        /// </summary>
        private float _interval;
        /// <summary>
        /// 回调事件
        /// </summary>
        private SchedulerFunc _callback;

        public float CurrentTime
        {
            get { return _currentTime; }
            set { _currentTime = value; }
        }
        
        public float Interval
        {
            get { return _interval; }
            set { _interval = value; }
        }
        
        public SchedulerFunc Callback
        {
            get { return _callback; }
            set { _callback = value; }
        }

        public Scheduler(float interval, SchedulerFunc callback)
        {
            _interval = interval;
            _callback = callback;
        }

        protected void DoCallback()
        {
            if (_callback == null)
            {
                return;
            }
            var func = _callback;

            func();
        }

        protected virtual void OnTriggerEvent()
        {
            DoCallback();
            this.Dispose();
        }

        public virtual void Dispose()
        {
            SchedulerMananger.Instance.RemoveScheduler(this);
        }

        public void OnTimeUp(float dt)
        {
            _currentTime += dt;
            if (_currentTime < _interval)
            {
                return;
            }

            OnTriggerEvent();
            _currentTime -= _interval;
        }
    }
}
