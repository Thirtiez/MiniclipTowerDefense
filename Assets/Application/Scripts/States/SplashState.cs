using System;
using System.Linq;
using UnityEngine;

namespace Thirties.Miniclip.TowerDefense
{
    public class SplashState : BaseState
    {
        private SplashView view;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);

            view = applicationController.CurrentView as SplashView;

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
