using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Thirties.Miniclip.TowerDefense
{
    public abstract class BaseView : MonoBehaviour
    {
        protected ApplicationController applicationController;

        protected virtual void Start()
        {
            StartCoroutine(RegisterToApplicationController());
        }

        protected virtual void OnDestroy()
        {
            ApplicationController.Instance.CurrentView = null;
        }

        private IEnumerator RegisterToApplicationController()
        {
            while (ApplicationController.Instance == null)
            {
                yield return null;
            }

            applicationController = ApplicationController.Instance;
            applicationController.CurrentView = this;
            Initialize();
        }

        protected virtual void Initialize() { }
    }
}
