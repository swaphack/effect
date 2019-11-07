using UnityEngine;

namespace Game.Foundation.Actions
{
    /// <summary>
    /// 间隔动作
    /// </summary>
    public class ActionInterval : ActionBase
    {
        public override bool IsDone
        {
            get
            {
                return CurrentTime >= TotalTime;
            }
        }

        public float CurrentTime { get; protected set; }

        public float TotalTime { get; set; }

        public override void Reset()
        {
            base.Reset();
            CurrentTime = 0;
        }

        public override void Update(float dt)
        {
            if (TotalTime == 0)
            {
                return;
            }

            CurrentTime += dt;
            float percent = CurrentTime / TotalTime;
            this.DoInterval(dt);
            this.DoStep(percent);
        }

        public override void InitWithTarget(Component target)
        {
            base.InitWithTarget(target);
            this.DoWithTarget();
        }

        /// <summary>
        /// 间隔
        /// </summary>
        /// <param name="dt"></param>
        protected virtual void DoInterval(float dt)
        {
        }

        /// <summary>
        /// 百分比
        /// </summary>
        /// <param name="percent"></param>
        protected virtual void DoStep(float percent)
        { 
        }

        /// <summary>
        /// 初始化
        /// </summary>
        protected virtual void DoWithTarget()
        { 
        }
    }
}

