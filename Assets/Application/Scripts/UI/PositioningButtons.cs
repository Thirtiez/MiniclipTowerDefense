using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Thirties.Miniclip.TowerDefense
{
    public class PositioningButtons : MonoBehaviour
    {
        [SerializeField]
        private Button confirmButton;
        [SerializeField]
        private Button cancelButton;

        private Transform follow;

        public void Initialize(Transform follow, UnityAction onConfirm, UnityAction onCancel)
        {
            this.follow = follow;

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
    }
}
