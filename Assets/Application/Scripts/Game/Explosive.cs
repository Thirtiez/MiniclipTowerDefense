﻿using System.Linq;
using UnityEngine;

namespace Thirties.Miniclip.TowerDefense
{
    public class Explosive : MonoBehaviour
    {
        [Header("Parameters")]
        [SerializeField]
        private float explosionPower = 50.0f;
        public float ExplosionPower { get { return explosionPower; } set { explosionPower = value; } }
        [SerializeField]
        private float explosionRadius = 5.0f;
        public float ExplosionRadius { get { return explosionRadius; } }

        [Header("Particles")]
        [SerializeField]
        private ParticleSystem explosionParticlesPrefab;

        public void Explode()
        {
            var hits = Physics.OverlapSphere(transform.position, explosionRadius, Layer.Enemy);
            if (!hits.IsNullOrEmpty())
            {
                var damageables = hits.Select(x => x.transform.GetComponent<Damageable>()).ToList();

                damageables.ForEach(damageable =>
                {
                    Debug.Log($"{transform.name} exploded hitting {damageable.name} for {explosionPower} damage");

                    damageable.Damage(explosionPower);
                });
            }

            Instantiate(explosionParticlesPrefab, transform.position, transform.rotation);

            Destroy(gameObject);
        }
    }
}
