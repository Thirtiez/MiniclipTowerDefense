using UnityEditor.Animations;
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
        private AudioFader audioFader;
        public AudioFader AudioFader { get { return audioFader; } }

        [Header("UI")]
        [SerializeField]
        private ConfirmationModal confirmationModal;
        public ConfirmationModal ConfirmationModal { get { return confirmationModal; } }

        [Header("Data")]
        [SerializeField]
        private DeployableData deployableData;
        public DeployableData DeployableData { get { return deployableData; } }
        [SerializeField]
        private EnemyData enemyData;
        public EnemyData EnemyData { get { return enemyData; } }


        public static ApplicationController Instance { get; private set; }

        public BaseView CurrentView { get; set; }

        public int CurrentMoney { get; set; }

        private void Start()
        {
            if (Instance == null)
            {
                InitializeLog();

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
    }
}
