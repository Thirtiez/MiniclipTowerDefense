using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Thirties.Miniclip.TowerDefense
{
    public class SceneLoaderState : BaseState
    {
        [SerializeField]
        private SceneField sceneToLoad;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);

            applicationController.CurrentView = null;
            LoadScene();

            GoForward();
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateExit(animator, stateInfo, layerIndex);
        }

        private void LoadScene()
        {
            Debug.Log($"Loading scene {sceneToLoad.SceneName}...");

            SceneManager.LoadScene(sceneToLoad.SceneName, LoadSceneMode.Single);
            Resources.UnloadUnusedAssets();
            System.GC.Collect();

            Debug.Log($"Scene {sceneToLoad.SceneName} succesfuly loaded");
        }
    }
}

