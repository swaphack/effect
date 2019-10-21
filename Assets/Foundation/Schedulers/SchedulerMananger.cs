using Assets.Foundation.Managers;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Foundation.Schedulers
{
    /// <summary>
    /// 调度器管理
    /// </summary>
    public class SchedulerMananger : SingletonBehaviour<SchedulerMananger>
    {
        /// <summary>
        /// 上次更新时间
        /// </summary>
        private float _lastUpdateShowTime;
        /// <summary>
        /// 调度器
        /// </summary>
        private HashSet<IScheduler> _schedulers = new HashSet<IScheduler>();

        /// <summary>
        /// 添加调度器
        /// </summary>
        /// <param name="s"></param>
        public void AddScheduler(IScheduler s)
        {
            if (s == null)
            {
                return;
            }

            _schedulers.Add(s);
        }

        /// <summary>
        /// 移除调度器
        /// </summary>
        /// <param name="s"></param>
        public void RemoveScheduler(IScheduler s)
        {
            if (s == null)
            {
                return;
            }

            _schedulers.Remove(s);
        }


        void Start()
        {
            _lastUpdateShowTime = Time.realtimeSinceStartup;
        }

        void Update()
        {
            float now = Time.realtimeSinceStartup;
            float dt = now - _lastUpdateShowTime;

            if (_schedulers.Count == 0)
            {
                return;
            }

            Scheduler[] schedulers = new Scheduler[] { };
           
            _schedulers.CopyTo(schedulers, 0);

            for (var i = 0; i < schedulers.Length; i++)
            {
                schedulers[i].OnTimeUp(dt);
            }
        }
    }
}
