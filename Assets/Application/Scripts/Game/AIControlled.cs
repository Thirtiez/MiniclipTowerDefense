using System.Linq;
using UnityEngine;
using UnityEngine.AI;

namespace Thirties.Miniclip.TowerDefense
{
    [RequireComponent(typeof(Shooter))]
    [RequireComponent(typeof(NavMeshAgent))]
    public class AIControlled : MonoBehaviour
    {
        private NavMeshAgent navMeshAgent;
        private Shooter shooter;
        private Damageable currentDestination;

        [Header("Parameters")]
        [SerializeField]
        private float speed = 2.0f;
        [SerializeField]
        private float size = 1.0f;
        public float Size { get { return size; } }

        protected void OnEnable()
        {
            shooter = GetComponent<Shooter>();

            navMeshAgent = GetComponent<NavMeshAgent>();
            navMeshAgent.speed = speed;
            navMeshAgent.radius = size * 0.5f;
            navMeshAgent.height = 1;
        }

        protected void OnDestroy()
        {
            shooter.TargetFound -= Stop;
            if (currentDestination != null)
            {
                currentDestination.Destroyed -= LookForDestination;
            }
        }

        public void LookForDestination()
        {
            shooter.TargetFound += Stop;

            if (currentDestination != null)
            {
                currentDestination.Destroyed -= LookForDestination;
            }

            var hits = Physics.OverlapSphere(transform.position, Mathf.Infinity, shooter.TargetLayer);
            if (!hits.IsNullOrEmpty())
            {
                currentDestination = hits
                    .OrderBy(x => Vector3.Distance(x.transform.position, transform.position))
                    .Select(x => x.transform.GetComponent<Damageable>())
                    .FirstOrDefault(x => x != null && x.IsAlive);

                if (currentDestination != null)
                {
                    currentDestination.Destroyed += LookForDestination;

                    navMeshAgent.isStopped = false;
                    navMeshAgent.SetDestination(currentDestination.transform.position);
                }
            }
        }

        private void Stop()
        {
            shooter.TargetFound -= Stop;

            navMeshAgent.isStopped = true;
        }
    }
}
