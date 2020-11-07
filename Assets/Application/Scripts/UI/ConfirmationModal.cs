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

        private UnityAction onConfirm;
        private UnityAction onCancel;

        private static readonly string FadeIn = "FadeIn";
        private static readonly string FadeOut = "FadeOut";

        public void Show(string title, string description, UnityAction onConfirm = null, UnityAction onCancel = null)
        {
            this.onConfirm = onConfirm;
            this.onCancel = onCancel;

            titleText.text = title;
            descriptionText.text = description;
 
            confirmButton.onClick.AddListener(OnConfirmButtonPressed);
            cancelButton.onClick.AddListener(OnCancelButtonPressed);

            animator.SetTrigger(FadeIn);
        }

        private void OnConfirmButtonPressed()
        {
            animator.SetTrigger(FadeOut);

            onConfirm?.Invoke();
            onConfirm = null;
        }

        private void OnCancelButtonPressed()
        {
            animator.SetTrigger(FadeOut);

            onCancel?.Invoke();
            onCancel = null;
        }
    }
}
