using UnityEngine;

namespace Game.Foundation.Actions
{
    /// <summary>
    /// 循环动作
    /// </summary>
    public class Repeate : ActionBase
    {
        private int _count;

        public int RepeateCount { get; set; } = 0;

        public IAction Action { get; set; }

        public override bool IsDone
        {
            get
            {
                if (Action == null)
                {
                    return true;
                }
                if (RepeateCount == 0)
                {
                    return false;
                }

                return _count >= RepeateCount;
            }
        }

        public override void Reset()
        {
            base.Reset();
            _count = 0;
        }

        public override void InitWithTarget(Component target)
        {
            base.InitWithTarget(target);
            if (Action != null)
            {
                Action.InitWithTarget(target);
            }
        }

        public override void Update(float dt)
        {
            if (Action != null)
            {
                if (Action.IsDone)
                {
                    _count++;
                    Action.InitWithTarget(Target);
                    Action.Reset();
                }
                else
                {
                    if (Action.IsPlaying)
                    {
                        Action.Update(dt);
                    }
                }
            }
        }

        /// <summary>
        /// 重复多少次动作
        /// </summary>
        /// <param name="nCount"></param>
        /// <param name="repeateAction"></param>
        /// <returns></returns>
        public static Repeate Create(int nCount, IAction repeateAction)
        {
            var action = new Repeate();
            action.Action = repeateAction;
            action.RepeateCount = nCount;
            return action;
        }

        /// <summary>
        /// 一直重复的动作
        /// </summary>
        /// <param name="repeateAction"></param>
        /// <returns></returns>
        public static Repeate CreateForever(IAction repeateAction)
        {
            var action = new Repeate();
            action.Action = repeateAction;
            return action;
        }
    }
}
