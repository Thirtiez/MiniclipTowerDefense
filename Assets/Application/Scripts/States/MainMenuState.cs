﻿using UnityEngine;

namespace Thirties.Miniclip.TowerDefense
{
    public class MainMenuState : BaseState
    {
        private MainMenuView view;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);

            view = applicationController.CurrentView as MainMenuView;

            if (view != null)
            {
                imageFader.FadeIn();
                audioFader.FadeIn();

                view.PlayButtonPressed += StartGame;
            }
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (view != null)
            {
                view.PlayButtonPressed -= StartGame;
            }

            base.OnStateExit(animator, stateInfo, layerIndex);
        }

        private void StartGame()
        {
            applicationController.CurrentMoney = 200;

            imageFader.FadeOut(GoForward);
            audioFader.FadeOut();
        }
    }
}
