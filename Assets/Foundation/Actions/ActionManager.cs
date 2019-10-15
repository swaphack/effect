using UnityEngine;
using System.Collections.Generic;
using Assets.Foundation.Managers;

namespace Assets.Foundation.Actions
{
    /// <summary>
    ///  动作管理
    /// </summary>
    public sealed class ActionManager : Singleton<ActionManager>
    {
        private Dictionary<Object, IActionBehaviour> _actions;

        private ActionManager()
        {
            _actions = new Dictionary<Object, IActionBehaviour>();
        }
        public void RemoveBehaviour(Object obj)
        {
            if (obj == null)
            {
                return;
            }

            _actions.Remove(obj);
        }

        public void AddBehaviour(Object obj, IActionBehaviour ab)
        {
            if (ab == null)
            {
                return;
            }
            if (_actions.ContainsKey(obj))
            {
                _actions[obj] = ab;
            }
            else
            {
                _actions.Add(obj, ab);
            }
        }

        public void RemoveAllBehaviours()
        {
            _actions.Clear();
        }

        private float _lastUpdateShowTime;
        void Start()
        {
            _lastUpdateShowTime = Time.realtimeSinceStartup;
        }

        void Update()
        {
            float now = Time.realtimeSinceStartup;

            float dt = now - _lastUpdateShowTime;

            _lastUpdateShowTime = now;

            var map = new Dictionary<object, IActionBehaviour>();
            foreach (var item in _actions)
            {
                map.Add(item.Key, item.Value);
            }

            foreach (var item in map)
            {
                item.Value.UpdateAction(dt);
            }
        }
    }
}
