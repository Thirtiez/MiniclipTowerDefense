using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Thirties.Miniclip.TowerDefense
{
    public class MainMenuView : BaseView
    {
        #region Inspector fields

        [SerializeField]
        private Button playButton;

        #endregion

        #region Private fields

        #endregion

        #region Public fields


        #endregion

        #region Events

        public UnityAction StartingGame { get; set; }

        #endregion

        #region Private methods

        protected override void Initialize()
        {
            playButton.onClick.AddListener(OnPlayButtonPressed);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }

        private void OnPlayButtonPressed()
        {
            StartingGame?.Invoke();
        }

        #endregion
    }
}
