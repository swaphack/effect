using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Foundation.Schedulers
{
    /// <summary>
    /// 调度器回调处理
    /// </summary>
    public delegate void SchedulerDelegate();

    public class Scheduler : IScheduler, IDisposable
    {
        public float CurrentTime { get; set; }

        public float Interval { get; set; }

        public SchedulerDelegate Callback { get; set; }

        public Scheduler(float interval, SchedulerDelegate callback)
        {
            Interval = interval;
            Callback = callback;
        }

        protected void DoCallback()
        {
            if (Callback == null)
            {
                return;
            }
            var func = Callback;

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
            CurrentTime += dt;
            if (CurrentTime < Interval)
            {
                return;
            }

            OnTriggerEvent();
            CurrentTime -= Interval;
        }
    }
}
