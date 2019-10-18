using Assets.Foundation.Extensions;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Foundation.Events
{    
    /// <summary>
    /// 点击行为
    /// </summary>
    public abstract class TouchBehaviour : EventBehaviour, ITouchProtocol
    {
        /// <summary>
        /// 目标
        /// </summary>
        public GameObject Target
        {
            get
            {
                return this.gameObject;
            }
        }

        protected override void UpdateEventStatus(bool status)
        {
            if (TouchManager.Instance != null)
            {
                if (status)
                {
                    TouchManager.Instance.AddBehaviour(this);
                }
                else
                {
                    TouchManager.Instance.RemoveBehaviour(this);
                }
            }
        }

        public abstract void DispatchTouches(Touch[] touches);
    }

    /// <summary>
    /// 单点触摸
    /// </summary>
    public class SingleTouchBehaviour : TouchBehaviour, ISingleTouchProtocol
    {
        public override void DispatchTouches(Touch[] touches)
        {
            if (touches == null || touches.Length == 0)
            {
                return;
            }

            var touch = touches[0];

            switch (touch.phase)
            {
                case TouchPhase.Began: this.TouchBegan(touch); break;
                case TouchPhase.Stationary: this.TouchStationary(touch); break;
                case TouchPhase.Moved: this.TouchMoved(touch); break;
                case TouchPhase.Canceled: this.TouchEnded(touch); break;
                case TouchPhase.Ended: this.TouchCanceled(touch); break;
            }
        }

        public virtual void TouchBegan(Touch touch)
        {
        }

        public virtual void TouchStationary(Touch touch)
        { 
        }

        public virtual void TouchMoved(Touch touch)
        {
        }

        public virtual void TouchEnded(Touch touch)
        {
        }

        public virtual void TouchCanceled(Touch touch)
        {
        }        
    }

    /// <summary>
    /// 多点触摸
    /// </summary>
    public class MultiTouchBehaviour : TouchBehaviour, IMutliTouchProtocol
    {
        public override void DispatchTouches(Touch[] touches)
        {
            if (touches == null || touches.Length == 0)
            {
                return;
            }

            var beganTouches = new List<Touch>();
            var stationaryTouches = new List<Touch>();
            var movedTouches = new List<Touch>();
            var canceledTouches = new List<Touch>();
            var endedTouches = new List<Touch>();

            for (var i = 0; i < touches.Length; i++)
            {
                var touch = touches[i];
                switch (touch.phase)
                {
                    case TouchPhase.Began: beganTouches.Add(touch); break;
                    case TouchPhase.Stationary: stationaryTouches.Add(touch); break;
                    case TouchPhase.Moved: movedTouches.Add(touch); break;
                    case TouchPhase.Canceled: canceledTouches.Add(touch); break;
                    case TouchPhase.Ended: endedTouches.Add(touch); break;
                }
            }

            TouchBegan(beganTouches.ToArray());
            TouchBegan(stationaryTouches.ToArray());
            TouchBegan(movedTouches.ToArray());
            TouchBegan(canceledTouches.ToArray());
            TouchBegan(endedTouches.ToArray());
        }

        public virtual void TouchBegan(Touch[] touches)
        {
        }

        public virtual void TouchStationary(Touch[] touches)
        {
        }        

        public virtual void TouchMoved(Touch[] touches)
        {
        }

        public virtual void TouchEnded(Touch[] touches)
        {
        }

        public virtual void TouchCanceled(Touch[] touches)
        {
        }
    }
}
