using System;
using System.Linq;
using UnityEngine;

namespace Thirties.Miniclip.TowerDefense
{
    public class RewardState : BaseState
    {
        private RewardView view;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);

            view = applicationController.CurrentView as RewardView;

            if (view != null)
            {

            }
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (view != null)
            {

            }

            base.OnStateExit(animator, stateInfo, layerIndex);
        }
    }
}
