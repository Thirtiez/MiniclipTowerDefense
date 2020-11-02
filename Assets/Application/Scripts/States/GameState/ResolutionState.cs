using System;
using UnityEngine;

namespace Thirties.Miniclip.TowerDefense
{
    public class ResolutionState : BaseState
    {
        private GameView view;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);

            view = applicationController.CurrentView as GameView;

            if (view != null)
            {
                view.StartResolution();

                view.ExitButtonPressed += GoToMainMenu;
                view.RestartButtonPressed += GoToGame;
            }
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (view != null)
            {
                view.ExitButtonPressed -= GoToMainMenu;
                view.RestartButtonPressed -= GoToGame;
            }

            base.OnStateExit(animator, stateInfo, layerIndex);
        }

        private void GoToMainMenu()
        {
            imageFader.FadeOut(() => GoTo(FSMTrigger.Scene.MainMenu));
            audioFader.FadeOut();
        }

        private void GoToGame()
        {
            imageFader.FadeOut(() => GoTo(FSMTrigger.Scene.Game));
            audioFader.FadeOut();
        }
    }
}
