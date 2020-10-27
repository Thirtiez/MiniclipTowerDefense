using UnityEngine;

namespace Thirties.Miniclip.TowerDefense
{
    public class GameState : BaseState
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
