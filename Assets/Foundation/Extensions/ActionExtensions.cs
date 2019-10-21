using Assets.Foundation.Actions;
using UnityEngine;

namespace Assets.Foundation.Extensions
{
    /// <summary>
    /// 动作扩展
    /// </summary>
    public static class ActionExtensions
    {
        /// <summary>
        /// 播放动作
        /// </summary>
        /// <param name="go"></param>
        /// <param name="action"></param>
        public static void RunAction(this Component go, IAction action)
        {
            if (go == null || go.gameObject == null)
            {
                return;
            }
            var actionBehaviour = go.CreateComponent<ActionBehaviour>();
            actionBehaviour.RunAction(action);
        }

        /// <summary>
        /// 播放动作
        /// </summary>
        /// <param name="go"></param>
        /// <param name="action"></param>
        public static void StopAction(this Component go, IAction action)
        {
            if (go == null || go.gameObject == null)
            {
                return;
            }
            var actionBehaviour = go.CreateComponent<ActionBehaviour>();

            actionBehaviour.RunAction(action);
        }

        /// <summary>
        /// 播放动作
        /// </summary>
        /// <param name="go"></param>
        /// <param name="action"></param>
        public static void PauseAction(this Component go, IAction action)
        {
            if (go == null || go.gameObject == null)
            {
                return;
            }
            var actionBehaviour = go.CreateComponent<ActionBehaviour>();

            actionBehaviour.RunAction(action);
        }

        /// <summary>
        /// 播放动作
        /// </summary>
        /// <param name="go"></param>
        /// <param name="action"></param>
        public static void ResumeAction(this Component go, IAction action)
        {
            if (go == null || go.gameObject == null)
            {
                return;
            }
            var actionBehaviour = go.CreateComponent<ActionBehaviour>();

            actionBehaviour.RunAction(action);
        }


        /// <summary>
        /// 停止所有动作
        /// </summary>
        /// <param name="go"></param>
        public static void Stop(this Component go)
        {
            if (go == null)
            {
                return;
            }

            var actionBehaviour = go.GetComponent<ActionBehaviour>();
            if (actionBehaviour == null)
            {
                return;
            }

            actionBehaviour.StopAllActions();
        }

        /// <summary>
        /// 暂停所有动作
        /// </summary>
        /// <param name="go"></param>
        public static void Pause(this Component go)
        {
            if (go == null)
            {
                return;
            }

            var actionBehaviour = go.GetComponent<ActionBehaviour>();
            if (actionBehaviour == null)
            {
                return;
            }

            actionBehaviour.enabled = false;
        }

        /// <summary>
        /// 恢复所有动作
        /// </summary>
        /// <param name="go"></param>
        public static void Resume(this Component go)
        {
            if (go == null)
            {
                return;
            }

            var actionBehaviour = go.GetComponent<ActionBehaviour>();
            if (actionBehaviour == null)
            {
                return;
            }

            actionBehaviour.StopAllActions();
        }
    }
}
