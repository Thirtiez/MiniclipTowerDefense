using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Thirties.Miniclip.TowerDefense
{
    public class DeployableUIElement : MonoBehaviour
    {
        [SerializeField]
        public Button button;
        [SerializeField]
        public Deployable deployablePrefab;

        public void Initialize(UnityAction<Deployable> buttonPressed)
        {
            button.onClick.AddListener(() => buttonPressed?.Invoke(deployablePrefab));
        }

        public void SetInteractable(bool value)
        {
            button.interactable = value;
        }
    }
}
