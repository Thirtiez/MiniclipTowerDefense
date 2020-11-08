using UnityEngine;

namespace Thirties.Miniclip.TowerDefense
{
    public class GameInitializationState : BaseState
    {
        private GameView view;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);

            view = applicationController.CurrentView as GameView;

            if (view != null)
            {
                applicationController.PlayMusic(applicationController.Settings.GameClip, 0.5f);
                applicationController.CurrentMoney = applicationController.Settings.StartingMoney;

                GoForward();
            }
        }
    }
}
