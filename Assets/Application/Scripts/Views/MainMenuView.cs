﻿using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Thirties.Miniclip.TowerDefense
{
    public class MainMenuView : BaseView
    {
        #region Events

        public UnityAction PlayButtonPressed { get; set; }

        #endregion

        #region Inspector fields

        [SerializeField]
        private Button playButton;

        #endregion

        #region Protected methods

        protected override void Initialize()
        {
            playButton.onClick.AddListener(OnPlayButtonPressed);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }

        #endregion

        #region Private methods

        private void OnPlayButtonPressed()
        {
            PlayButtonPressed?.Invoke();
        }

        #endregion
    }
}
