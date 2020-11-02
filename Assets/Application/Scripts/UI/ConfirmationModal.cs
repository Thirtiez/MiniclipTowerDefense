using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Thirties.Miniclip.TowerDefense
{
    public class ConfirmationModal : MonoBehaviour
    {
        [Header("Animator")]
        [SerializeField]
        private Animator animator;

        [Header("Texts")]
        [SerializeField]
        private TMP_Text titleText;
        [SerializeField]
        private TMP_Text descriptionText;

        [Header("Buttons")]
        [SerializeField]
        private Button confirmButton;
        [SerializeField]
        private Button cancelButton;

        private static readonly string FadeIn = "FadeIn";
        private static readonly string FadeOut = "FadeOut";

        public void Show(string title, string description, UnityAction onConfirm = null, UnityAction onCancel = null)
        {
            titleText.text = title;
            descriptionText.text = description;

            confirmButton.onClick.AddListener(() => onConfirm?.Invoke());
            confirmButton.onClick.AddListener(Hide);
            cancelButton.onClick.AddListener(() => onCancel?.Invoke());
            cancelButton.onClick.AddListener(Hide);

            animator.SetTrigger(FadeIn);
        }

        private void Hide()
        {
            confirmButton.onClick.RemoveAllListeners();
            cancelButton.onClick.RemoveAllListeners();

            animator.SetTrigger(FadeOut);
        }
    }
}
