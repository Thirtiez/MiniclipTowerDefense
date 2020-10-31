using System;
using UnityEngine;

namespace Thirties.Miniclip.TowerDefense
{
    public class PositioningState : BaseState
    {
        private GameView view;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);

            view = applicationController.CurrentView as GameView;

            if (view != null)
            {
                view.StartPositioning();

                view.PositioningConfirmButtonPressed += ConfirmPositioning; 
                view.PositioningCancelButtonPressed += CancelPositioning;
            }
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (view != null)
            {
                view.PositioningConfirmButtonPressed -= ConfirmPositioning;
                view.PositioningCancelButtonPressed -= CancelPositioning;
            }

            base.OnStateExit(animator, stateInfo, layerIndex);
        }

        private void ConfirmPositioning(Deployable deployable)
        {
            applicationController.CurrentMoney -= deployable.Cost;

            GoTo(FSMTrigger.Game.Planning);
        }

        private void CancelPositioning()
        {
            GoTo(FSMTrigger.Game.Planning);
        }
    }
}
