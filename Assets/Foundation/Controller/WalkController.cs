using UnityEngine;
using System.Collections;
using UnityEngine.AI;
namespace Assets.Foundation.Controller
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class WalkController : MonoBehaviour
    {
        [SerializeField]
        private Vector3 _destination;

        public Vector3 Destination
        {
            get
            {
                return _destination;
            }
            set
            {
                _destination = value;
                if (Agent != null)
                {
                    Agent.destination = value;
                }
            }
        }

        private NavMeshAgent Agent
        {
            get
            {
                return this.GetComponent<NavMeshAgent>();
            }
        }

        private void Start()
        {
            Agent.destination = Destination;
            Agent.stoppingDistance = 0.5f;
        }

        private void Update()
        {
#if UNITY_EDITOR
            Agent.destination = Destination;
#endif
        }
    }
}
