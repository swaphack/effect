using UnityEngine;
using System.Collections.Generic;

namespace Game.Foundation.Actions
{
    /// <summary>
    /// 动作集合，处理多个动作
    /// </summary>
    public class ActionSet : ActionBase
    {
        protected List<IAction> Actions { get; }
        public ActionSet()
        {
            Actions = new List<IAction>();
        }

        public void Add(IAction action)
        {
            if (action == null)
            {
                return;
            }
            Actions.Add(action);
        }

        public void Append(IAction[] actions)
        {
            if (actions == null || actions.Length == 0)
            {
                return;
            }
            Actions.AddRange(actions);
        }

        public void Append(List<IAction> actions)
        {
            if (actions == null || actions.Count == 0)
            {
                return;
            }
            Actions.AddRange(actions);
        }

        public void Remove(IAction action)
        {
            if (action == null)
            {
                return;
            }
            Actions.Remove(action);
        }

        public void RemoveAllActions()
        {
            Actions.Clear();
        }

        public override bool IsDone
        {
            get
            {
                bool bDone = true;
                foreach (var item in Actions)
                {
                    if (!item.IsDone)
                    {
                        bDone = false;
                        break;
                    }
                }

                return bDone;
            }
        }

        public override void InitWithTarget(Component target)
        {
            base.InitWithTarget(target);

            foreach (var item in Actions)
            {
                item.InitWithTarget(target);
            }
        }

        public override void Reset()
        {
            base.Reset();
            foreach (var item in Actions)
            {
                item.Reset();
            }
        }
    }

    /// <summary>
    /// 顺序动作
    /// </summary>
    public class Sequence : ActionSet
    {
        private int _lastDoneIndex;

        public override void Reset()
        {
            base.Reset();
            _lastDoneIndex = 0;
        }

        public override void Update(float dt)
        {
            if (Actions == null || Actions.Count == 0)
            {
                return;
            }
            if (_lastDoneIndex == Actions.Count)
            {
                return;
            }
            var action = Actions[_lastDoneIndex];
            if (action.IsDone)
            {
                _lastDoneIndex++;
            }
            else
            {
                if (action.IsPlaying)
                {
                    action.Update(dt);
                }
            }
        }

        public static Sequence Create(List<IAction> actions)
        {
            var action = new Sequence();
            action.Append(actions);
            return action;
        }

        public static Sequence Create(params IAction[] actions)
        {
            var action = new Sequence();
            action.Append(actions);
            return action;
        }

        public static Sequence CreateWithTwoActions(IAction action1, IAction action2)
        {
            var action = new Sequence();
            action.Add(action1);
            action.Add(action2);
            return action;
        }
    }

    public class Spawn : ActionSet
    {
        public override void Update(float dt)
        {
            if (Actions == null || Actions.Count == 0)
            {
                return;
            }
            foreach (var item in Actions)
            {
                if (!item.IsDone)
                {
                    if (item.IsPlaying)
                    {
                        item.Update(dt);
                    }
                }
            }
        }

        public static Spawn Create(List<IAction> actions)
        {
            var action = new Spawn();
            action.Append(actions);
            return action;
        }

        public static Spawn Create(params IAction[] actions)
        {
            var action = new Spawn();
            action.Append(actions);
            return action;
        }

        public static Spawn CreateWithTwoActions(IAction action1, IAction action2)
        {
            var action = new Spawn();
            action.Add(action1);
            action.Add(action2);
            return action;
        }
    }
}
