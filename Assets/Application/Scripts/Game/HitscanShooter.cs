using UnityEngine;

namespace Thirties.Miniclip.TowerDefense
{
    public class HitscanShooter : Shooter
    {
        [Header("Parameters")]
        [SerializeField]
        private float firePower = 5.0f;

        protected override void Shoot()
        {
            Debug.Log($"{transform.name} shoots {currentTarget.transform.name} for {firePower} damage");

            currentTarget.Damage(firePower);
        }
    }
}
