using UnityEngine;

namespace Assets.Foundation.Actions
{
    /// <summary>
    /// 循环动作
    /// </summary>
    public class Repeate : ActionBase
    {
        private int _count = 0;
        /// <summary>
        /// 重复次数，0-表示循环
        /// </summary>
        private int _repeateCount = 0;
        /// <summary>
        /// 目标动作
        /// </summary>
        private IAction _action;

        public int RepeateCount
        {
            get
            {
                return _repeateCount;
            }
            set
            {
                _repeateCount = value;
            }
        }

        public IAction Action
        {
            get
            {
                return _action;
            }
            set
            {
                _action = value;
            }
        }

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
