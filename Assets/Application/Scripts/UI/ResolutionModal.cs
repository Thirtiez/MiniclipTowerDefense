using I2.Loc;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Thirties.Miniclip.TowerDefense
{
    public class ResolutionModal : MonoBehaviour
    {
        [Header("Animator")]
        [SerializeField]
        private Animator animator;

        [Header("Background")]
        [SerializeField]
        private Image background;

        [Header("Texts")]
        [SerializeField]
        private TMP_Text titleText;

        [Header("Buttons")]
        [SerializeField]
        private Button exitButton;
        [SerializeField]
        private Button retryButton;

        private static readonly string FadeIn = "FadeIn";
        private static readonly string FadeOut = "FadeOut";

        public void Show(bool isVictory, UnityAction onExit = null, UnityAction onRetry = null)
        {
            var color = isVictory ? ApplicationController.Instance.Settings.PrimaryColor : ApplicationController.Instance.Settings.SecondaryColor;
            background.color = color.WithAlpha(0.4f);

            titleText.text = LocalizationManager.GetTranslation(isVictory ? LocalizationKey.ResolutionModalTitle_Victory : LocalizationKey.ResolutionModalTitle_Defeat);

            exitButton.onClick.AddListener(() => onExit?.Invoke());
            exitButton.onClick.AddListener(Hide);
            retryButton.onClick.AddListener(() => onRetry?.Invoke());
            retryButton.onClick.AddListener(Hide);

            animator.SetTrigger(FadeIn);
        }

        private void Hide()
        {
            exitButton.onClick.RemoveAllListeners();
            retryButton.onClick.RemoveAllListeners();

            animator.SetTrigger(FadeOut);
        }
    }
}
