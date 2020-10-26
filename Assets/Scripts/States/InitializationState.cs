using UnityEngine;
using UnityEngine.SceneManagement;

namespace Thirties.Miniclip.TowerDefense
{
    public class InitializationState : BaseState
    {
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);

            string sceneName = SceneManager.GetActiveScene().name;
            int trigger = FSMTrigger.SceneToTrigger[sceneName];

            GoTo(trigger);
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateExit(animator, stateInfo, layerIndex);
        }
    }
}

