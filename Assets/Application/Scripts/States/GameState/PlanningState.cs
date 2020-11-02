using System;
using UnityEngine;

namespace Thirties.Miniclip.TowerDefense
{
    public class PlanningState : BaseState
    {
        private GameView view;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);

            view = applicationController.CurrentView as GameView;

            if (view != null)
            {
                imageFader.FadeIn();
                audioFader.FadeIn();

                view.StartPlanning();

                view.DeployableButtonPressed += GoToPositioning;
                view.FightButtonPressed += GoToFighting;
            }
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (view != null)
            {
                view.DeployableButtonPressed -= GoToPositioning;
                view.FightButtonPressed -= GoToFighting;
            }

            base.OnStateExit(animator, stateInfo, layerIndex);
        }

        private void GoToPositioning()
        {
            GoTo(FSMTrigger.Game.Positioning);
        }

        private void GoToFighting()
        {
            GoTo(FSMTrigger.Game.Fighting);
        }
    }
}
