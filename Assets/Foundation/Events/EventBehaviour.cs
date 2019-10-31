using UnityEngine;

namespace Assets.Foundation.Events
{
    public abstract class EventBehaviour : MonoBehaviour
    {
        public bool IsEventEnable { get; set; } = true;
        private void Start()
        {
            UpdateEventStatus(IsEventEnable);
        }

        private void OnEnable()
        {
            UpdateEventStatus(IsEventEnable);
        }

        private void OnDisable()
        {
            UpdateEventStatus(false);
        }

        protected abstract void UpdateEventStatus(bool status);
    }
}
