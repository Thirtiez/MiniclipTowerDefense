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

        protected void Awake()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();

            shooter = GetComponent<Shooter>();
            shooter.TargetFound += Stop;
        }

        protected void Start()
        {
            SetDestination();
        }

        private void SetDestination()
        {
            var hits = Physics.OverlapSphere(transform.position, Mathf.Infinity, shooter.TargetLayer);
            if (!hits.IsNullOrEmpty())
            {
                currentDestination = hits
                    .OrderBy(x => Vector3.Distance(x.transform.position, transform.position))
                    .Select(x => x.transform.GetComponent<Damageable>())
                    .FirstOrDefault(x => x != null && x.IsAlive);

                if (currentDestination != null)
                {
                    currentDestination.Died += SetDestination;

                    navMeshAgent.isStopped = false;
                    navMeshAgent.SetDestination(currentDestination.transform.position);
                }
            }
        }

        private void Stop()
        {
            currentDestination = null;

            navMeshAgent.isStopped = true;
        }
    }
}
