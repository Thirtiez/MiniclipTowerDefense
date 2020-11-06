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

        [Header("Colors")]
        [SerializeField]
        private Color victoryColor;
        [SerializeField]
        private Color defeatColor;

        private static readonly string FadeIn = "FadeIn";
        private static readonly string FadeOut = "FadeOut";

        public void Show(bool isVictory, UnityAction onExit = null, UnityAction onRetry = null)
        {
            titleText.text = LocalizationManager.GetTranslation(isVictory ? LocalizationKey.ResolutionModalTitle_Victory : LocalizationKey.ResolutionModalTitle_Defeat);
            background.color = isVictory ? victoryColor : defeatColor;

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
