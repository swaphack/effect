using UnityEngine;

namespace Assets.Foundation.Events
{
    public abstract class EventBehaviour : MonoBehaviour
    {
        private bool _bEventEnable = false;
        public bool IsEventEnable
        {
            get { return _bEventEnable; }
            set { _bEventEnable = value; }
        }
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
