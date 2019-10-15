using UnityEngine;
using System.Collections.Generic;

namespace Assets.Foundation.Actions
{
    /// <summary>
    /// 基础动作
    /// </summary>
    public class ActionBase : IAction
    {
        /// <summary>
        /// 对象
        /// </summary>
        private Component _target;
        /// <summary>
        /// 是否正在播放动作
        /// </summary>
        private bool _playing;

        public ActionBase()
        {
            _playing = true;
        }

        public Component Target
        {
            get
            {
                return _target;
            }
            protected set
            {
                _target = value;
            }
        }

        public T GetTarget<T>() where T : Component
        {
            if (_target == null)
            {
                return null;
            }
            return _target.GetComponent<T>();
        }

        public bool IsPlaying
        {
            get
            {
                return _playing;
            }
        }

        public virtual bool IsDone { get { return true; } }

        public void Pause()
        {
            _playing = false;
        }

        public void Resume()
        {
            _playing = true;
        }

        public virtual void Reset()
        {
            _playing = true;
        }

        public virtual void Update(float dt)
        {

        }

        public virtual void InitWithTarget(Component target)
        {
            _target = target;
        }
    }
}
