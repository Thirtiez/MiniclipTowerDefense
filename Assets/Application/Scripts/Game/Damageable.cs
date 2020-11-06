using UnityEngine;
using UnityEngine.Events;

namespace Thirties.Miniclip.TowerDefense
{
    [RequireComponent(typeof(Collider))]
    public class Damageable : MonoBehaviour
    {
        public UnityAction Died { get; set; }

        public bool IsAlive { get { return health > 0; } }

        [Header("Parameters")]
        [SerializeField]
        private float health = 200.0f;

        [Header("Particles")]
        [SerializeField]
        private ParticleSystem deathParticlesPrefab;

        public void Damage(float amount)
        {
            health -= amount;
            if (health <= 0)
            {
                Died?.Invoke();

                Instantiate(deathParticlesPrefab, transform.position, transform.rotation);

                Destroy(gameObject);
            }
        }
    }
}
