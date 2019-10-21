
namespace Assets.Foundation.Schedulers
{
    /// <summary>
    /// 带次数限制的调度器
    /// </summary>
    public class SchedulerLimit : Scheduler
    {
        /// <summary>
        /// 次数
        /// </summary>
        private int _count;
        /// <summary>
        /// 最大次数
        /// </summary>
        private int _maxCount;
        public int MaxCount
        {
            get { return _maxCount; }
            set { _maxCount = value; }
        }
        public int Count
        {
            get { return _count; }
            set { _count = value; }
        }

        public SchedulerLimit(float interval, int count, SchedulerFunc callback)
            :base(interval, callback)
        {
            _maxCount = count;
        }

        protected override void OnTriggerEvent()
        {
            base.DoCallback();
            _count++;
            if (_count >= _maxCount)
            {
                Dispose();
            }
        }
    }
}
