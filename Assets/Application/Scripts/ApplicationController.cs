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

        [Header("Sound")]
        [SerializeField]
        private AudioSource musicSource;
        [SerializeField]
        private AudioSource sfxSource;

        [Header("UI")]
        [SerializeField]
        private ConfirmationModal confirmationModal;
        public ConfirmationModal ConfirmationModal { get { return confirmationModal; } }

        [Header("Data")]
        [SerializeField]
        private Settings settings;
        public Settings Settings { get { return settings; } }
        [SerializeField]
        private Prefabs prefabs;
        public Prefabs Prefabs { get { return prefabs; } }

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

        private void InitializeLog()
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            Debug.unityLogger.logEnabled = true;
#else
            Debug.unityLogger.logEnabled = false;
#endif
        }

        public void PlayMusic(AudioClip clip)
        {
            if (clip != musicSource.clip)
            {
                musicSource.Stop();
                musicSource.clip = clip;
                musicSource.Play();
            }
        }

        public void PlaySFX(AudioClip clip)
        {
            sfxSource.PlayOneShot(clip);
        }

        public void PauseTime()
        {
            Time.timeScale = 0;
        }

        public void ResumeTime()
        {
            Time.timeScale = 1;
        }

        public void DoubleTime()
        {
            Time.timeScale = 2;
        }
    }
}
