using UnityEngine;
using System.Collections.Generic;
using Assets.Foundation.Extensions;

namespace Assets.Foundation.Actions
{
    /// <summary>
    /// 动作行为
    /// </summary>
    public class ActionBehaviour : MonoBehaviour, IActionBehaviour
    {
        /// <summary>
        /// 动作集合
        /// </summary>
        private List<IAction> _actions;

        private bool _bActionEnabled;

        /// <summary>
        /// 动作是否可用
        /// </summary>
        public bool ActionEnabled 
        {
            get
            {
                return _bActionEnabled;
            }
            set
            {
                _bActionEnabled = value;
            }
        }

        public ActionBehaviour()
        {
            _actions = new List<IAction>();
            ActionEnabled = true;
        }

        private void OnEnable()
        {
            ActionEnabled = true;
        }

        private void OnDisable()
        {
            ActionEnabled = false;
        }

        /// <summary>
        /// 当前运行的动作数
        /// </summary>
        public int NumbersOfRunningActionCount 
        {
            get 
            {
                return _actions.Count;
            }
        }
        /// <summary>
        /// 播放动作
        /// </summary>
        /// <param name="action"></param>
        public void RunAction(IAction action)
        {
            if (action == null) {
                return;
            }
            if (_actions.Contains(action))
            {
                return;
            }
            this.AddToUpdate();
            action.InitWithTarget(this);
            _actions.Add(action);
            
        }

        /// <summary>
        /// 停止动作
        /// </summary>
        /// <param name="action"></param>
        public void StopAction(IAction action)
        {
            if (action == null)
            {
                return;
            }

            _actions.Remove(action);
            this.RemoveFromUpdate();
        }
        /// <summary>
        /// 停止所有动作
        /// </summary>
        public void StopAllActions()
        {
            _actions.Clear();
            this.RemoveFromUpdate();
        }
        /// <summary>
        /// 更新动作
        /// </summary>
        /// <param name="dt"></param>
        public void UpdateAction(float dt)
        {
            if (!ActionEnabled)
            {
                return;
            }
            List<IAction> removeActions = new List<IAction>();
            foreach (var item in _actions) 
            {
                if (item.IsDone)
                {
                    removeActions.Add(item);
                }
                else
                {
                    if (item.IsPlaying)
                    {
                        item.Update(dt);
                    }
                }
            }

            foreach (var item in removeActions) 
            {
                this.StopAction(item);
            }
        }

        /// <summary>
        /// 添加到更新
        /// </summary>
        private void AddToUpdate()
        {
            if (_actions.Count != 0)
            {
                return;
            }
            if (ActionManager.Instance != null)
            {
                ActionManager.Instance.AddBehaviour(this, this);
            }
        }

        /// <summary>
        /// 从更新中移除
        /// </summary>
        private void RemoveFromUpdate()
        {
            if (_actions.Count != 0)
            {
                return;
            }

            if (ActionManager.Instance != null)
            {
                ActionManager.Instance.RemoveBehaviour(this);
            }
        }
    }
}
