using UnityEngine;

namespace Thirties.Miniclip.TowerDefense
{
    public class ApplicationController : MonoBehaviour
    {
        [Header("FSM")]
        [SerializeField]
        private Animator fsm;

        [Header("Faders")]
        [SerializeField]
        private ImageFader imageFader;
        public ImageFader ImageFader { get { return imageFader; } }
        [SerializeField]
        private ImageFader audioFader;
        public ImageFader AudioFader { get { return audioFader; } }

        [Header("Data")]
        [SerializeField]
        private Dictionaries dictionaries;

        public static ApplicationController Instance { get; private set; }

        public BaseView CurrentView { get; set; }

        private void Start()
        {
            if (Instance == null)
            {
                InitializeLog();
                InitializeFaders();

                DontDestroyOnLoad(this);
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private static void InitializeLog()
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            Debug.unityLogger.logEnabled = true;
#else
            Debug.unityLogger.logEnabled = false;
#endif
        }

        private void InitializeFaders()
        {
            imageFader.gameObject.SetActive(false);
        }
    }
}
