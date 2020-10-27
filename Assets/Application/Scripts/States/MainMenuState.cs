using UnityEngine;

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
                view.StartingGame += GoForward;
            }
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (view != null)
            {
                view.StartingGame -= GoForward;
            }

            base.OnStateExit(animator, stateInfo, layerIndex);
        }
    }
}
