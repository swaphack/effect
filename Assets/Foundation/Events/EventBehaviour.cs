using UnityEngine;

namespace Assets.Foundation.Events
{
    public abstract class EventBehaviour : MonoBehaviour
    {

        private void OnEnable()
        {
            UpdateEventStatus(true);
        }

        private void OnDisable()
        {
            UpdateEventStatus(false);
        }

        protected abstract void UpdateEventStatus(bool status);
    }
}
