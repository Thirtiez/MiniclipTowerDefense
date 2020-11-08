using UnityEngine;

namespace Thirties.Miniclip.TowerDefense
{
    public class HitscanShooter : Shooter
    {
        [Header("Hitscan particles")]
        [SerializeField]
        private ParticleSystem hitParticles;

        protected override void Shoot()
        {
            Debug.Log($"{transform.name} shoots {currentTarget.name} for {firePower} damage");

            if (hitParticles != null)
            {
                Instantiate(hitParticles, currentTarget.transform);
            }

            currentTarget.Damage(firePower);
        }
    }
}
