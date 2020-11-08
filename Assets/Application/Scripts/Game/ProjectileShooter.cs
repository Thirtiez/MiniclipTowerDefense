using UnityEngine;

namespace Thirties.Miniclip.TowerDefense
{
    public class ProjectileShooter : Shooter
    {
        [Header("Projectile")]
        [SerializeField]
        private Projectile projectilePrefab;

        protected override void Shoot()
        {
            var projectile = Instantiate(projectilePrefab, firingPoint.position, Quaternion.identity, transform.parent);
            projectile.name = $"{transform.name}'s rocket";
            projectile.Initialize(transform, currentTarget.transform, firePower);
        }
    }
}
