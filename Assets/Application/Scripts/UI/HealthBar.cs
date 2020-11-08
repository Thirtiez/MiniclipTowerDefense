using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Thirties.Miniclip.TowerDefense
{
    [RequireComponent(typeof(Slider))]
    public class HealthBar : MonoBehaviour
    {
        private Slider slider;
        private Damageable damageable;

        public void Initialize(Damageable damageable)
        {
            this.damageable = damageable;

            transform.name = $"{damageable.name}'s Health Bar";

            slider = GetComponent<Slider>();

            damageable.Damaged += UpdateSlider;
            damageable.Destroyed += () => Destroy(gameObject);

            UpdateSlider();
        }

        private void Update()
        {
            if (damageable != null)
            {
                transform.position = Camera.main.WorldToScreenPoint(damageable.transform.position);
            }
        }

        private void UpdateSlider()
        {
            slider.value = damageable.CurrentHealth / damageable.MaxHealth;
        }
    }
}
