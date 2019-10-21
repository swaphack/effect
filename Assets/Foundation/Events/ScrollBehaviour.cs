using UnityEngine;

namespace Assets.Foundation.Events
{
    /// <summary>
    /// 鼠标缩放
    /// </summary>
    public class ScrollBehaviour : EventBehaviour, IScrollProtocol
    {
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
                    ScrollManager.Instance.AddBehaviour(this);
                }
                else
                {
                    ScrollManager.Instance.RemoveBehaviour(this);
                }
            }
        }

        public virtual void DoScale(float delta)
        {

        }
    }
    
}
