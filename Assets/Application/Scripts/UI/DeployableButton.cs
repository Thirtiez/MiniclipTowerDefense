﻿using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Thirties.Miniclip.TowerDefense
{
    public class DeployableButton : MonoBehaviour
    {
        [SerializeField]
        private Button button;
        [SerializeField]
        private Image normalIcon;
        [SerializeField]
        private Image highlightedIcon;

        private Deployable deployable;

        public bool Interactable { get { return button.interactable; } set { button.interactable = value; } }
        public bool IsPurchasable { get { return ApplicationController.Instance.CurrentMoney >= deployable.Cost; } }

        public void Initialize(Deployable deployable, UnityAction<Deployable> buttonPressed)
        {
            this.deployable = deployable;

            normalIcon.sprite = deployable.Icon;
            highlightedIcon.sprite = deployable.Icon;

            button.onClick.AddListener(() => buttonPressed?.Invoke(deployable));
        }
    }
}