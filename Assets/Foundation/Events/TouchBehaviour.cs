using Assets.Foundation.Extensions;
using UnityEngine;

namespace Assets.Foundation.Events
{    
    /// <summary>
    /// 点击行为
    /// </summary>
    public class TouchBehaviour : MonoBehaviour, ITouchProtocol
    {
        /// <summary>
        /// 是否可点击
        /// </summary>
        private bool _bTouchEnabled = false;
        public bool IsTouchEnabled
        {
            get
            {
                return _bTouchEnabled;
            }
            set
            {
                if (value == _bTouchEnabled)
                {
                    return;
                }
                _bTouchEnabled = value;
                if (TouchManager.Instance != null)
                {
                    if (_bTouchEnabled)
                    {
                        TouchManager.Instance.AddTouchBehaviour(this);
                    }
                    else
                    {
                        TouchManager.Instance.RemoveTouchBehaviour(this);
                    }
                }
            }
        }

        void OnEnable()
        {
            if (TouchManager.Instance != null)
            {
                if (IsTouchEnabled)
                {
                    TouchManager.Instance.AddTouchBehaviour(this);
                }
                else
                {
                    TouchManager.Instance.RemoveTouchBehaviour(this);
                }
            }
        }

        void OnDisable()
        {
            if (TouchManager.Instance != null)
            {
                TouchManager.Instance.RemoveTouchBehaviour(this);
            }
            
        }

        public virtual void TouchBegan(HitTouchInfo hitInfo)
        { 
        }
        public virtual void TouchMoved(HitTouchInfo hitInfo)
        { 
        }
        public virtual void TouchEnded(HitTouchInfo hitInfo)
        {
        }
        public virtual void TouchCanceled(HitTouchInfo hitInfo)
        {
        }
    }
}
