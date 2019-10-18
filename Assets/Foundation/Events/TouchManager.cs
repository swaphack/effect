﻿using Assets.Foundation.Managers;
using UnityEngine;
using System.Collections.Generic;

namespace Assets.Foundation.Events
{
    /// <summary>
    /// 单点管理
    /// </summary>
    public class TouchManager : Singleton<TouchManager>
    {
        private struct TouchInfo
        {
            public int fingerId;
            public GameObject target;

            public TouchInfo(int fingerId, GameObject target)
            {
                this.fingerId = fingerId;
                this.target = target;
            }
        }

        private Dictionary<GameObject, ITouchProtocol> _behaviours = new Dictionary<GameObject, ITouchProtocol>();
        private Dictionary<int, TouchInfo> _touchInfos = new Dictionary<int, TouchInfo>();

        /// <summary>
        /// 添加触摸处理
        /// </summary>
        /// <param name="behaviour"></param>
        public void AddBehaviour(ITouchProtocol behaviour)
        { 
            if (behaviour == null)
            {
                return;
            }
            _behaviours.Add(behaviour.Target, behaviour);
        }
        /// <summary>
        /// 移除触摸处理
        /// </summary>
        /// <param name="behaviour"></param>
        public void RemoveBehaviour(ITouchProtocol behaviour)
        {
            if (behaviour == null)
            {
                return;
            }

            _behaviours.Remove(behaviour.Target);
        }

        private GameObject GetHitTarget(Touch touch)
        {
            Vector3 pos = touch.position;

            // 3d
            {
                Ray ray = Camera.main.ScreenPointToRay(pos);
                RaycastHit raycastHitInfo;
                if (Physics.Raycast(ray, out raycastHitInfo))
                {
                    if (raycastHitInfo.collider != null)
                    {
                        return raycastHitInfo.collider.gameObject;
                    }
                }
            }

            // 2d
            {
                RaycastHit2D raycastHitInfo = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(pos), Vector2.zero);
                if (raycastHitInfo.collider != null)
                {
                    return raycastHitInfo.collider.gameObject;
                }
            }

            return null;
        }

        public void DispatchTouch(Touch touch)
        {
            DispatchTouches(new Touch[1] { touch });
        }

        /// <summary>
        /// 点击派发
        /// </summary>
        /// <param name="touches"></param>
        public void DispatchTouches(Touch[] touches)
        {
            if (touches == null || touches.Length == 0)
            {
                return;
            }

            var touch = touches[0];

            if (touch.phase == TouchPhase.Began)
            {
                if (!_touchInfos.ContainsKey(touch.fingerId))
                {
                    var go = GetHitTarget(touch);
                    if (go != null)
                    {

                        _touchInfos.Add(touch.fingerId, new TouchInfo(touch.fingerId, go));
                    }
                }
            }

            if (_touchInfos.ContainsKey(touch.fingerId))
            {
                var touchInfo = _touchInfos[touch.fingerId];
                var go = touchInfo.target;
                if (go == null)
                {
                    return;
                }
                if (!_behaviours.ContainsKey(go))
                {
                    return;
                }

                var behaviour = _behaviours[go];
                behaviour.DispatchTouches(touches);
            }

            if (touch.phase == TouchPhase.Canceled || touch.phase == TouchPhase.Ended)
            {
                if (_touchInfos.ContainsKey(touch.fingerId))
                {
                    _touchInfos.Remove(touch.fingerId);
                }
            }
        }
    }
}
