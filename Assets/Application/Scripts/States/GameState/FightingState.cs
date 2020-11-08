using I2.Loc;
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
                applicationController.PlayMusic(applicationController.Settings.GameClip, 1.0f);

                view.StartFighting();

                view.GiveUpButtonPressed += GiveUp;
                view.DoubleTimeButtonPressed += applicationController.DoubleTime;
                view.NormalTimeButtonPressed += applicationController.ResumeTime;
                view.HeadquartersDestroyed += GoToResolution;
                view.AllEnemiesDefeated += GoToResolution;
            }
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (view != null)
            {
                view.GiveUpButtonPressed -= GiveUp;
                view.DoubleTimeButtonPressed -= applicationController.DoubleTime;
                view.NormalTimeButtonPressed -= applicationController.PauseTime;
                view.HeadquartersDestroyed -= GoToResolution;
                view.AllEnemiesDefeated -= GoToResolution;
            }

            base.OnStateExit(animator, stateInfo, layerIndex);
        }

        private void GiveUp()
        {
            applicationController.PauseTime();
            applicationController.ConfirmationModal.Show(
                LocalizationManager.GetTranslation("GiveUpModalTitle").ToUpper(),
                LocalizationManager.GetTranslation("GiveUpModalDescription"),
                () => 
                {
                    applicationController.ResumeTime();

                    GoTo(FSMTrigger.Game.Resolution);
                },
                () => 
                {
                    applicationController.ResumeTime();
                });
        }

        private void GoToResolution()
        {
            GoTo(FSMTrigger.Game.Resolution);
        }
    }
}
