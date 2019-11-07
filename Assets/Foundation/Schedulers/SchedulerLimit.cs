
namespace Game.Foundation.Schedulers
{
    /// <summary>
    /// 带次数限制的调度器
    /// </summary>
    public class SchedulerLimit : Scheduler
    {
        public int MaxCount { get; set; }
        public int Count { get; set; }

        public SchedulerLimit(float interval, int count, SchedulerDelegate callback)
            :base(interval, callback)
        {
            MaxCount = count;
        }

        protected override void OnTriggerEvent()
        {
            DoCallback();
            Count++;
            if (Count >= MaxCount)
            {
                Dispose();
            }
        }
    }
}
