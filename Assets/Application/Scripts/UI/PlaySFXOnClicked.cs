using UnityEngine;
using UnityEngine.UI;

namespace Thirties.Miniclip.TowerDefense
{
    [RequireComponent(typeof(Button))]
    public class PlaySFXOnClicked : MonoBehaviour
    {
        [SerializeField]
        private AudioClip clip;

        void Awake()
        {
            var button = GetComponent<Button>();
            button.onClick.AddListener(() => ApplicationController.Instance.PlaySFX(clip));
        }
    }
}