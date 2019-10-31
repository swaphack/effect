using UnityEngine;
using System.Collections.Generic;

namespace Assets.Foundation.Actions
{
    /// <summary>
    /// 基础动作
    /// </summary>
    public class ActionBase : IAction
    {
        public ActionBase()
        {
            IsPlaying = true;
        }

        public Component Target { get; protected set; }

        public T GetTarget<T>() where T : Component
        {
            if (Target == null)
            {
                return null;
            }
            return Target.GetComponent<T>();
        }

        public bool IsPlaying { get; private set; }

        public virtual bool IsDone { get { return true; } }

        public void Pause()
        {
            IsPlaying = false;
        }

        public void Resume()
        {
            IsPlaying = true;
        }

        public virtual void Reset()
        {
            IsPlaying = true;
        }

        public virtual void Update(float dt)
        {

        }

        public virtual void InitWithTarget(Component target)
        {
            Target = target;
        }
    }
}
