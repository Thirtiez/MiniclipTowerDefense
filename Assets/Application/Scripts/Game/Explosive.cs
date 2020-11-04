using System.Linq;
using UnityEngine;

namespace Thirties.Miniclip.TowerDefense
{
    public class Explosive : MonoBehaviour
    {
        [SerializeField]
        private float explosionPower = 50.0f;
        [SerializeField]
        private float explosionRadius = 5.0f;

        public void Explode()
        {
            var hits = Physics.OverlapSphere(transform.position, explosionRadius, Layer.Enemy);
            if (!hits.IsNullOrEmpty())
            {

                var damageables = hits.Select(x => x.transform.GetComponent<Damageable>()).ToList();

                damageables.ForEach(damageable =>
                {
                    Debug.Log($"{transform.name} exploded hitting {damageable.transform.name} for {explosionPower} damage");

                    damageable.Damage(explosionPower);
                });
            }
        }
    }
}
