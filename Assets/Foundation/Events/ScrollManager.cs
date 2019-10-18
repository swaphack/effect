using Assets.Foundation.Managers;
using System.Collections.Generic;

namespace Assets.Foundation.Events
{
    /// <summary>
    /// 缩放管理
    /// </summary>
    public class ScrollManager : Singleton<ScrollManager>
    {
        private HashSet<IScrollProtocol> _behaviours = new HashSet<IScrollProtocol>();

        /// <summary>
        /// 添加触摸处理
        /// </summary>
        /// <param name="behaviour"></param>
        public void AddBehaviour(IScrollProtocol behaviour)
        {
            if (behaviour == null)
            {
                return;
            }

            _behaviours.Add(behaviour);
        }
        /// <summary>
        /// 移除触摸处理
        /// </summary>
        /// <param name="behaviour"></param>
        public void RemoveBehaviour(IScrollProtocol behaviour)
        {
            if (behaviour == null)
            {
                return;
            }

            _behaviours.Remove(behaviour);
        }

        public void DispatchScale(float scaleDelta)
        {
            foreach (var item in _behaviours)
            {
                item.DoScale(scaleDelta);
            }
        }
    }
}
