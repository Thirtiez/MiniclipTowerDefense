using UnityEngine;
using System.Collections;
using UnityEngine.Events;

namespace Thirties.Miniclip.TowerDefense
{
    public abstract class BaseFader<T> : MonoBehaviour
    where T : Component
    {
        [Header("Target Component")]
        [SerializeField]
        protected T targetComponent;

        [Header("Settings")]
        [Range(0f, 1f)]
        [SerializeField]
        private float fadeOutValue = 0f;
        public float FadeOutValue { get { return fadeOutValue; } set { fadeOutValue = value; } }

        [Range(0f, 1f)]
        [SerializeField]
        private float fadeInValue = 1f;
        public float FadeInValue { get { return fadeInValue; } set { fadeInValue = value; } }

        [SerializeField]
        private float defaultFadeTime = 0.5f;

        protected abstract float targetValue { get; }

        private Coroutine fadeCoroutine;

        protected abstract void UpdateTargetValue(float newValue);

        private IEnumerator FadeCoroutine(float valueFrom, float valueTo, float fadeTime, UnityAction onComplete)
        {
            float elapsedTime = 0f;

            while (elapsedTime < fadeTime)
            {
                elapsedTime += Time.unscaledDeltaTime;
                float t = Mathf.Clamp01(elapsedTime / fadeTime);
                float valueFade = Mathf.Lerp(valueFrom, valueTo, t);

                UpdateTargetValue(valueFade);

                yield return null;
            }

            fadeCoroutine = null;
            onComplete?.Invoke();
        }

        public virtual void FadeIn(UnityAction onCompleted = null, float? time = null)
        {
            if (fadeCoroutine != null)
            {
                StopCoroutine(fadeCoroutine);
            }

            if (targetValue != fadeInValue)
            {
                fadeCoroutine = StartCoroutine(FadeCoroutine(targetValue, fadeInValue, time ?? defaultFadeTime, onCompleted));
            }
        }

        public virtual void FadeOut(UnityAction onCompleted = null, float? time = null)
        {
            if (fadeCoroutine != null)
            {
                StopCoroutine(fadeCoroutine);
            }

            if (targetValue != fadeOutValue)
            {
                fadeCoroutine = StartCoroutine(FadeCoroutine(targetValue, fadeOutValue, time ?? defaultFadeTime, onCompleted));
            }
        }

        public virtual void Blink(UnityAction onCompleted = null, float? time = null)
        {
            if (fadeCoroutine != null)
            {
                StopCoroutine(fadeCoroutine);
            }

            fadeCoroutine = StartCoroutine(FadeCoroutine(fadeOutValue, fadeInValue, time ?? defaultFadeTime, onCompleted));
        }

        public void StopFade()
        {
            if (fadeCoroutine != null)
            {
                StopCoroutine(fadeCoroutine);

                fadeCoroutine = null;
            }
        }
    }
}