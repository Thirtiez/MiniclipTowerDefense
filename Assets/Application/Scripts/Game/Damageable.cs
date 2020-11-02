using UnityEngine;
using UnityEngine.Events;

namespace Thirties.Miniclip.TowerDefense
{
    [RequireComponent(typeof(Collider))]
    public class Damageable : MonoBehaviour
    {
        public UnityAction Died { get; set; }

        public bool IsAlive { get { return health > 0; } }

        [SerializeField]
        private float health = 200.0f;

        public void Damage(float amount)
        {
            health -= amount;
            if (health <= 0)
            {
                Died?.Invoke();

                Destroy(gameObject);
            }
        }
    }
}
