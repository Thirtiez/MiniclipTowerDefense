using System;
using UnityEngine;

namespace Thirties.Miniclip.TowerDefense
{
    public class FightingState : BaseState
    {
        private GameView view;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);

            view = applicationController.CurrentView as GameView;

            if (view != null)
            {
                view.StartFighting();

                view.GiveUpButtonPressed += GiveUp;
            }
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (view != null)
            {
                view.GiveUpButtonPressed -= GiveUp;
            }

            base.OnStateExit(animator, stateInfo, layerIndex);
        }

        private void GiveUp()
        {
            applicationController.ConfirmationModal.Show("WARNING", "Do you really want to give up?", () => GoTo(FSMTrigger.Scene.MainMenu));
        }
    }
}
