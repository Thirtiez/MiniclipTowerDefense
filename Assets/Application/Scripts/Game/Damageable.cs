using UnityEngine;
using UnityEngine.Events;

namespace Thirties.Miniclip.TowerDefense
{
    [RequireComponent(typeof(Collider))]
    public class Damageable : MonoBehaviour
    {
        public UnityAction Destroyed { get; set; }
        public UnityAction Damaged { get; set; }

        public float CurrentHealth { get; private set; }
        public bool IsAlive { get { return CurrentHealth > 0; } }

        [Header("Parameters")]
        [SerializeField]
        private float health = 200.0f;
        public float MaxHealth { get { return health; } }

        [Header("Particles")]
        [SerializeField]
        private ParticleSystem deathParticlesPrefab;


        protected void Awake()
        {
            var collider = GetComponent<CapsuleCollider>();
            var aiControlled = GetComponent<AIControlled>();
            var positionable = GetComponent<Positionable>();

            float size = positionable?.Size ?? aiControlled?.Size ?? 1.0f;
            collider.radius = size * 0.5f;
            collider.height = size;

            CurrentHealth = health;
        }

        public void Damage(float amount)
        {
            CurrentHealth -= amount;

            Damaged?.Invoke();

            if (CurrentHealth <= 0)
            {
                Instantiate(deathParticlesPrefab, transform.position, transform.rotation);

                Destroy(gameObject);

                Destroyed?.Invoke();
            }
        }
    }
}
