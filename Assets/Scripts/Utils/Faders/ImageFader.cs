﻿using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Thirties.Miniclip.TowerDefense
{
    public class ImageFader : AbstractFader<Image>
    {
        [SerializeField]
        private GraphicRaycaster canvasRaycaster;

        protected override float targetValue { get { return targetComponent.color.a; } }

        protected override void UpdateTargetValue(float newAlpha)
        {
            Color newColor = targetComponent.color;
            newColor.a = Mathf.Clamp01(newAlpha);
            targetComponent.color = newColor;
        }

        public override void FadeOut(UnityAction onCompleted, float? time = null)
        {
            canvasRaycaster.enabled = false;

            base.FadeOut(onCompleted, time);
        }

        public override void FadeIn(UnityAction onCompleted, float? time = null)
        {
            base.FadeIn(onCompleted, time);

            canvasRaycaster.enabled = true;
        }
    }
}