using Assets.Foundation.Managers;
using UnityEngine;
using System.Collections.Generic;

namespace Assets.Foundation.Events
{
    /// <summary>
    /// 点击管理
    /// </summary>
    public class TouchManager : Singleton<TouchManager>
    {
        private Dictionary<GameObject, TouchBehaviour> _behaviours = new Dictionary<GameObject, TouchBehaviour>();

        /// <summary>
        /// 添加触摸处理
        /// </summary>
        /// <param name="behaviour"></param>
        public void AddTouchBehaviour(TouchBehaviour behaviour)
        { 
            if (behaviour == null)
            {
                return;
            }

            _behaviours.Add(behaviour.gameObject, behaviour);
        }
        /// <summary>
        /// 移除触摸处理
        /// </summary>
        /// <param name="behaviour"></param>
        public void RemoveTouchBehaviour(TouchBehaviour behaviour)
        {
            if (behaviour == null)
            {
                return;
            }

            _behaviours.Remove(behaviour.gameObject);
        }

        /// <summary>
        /// 派发3d触摸事件
        /// </summary>
        /// <param name="hitInfo"></param>
        /// <param name="touchPhase"></param>
        private void Dispatch3DTouch(RaycastHit hitInfo, TouchPhase touchPhase)
        {
            if (!_behaviours.ContainsKey(hitInfo.collider.gameObject))
            {
                return;
            }

            var behaviour = _behaviours[hitInfo.collider.gameObject];
            HitTouchInfo hitTouchInfo = new HitTouchInfo();
            switch (touchPhase)
            {
                case TouchPhase.Began: behaviour.TouchBegan(hitTouchInfo); break;
                case TouchPhase.Moved: behaviour.TouchMoved(hitTouchInfo); break;
                case TouchPhase.Ended: behaviour.TouchEnded(hitTouchInfo); break;
                case TouchPhase.Canceled: behaviour.TouchCanceled(hitTouchInfo); break;
                default: break;
            }
        }

        /// <summary>
        /// 派发2d触摸事件
        /// </summary>
        /// <param name="hitInfo"></param>
        /// <param name="touchPhase"></param>
        private void Dispatch2DTouch(RaycastHit2D hitInfo, TouchPhase touchPhase)
        {
            if (!_behaviours.ContainsKey(hitInfo.collider.gameObject))
            {
                return;
            }

            var behaviour = _behaviours[hitInfo.collider.gameObject];
            HitTouchInfo hitTouchInfo = new HitTouchInfo();
            switch (touchPhase)
            {
                case TouchPhase.Began: behaviour.TouchBegan(hitTouchInfo); break;
                case TouchPhase.Moved: behaviour.TouchMoved(hitTouchInfo); break;
                case TouchPhase.Ended: behaviour.TouchEnded(hitTouchInfo); break;
                case TouchPhase.Canceled: behaviour.TouchCanceled(hitTouchInfo); break;
                default: break;
            }
        }

        /// <summary>
        /// 单点触摸
        /// </summary>
        /// <param name="touchInfo"></param>
        public void OnSingleTouch(TouchInfo touchInfo)
        {
            var pos = touchInfo.hitPosition;

            // 3d
            {
                Ray ray = Camera.main.ScreenPointToRay(pos);
                RaycastHit raycastHitInfo;
                if (Physics.Raycast(ray, out raycastHitInfo))
                {
                    if (raycastHitInfo.collider != null)
                    {
                        this.Dispatch3DTouch(raycastHitInfo, touchInfo.touchPhase);
                    }
                }
            }

            // 2d
            {
                RaycastHit2D raycastHitInfo = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(pos), Vector2.zero);
                if (raycastHitInfo.collider != null)
                {
                    Dispatch2DTouch(raycastHitInfo, touchInfo.touchPhase);
                }
            }
        }
    }
}
