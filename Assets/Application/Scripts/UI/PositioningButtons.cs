using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Thirties.Miniclip.TowerDefense
{
    public class PositioningButtons : MonoBehaviour
    {
        [Header("Buttons")]
        [SerializeField]
        private Button confirmButton;
        [SerializeField]
        private Button cancelButton;

        [Header("Texts")]
        [SerializeField]
        private TMP_Text nameText;
        [SerializeField]
        private TMP_Text dpsText;

        private Transform follow;

        public void Initialize(Deployable deployable, UnityAction onConfirm, UnityAction onCancel)
        {
            follow = deployable.transform;

            var shooter = deployable.GetComponent<Shooter>();
            var explosive = deployable.GetComponent<Explosive>();
            if (shooter != null)
            {
                dpsText.text = $"{shooter.Dps:0.0} DPS";
                if (shooter is ProjectileShooter)
                {
                    dpsText.text += " AOE";
                }
            }
            else if (explosive != null)
            {
                dpsText.text = $"{explosive.ExplosionPower:0.0} DMG";
            }

            nameText.text = deployable.Name;

            confirmButton.onClick.AddListener(onConfirm);
            cancelButton.onClick.AddListener(onCancel);
        }

        public void Update()
        {
            if (follow != null)
            {
                transform.position = Camera.main.WorldToScreenPoint(follow.position);
            }
        }

        public void SetValid(bool isValid)
        {
            confirmButton.interactable = isValid;
        }
    }
}
