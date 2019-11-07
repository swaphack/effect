using Game.Foundation.Common;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Foundation.Events
{
    /// <summary>
    /// 缩放管理
    /// </summary>
    public class ScrollManager : SingletonBehaviour<ScrollManager>
    {
        private struct TouchInfo
        {
            public int firstId { get; set; }
            public int secondId { get; set; }
            public Vector2 firstPos { get; set; }
            public Vector2 secondPos { get; set; }

            public GameObject target { get; set; }

            public TouchInfo(int fingerId, Vector2 firstPos, GameObject target)
            {
                this.firstId = fingerId;
                this.firstPos = firstPos;
                this.target = target;

                this.secondId = -1;
                this.secondPos = Vector2.zero;
            }

            public float GetDiff(Vector2 p1, Vector2 p2)
            {
                float oldDis = Vector2.Distance(firstPos, secondPos);
                float newDis = Vector2.Distance(p1, p2);

                firstPos = p1;
                secondPos = p2;

                return newDis - oldDis;
            }
        }

        private Dictionary<GameObject, IScrollProtocol> _behaviours = new Dictionary<GameObject, IScrollProtocol>();
        private Dictionary<int, TouchInfo> _touchInfos = new Dictionary<int, TouchInfo>();

        /// <summary>
        /// 添加触摸处理
        /// </summary>
        /// <param name="behaviour"></param>
        public void AddBehaviour(IScrollProtocol behaviour)
        {
            if (behaviour == null || behaviour.Target == null)
            {
                return;
            }

            _behaviours[behaviour.Target] = behaviour;
        }
        /// <summary>
        /// 移除触摸处理
        /// </summary>
        /// <param name="behaviour"></param>
        public void RemoveBehaviour(IScrollProtocol behaviour)
        {
            if (behaviour == null || behaviour.Target == null)
            {
                return;
            }

            _behaviours.Remove(behaviour.Target);
        }

        public void DispatchScale(float scaleDelta)
        {

            GameObject go = GetHitTarget(Input.mousePosition);
            if (go == null)
            {
                return;
            }
            if (!_behaviours.ContainsKey(go))
            {
                return;
            }
            var behaviour = _behaviours[go];
            behaviour.DoScale(scaleDelta);
        }

        private GameObject GetHitTarget(Vector2 position)
        {

            PointerEventData pointer = new PointerEventData(EventSystem.current);
            pointer.position = position;

            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointer, results);

            foreach (var item in results)
            {
                if (_behaviours.ContainsKey(item.gameObject))
                {
                    return item.gameObject;
                }
            }

            return null;
        }

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
                    var go = GetHitTarget(touch.position);
                    if (go != null)
                    {
                        _touchInfos[touch.fingerId] = new TouchInfo(touch.fingerId, touch.position, go);
                    }
                }
            }

            if (_touchInfos.ContainsKey(touch.fingerId))
            {
                var touchInfo = _touchInfos[touch.fingerId];
                var go = touchInfo.target;
                if (go == null)
                {
                    touchInfo.firstPos = touch.position;
                    return;
                }

                if (!_behaviours.ContainsKey(go))
                {
                    touchInfo.firstPos = touch.position;
                    return;
                }

                if (touches.Length < 2)
                {
                    touchInfo.firstPos = touch.position;
                    return;
                }
                var secondTouch = touches[1];

                SetSecondTouch(touchInfo, touch, secondTouch);
            }

            if (touch.phase == TouchPhase.Canceled || touch.phase == TouchPhase.Ended)
            {
                if (_touchInfos.ContainsKey(touch.fingerId))
                {
                    _touchInfos.Remove(touch.fingerId);
                }
            }
        }

        private void SetSecondTouch(TouchInfo info, Touch touch, Touch secondTouch)
        {
            if (secondTouch.phase == TouchPhase.Began)
            {
                info.secondId = touch.fingerId;
                info.secondPos = touch.position;
            }
            else if (secondTouch.phase == TouchPhase.Stationary
                || secondTouch.phase == TouchPhase.Moved
                || secondTouch.phase == TouchPhase.Canceled
                || secondTouch.phase == TouchPhase.Ended)
            {
                float dt = info.GetDiff(touch.position, secondTouch.position);
                var behaviour = _behaviours[info.target];
                behaviour.DoScale(dt);
            }
        }
    }
}
