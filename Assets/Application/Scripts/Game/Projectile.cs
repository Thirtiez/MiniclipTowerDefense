using UnityEngine;

namespace Thirties.Miniclip.TowerDefense
{
    [RequireComponent(typeof(Explosive))]
    [RequireComponent(typeof(CapsuleCollider))]
    public class Projectile : MonoBehaviour
    {
        [SerializeField]
        private float speed = 3.0f;

        private Transform source;
        private Transform target;
        private Explosive explosive;

        public void Initialize(Transform source, Transform target, float power)
        {
            this.source = source;
            this.target = target;

            explosive = GetComponent<Explosive>();
            explosive.ExplosionPower = power;
        }

        private void Update()
        {
            transform.LookAt(target);
            transform.position += transform.forward * speed * Time.deltaTime;
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log($"{source.name} hits {other.name}");

            var explosive = GetComponent<Explosive>();
            explosive.Explode();
        }
    }
}
