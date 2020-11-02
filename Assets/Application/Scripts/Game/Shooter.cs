using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Thirties.Miniclip.TowerDefense
{
    public class Shooter : MonoBehaviour
    {
        public UnityAction TargetFound { get; set; }

        public LayerMask TargetLayer { get { return targetLayer; } }

        [SerializeField]
        private float firePower = 5.0f;
        [SerializeField]
        private float fireDelay = 1.0f;
        [SerializeField]
        private float fireRange = 10.0f;
        [SerializeField]
        private LayerMask targetLayer;

        private Damageable currentTarget;
        private float elapsedTime = 0;

        protected void Update()
        {
            if (currentTarget == null)
            {
                var hits = Physics.OverlapSphere(transform.position, fireRange, targetLayer);
                if (!hits.IsNullOrEmpty())
                {
                    currentTarget = hits
                        .OrderBy(x => Vector3.Distance(x.transform.position, transform.position))
                        .Select(x => x.transform.GetComponent<Damageable>())
                        .FirstOrDefault(x => x != null && x.IsAlive && fireRange >= Vector3.Distance(x.transform.position, transform.position));

                    if (currentTarget != null)
                    {
                        currentTarget.Died += StopShooting;

                        elapsedTime = 0;

                        TargetFound?.Invoke();
                    }
                }
            }
            else
            {
                if (elapsedTime > fireDelay)
                {
                    Debug.Log($"{transform.name} shoots {currentTarget.transform.name} for {firePower} damage");

                    currentTarget.Damage(firePower);

                    elapsedTime -= fireDelay;
                }

                elapsedTime += Time.deltaTime;
            }
        }

        private void StopShooting()
        {
            currentTarget = null;
        }
    }
}
