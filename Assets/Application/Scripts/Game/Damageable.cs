using UnityEngine;
using UnityEngine.Events;

namespace Thirties.Miniclip.TowerDefense
{
    [RequireComponent(typeof(Collider))]
    public class Damageable : MonoBehaviour
    {
        public UnityAction Destroyed { get; set; }

        public bool IsAlive { get { return health > 0; } }

        [Header("Parameters")]
        [SerializeField]
        private float health = 200.0f;

        [Header("Particles")]
        [SerializeField]
        private ParticleSystem deathParticlesPrefab;

        protected void OnEnable()
        {
            var collider = GetComponent<CapsuleCollider>();
            var aiControlled = GetComponent<AIControlled>();
            var positionable = GetComponent<Positionable>();

            float size = positionable?.Size ?? aiControlled?.Size ?? 1.0f;
            collider.radius = size * 0.5f;
            collider.height = size;
        }

        public void Damage(float amount)
        {
            health -= amount;
            if (health <= 0)
            {
                Instantiate(deathParticlesPrefab, transform.position, transform.rotation);

                Destroy(gameObject);

                Destroyed?.Invoke();
            }
        }
    }
}
