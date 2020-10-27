using UnityEngine;
using UnityEngine.SceneManagement;

namespace Thirties.Miniclip.TowerDefense
{
    public class BaseState : StateMachineBehaviour
    {
        protected ApplicationController applicationController;
        protected ImageFader imageFader;
        protected AudioFader audioFader;

        private Animator fsm;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);

            fsm = animator;

            if (applicationController == null && ApplicationController.Instance != null)
            {
                applicationController = ApplicationController.Instance;
                imageFader = applicationController.ImageFader;
                audioFader = applicationController.AudioFader;
            }
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateUpdate(animator, stateInfo, layerIndex);
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateExit(animator, stateInfo, layerIndex);
        }

        protected void GoForward()
        {
            fsm.SetTrigger(FSMTrigger.Forward);
        }

        protected void GoBack()
        {
            fsm.SetTrigger(FSMTrigger.Back);
        }

        protected void GoTo(int trigger)
        {
            fsm.SetTrigger(trigger);
        }
    }
}