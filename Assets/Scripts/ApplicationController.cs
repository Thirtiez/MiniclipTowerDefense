using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Thirties.Miniclip.TowerDefense
{
    public class ApplicationController : MonoBehaviour
    {
        [Header("References")]

        [SerializeField]
        private Animator fsm;

        [SerializeField]
        private ImageFader imageFader;
        public ImageFader ImageFader { get { return imageFader; } }

        [SerializeField]
        private Dictionaries prefabsTable;

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
