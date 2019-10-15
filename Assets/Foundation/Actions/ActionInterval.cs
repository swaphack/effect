using UnityEngine;

namespace Assets.Foundation.Actions
{
    /// <summary>
    /// 间隔动作
    /// </summary>
    public class ActionInterval : ActionBase
    {
        /// <summary>
        /// 当前经过的时间
        /// </summary>
        private float _currentTime;
        /// <summary>
        /// 总时长
        /// </summary>
        private float _totalTime;

        public override bool IsDone
        {
            get
            {
                return _currentTime >= _totalTime;
            }
        }

        public float CurrentTime 
        {
            get
            {
                return _currentTime;
            }
            protected set
            {
                _currentTime = value;
            }
        }

        public float TotalTime 
        {
            get
            {
                return _totalTime;
            }
            set
            {
                _totalTime = value;
            }
        }

        public override void Reset()
        {
            base.Reset();
            _currentTime = 0;
        }

        public override void Update(float dt)
        {
            if (_totalTime == 0)
            {
                return;
            }

            _currentTime += dt;
            float percent = _currentTime / _totalTime;
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

