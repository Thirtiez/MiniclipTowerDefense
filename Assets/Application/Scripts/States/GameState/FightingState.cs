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
                view.StartFighting();

                view.GiveUpButtonPressed += GiveUp;
                view.DoubleTimeButtonPressed += DoubleTime;
                view.NormalTimeButtonPressed += NormalTime;
                view.HeadquartersDestroyed += GoToResolution;
                view.AllEnemiesDefeated += GoToResolution;
            }
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (view != null)
            {
                view.GiveUpButtonPressed -= GiveUp;
                view.DoubleTimeButtonPressed -= DoubleTime;
                view.NormalTimeButtonPressed -= NormalTime;
                view.HeadquartersDestroyed -= GoToResolution;
                view.AllEnemiesDefeated -= GoToResolution;
            }

            base.OnStateExit(animator, stateInfo, layerIndex);
        }

        private void GiveUp()
        {
            StopTime();
            applicationController.ConfirmationModal.Show(
                LocalizationManager.GetTranslation("GiveUpModalTitle").ToUpper(),
                LocalizationManager.GetTranslation("GiveUpModalDescription"),
                () => 
                {
                    NormalTime();
                    GoTo(FSMTrigger.Scene.MainMenu);
                },
                () => 
                {
                    NormalTime();
                });
        }

        private void DoubleTime()
        {
            Time.timeScale = 2;
        }

        private void NormalTime()
        {
            Time.timeScale = 1;
        }

        private void StopTime()
        {
            Time.timeScale = 0;
        }

        private void GoToResolution()
        {
            GoTo(FSMTrigger.Game.Resolution);
        }
    }
}
